using DFS;
using HYC.HTFile;
using HYC.HTFile.HTINI;
using HYC.HTLog;
using HYC.HTPCBase.Extension.Entities;
using HYC.HTPCBase.Extension.Recipe;
using HYC.SaturnAoi.Entities;
using HYC.SaturnDB;
using HYC.SaturnDFS.Devices;
using HYC.SaturnDFS.Entities;
using HYC.SaturnDFS.Param;
using HYC.SaturnMain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HYC.SaturnDFS
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            this.TopMost = false;
            InitDevices();
        }
        HTINIHelper Ini = new HTINIHelper(AppDomain.CurrentDomain.BaseDirectory + "Config.INI");
        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.Hide();
        
        }
        private void LoadPara(string recipeName)
        {
            var fileName = $"D:\\HYC.SaturnMain\\Config\\Recipe\\{recipeName}\\PanelPara.xml";
            Global.PanelPara=  HTXmlHelper.EntityFromXMLFile<PanelPara>(fileName);
        }
        private void InitDevices()
        {
            string connectStr = Ini.Read("DB", "ConnectionString", "");
            string sqlType = Ini.Read("DB", "SqlType", "");
            DBHelper.Init(connectStr);
            DFSManager.Instance.OnConnected += Instance_OnConnected;
            DFSManager.Instance.OnShowMsg += ShowDfsMsg;
            DFSManager.Instance.Init();
            DFSEntity.OnShowMsg += ShowDfsMsg;
            DFSEntity.Init();
            
            MasterPC.Instance.OnConnected += Instance_OnConnected;
            MasterPC.Instance.OnShowMsg += ShowMsg;
            ProcessCmd.OnShowMsg += ShowMsg;
            ProcessCmd.OnExecuteCommand += ProcessCmd_OnExecuteCommand;
            MasterPC.Instance.Init();
        }
        private void ShowDfsMsg(string logName,string msg,Brush bh)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string,string,Brush>(ShowDfsMsg),logName,msg,bh);
            }
            else
            {
                this.MsgBox.AddListBoxMsg(msg,bh);
                LogManager.GetLogger(logName).Info(msg);
            }
        }
        private void ProcessCmd_OnExecuteCommand(Command arg1, object arg2)
        {
            switch (arg1)
            {
                case Command.RecipeSync:
                    {
                        var curRecipe = (string)arg2;
                        LoadPara(curRecipe);
                        Global.CurRecipeID = curRecipe;
                    }
                    break;
                case Command.DfsUpload:
                    {
                        Task.Factory.StartNew(() => {
                            DFSEntity.SaveAoiDataToDFS((string)arg2);

                        });
                    }
                    break;
            }

        }

        private void Instance_OnConnected(string arg1, int arg2, bool arg3)
        {
            DeviceStsManager.UpdateDevStatus(arg1, arg3);
        }

        private void ShowMsg(string logName,string msg,Brush bh)
        {
            MessageManager.ShowMsg(msg, bh);
            LogManager.GetLogger(logName).Info(msg);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filePath = @"D:\DataFile\" + DateTime.Now.ToString("yyyyMMdd") + @"\";
            string glassID = "767Y433335B5A";
            if (string.IsNullOrEmpty(glassID)) return;
            //创建本地文件夹
            filePath += glassID + @"\";
            if (Directory.Exists(filePath) == false)
                Directory.CreateDirectory(filePath);
            DFSEntity.SaveCieDataToFile(filePath, glassID);
            //XmlTest();
        }
        private void XmlTest()
        {
            try
            {             
                string fileName = @"D:\Temp\Test.xml";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()); ;
            }
        }
    }
}
