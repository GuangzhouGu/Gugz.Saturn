using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DFS.Model
{
    public class PNL_PNT : DFSItemModel
    {
        public string POINT_NO { get; set; }        //eg. 1,2,3,445,468,472
        public string MODE1 { get; set; }           //eg. Normal1,Normal2,Normal6,HBM
        public string GRAY { get; set; }            //eg. 0gray,1gray,87gray,255gray
        public string COORDINATES_X { get; set; }   //eg. x0.310 y0.320
        public string COORDINATES_Y { get; set; }   //eg. x0.310 y0.320
        public string BRIGHTNESS { get; set; }      //eg. 450
        public string CCT { get; set; }             //色温 eg. 4201.1
        public string V1_V { get; set; }            //ELVDD-V
        public string V1_MA { get; set; }           //ELVDD(mA)-I
        public string V2_V { get; set; }            //ELVSS-V
        public string V2_MA { get; set; }           //ELVSS(mA)-I
        public string V3_V { get; set; }            //VDD1-V
        public string V3_MA { get; set; }           //VDD1(mA)-I
        public string V4_V { get; set; }            //VCI-V
        public string V4_MA { get; set; }           //VCI(mA)-I
        public string V5_V { get; set; }            //VBAT2-V
        public string V5_MA { get; set; }           //VBAT2(mA)-I
        public string V6_V { get; set; }            //VDD2-V
        public string V6_MA { get; set; }           //VDD2(mA)-I
        public string V7_V { get; set; }            //VEXT1-V
        public string V7_MA { get; set; }           //VEXT1(mA)-I
        public string V8_V { get; set; }            //VEXT2-V
        public string V8_MA { get; set; }           //VEXT2(mA)-I
        public string V9_V { get; set; }            //--
        public string V9_MA { get; set; }           //--
        public string ALL_POWER { get; set; }       //各画面功耗
        public string TUNINGJUDGE { get; set; }     //各画面光学结果
        public string VERIFYCURRENTJUDGE { get; set; }  //各画面电流check结果
        public string TUNINGTIME { get; set; }      //各画面工艺所需时间

        protected override string GetHeaderStart()
        {
            return "PNL_PNT^";
        }

        protected override string GetContentStart()
        {
            return "PNL_PNT_DATA^";
        }
    }
}
