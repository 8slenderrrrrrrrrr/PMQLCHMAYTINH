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
    public partial class NhaPPForm : DevExpress.XtraEditors.XtraForm
    {
        string manpp;
        public NhaPPForm()
        {
            InitializeComponent();
        }

        private void NhaPPForm_Load(object sender, EventArgs e)
        {
            QLCHViTinhEntities db = new QLCHViTinhEntities();
            gv_npp.DataSource = db.NHAPHANPHOIs.OrderBy(a => a.MANPP).ToList();
            btn_xoa.Enabled = false;
            btn_sua.Enabled = false;
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            if (txt_manpp.Text == "")
            {
                MessageBox.Show("Mã nhà phân phối không được phép rỗng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_manpp.Focus();
            }
            else if (txt_tennpp.Text == "")
            {
                MessageBox.Show("Tên nhà phân phối không được phép rỗng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_tennpp.Focus();
            }
            else if (txt_sdt.Text == "")
            {
                MessageBox.Show("Số điện thoại không được phép rỗng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_sdt.Focus();
            }
            else
            {
                DialogResult ds = MessageBox.Show("Thêm nhà phân phối mới ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (ds == DialogResult.OK)
                {
                    try
                    {
                        QLCHViTinhEntities db = new QLCHViTinhEntities();
                        QLCHMAYTINH.NHAPHANPHOI item = new NHAPHANPHOI()
                        {
                            MANPP = txt_manpp.Text,
                            TENNPP = txt_tennpp.Text,
                            DIACHI = txt_diachi.Text,
                            DIENTHOAI = txt_sdt.Text,
                            EMAIL = txt_email.Text,
                            FAX = txt_fax.Text,
                            TAIKHOAN = txt_taikhoan.Text,
                            MSTHUE = txt_masothue.Text,
                            GHICHU = txt_ghichu.Text
                        };
                        db.NHAPHANPHOIs.Add(item);
                        db.SaveChanges();
                        gv_npp.DataSource = db.NHAPHANPHOIs.OrderBy(a => a.MANPP).ToList();
                        ClearData();
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("Mã nhà phân phối trùng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //txt_manpp.Focus();
                        MessageBox.Show(ex.ToString());
                    }
                }
                else
                {
                    NhaPPForm_Load(sender, e);
                    ClearData();
                }
            }
        }

        public void ClearData()
        {
            txt_manpp.Text = "";
            txt_tennpp.Text = "";
            txt_diachi.Text = "";
            txt_sdt.Text = "";
            txt_email.Text = "";
            txt_fax.Text = "";
            txt_taikhoan.Text = "";
            txt_masothue.Text = "";
            txt_ghichu.Text = "";
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            DialogResult ds = MessageBox.Show("Xóa nhà phân phối ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (ds == DialogResult.OK)
            {
                try
                {
                    QLCHViTinhEntities db = new QLCHViTinhEntities();
                    QLCHMAYTINH.NHAPHANPHOI item = (from d in db.NHAPHANPHOIs
                                                    where d.MANPP == manpp
                                                    select d).Single();
                    db.NHAPHANPHOIs.Remove(item);
                    db.SaveChanges();
                    gv_npp.DataSource = db.NHAPHANPHOIs.OrderBy(a => a.MANPP).ToList();
                    ClearData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xóa thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                NhaPPForm_Load(sender, e);
                ClearData();
            }
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            btn_xoa.Enabled = true;
            btn_sua.Enabled = true;
            btn_them.Enabled = false;
            btn_timkiem.Enabled = false;
            manpp = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MANPP").ToString();
            txt_manpp.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MANPP").ToString();
            txt_tennpp.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TENNPP").ToString();
            txt_diachi.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DIACHI").ToString();
            txt_sdt.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DIENTHOAI").ToString();
            txt_email.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "EMAIL").ToString();
            txt_fax.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "FAX").ToString();
            txt_taikhoan.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TAIKHOAN").ToString();
            txt_masothue.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MSTHUE").ToString();
            txt_ghichu.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "GHICHU").ToString();
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            NhaPPForm_Load(sender, e);
            ClearData();
            btn_them.Enabled = true;
            btn_timkiem.Enabled = true;
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            if (txt_manpp.Text == "")
            {
                MessageBox.Show("Mã nhà phân phối không được phép rỗng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_manpp.Focus();
            }
            else if (txt_tennpp.Text == "")
            {
                MessageBox.Show("Tên nhà phân phối không được phép rỗng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_tennpp.Focus();
            }
            else if (txt_sdt.Text == "")
            {
                MessageBox.Show("Số điện thoại không được phép rỗng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_sdt.Focus();
            }
            else
            {
                DialogResult ds = MessageBox.Show("Sửa thông tin nhà phân phối mới ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (ds == DialogResult.OK)
                {
                    try
                    {
                        QLCHViTinhEntities db = new QLCHViTinhEntities();
                        var update = (from u in db.NHAPHANPHOIs where u.MANPP == manpp select u).Single();
                        update.MANPP = txt_manpp.Text;
                        update.TENNPP = txt_tennpp.Text;
                        update.DIACHI = txt_diachi.Text;
                        update.DIENTHOAI = txt_sdt.Text;
                        update.EMAIL = txt_email.Text;
                        update.FAX = txt_fax.Text;
                        update.TAIKHOAN = txt_taikhoan.Text;
                        update.MSTHUE = txt_masothue.Text;
                        update.GHICHU = txt_ghichu.Text;
                        db.SaveChanges();
                        gv_npp.DataSource = db.NHAPHANPHOIs.OrderBy(a => a.MANPP).ToList();
                        ClearData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Mã nhà phân phối trùng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txt_manpp.Focus();
                    }
                }
                else
                {
                    NhaPPForm_Load(sender, e);
                    ClearData();
                }
            }
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            QLCHViTinhEntities db = new QLCHViTinhEntities();
            gv_npp.DataSource = db.NHAPHANPHOIs.Where(a=>a.MANPP.Contains(txt_manpp.Text)
                && a.TENNPP.Contains(txt_tennpp.Text)
                && a.DIACHI.Contains(txt_diachi.Text)
                && a.DIENTHOAI.Contains(txt_sdt.Text)
                && a.EMAIL.Contains(txt_email.Text)
                && a.FAX.Contains(txt_fax.Text)
                && a.TAIKHOAN.Contains(txt_taikhoan.Text)
                && a.MSTHUE.Contains(txt_masothue.Text)
                && a.GHICHU.Contains(txt_ghichu.Text)).OrderBy(a=>a.MANPP).ToList();
        }
    }
}