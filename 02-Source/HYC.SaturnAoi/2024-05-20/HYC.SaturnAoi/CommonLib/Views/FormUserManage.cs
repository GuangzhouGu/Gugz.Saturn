using CommonLib.Entities;
using HYC.WindowsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonLib.Views
{
    public partial class FormUserManage : FormPopBase
    {
        public FormUserManage()
        {
            InitializeComponent();
            Title = "权限管理";
        }
        private void btnAddUser_Click(object sender, EventArgs e)
        {
            try
            {
                UserLevel level = this.rbtnEngineer.Checked ? UserLevel.Engineer : UserLevel.User;
                UserManager.Register(this.txtNewUser.Text, this.txtPwd.Text, this.txtPwdConfirm.Text, level);
                AddUsersToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
      

        private void btnModifyPwd_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listViewUser.SelectedItems.Count == 0)
                {
                    MessageBox.Show("未选中任何用户！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ListViewItem lvi = this.listViewUser.SelectedItems[0];
                string name = lvi.Text;
                UserManager.ModifyPassword(name, this.txtOldPwd.Text, this.txtNewPwd.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnModifyPermission_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listViewUser.SelectedItems.Count == 0)
                {
                    MessageBox.Show("未选中任何用户！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ListViewItem lvi = this.listViewUser.SelectedItems[0];
                string name = lvi.Text;

                int permission = 0;
                for (int i = 0; i < pnlPermission.Controls.Count; i++)
                {
                    if ((pnlPermission.Controls[i] as CheckBox).Checked)
                    {
                        int index = Convert.ToInt32(pnlPermission.Controls[i].Name.Substring(3));
                        permission = permission | (1 << index);
                    }
                }
                UserManager.ModifyPermission(name, permission);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listViewUser.SelectedItems.Count == 0)
                {
                    MessageBox.Show("未选中任何用户！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult dr = MessageBox.Show("确定要删除选中的用户吗？", "提示",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr != DialogResult.OK) return;

                ListViewItem lvi = this.listViewUser.SelectedItems[0];
                string name = lvi.Text;
                UserManager.DeleteUser(name);
                AddUsersToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void listViewUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listViewUser.SelectedItems.Count == 0) return;

            try
            {
                ListViewItem lvi = this.listViewUser.SelectedItems[0];
                User user = UserManager.GetUsers().Where(p => p.Name == lvi.Text).First();
                for (int i = 0; i < pnlPermission.Controls.Count; i++)
                {
                    int index = Convert.ToInt32(pnlPermission.Controls[i].Name.Substring(3));
                    (pnlPermission.Controls[i] as CheckBox).Checked = (user.Permission & (1 << index)) > 0 ? true : false;
                }
            }
            catch (Exception)
            { }
        }
        private void AddUsersToList()
        {
            this.listViewUser.Items.Clear();
            var lstUsers = UserManager.GetUsers();
            for (int i = 0; i < lstUsers.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = lstUsers[i].Name;
                lvi.SubItems.Add(lstUsers[i].Level.ToString());
                this.listViewUser.Items.Add(lvi);
            }
        }
        public static string GetDescription(Enum value, bool isTop = false)
        {
            Type enumType = value.GetType();
            DescriptionAttribute attr = null;
            if (isTop)
            {
                attr = Attribute.GetCustomAttribute(enumType, typeof(DescriptionAttribute)) as DescriptionAttribute;
            }
            else
            {
                // 获取枚举常数名称。
                string name = Enum.GetName(enumType, value);
                if (name != null)
                {
                    // 获取枚举字段。
                    FieldInfo fieldInfo = enumType.GetField(name);
                    if (fieldInfo != null)
                    {
                        // 获取描述的属性。
                        attr = Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute), false) as DescriptionAttribute;
                    }
                }
            }

            if (attr != null && !string.IsNullOrEmpty(attr.Description))
                return attr.Description;
            else
                return string.Empty;

        }
        public void UpdateUserMannage()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateUserMannage));
            }
            else
            {
                UserManager.LoadUsers();
                AddUsersToList();
                txtNewUser.Text = "";
                txtPwd.Text = "";
                txtPwdConfirm.Text = "";
                txtOldPwd.Text = "";
                txtNewPwd.Text = "";
                if (UserManager.IsLogin && UserManager.CurUser.Level == UserLevel.Developer)
                {
                    this.rbtnEngineer.Visible = true;
                    this.rbtnUser.Visible = true;
                }

            }

        }

        private void FormUserManage_Load(object sender, EventArgs e)
        {
            UserManager.LoadUsers();
            AddUsersToList();
            if (UserManager.IsLogin && UserManager.CurUser.Level == UserLevel.Developer)
            {
                this.rbtnEngineer.Visible = true;
                this.rbtnUser.Visible = true;
            }

            string[] strPermission = Enum.GetNames(typeof(UserPermission));
            for (int i = 0; i < strPermission.Length; i++)
            {
                UserPermission p = (UserPermission)Enum.Parse(typeof(UserPermission), strPermission[i]);
                CheckBox chk = new CheckBox()
                {
                    Width = 150,
                    Margin = new Padding(0, 2, 0, 0),
                    Name = "chk" + i,
                    Text = GetDescription(p)
                };
                this.pnlPermission.Controls.Add(chk);
            }
        }
    }
}
