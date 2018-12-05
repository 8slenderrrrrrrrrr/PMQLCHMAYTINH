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
using System.IO;

namespace QLCHMAYTINH
{
    public partial class LoaiSPForm : DevExpress.XtraEditors.XtraForm
    {
        string path = "../../Images/loaiSP";
        public LoaiSPForm()
        {
            InitializeComponent();
        }

        private void LoaiSPForm_Load(object sender, EventArgs e)
        {
            QLCHViTinhEntities db = new QLCHViTinhEntities();
            gv_loaisp.DataSource = db.LOAISANPHAMs.OrderBy(a => a.MALOAIHH).ToList();
            //btn_xoa.Enabled = false;
            //btn_sua.Enabled = false;
        }

        private void btn_them_Click(object sender, EventArgs e)
        {

        }

        //private void btn_them_Click(object sender, EventArgs e)
        //{
        //    if (txt_maloai.Text == "")
        //    {
        //        MessageBox.Show("Mã loại sản phẩm không được phép rỗng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        txt_maloai.Focus();
        //    }
        //    else if (txt_tenloai.Text == "")
        //    {
        //        MessageBox.Show("Tên loại sản phẩm không được phép rỗng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        txt_tenloai.Focus();
        //    }
        //    else
        //    {
        //        DialogResult ds = MessageBox.Show("Thêm loại sản phẩm phối mới ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        //        if (ds == DialogResult.OK)
        //        {
        //            try
        //            {
        //                QLCHViTinhEntities db = new QLCHViTinhEntities();
        //            }
        //            catch (Exception ex)
        //            { 
        //            }
        //        }
        //    }
        //}

        //private void btn_chonanh_Click(object sender, EventArgs e)
        //{
        //    openFileDialog1.Filter = "JPG Files|*.jpg|PNG Files|*.png|All Files|*.*";
        //    if (openFileDialog1.ShowDialog() == DialogResult.OK)
        //    {
        //        string filename = openFileDialog1.SafeFileName;
        //        string pathFile = path + "/" + filename;
        //        if (!File.Exists(pathFile))
        //        {
        //            File.Copy(openFileDialog1.FileName, pathFile);
        //        }
        //        pictureBox1.ImageLocation = pathFile;
        //    }
        //}
    }
}