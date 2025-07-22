using DoAnNhom3.QuanLy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnNhom3
{
    public partial class BaoCaoThongKe: UserControl
    {
        public BaoCaoThongKe()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void LoadBaoCao()
        {
            string query = "";
            SqlParameter[] parameters = {
            new SqlParameter("@TuNgay", dtpTuNgay.Value.Date),
            new SqlParameter("@DenNgay", dtpDenNgay.Value.Date)
            };

            if (rdoNgay.Checked)
            {
                query = @"
            SELECT MaBaoCaoNgay, NgayThang, TenMonAn, DonViTinh, SoLuong, DoanhThuNgay 
            FROM BaoCaoNgay
            WHERE NgayThang BETWEEN @TuNgay AND @DenNgay";
            }
            else if (rdoThang.Checked)
            {
                query = @"
            SELECT MaBaoCaoThang, NgayThang, TenMonAn, DonViTinh, SoLuong, DoanhThuThang 
            FROM BaoCaoThang
            WHERE NgayThang BETWEEN @TuNgay AND @DenNgay";
            }

            DataTable dt = Database.GetData(query, parameters);
            dgvBaoCao.DataSource = dt;

            // Hiển thị tổng doanh thu
            decimal tong = 0;
            foreach (DataRow row in dt.Rows)
            {
                if (rdoNgay.Checked)
                    tong += Convert.ToDecimal(row["DoanhThuNgay"]);
                else
                    tong += Convert.ToDecimal(row["DoanhThuThang"]);
            }
            lblTongDoanhThu.Text = "Tổng doanh thu: " + tong.ToString("N0") + " VNĐ";
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            LoadBaoCao();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
