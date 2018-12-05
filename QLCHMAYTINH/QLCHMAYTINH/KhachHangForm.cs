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
    public partial class KhachHangForm : DevExpress.XtraEditors.XtraForm
    {
        string makh;
        public KhachHangForm()
        {
            InitializeComponent();
        }

        private void KhachHangForm_Load(object sender, EventArgs e)
        {
            QLCHViTinhEntities db = new QLCHViTinhEntities();
            gv_khachhang.DataSource = db.KHACHHANGs.OrderBy(a => a.MAKH).ToList();
            btn_xoa.Enabled = false;
            btn_sua.Enabled = false;
        }

        private void btn_them_Click(object sender, EventArgs e)
        {

            if (txt_makh.Text == "")
            {
                MessageBox.Show("Mã khách hàng không được phép rỗng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_makh.Focus();
            }
            else if (txt_tenkh.Text == "")
            {
                MessageBox.Show("Tên khách hàng không được phép rỗng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_tenkh.Focus();
            }
            else if (txt_sdt.Text == "")
            {
                MessageBox.Show("Số điện thoại không được phép rỗng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_sdt.Focus();
            }
            else
            {
                DialogResult ds = MessageBox.Show("Tạo khách hàng mới ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (ds == DialogResult.OK)
                {
                    try
                    {
                        QLCHViTinhEntities db = new QLCHViTinhEntities();
                        QLCHMAYTINH.KHACHHANG item = new KHACHHANG()
                        {
                            MAKH = txt_makh.Text,
                            TENKH = txt_tenkh.Text,
                            EMAIL = txt_email.Text,
                            SDT = txt_sdt.Text,
                            DCHI = txt_diachi.Text,
                            TAIKHOAN = txt_taikhoan.Text,
                            GHICHU = txt_ghichu.Text
                        };
                        db.KHACHHANGs.Add(item);
                        db.SaveChanges();
                        gv_khachhang.DataSource = db.KHACHHANGs.OrderBy(a => a.MAKH).ToList();
                        ClearData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Mã khách hàng trùng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txt_makh.Focus();
                        //MessageBox.Show(ex.ToString());
                    }
                }
                else
                {
                    KhachHangForm_Load(sender, e);
                    ClearData();
                }
            }
        }

        public void ClearData()
        {
            txt_email.Text = "";
            txt_diachi.Text = "";
            txt_tenkh.Text = "";
            txt_taikhoan.Text = "";
            txt_makh.Text = "";
            txt_sdt.Text = "";
            txt_ghichu.Text = "";
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            DialogResult ds = MessageBox.Show("Xóa khách hàng ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (ds == DialogResult.OK)
            {
                try
                {
                    QLCHViTinhEntities db = new QLCHViTinhEntities();
                    QLCHMAYTINH.KHACHHANG item = (from d in db.KHACHHANGs
                                             where d.MAKH.ToString() == makh
                                             select d).Single();
                    db.KHACHHANGs.Remove(item);
                    db.SaveChanges();
                    gv_khachhang.DataSource = db.KHACHHANGs.OrderBy(a => a.MAKH).ToList();
                    ClearData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xóa thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                KhachHangForm_Load(sender, e);
                ClearData();
            }
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            btn_xoa.Enabled = true;
            btn_sua.Enabled = true;
            btn_them.Enabled = false;
            btn_timkiem.Enabled = false;
            makh = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MAKH").ToString();
            txt_makh.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MAKH").ToString();
            txt_taikhoan.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TAIKHOAN").ToString();
            txt_tenkh.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TENKH").ToString();
            txt_ghichu.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "GHICHU").ToString();
            txt_email.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "EMAIL").ToString();
            txt_sdt.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SDT").ToString();
            txt_diachi.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DCHI").ToString();
            
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            KhachHangForm_Load(sender, e);
            ClearData();
            btn_them.Enabled = true;
            btn_timkiem.Enabled = true;
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            if (txt_makh.Text == "")
            {
                MessageBox.Show("Mã khách hàng không được phép rỗng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_makh.Focus();
            }
            else if (txt_tenkh.Text == "")
            {
                MessageBox.Show("Tên khách hàng không được phép rỗng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_tenkh.Focus();
            }
            else if (txt_sdt.Text == "")
            {
                MessageBox.Show("Số điện thoại không được phép rỗng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_sdt.Focus();
            }
            else
            {
                DialogResult ds = MessageBox.Show("Sửa thông tin khách hàng ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (ds == DialogResult.OK)
                {
                    try
                    {
                        QLCHViTinhEntities db = new QLCHViTinhEntities();
                        var update = (from u in db.KHACHHANGs where u.MAKH.ToString() == makh select u).Single();
                        update.MAKH = txt_makh.Text;
                        update.TENKH = txt_tenkh.Text;
                        update.SDT = txt_sdt.Text;
                        update.EMAIL = txt_email.Text;
                        update.DCHI = txt_diachi.Text;
                        update.TAIKHOAN = txt_taikhoan.Text;
                        update.GHICHU = txt_ghichu.Text;
                        db.SaveChanges();
                        gv_khachhang.DataSource = db.KHACHHANGs.OrderBy(a => a.MAKH).ToList();
                        ClearData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Mã khách hàng trùng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txt_makh.Focus();
                    }
                }
                else
                {
                    KhachHangForm_Load(sender, e);
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
            gv_khachhang.DataSource = db.KHACHHANGs.Where(a => a.MAKH.Contains(txt_makh.Text)
                && a.TENKH.Contains(txt_tenkh.Text)
                && a.SDT.Contains(txt_sdt.Text)
                && a.EMAIL.Contains(txt_email.Text)
                && a.DCHI.Contains(txt_diachi.Text)
                && a.TAIKHOAN.Contains(txt_taikhoan.Text)
                && a.GHICHU.Contains(txt_ghichu.Text)).OrderBy(a => a.MAKH).ToList();
        }


    }
}