using HYC.HTLog;
using HYC.SaturnAoi.Controls;
using HYC.SaturnAoi.DetectFlow;
using HYC.SaturnAoi.DetectFlow.MultiAngleDetect;
using HYC.SaturnAoi.Entities;
using HYC.StandardAoi.Events;
using HYC.StandardAoi.UI;
using HYC.StandardAoi.UI.Controls;
using HYC.StandardAoi.UI.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HYC.SaturnAoi.SubForms
{
    public partial class ViewMain : ViewBaseEx
    {
      
        public ViewMain()
        {
            InitializeComponent();
            GetCtrl();
            MultiAngleDetectFlow.OnShowDetectValue += MultiAngleDetectFlow_OnShowDetectValue;
            ucDetectionGroup1.Title = "斜视Mura";
            ucDetectionGroup1.DetectionType = StandardAoi.Parameter.DetectionType.Mura;
            SignalProvider.OnShowResult += ShowDetectionResult;
            ImageProducer.OnShowImage += ImageProducer_OnShowImage;       
        }

        private void ImageProducer_OnShowImage(string ptnName,StandardAoi.Parameter.ImageData obj)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string,StandardAoi.Parameter.ImageData>(ImageProducer_OnShowImage), ptnName, obj);
            }
            else
            {
                ucDetectionGroup1.ImageBox.ShowImage(ptnName,obj);
            }
        }

        Dictionary<string, UcMultiAngleDetectDetail> UcMultiAngleDetectDetailDic = null;
        private void FrmDetectDeail_Load(object sender, EventArgs e)
        {
            var manager=FrmMain.Instance.FrmStandardAoi.GetManager(0);
            ucDetectionGroup1.SetManager(manager);
           
        }

      

        public  UcDetectionGroup GetDetectionGroup()
        {
            return ucDetectionGroup1;
        }
         public void ShowDetectionResult(DetectionCompletedArgs args)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<DetectionCompletedArgs>(ShowDetectionResult), args);
            }
            else
            {
                ucDetectionGroup1.ShowDetectionResult(args.Result);
                ucDetectionGroup1.ShowProcessedImages(args.Result);
                ucDetectionGroup1.ShowPanelID(args.Result.Detail.PanelID);
            }
        
        }
        private void GetCtrl()
        {
            try
            {
                var ucMultiAngleDetectDetailList = new List<UcMultiAngleDetectDetail>();
                for (int i = 0; i < 4; i++)
                {
                    string name = $"ucMultiAngleDetectDetail{i + 1}";
                    var ctrls = Controls.Find(name, true);
                    if (ctrls.Length > 0)
                    {
                        ucMultiAngleDetectDetailList.Add(ctrls[0] as UcMultiAngleDetectDetail);
                    }
                }
                UcMultiAngleDetectDetailDic = ucMultiAngleDetectDetailList.ToDictionary(p => p.PatternName);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("FrmDetectDeail").Info($"GetCtrl方法执行异常，异常信息：{ex}"); ;
            }
        }
        private void MultiAngleDetectFlow_OnShowDetectValue(PatternDetectInfo info,string cellID)
        {
            try
            {
                if (info == null)
                {
                    if (UcMultiAngleDetectDetailDic != null)
                    {
                        foreach (var item in UcMultiAngleDetectDetailDic)
                        {
                            item.Value.ShowDetectDetail(null,null);
                        }
                    }
                }
                else
                {
                    if (UcMultiAngleDetectDetailDic != null && UcMultiAngleDetectDetailDic.Keys.Contains(info.PatternInfo.PatternName))
                    {
                        UcMultiAngleDetectDetailDic[info.PatternInfo.PatternName].ShowDetectDetail(info, cellID);
                    }
                }
            
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("FrmDetectDeail").Info($"GetCtrl方法执行异常，异常信息：{ex}"); ;
            }

        }
    }
}
