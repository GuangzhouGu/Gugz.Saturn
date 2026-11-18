/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Technology Co.,LTD
// 版权所有。
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：HYC.SaturnMain.Devices.Cim
// 文件名称    ：LinkSignals.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/01/29 09:00:00 
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HYC.SaturnMain.Devices.Cim
{
    /// <summary>
    /// 上游发送给下游的信号
    /// </summary>
    public enum UpStreamSignal
    {
        Upstream_Inline,
        Upstream_Trouble,
        Stretch_Complete,
        Slot_Number_1,
        Slot_Number_2,
        Slot_Number_3,
        Slot_Number_4,
        Slot_Number_5,
        Slot_Number_6,
        Send_Ready,
        Slot_Pair_Flag,
        Arm_Slot_Pair_Flag,
        Job_Transfer_Signal_1,
        Job_Transfer_Signal_2,
        Send_Able,
        Send_Start,
        Send_Complete,
        Exchange_Possible,
        Exchange_Execute,
        Resume_Request,
        Resume_Ack,
        Resume_Nack,
        Cancel_Request,
        Cancel_Ack,
        Cancel_Nack,
        Conveyer_State,
        Shutter_State,
        Pin_State,
        Robot_Interlock,
        Robot_Vacuum,
        Grip_Up,
        Grip_Complete,
        //Pre_Check_Point,
        Abnormal,
        Empty,
        Idle,
        Run,
        Complete,
        Lift_Up_or_Pin_Up,
        Lift_Down_or_Pin_Down,
        Stopper_Up_or_Shutter_Open,
        Stopper_Down_or_Shutter_Close,
        Manual_Operation,
        Body_Moving,
        Glass_Exist_Arm_1,
        Glass_Exist_Arm_2,
        Glass_Exist_Arm_3,
        Glass_Exist_Arm_4,
        Unit_Number1,
        Unit_Number2,
        Unit_Number3,
        Pair_Slot_Number1,
        Pair_Slot_Number2,
        Pair_Slot_Number3,
        Pair_Slot_Number4,
        Pair_Slot_Number5,
        Pair_Slot_Number6,
        Position_Front,
        Position_Back,
        //Reserved,

    }
    /// <summary>
    /// 下游发送给上游的信号
    /// </summary>
    public enum DownStreamSignal
    {
        Downstream_Inline,
        Downstream_Trouble,
        Stretch_Complete,
        Slot_Number_1,
        Slot_Number_2,
        Slot_Number_3,
        Slot_Number_4,
        Slot_Number_5,
        Slot_Number_6,
        Receive_Ready,
        Slot_Pair_Flag,
        Arm_Slot_Pair_Flag,
        Job_Transfer_Signal_1,
        Job_Transfer_Signal_2,
        Receive_Able,
        Receive_Start,
        Receive_Complete,
        Exchange_Possible,
        Exchange_Execute,
        Resume_Request,
        Resume_Ack,
        Resume_Nack,
        Cancel_Request,
        Cancel_Ack,
        Cancel_Nack,
        Conveyer_State,
        Shutter_State,
        Pin_State,
        Robot_Interlock,
        Robot_Vacuum,
        Grip_Up,
        Grip_Complete,
        //Pre_Check_Point,
        Abnormal,
        Empty,
        Idle,
        Run,
        Complete,
        Lift_Up_or_Pin_Up,
        Lift_Down_or_Pin_Down,
        Stopper_Up_or_Shutter_Open,
        Stopper_Down_or_Shutter_Close,
        Manual_Operation,
        Body_Moving,
        Glass_Exist_Arm_1,
        Glass_Exist_Arm_2,
        Glass_Exist_Arm_3,
        Glass_Exist_Arm_4,
        Unit_Number1,
        Unit_Number2,
        Unit_Number3,
        Pair_Slot_Number1,
        Pair_Slot_Number2,
        Pair_Slot_Number3,
        Pair_Slot_Number4,
        Pair_Slot_Number5,
        Pair_Slot_Number6,
        Position_Front,
        Position_Back,
        //Reserved,
    }
}
