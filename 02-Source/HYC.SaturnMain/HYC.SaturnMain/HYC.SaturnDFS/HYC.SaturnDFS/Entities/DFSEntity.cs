using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using DFS;
using DFS.DataFile;
using DFS.Model;
using System.Windows.Forms;
using CIMBOE.Block;
using HYC.SaturnDB.DAL;

using HYC.HTPCBase.Extension.Recipe;
using HYC.SaturnMain;
using HYC.SaturnDB.Model;
using HYC.HTFile;
using System.ComponentModel;
using System.Reflection;
using System.Xml.Serialization;
using System.Xml;
using HYC.HTPCBase.Extension.Entities;
using HYC.HTLog;
using HYC.SaturnDFS.Entities;

namespace HYC.SaturnMain.Entities
{
    public class DFSEntity
    {
        public static DataFile_Saturn DataFileSaturn =null;
        public static DataFile_Saturn CIEDataFileSaturn = null;
        public static GLASS_RAW_XML GLASS_RAW_XML = null;
        public static event Action<string, string, Brush> OnShowMsg;
        /// <summary>
        /// 初始化，程序启动时调用一次
        /// </summary>
        public static void Init()
        {
            StartUpload();
        }

        /// <summary>
        /// 保存光学检测结果到DFS数据块
        /// </summary>
        /// <param name="glassID">检测结果类</param>
        private static void SaveCIEDataToDFS(string glassID)
        {
            try
            {
                CIEDataFileSaturn = new DataFile_Saturn();
                var glassDataModel = new JobDataDal().Get(glassID);
                if (glassDataModel != null)
                {
                    var glassData = new GlassDataA();
                    glassData.FromString(glassDataModel.JobData);
                    var cellStartModel = new CellStartTimeDal().GetByID(glassID);
                    var totalModel = new TotalInfoDal().GetByIDAndTime(glassID, cellStartModel.StartTime);
                    GLS_INF gLS_INF = CIEDataFileSaturn.GLS_INF;
                    gLS_INF.FIELD_COUNT = $"{Global.PanelPara.CellCountX * Global.PanelPara.CellCountY}";
                    gLS_INF.LOT_ID = glassData.Lot_ID;
                    gLS_INF.STEP_ID = DFSSettings.StepID;
                    gLS_INF.HGLS_ID = glassData.Glass_ID;
                    gLS_INF.SLOT_ID = "1";
                    gLS_INF.HGLS_JUDGE = totalModel.JobJudge;
                    gLS_INF.HGLS_GRADE = totalModel.JobGrade;
                    gLS_INF.PRODUCT_ID = glassData.Prod_ID;
                    gLS_INF.EQUIPMENT_ID = DFSSettings.EQPID;
                    gLS_INF.RECIPE_NAME = Global.CurRecipeID;
                    if (totalModel.StartTime != null && totalModel.EndTime != null)
                    {
                        gLS_INF.START_TIME = totalModel.StartTime?.ToString("yyyyMMddHHmmss");
                        gLS_INF.END_TIME = totalModel.EndTime?.ToString("yyyyMMddHHmmss");
                        gLS_INF.PROCESS_TIME = totalModel.EndTime?.Subtract((DateTime)totalModel.StartTime).TotalSeconds.ToString();
                    }
                    gLS_INF.PROGRAM = DateTime.Now.ToString("yyyy_MM_dd");

                    GLS_SUM gLS_SUM = CIEDataFileSaturn.GLS_SUM;
                    var pnl_INFList = CIEDataFileSaturn.List_PNL_INF;
                    var pnl_SUMList = CIEDataFileSaturn.List_PNL_SUM;
                    gLS_SUM.FIELD_COUNT = Global.PanelPara.CellCountX * Global.PanelPara.CellCountY;


                    GLASS_RAW_XML = new GLASS_RAW_XML();
                    GLASS_RAW_XML.GLASS_INF = new GLASS_INF_RAW_XML();
                    GLASS_RAW_XML.GLASS_INF.LOT_ID = glassData.Lot_ID;
                    GLASS_RAW_XML.GLASS_INF.STEP_ID = DFSSettings.StepID;
                    GLASS_RAW_XML.GLASS_INF.SLOT_ID = "1";
                    GLASS_RAW_XML.GLASS_INF.PRODUCT_ID = glassData.Prod_ID;
                    GLASS_RAW_XML.GLASS_INF.START_TIME = totalModel.StartTime?.ToString("yyyyMMddHHmmss");
                    GLASS_RAW_XML.GLASS_INF.PROGRAM = DateTime.Now.ToString("yyyy_MM_dd");
                    GLASS_RAW_XML.GLASS_INF.GLS_ID = glassData.Glass_ID;
                    GLASS_RAW_XML.PNL_INF = new List<PANEL_RAW_XML>();
                }

            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DFSEntity").Info($"SaveDFSData方法执行异常，异常信息：{ex}");
            }

        }

