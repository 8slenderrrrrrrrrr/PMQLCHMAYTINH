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
using System.Data.SqlClient;
using System.Data;
namespace QLCHMAYTINH.FormReport
{
    public partial class NhanVienReportForm : DevExpress.XtraEditors.XtraForm
    {
        QLCHMAYTINH.QLCHViTinhEntities db = new QLCHViTinhEntities();
        public NhanVienReportForm()
        {
            InitializeComponent();
        }

        private void NhanVienReportForm_Load(object sender, EventArgs e)
        {
            //SqlConnection cnn = new SqlConnection("Data Source=DESKTOP-KS2AHB8\\SQLEXPRESS;Initial Catalog=QLCHViTinh;Integrated Security=True");
            //SqlDataAdapter da = new SqlDataAdapter();
            //DataTable dt = new System.Data.DataTable();
            //da.Fill(dt);
            
            Report.NhanVienReport rpt = new Report.NhanVienReport();
            NhanVienForm item = new NhanVienForm();
            rpt.SetDataSource(item.GetAllNhanVien());
            crystalReportViewer1.ReportSource = rpt;
        }
    }
}