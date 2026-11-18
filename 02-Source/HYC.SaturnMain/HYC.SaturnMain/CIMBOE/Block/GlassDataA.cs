/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2023 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：CIMBOE.Block
// 文件名称    ：GlassDataA
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：
// 功能描述    ：
// 使用说明    ：
//
//
// 创建标识：
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/

using HYC.HTLog;
using HYC.HTTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CIMBOE.Block
{
    public class GlassDataA : IArrayAble
    {
        /// <summary>
        /// 长度10个ushort，20个字节
        /// </summary>
        public string Lot_ID { get; set; } = string.Empty;
        /// <summary>
        /// 长度10个ushort, 20个字节
        /// </summary>
        public string Glass_ID { get; set; } = string.Empty;
        /// <summary>
        /// 长度5个ushort, 10个字节
        /// </summary>
        public string OPER_ID { get; set; } = string.Empty;
        /// <summary>
        /// 长度1个ushort, (1~65535)
        /// </summary>
        public ushort LOT_Sequence_No { get; set; }
        /// <summary>
        /// 长度1个ushort, (1~65535)
        /// </summary>
        public ushort Slot_Sequence_No{ get; set; }
        /// <summary>
        /// 长度1个ushort，2个字节
        /// </summary>
        public ushort Glass_Judge_Code { get; set; }
        /// <summary>
        /// 长度1个ushort，2个字节
        /// </summary>
        public ushort Glass_Grade_Code { get; set; } 
        /// <summary>
        /// 长度20个ushort, (1~65535)
        /// </summary>
        public ushort[] PPID { get; set; } = new ushort[20];
        /// <summary>
        /// 长度1个ushort
        /// </summary>
        public ushort Substrate_Type { get; set; }
        /// <summary>
        /// 长度1个ushort
        /// </summary>
        public ushort Pair_Lot_Sequence_Number { get; set; }
        /// <summary>
        /// 长度1个ushort
        /// </summary>
        public ushort Pair_Slot_Sequence_Number { get; set; }
        /// <summary>
        /// 长度1个ushort
        /// </summary>
        public ushort Pair_Flag { get; set; }
        /// <summary>
        /// 长度10个ushort, 20个字节
        /// </summary>
        public string Prod_ID { get; set; }
        /// <summary>
        /// 长度2个ushort,
        /// 0:Non Processed,1: Processed
        /// </summary>
        public ushort[] Processing_Flag { get; set; } = new ushort[2];
        /// <summary>
        /// 长度2个ushort，
        /// Machine01 Inspection Judge Data：Bit0-Bit3，Machine02 Inspection Judge Data：Bit4-Bit7，Machine03 Inspection Judge Data：Bit8-Bit11，Machine04 Inspection Judge Data：Bit12-Bit15
        /// 00= Inspect Skip or No Judge ,01=“G” Good ,02=“R” Reprocess ,03=“N” NG
        ///04=“V” Need CVD,05=“C” Need CSI,06=“S” Scrap ,07=“W” R/W
        ///08=“K” Process Skip,09=“T” Retest ,10=“M” Mac/Mic ,11=      
        //12=      ,13=      ,14=      ,15=  
        /// </summary>
        public ushort[] Inspection_Judge_Data { get; set; } = new ushort[2];
        /// <summary>
        /// 长度2个ushort
        /// 0: Processing
        ///1: Skip
        /// </summary>
        public ushort[] Skip_Flag { get; set; } = new ushort[2];
        /// <summary>
        /// 长度2个ushort
        /// 0: Skip
        ///1: Inspection
        /// </summary>
        public ushort[] Inspection_Flag { get; set; } = new ushort[2];
        /// <summary>
        /// 长度1个ushort
        /// </summary>
        public ushort Mode { get; set; }
        /// <summary>
        /// 长度2个ushort
        /// </summary>
        public ushort[] Glass_Type { get; set; } = new ushort[2];
        #region Glass_Type[0]
        /// <summary>
        ///Glass_Type[0] Bit0-Bit3
        /// </summary>
        public ushort Glass_Size_Code { get; set; }
        /// <summary>
        ///Glass_Type[0] Bit4
        /// </summary>
        public ushort First_Lot_Glass_Code { get; set; }
        /// <summary>
        ///Glass_Type[0] Bit5
        /// </summary>
        public ushort Last_Lot_Glass { get; set; }
        /// <summary>
        ///Glass_Type[0] Bit6
        /// </summary>
        public ushort Dummy_Glass { get; set; }
        /// <summary>
        ///Glass_Type[0] Bit8-Bit11
        /// </summary>
        public ushort Glass_Thickness_Code { get; set; }
        /// <summary>
        ///Glass_Type[0] Bit12
        /// </summary>
        public ushort First_Recipe_Glass_In_One_Lot { get; set; }
        /// <summary>
        ///Glass_Type[0] Bit13
        /// </summary>
        public ushort Last_Recipe_Glass_In_One_Lot { get; set; }
        /// <summary>
        ///Glass_Type[0] Bit15
        /// </summary>
        public ushort Assembled { get; set; }
        #endregion

        #region Glass_Type[1]
        /// <summary>
        ///Glass_Type[0] Bit0-Bit1
        /// </summary>
        public ushort Glass_Angle { get; set; }
        /// <summary>
        ///Glass_Type[0] Bit2
        /// </summary>
        public ushort Glass_Flip { get; set; }
        /// <summary>
        ///Glass_Type[0] Bit3
        /// </summary>
        public ushort First_JOB_Glass_In_One_LOT { get; set; }
        /// <summary>
        ///Glass_Type[0] Bit4
        /// </summary>
        public ushort Last_JOB_Glass_In_One_LOT { get; set; }
        /// <summary>
        ///Glass_Type[0] Bit8-Bit10
        /// </summary>
        public ushort PortNoOfGlassSuppliedForSputterNPECVD { get; set; }
        /// <summary>
        ///Glass_Type[0] Bit11-Bit12
        /// </summary>
        public ushort Cutting_Glass_Type { get; set; }
        /// <summary>
        ///Glass_Type[0] Bit14，0:OK, 1:NG
        /// </summary>
        public ushort A_Type_Judge_Data_for_Half_Cutting { get; set; }
        /// <summary>
        ///Glass_Type[0] Bit15，0:OK, 1:NG
        /// </summary>
        public ushort B_Type_Judge_Data_for_Half_Cutting { get; set; }
        #endregion
        /// <summary>
        /// 长度1个ushort
        /// 0:Not Dummy
        /// 1: Normal
        /// 2: Bare Dummy
        /// 3:Tooling Dummy
        /// 4:Offset Dummy
        /// 5:PI Dummy
        /// 6:Key Dummy
        /// 7:TFE CVD Dummy
        /// 8:TFE Inkjet Dummy
        /// 9: Manual Dummy
        /// 10~ : Reserved
        /// </summary>
        public ushort Dummy_Type { get; set; }
        /// <summary>
        /// 长度1个ushort
        /// </summary>
        public ushort Processing_Count { get; set; }
        /// <summary>
        /// 长度8个ushort
        /// </summary>
        public string Option_Value  { get; set; }
        public ushort[] ToArray()
        {
            try
            {
                List<ushort> result = new List<ushort>();
                result.AddRange(HTValueConverter.AsciiStringToUshortArray(Lot_ID, 10));
                result.AddRange(HTValueConverter.AsciiStringToUshortArray(Glass_ID, 10));
                result.AddRange(HTValueConverter.AsciiStringToUshortArray(OPER_ID, 5));
                result.Add(LOT_Sequence_No);
                result.Add(Slot_Sequence_No);
                result.Add(Glass_Judge_Code);
                result.Add(Glass_Grade_Code);
                result.AddRange(PPID);
                result.Add(Substrate_Type);
                result.Add(Pair_Lot_Sequence_Number);
                result.Add(Pair_Slot_Sequence_Number);
                result.Add(Pair_Flag);
                result.AddRange(HTValueConverter.AsciiStringToUshortArray(Prod_ID, 10));
                result.AddRange(Processing_Flag);
                result.AddRange(Inspection_Judge_Data);
                result.AddRange(Skip_Flag);
                result.AddRange(Inspection_Flag);
                result.Add(Mode);

                result.AddRange(Glass_Type);
                result.Add(Dummy_Type);
                result.Add(Processing_Count);
                result.AddRange(HTValueConverter.AsciiStringToUshortArray(Option_Value, 8));
                return result.ToArray();
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("CimBlock").Fatal(ex.ToString());
            }
            return null;
        }
   
        public void FromArray(ushort[] data)
        {
            try
            {
                Lot_ID = HTValueConverter.UshortArrayToAsciiString(data, 0, 10);
                Glass_ID = HTValueConverter.UshortArrayToAsciiString(data, 10, 10);
                OPER_ID = HTValueConverter.UshortArrayToAsciiString(data, 20, 5);
                LOT_Sequence_No = data[25];
                Slot_Sequence_No = data[26];
                Glass_Judge_Code = data[27];
                Glass_Grade_Code = data[28];
                for (int i = 0; i < 20; i++)
                {
                    PPID[i] = data[29 + i];
                }
                Substrate_Type = data[49];
                Pair_Lot_Sequence_Number = data[50];
                Pair_Slot_Sequence_Number = data[51];
                Pair_Flag = data[52];
                Prod_ID = HTValueConverter.UshortArrayToAsciiString(data, 53, 10);
                Processing_Flag = data.Skip(63).Take(2).ToArray();
                Inspection_Judge_Data = data.Skip(65).Take(2).ToArray();
                Skip_Flag = data.Skip(67).Take(2).ToArray();
                Inspection_Flag = data.Skip(69).Take(2).ToArray();
                Mode = data[71];
                Glass_Type = data.Skip(72).Take(2).ToArray();
                AnalyzeGlassType();
                Dummy_Type = data[74];
                Processing_Count = data[75];
                Option_Value = HTValueConverter.UshortArrayToAsciiString(data, 76, 8);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("CimBlock").Fatal(ex.ToString());
            }
        }
        private void AnalyzeGlassType()
        {
            Glass_Size_Code = (ushort)(Glass_Type[0] & 0xf);
            First_Lot_Glass_Code = (ushort)(Glass_Type[0] & 0x10);
            Last_Lot_Glass = (ushort)(Glass_Type[0] & 0x20);
            Dummy_Glass = (ushort)(Glass_Type[0] & 0x40);
            Glass_Thickness_Code = (ushort)(Glass_Type[0] & 0xf00);
            First_Recipe_Glass_In_One_Lot = (ushort)(Glass_Type[0] & 0x1000);
            Last_Recipe_Glass_In_One_Lot = (ushort)(Glass_Type[0] & 0x2000);
            Assembled = (ushort)(Glass_Type[0] & 0x8000);

            Glass_Angle = (ushort)(Glass_Type[1] & 0x3);
            Glass_Flip = (ushort)(Glass_Type[1] & 0x4);
            First_JOB_Glass_In_One_LOT = (ushort)(Glass_Type[1] & 0x8);
            Last_JOB_Glass_In_One_LOT = (ushort)(Glass_Type[1] & 0x10);
            PortNoOfGlassSuppliedForSputterNPECVD = (ushort)(Glass_Type[1] & 0x700);
            Cutting_Glass_Type = (ushort)(Glass_Type[1] & 0x1800);
            A_Type_Judge_Data_for_Half_Cutting = (ushort)(Glass_Type[1] & 0x4000);
            B_Type_Judge_Data_for_Half_Cutting = (ushort)(Glass_Type[1] & 0x8000);
        }

        public override string ToString()
        {
            try
            {
                string build =string.Empty;
                build += "Lot_ID:" + Lot_ID + "\r";
                build += "Glass_ID:" + Glass_ID + "\r";
                build += "OPER_ID:" + OPER_ID + "\r";
                build += "LOT_Sequence_No:" + LOT_Sequence_No + "\r";
                build += "Slot_Sequence_No:" + Slot_Sequence_No + "\r";
                build += "Glass_Judge_Code:" + Glass_Judge_Code + "\r";
                build += "Glass_Grade_Code:" + Glass_Grade_Code + "\r";
                build += $"PPID:" + string.Join("?", PPID) + "\r";
                build += "Substrate_Type:" + Substrate_Type.ToString() + "\r";
                build += "Pair_Lot_Sequence_Number:" + Pair_Lot_Sequence_Number + "\r";
                build += "Pair_Slot_Sequence_Number:" + Pair_Slot_Sequence_Number + "\r";
                build += "Pair_Flag:" + Pair_Flag + "\r";
                build += "Prod_ID:" + Prod_ID + "\r";
                build += "Processing_Flag:" + string.Join("?", Processing_Flag) + "\r";
                build += "Inspection_Judge_Data:" + string.Join("?", Inspection_Judge_Data) + "\r";
                build += "Skip_Flag:" + string.Join("?", Skip_Flag) + "\r";
                build += "Inspection_Flag:" + string.Join("?", Inspection_Flag) + "\r";
                build += "Mode:" + Mode + "\r";
                build += "Glass_Type:" + string.Join("?", Glass_Type) + "\r";

                build += "Glass_Size_Code:" + Glass_Size_Code + "\r";
                build += "First_Lot_Glass_Code:" + First_Lot_Glass_Code + "\r";
                build += "Last_Lot_Glass:" + Last_Lot_Glass + "\r";
                build += "Dummy_Glass:" + Dummy_Glass + "\r";
                build += "Glass_Thickness_Code:" + Glass_Thickness_Code + "\r";
                build += "First_Recipe_Glass_In_One_Lot:" + First_Recipe_Glass_In_One_Lot + "\r";
                build += "Last_Recipe_Glass_In_One_Lot:" + Last_Recipe_Glass_In_One_Lot + "\r";
                build += "Assembled:" + Assembled + "\r";
                build += "Glass_Angle:" + Glass_Angle + "\r";
                build += "Glass_Flip:" + Glass_Flip + "\r";
                build += "First_JOB_Glass_In_One_LOT:" + First_JOB_Glass_In_One_LOT + "\r";
                build += "Last_JOB_Glass_In_One_LOT:" + Last_JOB_Glass_In_One_LOT + "\r";
                build += "PortNoOfGlassSuppliedForSputterNPECVD:" + PortNoOfGlassSuppliedForSputterNPECVD + "\r";
                build += "Cutting_Glass_Type:" + Cutting_Glass_Type + "\r";
                build += "A_Type_Judge_Data_for_Half_Cutting:" + A_Type_Judge_Data_for_Half_Cutting + "\r";
                build += "B_Type_Judge_Data_for_Half_Cutting:" + B_Type_Judge_Data_for_Half_Cutting + "\r";

                build += "Dummy_Type:" + Dummy_Type + "\r";
                build += "Processing_Count:" + Processing_Count + "\r";
                build += "Option_Value:" + Option_Value + "\r";
                return build;
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("CimBlock").Fatal(ex.ToString());
                return null;
            }
        }
        public string ToString2()
        {
            string ret = this.ToString().Replace("\r", ",").TrimEnd(',');
            return ret;
        }
        public void FromString(string value)
        {
            try
            {
                string[] temp = value.Split(',');
                PropertyInfo[] propertys = this.GetType().GetProperties();
                for (int i = 0; i < temp.Length; i++)
                {
                    string[] temp2 = temp[i].Split(':');
                    if (temp2[0] == propertys[i].Name)
                    {
                        if (temp2[1].Contains("?"))
                        {
                            string[] temp3 = temp2[1].Split('?');
                            Array array = (Array)propertys[i].GetValue(this);
                            Type type = array.GetValue(0).GetType();
                            for (int j = 0; j < array.Length; j++)
                            {
                                object value2 = Convert.ChangeType(temp3[j], type);
                                array.SetValue(value2, j);
                            }
                        }
                        else
                        {
                            object value2 = Convert.ChangeType(temp2[1], propertys[i].PropertyType);
                            propertys[i].SetValue(this, value2, null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("CimBlock").Fatal(ex.ToString());
            }
        }
    }
}