        /// <summary>
        /// 保存AOI检测结果到DFS数据块
        /// </summary>
        /// <param name="glassID">GlassID</param>
        public static void SaveAoiDataToDFS(string glassID)
        {
            try
            {
                DataFileSaturn = new DataFile_Saturn();
                var glassDataModel = new JobDataDal().Get(glassID);
                if (glassDataModel!=null)
                {
                    var glassData = new GlassDataA();
                    glassData.FromString(glassDataModel.JobData);
                    var cellStartModel = new CellStartTimeDal().GetByID(glassID);
                    var totalModel = new TotalInfoDal().GetByIDAndTime(glassID, cellStartModel.StartTime);
                    GLS_INF gLS_INF = DataFileSaturn.GLS_INF;
                    gLS_INF.FIELD_COUNT =$"{Global.PanelPara.CellCountX * Global.PanelPara.CellCountY}" ;
                    gLS_INF.LOT_ID = glassData.Lot_ID;
                    gLS_INF.STEP_ID = DFSSettings.StepID;
                    gLS_INF.HGLS_ID = glassData.Glass_ID;
                    gLS_INF.SLOT_ID = "1";
                    gLS_INF.HGLS_JUDGE = totalModel.JobJudge;
                    gLS_INF.HGLS_GRADE = totalModel.JobGrade;
                    gLS_INF.PRODUCT_ID = glassData.Prod_ID;
                    gLS_INF.EQUIPMENT_ID = DFSSettings.EQPID;
                    gLS_INF.RECIPE_NAME = Global.CurRecipeID;
                    gLS_INF.CAMERA1_TEST_START_PNL = "4";
                    gLS_INF.CAMERA1_TEST_END_PNL = "1";
                    gLS_INF.CAMERA2_TEST_START_PNL = "4";
                    gLS_INF.CAMERA2_TEST_END_PNL = "1";
                    gLS_INF.CAMERA3_TEST_START_PNL = "4";
                    gLS_INF.CAMERA3_TEST_END_PNL = "1";
                    gLS_INF.CAMERA4_TEST_START_PNL = "9";
                    gLS_INF.CAMERA4_TEST_END_PNL = "5";
                    gLS_INF.CAMERA5_TEST_START_PNL = "9";
                    gLS_INF.CAMERA5_TEST_END_PNL = "5";
                    gLS_INF.CAMERA6_TEST_START_PNL = "9";
                    gLS_INF.CAMERA6_TEST_END_PNL = "5";
                    if (totalModel.StartTime != null && totalModel.EndTime != null)
                    {
                        gLS_INF.START_TIME = totalModel.StartTime?.ToString("yyyyMMddHHmmss");
                        gLS_INF.END_TIME = totalModel.EndTime?.ToString("yyyyMMddHHmmss");
                        gLS_INF.PROCESS_TIME = totalModel.EndTime?.Subtract((DateTime)totalModel.StartTime).TotalSeconds.ToString();
                    }
                    gLS_INF.PROGRAM = DateTime.Now.ToString("yyyy_MM_dd");

                    GLS_SUM gLS_SUM = DataFileSaturn.GLS_SUM;
                    var pnl_INFList = DataFileSaturn.List_PNL_INF;
                    var pnl_SUMList = DataFileSaturn.List_PNL_SUM;
                    var pnl_DFTList = DataFileSaturn.List_PNL_DFT;
                    gLS_SUM.FIELD_COUNT = Global.PanelPara.CellCountX * Global.PanelPara.CellCountY;
                    for (int i = 0; i < Global.PanelPara.CellCountX; i++)
                    {
                        for (int j = 0; j < Global.PanelPara.CellCountY; j++)
                        {
                            var aoiDetectModel = new AoiDetectInfoDal().GetByID(glassID, Global.GetCellID(i, j), "Aoi");
                            if (aoiDetectModel != null)
                            {
                                var pnlINF = new PNL_INF();
                                pnlINF.PNL_ID = glassID + Global.GetCellID(i, j);
                                pnlINF.PNL_JUDGE = "***";
                                pnlINF.PNL_GRADE = "***";
                                pnlINF.PNL_X_SIZE = Global.PanelPara.CellSizeX;
                                pnlINF.PNL_Y_SIZE = Global.PanelPara.CellSizeY;
                                pnlINF.PNL_X_POS = Global.PanelPara.CellEdgeOffsetX;
                                pnlINF.PNL_Y_POS = Global.PanelPara.CellEdgeOffsetY;

                                pnl_INFList.Add(pnlINF);

                                var pnlSUM = new PNL_SUM();
                                pnlSUM.DEFECT_QTY = aoiDetectModel.TotalDefectCount;
                                pnlSUM.MASK_DEFECT_QTY = aoiDetectModel.BirghtDotDefectCount;
                                pnlSUM.MURA_DEFECT_PNL = aoiDetectModel.LineDefectCount;
                                pnlSUM.PARTICLE_QTY = aoiDetectModel.DarkDotDefectCount;

                                pnl_SUMList.Add(pnlSUM);

                                gLS_SUM.INPUT_PNL_QTY += 1;
                                gLS_SUM.PNL_JUDGE_G_QTY += 1;
                                gLS_SUM.HGlass_DEFECT_TDQTY = 0;
                                gLS_SUM.S_DEFECT_TDQTY = 0;
                                if (aoiDetectModel.Result != "OK")
                                {
                                    gLS_SUM.M_DEFECT_TDQTY -= 1;
                                    gLS_SUM.L_DEFECT_TDQTY += 1;
                                    gLS_SUM.E_DEFECT_TDQTY += aoiDetectModel.TotalDefectCount;
                                    gLS_SUM.U_DEFECT_TDQTY += aoiDetectModel.BirghtDotDefectCount;
                                    gLS_SUM.O_DEFECT_TDQTY += aoiDetectModel.LineDefectCount;
                                    gLS_SUM.Black_DEFECT_TDQTY += aoiDetectModel.DarkDotDefectCount;
                                    gLS_SUM.White_DEFECT_TDQTY += aoiDetectModel.MuraDefectCount;

                                    gLS_SUM.AA_DEFECT_TDQTY += aoiDetectModel.XsycDefectCount;

                                    var defectModelList = new AoiNGInfoDal().GetByID(glassID, Global.GetCellID(i, j), "Aoi");
                                    if (defectModelList != null)
                                    {
                                        for (int m = 0; m < defectModelList.Count; m++)
                                        {
                                            try
                                            {
                                                var pnlDFT = new PNL_DFT();
                                                pnlDFT.PNL_ID = pnlINF.PNL_ID;
                                                pnlDFT.DEFECT_NO = defectModelList[m].DefectNum.ToString();
                                                pnlDFT.AOI_DEFECT_CODE = defectModelList[m].ErrorCode.ToString();
                                                pnlDFT.DEFECT_TYPE_1 = defectModelList[m].DefectType.ToString();
                                                pnlDFT.PNL_COOR_X = defectModelList[m].CoordinateX.ToString();
                                                pnlDFT.PNL_COOR_Y = defectModelList[m].CoordinateY.ToString();
                                                pnlDFT.DEFECT_SIZE_X = defectModelList[m].DefectSizeX.ToString();
                                                pnlDFT.DEFECT_SIZE_Y = defectModelList[m].DefectSizeY.ToString();
                                                pnlDFT.AOI_DEFECT_SIZE = defectModelList[m].DefectSizeX.ToString();
                                                pnlDFT.DEFECT_LENGTH = "***";
                                                pnlDFT.DEFECT_AREA = "***";
                                                pnlDFT.DEFECT_TYPE_2 = "Aoi";
                                                pnl_DFTList.Add(pnlDFT);
                                            }
                                            catch (Exception ex)
                                            {
                                                LogManager.GetLogger("DFSEntity").Info($"保存Aoi不良数据异常，异常信息：{ex}");
                                            }
                                         
                                        }
                                    }

                                    defectModelList = new AoiNGInfoDal().GetByID(glassID, Global.GetCellID(i, j), "Mura");
                                    if (defectModelList != null)
                                    {
                                        for (int m = 0; m < defectModelList.Count; m++)
                                        {
                                            try
                                            {
                                                var pnlDFT = new PNL_DFT();
                                                pnlDFT.PNL_ID = pnlINF.PNL_ID;
                                                pnlDFT.DEFECT_NO = defectModelList[m].DefectNum.ToString();
                                                pnlDFT.AOI_DEFECT_CODE = defectModelList[m].ErrorCode.ToString();
                                                pnlDFT.DEFECT_TYPE_1 = defectModelList[m].DefectType.ToString();
                                                pnlDFT.PNL_COOR_X = defectModelList[m].CoordinateX.ToString();
                                                pnlDFT.PNL_COOR_Y = defectModelList[m].CoordinateY.ToString();
                                                pnlDFT.DEFECT_SIZE_X = defectModelList[m].DefectSizeX.ToString();
                                                pnlDFT.DEFECT_SIZE_Y = defectModelList[m].DefectSizeY.ToString();
                                                pnlDFT.AOI_DEFECT_SIZE = defectModelList[m].DefectSizeX.ToString();
                                                pnlDFT.DEFECT_LENGTH = "***";
                                                pnlDFT.DEFECT_AREA = "***";
                                                pnlDFT.DEFECT_TYPE_2 = "Mura";
                                                pnl_DFTList.Add(pnlDFT);
                                            }
                                            catch (Exception ex)
                                            {
                                                LogManager.GetLogger("DFSEntity").Info($"保存Mura不良数据异常，异常信息：{ex}");
                                            }
                                        }
                                    }

                                    defectModelList = new AoiNGInfoDal().GetByID(glassID, Global.GetCellID(i, j), "SquintMura");
                                    if (defectModelList != null)
                                    {
                                        for (int m = 0; m < defectModelList.Count; m++)
                                        {
                                            try
                                            {
                                                var pnlDFT = new PNL_DFT();
                                                pnlDFT.PNL_ID = pnlINF.PNL_ID;
                                                pnlDFT.DEFECT_NO = defectModelList[m].DefectNum.ToString();
                                                pnlDFT.AOI_DEFECT_CODE = defectModelList[m].ErrorCode.ToString();
                                                pnlDFT.DEFECT_TYPE_1 = defectModelList[m].DefectType.ToString();
                                                pnlDFT.PNL_COOR_X = defectModelList[m].CoordinateX.ToString();
                                                pnlDFT.PNL_COOR_Y = defectModelList[m].CoordinateY.ToString();
                                                pnlDFT.DEFECT_SIZE_X = defectModelList[m].DefectSizeX.ToString();
                                                pnlDFT.DEFECT_SIZE_Y = defectModelList[m].DefectSizeY.ToString();
                                                pnlDFT.AOI_DEFECT_SIZE = defectModelList[m].DefectSizeX.ToString();
                                                pnlDFT.DEFECT_LENGTH = "***";
                                                pnlDFT.DEFECT_AREA = "***";
                                                pnlDFT.DEFECT_TYPE_2 = "SquintMura";
                                                pnl_DFTList.Add(pnlDFT);
                                            }
                                            catch (Exception ex)
                                            {
                                                LogManager.GetLogger("DFSEntity").Info($"保存斜视Mura不良数据异常，异常信息：{ex}");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
                SaveDFSFile();
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("DFSEntity").Info($"SaveDFSData方法执行异常，异常信息：{ex}");
            }
        }

        /// <summary>
        /// 添加不良明细
        /// </summary>
        /// <param name="model">Defect类</param>
        public static void AddDefectList( DFSItemModel model)
        {
            DataFileSaturn.List_PNL_DFT.Add((PNL_DFT)model);
        }

        /// <summary>
        /// 将DFS数据写入本地文件，并储存数据块记录
        /// </summary>

        public static void SaveDFSFile()
        {
            if (DFSSettings.IsDFSEnabled == false) return;
            List<string> patternList = new List<string>();
            try
            {
                string filePath = @"D:\DataFile\" + DateTime.Now.ToString("yyyyMMdd") + @"\";
                string glassID = DataFileSaturn.GLS_INF.HGLS_ID;
                if (string.IsNullOrEmpty(glassID)) return;
                //创建本地文件夹
                filePath += glassID + @"\";
                if (Directory.Exists(filePath) == false)
                    Directory.CreateDirectory(filePath);
                //写入本地文件
                string fileName = filePath + DFSSettings.GetDataFileName(DataFileSaturn.GLS_INF.HGLS_ID);
                string xmlFileName1 = filePath + DFSSettings.GetXmlFileName(DataFileSaturn.GLS_INF.LOT_ID, DataFileSaturn.GLS_INF.HGLS_ID,DataFileSaturn.GLS_INF.HGLS_JUDGE, DataFileSaturn.GLS_INF.HGLS_GRADE, DataFileSaturn.GLS_INF.END_TIME, DataFileSaturn.GLS_INF.START_TIME);
                string xmlFileName2 = filePath + DFSSettings.GetXmlFileName2(DataFileSaturn.GLS_INF.LOT_ID, Global.GetCellID(0,Global.PanelPara.CellCountY), Global.GetCellID(0, 0));
                DataFileSaturn.WriteFile(fileName);
                string dmyFileName = $"{fileName}.dmy";
                using (FileStream fsWrite = new FileStream(dmyFileName, FileMode.Create, FileAccess.Write)) { }
                //生成xml
                GLASS gLASS = new GLASS { GLASS_INF= DataFileSaturn.GLS_INF ,PNL_INF= DataFileSaturn.List_PNL_INF};
                EntityToXMLFile(gLASS, xmlFileName1);
                GLASS2 gLASS2 = new GLASS2 { GLASS_INF = DataFileSaturn.GLS_INF, PNL_INF = DataFileSaturn.List_PNL_DFT };
                EntityToXMLFile(gLASS2, xmlFileName2);
                SaveCieDataToFile(filePath, glassID);
                //写入数据库记录
                DfsUploadInfoModel model = new DfsUploadInfoModel();
                model.GlassID = glassID;
                model.StartTime = DateTime.Now;
                model.PcIndex =-1;
                model.FilePath = fileName;
                model.SendFlg = 1;
                model.DetectType = "Aoi";
                DfsUploadInfoDal dal = new DfsUploadInfoDal();
                dal.Insert(model);
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("DFSEntity","DFS文件保存失败！异常信息：" + ex.Message, Brushes.Red);
            }
        }

        public static void SaveCieDataToFile(string filePath,string glassID)
        {
            try
            {
                SaveCIEDataToDFS(glassID);
                string fileNameCie = filePath + DFSSettings.GetCIEDataFileName(glassID);
                CIEDataFileSaturn.WriteFile(fileNameCie);
                string aoiPc1Path = @"\\192.168.1.102\MultiAngleData";
                string aoiPc2Path = @"\\192.168.1.103\MultiAngleData";
                string fileName1 = Path.Combine(aoiPc1Path,DateTime.Now.ToString("yyyy-MM-dd"), glassID) +".csv";
                using (StreamWriter sw = new StreamWriter(fileNameCie,true))
                {
                    if (File.Exists(fileName1))
                    {
                        using (StreamReader reader = new StreamReader(fileName1))
                        {
                            var str= reader.ReadToEnd();
                            GetRawData(str, glassID);
                            sw.WriteLine(str);
                        }
                    }
                    string fileName2 = Path.Combine(aoiPc2Path, DateTime.Now.ToString("yyyy-MM-dd"), glassID) + ".csv";
                    if (File.Exists(fileName2))
                    {
                        using (StreamReader reader = new StreamReader(fileName2))
                        {
                            var str = reader.ReadToEnd();
                            GetRawData(str, glassID);
                            sw.WriteLine(str);
                        }
                    }
                }
               var str2= GLASS_RAW_XML.EntityToString();
                string fileNameCie2 = filePath + DFSSettings.GetXmlFileName2(CIEDataFileSaturn.GLS_INF.LOT_ID, Global.GetCellID(0, Global.PanelPara.CellCountY), Global.GetCellID(0, 0),true);
                using (StreamWriter sw = new StreamWriter(fileNameCie2, true))
                {
                    sw.WriteLine(str2);
                }
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("DFSEntity", "SaveCieDataToFile方法执行失败！异常信息：" + ex.Message, Brushes.Red);
            }
        }
        #region 上传线程
        private static void StartUpload()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    DfsUploadDoWork();
                    Thread.Sleep(1000);
                }
            });
        }

        private static void DfsUploadDoWork()
        {
            try
            {
                if (DFSSettings.IsDFSEnabled == false) return;
                DfsUploadInfoDal dal = new DfsUploadInfoDal();
                var lst = dal.GetAll();
                if (lst!=null)
                {
                    for (int i = 0; i < lst.Count; i++)
                    {
                        DfsUploadInfoModel model = lst[i];
                        if (model.PcIndex==-1)
                        {
                            if (string.IsNullOrEmpty(model.GlassID)) continue;
                            //开始上传文件
                            FileInfo file = new FileInfo(model.FilePath);
                            if (file.Exists)
                            {
                                bool result = DFSManager.Instance.UploadDataFile(model.GlassID, model.FilePath);
                                if (result)
                                {
                                    //更新DB
                                    model.SendFlg = 0;
                                    dal.Update(model);
                                }
                                else
                                {
                                    //更新DB
                                    model.SendFlg = 2;
                                    dal.Update(model);
                                }
                            }
                            else
                            {
                                //更新DB
                                model.SendFlg = -1;
                                dal.Update(model);
                            }
                        }
               
                    }
                }   
     
            }
            catch (System.Data.SqlClient.SqlException) { }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("DFSEntity",$"DfsUploadDoWork方法执行失败，异常信息：{ex}"  , Brushes.Red);
                LogManager.GetLogger("DFSEntity").Info(ex.ToString());
            }
        }

        #endregion

        public static void EntityToXMLFile<T>(T entity, string fileName)
        {
            try
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.OmitXmlDeclaration = true;
                settings.Indent = true;
                XmlWriter writer = XmlWriter.Create(fileName, settings);
                XmlSerializer xmlSerializer = new XmlSerializer(entity.GetType());
                xmlSerializer.Serialize(writer, entity, ns);
                writer.Close();
                writer.Dispose();
            }
            catch (Exception ex)
            {
                OnShowMsg?.Invoke("DFSEntity", $"EntityToXMLFile方法执行失败，异常信息：{ex}", Brushes.Red);
            }
        }
        private static void GetRawData(string str,string glassID)
        {
            try
            {
                var strs = str.Split().Where(p => p != "").ToArray();
                PANEL_RAW_XML[] panel = null;
                if (strs != null && strs.Length > 2)
                {
                    var headerTxt = strs[0].Split(',');
                    for (int i = 1; i < strs.Length; i++)
                    {
                        var rowStrs = strs[i].Split(',');
                        if (string.IsNullOrEmpty(rowStrs[0])==false)
                        {
                            if (panel != null)
                            {
                                GLASS_RAW_XML.PNL_INF.AddRange(panel);
                            }
                            panel = new PANEL_RAW_XML[8];
                            for (int j = 0; j < 8; j++)
                            {
                                panel[j] = new PANEL_RAW_XML();
                                panel[j].PANEL_ID = glassID + rowStrs[0];
                                panel[j].INDEX = rowStrs[1];
                                panel[j].ITME = headerTxt[5 + j];
                                panel[j].RAWs = new List<string>();
                                panel[j].RAWs.Add(rowStrs[5 + j]);
                            }
                        }
                        else
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                panel[j].RAWs.Add(rowStrs[5 + j]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
           
        }
    }
}
