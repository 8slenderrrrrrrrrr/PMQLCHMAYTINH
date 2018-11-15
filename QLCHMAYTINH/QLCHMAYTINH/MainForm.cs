using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace QLCHMAYTINH
{
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public void ViewChildForm(Form _form)
        {
            if (!IsFormAcived(_form))
            {
                _form.MdiParent = this;
                _form.Show();
            }
        }

        private bool IsFormAcived(Form form)
        {
            bool IsOpenend = false;
            if (MdiChildren.Count() > 0)
            {
                foreach (var item in MdiChildren)
                {
                    if (form.Name == item.Name)
                    {
                        xtraTabbedMdiManager1.Pages[item].MdiChild.Activate();
                        IsOpenend = true;
                    }
                }
            }
            return IsOpenend;
        }

        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            
            NhanVienForm nhanvien = new NhanVienForm();
            nhanvien.Name = "NhanVienForm";
            ViewChildForm(nhanvien);
        }

        private void barButtonItem18_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Hide();
            var flogin = new DangNhap();
            flogin.Closed += (s, args) => this.Close();
            flogin.Show();
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            UserForm user = new UserForm();
            user.Name = "UserForm";
            ViewChildForm(user);

        }
    }
}