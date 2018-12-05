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
using System.Data.Entity.Validation;


namespace QLCHMAYTINH
{
    public partial class NhanVienForm : DevExpress.XtraEditors.XtraForm
    {
        string manv;
        public NhanVienForm()
        {
            InitializeComponent();
            
        }

        private void NhanVienForm_Load(object sender, EventArgs e)
        {
            QLCHViTinhEntities db = new QLCHViTinhEntities();
            gv_nhanvien.DataSource = GetAllNhanVien();
            date_ngaysinh.EditValue = DateTime.Now;
        }

        public List<NhanVienModel> GetAllNhanVien()
        {
            QLCHViTinhEntities db = new QLCHViTinhEntities();
            return db.NHANVIENs.OrderBy(a => a.MANV).Select(s => new NhanVienModel
            {
                MANV = s.MANV,
                TENNV = s.TENNV,
                SDT = s.SDT,
                EMAIL = s.EMAIL,
                DCHI = s.DCHI,
                NGAYSINH = s.NGAYSINH.ToString(),
                CMND = s.CMND
            }).ToList();
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            if (txt_manv.Text == "")
            {
                MessageBox.Show("Mã nhân viên không được phép rỗng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_tennv.Focus();
            }
            else
            {
                if (txt_tennv.Text == "")
                {
                    MessageBox.Show("Tên nhân viên không được phép rỗng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_tennv.Focus();
                }
                else
                {
                    DialogResult ds = MessageBox.Show("Tạo nhân viên mới ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (ds == DialogResult.OK)
                    {
                        try
                        {
                            QLCHViTinhEntities db = new QLCHViTinhEntities();
                            QLCHMAYTINH.NHANVIEN item = new QLCHMAYTINH.NHANVIEN()
                            {
                                MANV = txt_manv.Text,
                                TENNV = txt_tennv.Text,
                                NGAYSINH = (DateTime)date_ngaysinh.EditValue,
                                EMAIL = txt_email.Text,
                                DCHI = txt_diachi.Text,
                                SDT = txt_sdt.Text,
                                CMND = txt_cmnd.Text
                            };
                            db.NHANVIENs.Add(item);
                            db.SaveChanges();
                            gv_nhanvien.DataSource = db.NHANVIENs.OrderBy(a => a.MANV).ToList();
                            ClearData();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Mã nhân viên đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        NhanVienForm_Load(sender, e);
                        ClearData();
                    }
                }
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
             DialogResult ds = MessageBox.Show("Xóa nhân viên ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
             if (ds == DialogResult.OK)
             {
                 try
                 {
                     QLCHViTinhEntities db = new QLCHViTinhEntities();
                     QLCHMAYTINH.NHANVIEN item = (from d in db.NHANVIENs
                                   where d.MANV == manv
                                   select d).Single();
                     db.NHANVIENs.Remove(item);
                     db.SaveChanges();
                     gv_nhanvien.DataSource = db.NHANVIENs.OrderBy(a => a.MANV).ToList();
                     ClearData();
                 }
                 catch (Exception ex)
                 {
                     throw ex;
                 }
             }
             else
             {
                 NhanVienForm_Load(sender, e);
                 ClearData();
             }
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            if (txt_manv.Text == "")
            {
                MessageBox.Show("Mã nhân viên không được phép rỗng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_tennv.Focus();
            }
            else
            {
                if (txt_tennv.Text == "")
                {
                    MessageBox.Show("Tên nhân viên không được phép rỗng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_tennv.Focus();
                }
                else
                {
                    DialogResult ds = MessageBox.Show("Sửa nhân viên mới ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (ds == DialogResult.OK)
                    {
                        try
                        {
                            QLCHViTinhEntities db = new QLCHViTinhEntities();
                            var update = (from u in db.NHANVIENs where u.MANV.ToString() == manv select u).Single();
                            update.MANV = txt_manv.Text;
                            update.TENNV = txt_tennv.Text;
                            update.NGAYSINH =(DateTime) date_ngaysinh.EditValue;
                            update.SDT = txt_sdt.Text;
                            update.DCHI = txt_diachi.Text;
                            update.CMND = txt_cmnd.Text;
                            update.EMAIL = txt_email.Text;
                            db.SaveChanges();
                            gv_nhanvien.DataSource = db.NHANVIENs.OrderBy(a => a.MANV).ToList();
                            ClearData();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Mã nhân viên đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        NhanVienForm_Load(sender, e);
                        ClearData();
                    }
                }
            }
        }

        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            QLCHViTinhEntities db = new QLCHViTinhEntities();
            gv_nhanvien.DataSource = db.NHANVIENs.Where(a => a.MANV.Contains(txt_manv.Text)
                && a.TENNV.Contains(txt_tennv.Text)
                && a.CMND.Contains(txt_cmnd.Text)
                && a.DCHI.Contains(txt_diachi.Text)
                && a.EMAIL.Contains(txt_email.Text)
                && a.SDT.Contains(txt_sdt.Text)).OrderBy(a => a.MANV).ToList();
        }


        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ClearData()
        {
            txt_manv.Text = "";
            txt_tennv.Text = "";
            txt_cmnd.Text = "";
            txt_sdt.Text = "";
            txt_email.Text = "";
            txt_diachi.Text = "";
            date_ngaysinh.EditValue = DateTime.Now;
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            manv = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MANV").ToString();
            txt_manv.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MANV").ToString();
            txt_tennv.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TENNV").ToString();
            date_ngaysinh.EditValue = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "NGAYSINH");
            txt_cmnd.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CMND").ToString();
            txt_diachi.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DCHI").ToString();
            txt_email.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "EMAIL").ToString();
            txt_sdt.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SDT").ToString();
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            NhanVienForm_Load(sender, e);
            ClearData();
        }

        private void btn_report_Click(object sender, EventArgs e)
        {
            FormReport.NhanVienReportForm frm = new FormReport.NhanVienReportForm();
            frm.Show();
        }
    }
}