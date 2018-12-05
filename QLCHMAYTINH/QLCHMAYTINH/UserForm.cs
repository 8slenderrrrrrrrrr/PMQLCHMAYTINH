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
    public partial class UserForm : DevExpress.XtraEditors.XtraForm
    {
        string id;
        public UserForm()
        {
            InitializeComponent();
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            QLCHViTinhEntities db = new QLCHViTinhEntities();
            gv_user.DataSource = db.USERs.OrderBy(a => a.ID).ToList();
            datetime_ngaytao.EditValue = DateTime.Now;
            btn_xoa.Enabled = false;
            btn_sua.Enabled = false;
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            if (txt_taikhoan.Text == "")
            {
                MessageBox.Show("Tài khoản không được phép rỗng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_taikhoan.Focus(); 
            }
            else if (txt_matkhau.Text == "")
            {
                MessageBox.Show("Mật khẩu không được phép rỗng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_matkhau.Focus();
            }
            else if (txt_hoten.Text == "")
            {
                MessageBox.Show("Tên không được phép rỗng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_hoten.Focus();
            }
            else
            {
                DialogResult ds = MessageBox.Show("Tạo người dùng mới ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (ds == DialogResult.OK)
                {
                    try
                    {
                        QLCHViTinhEntities db = new QLCHViTinhEntities();
                        QLCHMAYTINH.USER item = new USER()
                        {
                            TAIKHOAN = txt_taikhoan.Text,
                            MATKHAU = txt_matkhau.Text,
                            HOTEN = txt_hoten.Text,
                            EMAIL = txt_email.Text,
                            SDT = txt_sdt.Text,
                            DIACHI = txt_diachi.Text,
                            NGAYTAO = (DateTime)datetime_ngaytao.EditValue
                        };
                        db.USERs.Add(item);
                        db.SaveChanges();
                        gv_user.DataSource = db.USERs.OrderBy(a => a.ID).ToList();
                        ClearData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Tạo mới thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    UserForm_Load(sender, e);
                    ClearData();
                }
            }
        }

        public void ClearData()
        {
            txt_email.Text = "";
            txt_diachi.Text = "";
            txt_hoten.Text = "";
            txt_taikhoan.Text = "";
            txt_matkhau.Text = "";
            txt_sdt.Text = "";
            datetime_ngaytao.EditValue = DateTime.Now;
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            DialogResult ds = MessageBox.Show("Xóa người dùng ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (ds == DialogResult.OK)
            {
                try
                {
                    QLCHViTinhEntities db = new QLCHViTinhEntities();
                    QLCHMAYTINH.USER item = (from d in db.USERs
                                             where d.ID.ToString() == id
                                             select d).Single();
                    db.USERs.Remove(item);
                    db.SaveChanges();
                    gv_user.DataSource = db.USERs.OrderBy(a => a.ID).ToList();
                    ClearData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xóa thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                UserForm_Load(sender, e);
                ClearData();
            }
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            btn_xoa.Enabled = true;
            btn_sua.Enabled = true;
            btn_them.Enabled = false;
            btn_timkiem.Enabled = false;
            id = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ID").ToString();
            txt_taikhoan.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TAIKHOAN").ToString();
            txt_matkhau.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MATKHAU").ToString();
            txt_hoten.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "HOTEN").ToString();
            txt_email.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "EMAIL").ToString();
            txt_sdt.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SDT").ToString();
            txt_diachi.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DIACHI").ToString();
            datetime_ngaytao.EditValue = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "NGAYTAO");
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            UserForm_Load(sender, e);
            ClearData();
            btn_them.Enabled = true;
            btn_timkiem.Enabled = true;
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            if (txt_taikhoan.Text == "")
            {
                MessageBox.Show("Tài khoản không được phép rỗng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_taikhoan.Focus();
            }
            else if (txt_matkhau.Text == "")
            {
                MessageBox.Show("Mật khẩu không được phép rỗng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_matkhau.Focus();
            }
            else if (txt_hoten.Text == "")
            {
                MessageBox.Show("Tên không được phép rỗng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_hoten.Focus();
            }
            else
            {
                DialogResult ds = MessageBox.Show("Sửa thông tin người dùng ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (ds == DialogResult.OK)
                {
                    try
                    {
                        QLCHViTinhEntities db = new QLCHViTinhEntities();
                        var update = (from u in db.USERs where u.ID.ToString() == id select u).Single();
                        update.TAIKHOAN = txt_taikhoan.Text;
                        update.MATKHAU = txt_matkhau.Text;
                        update.HOTEN = txt_hoten.Text;
                        update.SDT = txt_sdt.Text;
                        update.EMAIL = txt_email.Text;
                        update.DIACHI = txt_diachi.Text;
                        update.NGAYTAO = (DateTime)datetime_ngaytao.EditValue;
                        db.SaveChanges();
                        gv_user.DataSource = db.USERs.OrderBy(a => a.ID).ToList();
                        ClearData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Sửa người dùng thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    UserForm_Load(sender, e);
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
            gv_user.DataSource = db.USERs.Where(a => a.TAIKHOAN.Contains(txt_taikhoan.Text)
                && a.MATKHAU.Contains(txt_matkhau.Text)
                && a.HOTEN.Contains(txt_hoten.Text)
                && a.SDT.Contains(txt_sdt.Text)
                && a.EMAIL.Contains(txt_email.Text)
                && a.DIACHI.Contains(txt_diachi.Text)).OrderBy(a => a.ID).ToList();
        }
    }
}