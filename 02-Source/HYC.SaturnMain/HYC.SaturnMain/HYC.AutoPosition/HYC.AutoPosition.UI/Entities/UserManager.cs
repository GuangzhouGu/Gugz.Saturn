/*----------------------------------------------------------------
// Copyright (C) Suzhou HYC Electronic Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.0.30319.42000
// 命名空间    ：HYC.AutoPosition.UI.Entities
// 文件名称    ：UserManager.cs
// =================================
// 创 建 者    ：yangkang
// 创建日期    ：2020/4/9 9:49:58 
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
using HYC.AutoPosition.UI.Helper;
using HYC.HTFile.HTXML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HYC.AutoPosition.UI.Entities
{
    /// <summary>
    /// 用户管理类
    /// </summary>
    public class UserManager
    {
        /// <summary>
        /// 用户列表
        /// </summary>
        private List<User> lstUsers = new List<User>();
        private string PwdSolt = "*##*";
        private System.Timers.Timer timer = null;
        private int curLoginSeconds = 0;

        /// <summary>
        /// 当前的用户
        /// </summary>
        public User CurUser { get; set; }
        /// <summary>
        /// 用户是否已登录
        /// </summary>
        public bool IsLogin { get; set; }
        /// <summary>
        /// 登录超时时间
        /// </summary>
        public int TimeOut { get; set; } = 5 * 60;

        /// <summary>
        /// 用户更改事件
        /// </summary>
        public event EventHandler<UserEventArgs> UserChanged;

        /// <summary>
        /// 加载所有用户
        /// </summary>
        public void LoadUsers()
        {
            lstUsers.Clear();

            LoadUsersFromFile();

            // 管理员用户，具有最高权限
            User userAdmin = new User();
            userAdmin.Name = "Admin";
            userAdmin.Password = "5661125189EC7A005FF616422792F2EC";  //hyc8816
            userAdmin.Level = UserLevel.Developer;
            userAdmin.Permission = 0x7FFFFFFF;
            lstUsers.Add(userAdmin);
            if (lstUsers.Where(p => p.Name.ToLower() == "hyc").Count() == 0)
            {
                // HYC工程师账户，具有较高的权限
                User userDefault = new User();
                userDefault.Name = "HYC";
                userDefault.Password = "9D77E6BBD1CB25B46F445FFBA7C1CCD8";  //8816
                userDefault.Level = UserLevel.Engineer;
                lstUsers.Add(userDefault);
            }
            lstUsers.Sort((x, y) => x.Name.CompareTo(y.Name));

            // 启动登录计时定时器
            if (timer == null)
            {
                timer = new System.Timers.Timer(1000);
                timer.Elapsed += Timer_Elapsed;
                timer.Start();
            }
        }

        /// <summary>
        /// 定时时间到，退出登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (IsLogin && CurUser.Level > UserLevel.User)
            {
                curLoginSeconds++;
                if (curLoginSeconds >= TimeOut)
                {
                    string name = CurUser?.Name;
                    CurUser = null;
                    IsLogin = false;
                    //触发事件
                    if (!string.IsNullOrEmpty(name))
                    {
                        UserEventArgs args = new UserEventArgs()
                        {
                            Name = name,
                            Action = UserEventAction.TimeOut
                        };
                        UserChanged?.Invoke(this, args);
                    }
                }
            }
            else
            {
                curLoginSeconds = 0;
            }
        }

        /// <summary>
        /// 重置定时器
        /// </summary>
        public void ResetTimer()
        {
            curLoginSeconds = 0;
        }

        /// <summary>
        /// 获取除了管理员账户以外的所有账户列表
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsers()
        {
            return lstUsers.Where(p => p.Name != "Admin").ToList();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns>true:登录成功 false:登录失败</returns>
        public bool Login(string name, string pwd)
        {
            var lst = lstUsers.Where(p => p.Name.ToLower() == name.ToLower()).ToList();
            if (lst.Count <= 0)
            {
                throw new Exception(LanguageHelper.Msg.UserManager_UserNameNotExist);
            }

            User user = lst[0];
            string tmp = GetMD5(PwdSolt + pwd);
            if (tmp != user.Password)
            {
                throw new Exception(LanguageHelper.Msg.UserManager_PasswordError);
            }

            CurUser = user;
            IsLogin = true;
            //触发事件
            UserEventArgs args = new UserEventArgs()
            {
                Name = name,
                Action = UserEventAction.Login
            };
            UserChanged?.Invoke(this, args);

            return true;
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        public void Logout()
        {
            string name = CurUser?.Name;
            CurUser = null;
            IsLogin = false;
            //触发事件
            if (!string.IsNullOrEmpty(name))
            {
                UserEventArgs args = new UserEventArgs()
                {
                    Name = name,
                    Action = UserEventAction.Logout
                };
                UserChanged?.Invoke(this, args);
            }
        }

        /// <summary>
        /// 用户验证
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns>true:用户存在且密码正确</returns>
        public bool UserValidate(string name, string pwd)
        {
            var lst = lstUsers.Where(p => p.Name.ToLower() == name.ToLower()).ToList();
            if (lst.Count <= 0)
            {
                throw new Exception(LanguageHelper.Msg.UserManager_UserNameNotExist);
            }

            User user = lst[0];
            string tmp = GetMD5(PwdSolt + pwd);
            if (tmp != user.Password)
            {
                throw new Exception(LanguageHelper.Msg.UserManager_PasswordError);
            }

            return true;
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="pwdConfirm">密码确认</param>
        /// <param name="level">用户等级</param>
        /// <param name="permission">用户权限</param>
        /// <returns>true:注册成功 false:注册失败</returns>
        public bool Register(string name, string pwd, string pwdConfirm, UserLevel level, int permission = 0)
        {
            var lst = lstUsers.Where(p => p.Name.ToLower() == name.ToLower()).ToList();
            if (lst.Count > 0)
            {
                throw new Exception(LanguageHelper.Msg.UserManager_UserNameAlreadyExist);
            }
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(pwd))
            {
                throw new Exception(LanguageHelper.Msg.UserManager_NameOrPwdEmpty);
            }

            if (pwd != pwdConfirm)
            {
                throw new Exception(LanguageHelper.Msg.UserManager_PasswordsDifferent);
            }
            else
            {
                User user = new User();
                user.Name = name;
                user.Password = GetMD5(PwdSolt + pwd);
                user.Level = level;
                lstUsers.Add(user);
                SaveUsersToFile();
            }

            return true;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="name">用户名</param>
        /// <returns>true:删除成功 false:删除失败</returns>
        public bool DeleteUser(string name)
        {
            var lst = lstUsers.Where(p => p.Name.ToLower() == name.ToLower()).ToList();
            if (lst.Count <= 0)
            {
                throw new Exception(LanguageHelper.Msg.UserManager_UserNameNotExist);
            }
            if (name == "Admin")
            {
                throw new Exception(LanguageHelper.Msg.UserManager_AdminCanNotDelete);
            }
            if (IsLogin && name.ToLower() == CurUser.Name.ToLower())
            {
                throw new Exception(LanguageHelper.Msg.UserManager_UserInUse);
            }

            lstUsers.RemoveAll(p => p.Name.ToLower() == name.ToLower());
            SaveUsersToFile();

            return true;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="oldpwd">旧密码</param>
        /// <param name="newpwd">新密码</param>
        /// <returns>true:修改成功 false:修改失败</returns>
        public bool ModifyPassword(string name, string oldpwd, string newpwd)
        {
            var lst = lstUsers.Where(p => p.Name.ToLower() == name.ToLower()).ToList();
            if (lst.Count <= 0)
            {
                throw new Exception(LanguageHelper.Msg.UserManager_UserNameNotExist);
            }
            if (name == "Admin")
            {
                throw new Exception(LanguageHelper.Msg.UserManager_AdminPwdCanNotModify);
            }

            User user = lst[0];
            string tmp = GetMD5(PwdSolt + oldpwd);
            if (tmp != user.Password)
            {
                throw new Exception(LanguageHelper.Msg.UserManager_OldPwdError);
            }

            user.Password = GetMD5(PwdSolt + newpwd);
            SaveUsersToFile();

            return true;
        }

        /// <summary>
        /// 修改用户等级
        /// </summary>
        /// <param name="name"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public bool ModifyUserLevel(string name, UserLevel level)
        {
            var lst = lstUsers.Where(p => p.Name.ToLower() == name.ToLower()).ToList();
            if (lst.Count <= 0)
            {
                throw new Exception(LanguageHelper.Msg.UserManager_UserNameNotExist);
            }
            if (name == "Admin")
            {
                throw new Exception(LanguageHelper.Msg.UserManager_AdminLevelCanNotModify);
            }

            User user = lst[0];
            user.Level = level;
            SaveUsersToFile();

            return true;
        }

        /// <summary>
        /// 修改用户权限
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="permission">权限</param>
        /// <returns>true:修改成功 false:修改失败</returns>
        public bool ModifyPermission(string name, int permission)
        {
            var lst = lstUsers.Where(p => p.Name.ToLower() == name.ToLower()).ToList();
            if (lst.Count <= 0)
            {
                throw new Exception(LanguageHelper.Msg.UserManager_UserNameNotExist);
            }
            if (name == "Admin")
            {
                throw new Exception(LanguageHelper.Msg.UserManager_AdminPermissionCanNotModify);
            }

            User user = lst[0];
            user.Permission = permission;
            SaveUsersToFile();

            return true;
        }

        /// <summary>
        /// 判断当前用户是否具有某个权限
        /// </summary>
        /// <param name="flag"></param>
        /// <returns>true:有权限 false:无权限</returns>
        public bool HasPermission(int flag)
        {
            if (IsLogin)
            {
                return (CurUser.Permission & flag) > 0;
            }
            return false;
        }

        /// <summary>
        /// 获取字符串的MD5值
        /// </summary>
        /// <param name="str">输入字符串</param>
        /// <returns>输出字符串</returns>
        private string GetMD5(string str)
        {
            //创建MD5对象
            MD5 md5 = MD5.Create();
            //需要将字符转换成字节数组
            byte[] buffer = Encoding.Default.GetBytes(str);
            //返回一个加密号的字节数组
            byte[] MD5Buffer = md5.ComputeHash(buffer);
            //将字节数组转换成字符串
            string md5str = "";
            for (int i = 0; i < MD5Buffer.Length; i++)
            {
                md5str += MD5Buffer[i].ToString("X2");
            }
            return md5str;
        }

        /// <summary>
        /// 从文件中加载用户
        /// </summary>
        private void LoadUsersFromFile()
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                path += @"\HYC\" + AppDomain.CurrentDomain.FriendlyName;
                string strFileName = path + @"\User.dat";
                if (File.Exists(strFileName))
                {
                    lstUsers = HTXMLSerializer.EntityFromXMLFile<List<User>>(strFileName);
                }
            }
            catch (Exception)
            { }
        }

        /// <summary>
        /// 将用户列表保存至文件
        /// </summary>
        private void SaveUsersToFile()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            path += @"\HYC\" + AppDomain.CurrentDomain.FriendlyName;
            string strFileName = path + @"\User.dat";
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            var lst = lstUsers.Where(p => p.Name != "Admin").ToList();
            HTXMLSerializer.EntityToXMLFile(lst, strFileName);
        }
    }

    /// <summary>
    /// 用户事件参数类
    /// </summary>
    public class UserEventArgs : EventArgs
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 动作
        /// </summary>
        public UserEventAction Action { get; set; }
    }

    /// <summary>
    /// 用户事件类型
    /// </summary>
    public enum UserEventAction
    {
        /// <summary>
        /// 登录
        /// </summary>
        Login,
        /// <summary>
        /// 注销
        /// </summary>
        Logout,
        /// <summary>
        /// 超时自动退出
        /// </summary>
        TimeOut
    }

    /// <summary>
    /// 用户类
    /// </summary>
    public class User
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = string.Empty;
        /// <summary>
        /// 用户等级
        /// </summary>
        public UserLevel Level { get; set; } = UserLevel.User;
        /// <summary>
        /// 用户权限
        /// </summary>
        public int Permission { get; set; }

        /// <summary>
        /// 转换为文本
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Name:{0}, Level:{1}, Permission:{2}", Name, Level, Permission.ToString("X8"));
        }
    }

    /// <summary>
    /// 用户等级
    /// </summary>
    public enum UserLevel
    {
        /// <summary>
        /// 普通用户
        /// </summary>
        User,
        /// <summary>
        /// 工程师用户
        /// </summary>
        Engineer,
        /// <summary>
        /// 开发者用户
        /// </summary>
        Developer
    }

    /// <summary>
    /// 用户权限
    /// </summary>
    [Flags]
    public enum UserPermission
    {
        /// <summary>
        /// 未定义
        /// </summary>
        [Description("-")]
        Unknown = 0x00000001,
    }
}
