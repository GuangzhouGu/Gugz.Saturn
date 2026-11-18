/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.UI.Helper
// 文件名称    ：LanguageHelper.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/7/7 22:50:11 
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
using HYC.HTFile.HTXML;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace HYC.AutoPosition.UI.Helper
{
    class LanguageHelper
    {
        /// <summary>
        /// 消息文本定义
        /// </summary>
        public static MsgDef Msg = new MsgDef();

        /// <summary>
        /// 修改语言
        /// </summary>
        /// <param name="lang">待设置语言</param>
        public static void SetLanguage(string lang)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);

            if (lang != "zh-CN")
            {
                string folder = $"Language\\{lang}\\";
                string filename = folder + "HYC.AutoPosition.UI.lang.xml";
                if (File.Exists(filename))
                {
                    Msg = HTXMLSerializer.EntityFromXMLFile<MsgDef>(filename);
                }
                else
                {
                    if (lang == "en-US")
                    {
                        Msg.SetToEnglish();
                    }
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);
                    HTXMLSerializer.EntityToXMLFile(Msg, filename);
                }
            }
            else
            {
                Msg = new MsgDef();
            }
        }

        /// <summary>
        /// 加载语言
        /// </summary>
        /// <param name="form">加载语言的窗口</param>
        public static void LoadLanguage(Form form)
        {
            if (form != null)
            {
                ComponentResourceManager resources = new ComponentResourceManager(form.GetType());
                resources.ApplyResources(form, "$this");
                Loading(form, resources);
            }
        }

        /// <summary>
        /// 加载语言
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="resources">语言资源</param>
        private static void Loading(Control control, ComponentResourceManager resources)
        {
            if (control is Form)
            {
                resources = new ComponentResourceManager(control.GetType());
                resources.ApplyResources(control, "$this");
            }

            if (control is UserControl)
            {
                resources = new ComponentResourceManager(control.GetType());
            }

            if (control is MenuStrip)
            {
                //将资源与控件对应
                resources.ApplyResources(control, control.Name);
                MenuStrip ms = (MenuStrip)control;
                if (ms.Items.Count > 0)
                {
                    foreach (ToolStripMenuItem c in ms.Items)
                    {
                        //遍历菜单
                        Loading(c, resources);
                    }
                }
            }

            if (control is ComboBox)
            {
                ComboBox cbo = control as ComboBox;
                int selectedIndex = cbo.SelectedIndex;
                List<object> lst = new List<object>();
                for (int i = 0; i < cbo.Items.Count; i++)
                {
                    string str = string.Empty;
                    if (i == 0)
                        str = resources.GetString(cbo.Name + ".Items");
                    else
                        str = resources.GetString(cbo.Name + ".Items" + i);
                    if (!string.IsNullOrEmpty(str))
                    {
                        lst.Add(str);
                    }
                }
                if (lst.Count > 0)
                {
                    cbo.Items.Clear();
                    cbo.Items.AddRange(lst.ToArray());
                    cbo.SelectedIndex = selectedIndex;
                }
            }

            foreach (Control c in control.Controls)
            {
                if (c is TextBox)
                {
                    string text = c.Text;
                    resources.ApplyResources(c, c.Name);
                    c.Text = text;
                }
                else if (!(c is TableLayoutPanel))
                {
                    resources.ApplyResources(c, c.Name);
                }
                Loading(c, resources);
            }
        }

        /// <summary>
        /// 遍历菜单
        /// </summary>
        /// <param name="item">菜单项</param>
        /// <param name="resources">语言资源</param>
        private static void Loading(ToolStripMenuItem item, ComponentResourceManager resources)
        {
            resources.ApplyResources(item, item.Name);
            if (item.DropDownItems.Count > 0)
            {
                foreach (ToolStripMenuItem c in item.DropDownItems)
                {
                    Loading(c, resources);
                }
            }
        }
    }

    /// <summary>
    /// 消息文本，默认语言
    /// </summary>
    public class MsgDef
    {
        /// <summary>
        /// 相机名称
        /// </summary>
        public string PosCamera_Name = "相机";
        /// <summary>
        /// 相机连接成功
        /// </summary>
        public string PosCamera_ConnectSuccess = "相机{0}连接成功！";
        /// <summary>
        /// 相机连接失败
        /// </summary>
        public string PosCamera_ConnectFailed = "相机{0}连接失败！";
        /// <summary>
        /// 相机初始化异常
        /// </summary>
        public string PosCamera_InitError = "相机{0}初始化异常!";

        /// <summary>
        /// 平台名称
        /// </summary>
        public string PosPlatform_Name = "平台";
        /// <summary>
        /// 平台初始化失败
        /// </summary>
        public string PosPlatform_InitError = "对位平台初始化失败！";

        /// <summary>
        /// 收到自动对位请求
        /// </summary>
        public string PosSignal_AutoPosRequestReceived = "收到自动对位请求，组{0}，工位{1}";
        /// <summary>
        /// 创建自动对位流程异常
        /// </summary>
        public string PosSignal_ErrorToStartAutoPos = "创建自动对位流程异常：";
        /// <summary>
        /// 收到自动标定请求
        /// </summary>
        public string PosSignal_CalibrationRequest = "收到自动标定请求，组{0}，工位{1}";
        /// <summary>
        /// 创建自动标定流程异常
        /// </summary>
        public string PosSignal_ErrorToStartCalibration = "创建自动标定流程异常：";
        /// <summary>
        /// 收到运动请求
        /// </summary>
        public string PosSignal_MoveRequestReceived = "收到运动请求，组{0}，类型{1}";
        /// <summary>
        /// 运动完成
        /// </summary>
        public string PosSignal_MoveCompleted = "运动完成，组{0}，类型{1}，结果{2}";
        /// <summary>
        /// 运动异常
        /// </summary>
        public string PosSignal_MoveError = "运动异常，组{0}，类型{1}，异常：";

        /// <summary>
        /// 用户名不存在
        /// </summary>
        public string UserManager_UserNameNotExist = "用户名不存在！";
        /// <summary>
        /// 用户名已存在
        /// </summary>
        public string UserManager_UserNameAlreadyExist = "用户名已存在！";
        /// <summary>
        /// 密码错误
        /// </summary>
        public string UserManager_PasswordError = "密码错误！";
        /// <summary>
        /// 用户名和密码不能为空
        /// </summary>
        public string UserManager_NameOrPwdEmpty = "用户名和密码不能为空！";
        /// <summary>
        /// 两次输入的密码不一致
        /// </summary>
        public string UserManager_PasswordsDifferent = "两次输入的密码不一致";
        /// <summary>
        /// 管理员账户无法删除
        /// </summary>
        public string UserManager_AdminCanNotDelete = "管理员账户无法删除！";
        /// <summary>
        /// 用户正在使用中
        /// </summary>
        public string UserManager_UserInUse = "用户正在使用中！";
        /// <summary>
        /// 管理员密码无法修改
        /// </summary>
        public string UserManager_AdminPwdCanNotModify = "管理员密码无法修改！";
        /// <summary>
        /// 原密码错误
        /// </summary>
        public string UserManager_OldPwdError = "原密码错误！";
        /// <summary>
        /// 管理员等级无法修改
        /// </summary>
        public string UserManager_AdminLevelCanNotModify = "管理员等级无法修改！";
        /// <summary>
        /// 管理员权限无法修改
        /// </summary>
        public string UserManager_AdminPermissionCanNotModify = "管理员权限无法修改！";

        /// <summary>
        /// 手动标定提示
        /// </summary>
        public string ViewManual_ManualCalibrationTip = "手动标定将覆盖目标像素坐标值，是否继续？";
        /// <summary>
        /// 打开文件的Filter
        /// </summary>
        public string ViewManual_OpenFileFilter = "图片文件(*.bmp)|*.bmp";
        /// <summary>
        /// 安全条件OFF
        /// </summary>
        public string ViewManual_SafetyConditionOff = "安全条件OFF，流程结束！";

        /// <summary>
        /// 机种已存在
        /// </summary>
        public string ViewRecipeManage_RecipeAlreadyExist = "机种已存在！";
        /// <summary>
        /// 新建机种失败
        /// </summary>
        public string ViewRecipeManage_FailedToCreateNewRecipe = "新建机种失败！";
        /// <summary>
        /// 请选择需要另存的机种
        /// </summary>
        public string ViewRecipeManage_SelectRecipeToCopy = "请选择需要另存的机种！";
        /// <summary>
        /// 另存机种失败
        /// </summary>
        public string ViewRecipeManage_FailedToSaveRecipeAs = "另存机种失败！";
        /// <summary>
        /// 请选择需要删除的机种
        /// </summary>
        public string ViewRecipeManage_SelectRecipeToDelete = "请选择需要删除的机种！";
        /// <summary>
        /// 删除机种确认
        /// </summary>
        public string ViewRecipeManage_RecipeDeleteConfirm = "确定要删除当前选定的机种吗？";
        /// <summary>
        /// 机种删除失败
        /// </summary>
        public string ViewRecipeManage_FailedToDeleteRecipe = "机种删除失败！";

        /// <summary>
        /// 保存失败
        /// </summary>
        public string ViewSetting_SaveError = "*保存失败，请检查参数：";
        /// <summary>
        /// 对位组名称错误
        /// </summary>
        public string ViewSetting_GroupNameError = "对位组名称标识必须不一致！";

        /// <summary>
        /// RecipeID长度错误
        /// </summary>
        public string FormRecipeNew_RecipeIDLengthError = "*RecipeID长度不可大于50";
        /// <summary>
        /// 已存在RecipeID
        /// </summary>
        public string FormRecipeNew_RecipeIDExists = "*已存在RecipeID";

        /// <summary>
        /// 提示
        /// </summary>
        public string StrTip = "提示";

        /// <summary>
        /// 设置成英文
        /// </summary>
        public void SetToEnglish()
        {
            PosCamera_Name = "Camera ";
            PosCamera_ConnectSuccess = "Camera {0} connect success!";
            PosCamera_ConnectFailed = "Camera {0} connect failed!";
            PosCamera_InitError = "Camera {0} init error!";

            PosPlatform_Name = "Platform ";
            PosPlatform_InitError = "Platform init error!";

            PosSignal_AutoPosRequestReceived = "AutoPos request received, group {0}, station {1}";
            PosSignal_ErrorToStartAutoPos = "Error to start AutoPos flow: ";
            PosSignal_CalibrationRequest = "Calibration request received, group {0}, station {1}";
            PosSignal_ErrorToStartCalibration = "Error to start Calibration flow: ";
            PosSignal_MoveRequestReceived = "Move request received, group {0}, type {1}";
            PosSignal_MoveCompleted = "Move completed, group {0}, type {1}, result {2}";
            PosSignal_MoveError = "Move error, group {0}, type {1}, exception:";

            UserManager_UserNameNotExist = "User name is not exist!";
            UserManager_UserNameAlreadyExist = "User name is already exist!";
            UserManager_PasswordError = "Password is wrong!";
            UserManager_NameOrPwdEmpty = "User name and password can not be empty!";
            UserManager_PasswordsDifferent = "The two passwords is different!";
            UserManager_AdminCanNotDelete = "Admin can not be deleted!";
            UserManager_UserInUse = "User is in use!";
            UserManager_AdminPwdCanNotModify = "Password of Admin can not be modified!";
            UserManager_OldPwdError = "Old password is wrong!";
            UserManager_AdminLevelCanNotModify = "Level of Admin can not be modified!";
            UserManager_AdminPermissionCanNotModify = "Permission of Admin can not be modified!";

            ViewManual_ManualCalibrationTip = "Target pixel position in parameter will be overrided, continue?";
            ViewManual_OpenFileFilter = "Image File(*.bmp)|*.bmp";
            ViewManual_SafetyConditionOff = "Safety condition off, the flow is canceled!";

            ViewRecipeManage_RecipeAlreadyExist = "The recipe is already exist!";
            ViewRecipeManage_FailedToCreateNewRecipe = "Failed to create a new recipe!";
            ViewRecipeManage_SelectRecipeToCopy = "Please select a recipe to copy from!";
            ViewRecipeManage_FailedToSaveRecipeAs = "Failed to save the recipe as a new recipe!";
            ViewRecipeManage_SelectRecipeToDelete = "Please select a recipe to delete!";
            ViewRecipeManage_RecipeDeleteConfirm = "Are you sure to delete the selected recipe?";
            ViewRecipeManage_FailedToDeleteRecipe = "Failed to delete the recipe!";

            ViewSetting_SaveError = "*Save failed, please check your inputs.";
            ViewSetting_GroupNameError = "The name must be different between two groups!";

            FormRecipeNew_RecipeIDLengthError = "*The length of RecipeID should be less than 50";
            FormRecipeNew_RecipeIDExists = "*The RecipeID is already exist";

            StrTip = "Tips";
        }
    }
}
