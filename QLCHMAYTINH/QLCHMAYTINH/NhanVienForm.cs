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
using System.Data.Linq;

namespace QLCHMAYTINH
{
    public partial class NhanVienForm : DevExpress.XtraEditors.XtraForm
    {
        Table<NHANVIEN> Bang_NHANVIEN;
        BindingManagerBase DSNV;
        DataClassesDataContext db;
        public NhanVienForm()
        {
            InitializeComponent();
        }

        private void textEdit3_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void textEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void NhanVienForm_Load(object sender, EventArgs e)
        {
            db = new DataClassesDataContext();
            Bang_NHANVIEN = db.NHANVIENs;

            loadDGVNhanVien();
            txt_manv.DataBindings.Add("text", Bang_NHANVIEN, "MANV", true);
            txt_tennv.DataBindings.Add("text", Bang_NHANVIEN, "TENNV", true);
            date_ngaysinh.DataBindings.Add("text", Bang_NHANVIEN, "NGAYSINH", true);
            txt_cmnd.DataBindings.Add("text", Bang_NHANVIEN, "CMND", true);
            txt_sdt.DataBindings.Add("text", Bang_NHANVIEN, "SDT", true);
            txt_email.DataBindings.Add("text", Bang_NHANVIEN, "EMAIL", true);
            txt_diachi.DataBindings.Add("text", Bang_NHANVIEN, "DCHI", true);

            DSNV = this.BindingContext[Bang_NHANVIEN];
            enableNutLenh(false);
        }

        private void enableNutLenh(bool p)
        {
            btn_them.Enabled = !p;
            btn_xoa.Enabled = !p;
            btn_sua.Enabled = !p;
            btn_luu.Enabled = p;
            btn_thoat.Enabled = !p;
        }

        private void loadDGVNhanVien()
        {
            gv_nhanvien.DataSource = Bang_NHANVIEN;
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            DSNV.AddNew();
            enableNutLenh(true);
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            try
            {
                DSNV.EndCurrentEdit();
                db.SubmitChanges();
                enableNutLenh(false);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            DSNV.RemoveAt(DSNV.Position);
            db.SubmitChanges();
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            enableNutLenh(true);
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}