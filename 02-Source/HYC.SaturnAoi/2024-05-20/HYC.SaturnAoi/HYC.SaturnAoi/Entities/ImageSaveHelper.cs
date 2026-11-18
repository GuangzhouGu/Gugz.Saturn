
using DFS.Model;
using HYC.HTImage;
using HYC.HTLog;
using HYC.HTPCBase.Extension.Recipe;
using HYC.SaturnAoi.Params;
using HYC.SaturnDB.DAL;
using HYC.SaturnDB.Model;
using HYC.StandardAoi.Events;
using HYC.StandardAoi.Parameter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnAoi.Entities
{
   public class ImageSaveHelper
    {
        #region 保存不良局部图
        public static void SavePartialImage(DetectionCompletedArgs args)
        {
            try
            {
                DetectionResult detection = args.Result;
                if (detection.Detail.TestResult ==ResultType.OK)
                {
                    return;
                }
                int saveCount = 0;
                int maxCount = 10;
                //创建存储文件夹
                string strPath = string.Empty;
                if (string.IsNullOrEmpty(detection.Detail.ImgPath) == false)
                {
                    strPath = Path.GetDirectoryName(detection.Detail.ImgPath)+ $"\\PartialImage\\{detection.Detail.PanelID}\\";
                    if (Directory.Exists(strPath) == false)
                    {
                        Directory.CreateDirectory(strPath);
                    }
                }
                //初始化示意图
                double ratio = detection.Detail.ImgROI.Height * 1.0 / detection.Detail.ImgROI.Width;
                if (!Double.IsNaN(ratio))
                {
                    Rectangle rectAll = new Rectangle(0, 0, 300, (int)(300 * ratio));
                    Bitmap imgAll = new Bitmap(rectAll.Width, rectAll.Height);
                    using (Graphics g = Graphics.FromImage(imgAll))
                    {
                        g.Clear(Color.White);
                        Pen blackPen = new Pen(Color.Black, 3f);
                        g.DrawRectangle(blackPen, new Rectangle(1, 1, rectAll.Width - 3, rectAll.Height - 3));
                    }
                    //生成小图--主要不良
                    ImgGenerate(detection, ref saveCount, maxCount, strPath, rectAll, imgAll, true,args.DetectionType.ToString());
                    //生成小图--次要不良
                    ImgGenerate(detection, ref saveCount, maxCount, strPath, rectAll, imgAll, false, args.DetectionType.ToString());
                    //保存示意图
                    if (saveCount > 0)
                    {
                        string strFileNameAll = detection.Detail.PanelID + ".jpg";
                        if (string.IsNullOrEmpty(strPath) == false)
                        {
                            imgAll.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            imgAll.Save(strPath + strFileNameAll, ImageFormat.Jpeg);
                        }
                    }
                    //写入数据库记录
                    DfsUploadInfoModel model = new DfsUploadInfoModel();
                    model.GlassID = args.Result.Detail.PanelID;
                    model.StartTime = DateTime.Now;
                    model.PcIndex = Param.System.PcIndex;
                    model.FilePath = strPath;
                    model.SendFlg = 1;
                    model.DetectType = args.DetectionType.ToString();
                    DfsUploadInfoDal dal = new DfsUploadInfoDal();
                    dal.Insert(model);
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("ImageSaveHelper").Info(ex.ToString());
            }
        }

        private static void ImgGenerate(DetectionResult detection, ref int saveCount, int maxCount, string strPath, Rectangle rectAll, Bitmap imgAll, bool isPrimary,string detectType)
        {
            try
            {
                for (int i = 0; i < detection.Detail.ListPattern.Count; i++)
                {
                    DefectPattern pattern = detection.Detail.ListPattern[i];
                    for (int j = 0; j < pattern.ListReason.Count; j++)
                    {
                        if (saveCount >= maxCount)
                        {
                            break;
                        }
                        DefectReason reason = pattern.ListReason[j];
                        //不良名称
                        string strCodeName = reason.DefectCode.ToString();
                      
                        //查找不良
                        bool isEqual = reason.DefectCode == detection.Detail.MainDefectCode;
                        if (isPrimary == false) isEqual = !isEqual;
                        if (isEqual)
                        {
                            string strPatternId = pattern.PatternName;
                            int ptnIndex = 0;
                            foreach (var item in detection.Detail.DictInfo)
                            {
                                if (item.Value.PatternName== strPatternId)
                                {
                                    ptnIndex = item.Value.PatternIndex;
                                    break;
                                }
                            }
                      
                            //获取原始图片
                            Bitmap srcImg = detection.OriginalImages[ptnIndex].ToBitmap();
                         
                            for (int k = 0; k < reason.DefectAreas.Count; k++)
                            {
                                if (saveCount >= maxCount)
                                {
                                    break;
                                }
                                DefectArea area = reason.DefectAreas[k];

                                //绘制图片需加上Roi区域信息
                                int x2 = area.X;
                                int y2 = area.Y;
                                string[] id = detection.Detail.PanelID.Split('_');
                                //绘制小图
                                Rectangle rect = new Rectangle(x2 - 300, y2 - 300, 600, 600);
                                Bitmap img = new Bitmap(rect.Width, rect.Height);
                                using (Graphics g = Graphics.FromImage(img))
                                {
                                    g.InterpolationMode = InterpolationMode.Low;
                                    g.DrawImage(srcImg, new Rectangle(0, 0, rect.Width, rect.Height),
                                                rect, GraphicsUnit.Pixel);
                                    StringBuilder sb = new StringBuilder();
                                    sb.AppendLine($"EquipID:{DFS.DFSSettings.EQPID},RecipeID:{RecipeManager.CurRecipe.RecipeID},Time:{detection.Detail.StartTime}");
                                    sb.AppendLine($"GlassID:{id[0]},CellID:{id[1]},X/Y:{area.X}/{area.Y},Width/Height:{area.Width}/{area.Height}");
                                    sb.Append($"DetectType:{detectType},DefectCode:{reason.DefectCode}");
                                    g.DrawString(sb.ToString(), new Font("微软雅黑",9),Brushes.Lime,10,img.Height-50);
                                }
                                //保存小图
                                string strFileName = string.Join("_", detection.Detail.PanelID, DFS.DFSSettings.StepID, saveCount + 1,
                                    area.X, area.Y, strCodeName, detection.Detail.StartTime.ToString("yyyyMMdd_HHmmss"), detectType, pattern.PatternName) + ".jpg";
                                if (string.IsNullOrEmpty(strPath) == false)
                                {
                                    img.Save(strPath + strFileName, ImageFormat.Jpeg);
                                }
                                //绘制示意图
                                if (imgAll != null)
                                {
                                    using (Graphics g = Graphics.FromImage(imgAll))
                                    {
                                        int x1 = (int)(area.X * 1.0 / detection.Detail.ImgROI.Width * rectAll.Width);
                                        int y1 = (int)(area.Y * 1.0 / detection.Detail.ImgROI.Height * rectAll.Height);
                                        g.FillRectangle(Brushes.Black, x1, y1, 10, 10);
                                    }
                                }
                                saveCount++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("ImageSaveHelper").Info(ex.ToString());
            }
        }

    
        #endregion
    }
}
