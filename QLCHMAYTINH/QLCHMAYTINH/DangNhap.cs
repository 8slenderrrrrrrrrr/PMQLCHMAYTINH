using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLCHMAYTINH
{
    public partial class DangNhap : DevExpress.XtraEditors.XtraForm
    {
        public DangNhap()
        {
            InitializeComponent();
        }

        private void btn_dangnhap_Click(object sender, EventArgs e)
        {
            using (var db = new QLCHViTinhEntities())
            {
                IEnumerable<USER> User = from user in db.USERs
                                         where user.TAIKHOAN == txt_taikhoan.Text && user.MATKHAU == txt_matkhau.Text
                                         select user;
                if (User.Count() == 0)
                {
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    this.Hide();
                    var formmain = new MainForm();
                    formmain.Closed += (s, args) => this.Close();
                    formmain.Show();
                }
            }
        }
    }
}