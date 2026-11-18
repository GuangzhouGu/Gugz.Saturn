using CommonLib.Entities;
using HYC.HTTools;
using HYC.SaturnAoi.Devices.Client;
using HYC.SaturnAoi.Devices.ClientPC;
using HYC.SaturnAoi.Entities;
using HYC.SaturnAoi.Params;
using HYC.SaturnDB;
using HYC.SaturnDB.DAL;
using HYC.StandardAoi.UI.View;
using NullFX.CRC;
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
    public partial class FrmCheck : ViewBaseEx
    {
        public FrmCheck()
        {
            InitializeComponent();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<byte> tempList = new List<byte>();
            tempList.AddRange(BitConverter.GetBytes((short)0x1063));
            tempList.AddRange(BitConverter.GetBytes((short)0x0000));
            tempList.AddRange(BitConverter.GetBytes((short)0x0000));
            //var ret2 = CrcHelper.ComputeChecksum(Crc16Algorithm.Standard,tempList.ToArray(), 0,tempList.Count());
            var ret2 = CrcHelper.Crc16( tempList.ToArray(), tempList.Count());
        }
        byte[] Datas = null;
        DateTime DateTime = DateTime.Now;
        private void button1_Click_1(object sender, EventArgs e)
        {
            DetectReqArgs detectReqArgs = new DetectReqArgs();
            detectReqArgs.GlassID = "1234";
            detectReqArgs.GlassNum = 0;
            detectReqArgs.CellID = "AL05";
            detectReqArgs.ColumnNum = 4;
            detectReqArgs.PCIndex = 0;

            DetectResultArgs detectResultArgs = new DetectResultArgs();
            detectResultArgs.StartTime = DateTime.Now.AddSeconds(-12).ToString("yyyyMMdd HH:mm:ss"); ;
            detectResultArgs.EndTime = DateTime.Now.ToString("yyyyMMdd HH:mm:ss"); ;
            detectResultArgs.Result = "OK";
            detectResultArgs.DetectReqArgs = detectReqArgs;
            Datas = ClassHelper.ObjectToBytes(detectResultArgs);
            ClientHelper.Instance.SendDetectResult(detectResultArgs);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var obj=ClassHelper.BytesToObject<DetectResultArgs>(Datas);
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            //var cellStartModel = new CellStartTimeDal().GetByID(textBox1.Text);
            //if (cellStartModel!=null)
            //{
            //    MessageBox.Show("查询成功");
            //}

        }

        private void FrmCheck_Load(object sender, EventArgs e)
        {
           
        }
    }
}
