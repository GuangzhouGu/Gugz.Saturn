/*----------------------------------------------------------------
// Copyright (C)  Suzhou HYC Technology Co.,LTD
// 版权所有。
//
// =================================
// CLR版本     ：4.6.81.0
// 命名空间    ：PPITester.Entities
// 文件名称    ：UserManager.cs
// =================================
// 创 建 者    ：guguangzhou
// 创建日期    ：2021/01/29 09:00:00 
// 功能描述    ：用户权限管理类
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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Entities
{
    public class UserManager
    {
        /// <summary>
        /// 用户列表
        /// </summary>
        private static List<User> lstUsers = new List<User>();
        /// <summary>
        /// 当前用户
        /// </summary>
        public static User CurUser = null;
        /// <summary>
        /// 是否登录
        /// </summary>
        public static bool IsLogin = false;
        private static string PwdSolt = "*##*";
        /// <summary>
        /// 加载当前用户
        /// </summary>
        public static void LoadUsers()
        {
            lstUsers.Clear();

            LoadUsersFromFile();

            User userAdmin = new User();
            userAdmin.Name = "Admin";
            userAdmin.Password = "5661125189EC7A005FF616422792F2EC"; //hyc8816
            userAdmin.Level = UserLevel.Developer;
            userAdmin.Permission = 0xFFFF;
            lstUsers.Add(userAdmin);
            if (lstUsers.Where(p => p.Name.ToLower() == "hyc").Count() == 0)
            {
                User userDefault = new User();
                userDefault.Name = "HYC";
                userDefault.Password = "9D77E6BBD1CB25B46F445FFBA7C1CCD8"; //8816
                userDefault.Level = UserLevel.Engineer;
                lstUsers.Add(userDefault);
            }
            lstUsers.Sort((x, y) => x.Name.CompareTo(y.Name));
        }

        public static List<User> GetUsers()
        {
            return lstUsers.Where(p => p.Name != "Admin").ToList();
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static bool Login(string name, string pwd)
        {
            var lst = lstUsers.Where(p => p.Name.ToLower() == name.ToLower()).ToList();
            if (lst.Count <= 0)
            {
                throw new Exception("用户名不存在！");
            }

            User user = lst[0];
            string tmp = GetMD5(PwdSolt + pwd);
            if (tmp != user.Password)
            {
                throw new Exception("密码错误！");
            }

            CurUser = user;
            IsLogin = true;

            return true;
        }

        public static void Logout()
        {
            CurUser = null;
            IsLogin = false;
        }

        public static bool UserValidate(string name, string pwd)
        {
            var lst = lstUsers.Where(p => p.Name.ToLower() == name.ToLower()).ToList();
            if (lst.Count <= 0)
            {
                throw new Exception("用户名不存在！");
            }

            User user = lst[0];
            string tmp = GetMD5(PwdSolt + pwd);
            if (tmp != user.Password)
            {
                throw new Exception("密码错误！");
            }

            return true;
        }

        public static bool Register(string name, string pwd, string pwdConfirm, UserLevel level, int permission = 0)
        {
            if (IsLogin == false || CurUser.Level < UserLevel.Engineer)
            {
                throw new Exception("您没有权限进行该操作！");
            }

            var lst = lstUsers.Where(p => p.Name.ToLower() == name.ToLower()).ToList();
            if (lst.Count > 0)
            {
                throw new Exception("用户名已存在！");
            }
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(pwd))
            {
                throw new Exception("用户名和密码不能为空！");
            }

            if (pwd != pwdConfirm)
            {
                throw new Exception("两次输入的密码不一致");
            }
            else
            {
                User user = new User();
                user.Name = name;
                user.Password = GetMD5(PwdSolt + pwd);
                user.Level = level;
                user.Permission = permission;
                lstUsers.Add(user);

                SaveUsersToFile();
            }

            return true;
        }

        public static bool DeleteUser(string name)
        {
            if (IsLogin == false || CurUser.Level < UserLevel.Engineer)
            {
                throw new Exception("您没有权限进行该操作！");
            }

            var lst = lstUsers.Where(p => p.Name.ToLower() == name.ToLower()).ToList();
            if (lst.Count <= 0)
            {
                throw new Exception("用户名不存在！");
            }
            if (name == "Admin")
            {
                throw new Exception("管理员账户无法删除！");
            }
            if (name.ToLower() == "hyc")
            {
                throw new Exception("默认账户无法删除！");
            }

            lstUsers.RemoveAll(p => p.Name.ToLower() == name.ToLower());

            SaveUsersToFile();

            return true;
        }

        public static bool ModifyPassword(string name, string oldpwd, string newpwd)
        {
            if (IsLogin == false || CurUser.Level < UserLevel.Engineer)
            {
                throw new Exception("您没有权限进行该操作！");
            }

            var lst = lstUsers.Where(p => p.Name.ToLower() == name.ToLower()).ToList();
            if (lst.Count <= 0)
            {
                throw new Exception("用户名不存在！");
            }
            if (name == "Admin")
            {
                throw new Exception("管理员密码无法修改！");
            }

            User user = lst[0];
            string tmp = GetMD5(PwdSolt + oldpwd);
            if (tmp != user.Password)
            {
                throw new Exception("原密码错误！");
            }

            user.Password = GetMD5(PwdSolt + newpwd);
            SaveUsersToFile();

            return true;
        }

        public static bool ModifyPermission(string name, int permission)
        {
            if (IsLogin == false || CurUser.Level < UserLevel.Engineer)
            {
                throw new Exception("您没有权限进行该操作！");
            }

            var lst = lstUsers.Where(p => p.Name.ToLower() == name.ToLower()).ToList();
            if (lst.Count <= 0)
            {
                throw new Exception("用户名不存在！");
            }
            if (name == "Admin")
            {
                throw new Exception("管理员权限无法修改！");
            }

            User user = lst[0];
            user.Permission = permission;
            SaveUsersToFile();

            return true;
        }

        private static void LoadUsersFromFile()
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

        private static void SaveUsersToFile()
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
        /// <summary>
        /// 获取字符串的MD5值
        /// </summary>
        /// <param name="str">输入字符串</param>
        /// <returns>输出字符串</returns>
        private static string GetMD5(string str)
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

    }

    public class User
    {
        public User()
        {
            Name = string.Empty;
            Password = string.Empty;
            Level = UserLevel.User;
        }

        public string Name { get; set; }
        public string Password { get; set; }
        public UserLevel Level { get; set; }
        public int Permission { get; set; }

        public override string ToString()
        {
            return string.Format("Name:{0}, Level:{1}", Name, Level);
        }
    }

    public enum UserLevel
    {
        User,
        Engineer,
        Developer
    }

    public enum UserPermission
    {
        [Description("创建用户")]
        CreateUser,
        [Description("删除用户")]
        DeleteUser,
        [Description("修改密码")]
        ModifyPassword,
        [Description("修改权限")]
        ModifyPermission,

        [Description("修改机种参数")]
        ModifyRecipe,
        [Description("修改伺服参数")]
        ModifyServoPara,
    }
}
