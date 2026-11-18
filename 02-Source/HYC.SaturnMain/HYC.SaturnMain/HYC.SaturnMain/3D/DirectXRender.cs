/*----------------------------------------------------------------
//公司名：   Suzhou HYC Technology Co.,LTD
//版权申明： Copyright © 2020 HYC. All Rights Reserved.
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：DirectXFormHost
// 文件名称    ：DirectXRender.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/8/23 12:32:49 
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
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DirectXFormHost
{
    class DirectXRender
    {
        [DllImport("DirectXRender.dll")]
        public static extern bool RenderInitD3D(IntPtr hwnd);

        [DllImport("DirectXRender.dll")]
        public static extern void RenderRun();

        [DllImport("DirectXRender.dll")]
        public static extern void RenderAddModel(int parentId, ModelInfo info);

        [DllImport("DirectXRender.dll")]
        public static extern void RenderUpdateModel(ModelInfo info);

        [DllImport("DirectXRender.dll")]
        public static extern void RenderDeleteModel(int modelId);

        [DllImport("DirectXRender.dll")]
        public static extern void RenderSetPosition(int modelId, float x, float y, float z);

        [DllImport("DirectXRender.dll")]
        public static extern void RenderSetRotation(int modelId, float x, float y, float z);

        [DllImport("DirectXRender.dll")]
        public static extern void RenderRotateCenter(int modelId, float x, float y, float z);

        [DllImport("DirectXRender.dll")]
        public static extern int RenderMsgProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam);
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct ModelInfo
    {
        //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
        [Description("模型文件名"), ReadOnly(true)]
        public string FileName { get; set; }

        [Description("模型Id"), ReadOnly(true)]
        public int ModelId { get; set; }                //模型Id

        //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
        [Description("模型名称")]
        public string ModelName { get; set; }            //模型名称

        [Description("缩放X")]
        public float ScaleX { get; set; }               //缩放X

        [Description("缩放Y")]
        public float ScaleY { get; set; }               //缩放Y

        [Description("缩放Z")]
        public float ScaleZ { get; set; }               //缩放Z

        [Description("旋转X")]
        public float RotationX { get; set; }            //旋转X

        [Description("旋转Y")]
        public float RotationY { get; set; }            //旋转Y

        [Description("旋转Z")]
        public float RotationZ { get; set; }            //旋转Z

        [Description("平移X")]
        public float PositionX { get; set; }            //平移X

        [Description("平移Y")]
        public float PositionY { get; set; }            //平移Y

        [Description("平移Z")]
        public float PositionZ { get; set; }            //平移Z

        [Description("设置到中心位置")]
        public bool IsSetToCenter { get; set; }         //设置到中心位置


    }
}
