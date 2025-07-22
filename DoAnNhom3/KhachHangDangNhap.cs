using DoAnNhom3.Model;
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
using Microsoft.Data.SqlClient;

namespace DoAnNhom3
{
    public partial class KhachHangDangNhap : Form
    {
        public KhachHangDangNhap()
        {
            InitializeComponent();
        }

        private void baoCaoThongKe1_Load(object sender, EventArgs e)
        {

        }
        public static string SDTKhachHang;
        private void KhachHangDangNhap_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=QuanLyBanHangOnline;Integrated Security=True;Encrypt=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT MaMon, TenMon, GiaTien, HinhAnh FROM MonAn";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string ma = reader["MaMon"].ToString();
                        string ten = reader["TenMon"].ToString();
                        decimal gia = Convert.ToDecimal(reader["GiaTien"]);
                        string hinh = reader["HinhAnh"].ToString();

                        ucMonAn uc = new ucMonAn();
                        uc.SetData(ma, ten, gia, hinh);
                        flowLayoutPanel1.Controls.Add(uc);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        // Khai báo biến để tái sử dụng các UserControl
        private ucDonHang ucDonHang = new ucDonHang();
        private HoaDon ucHoaDon = new HoaDon();

        private void LoadUserControl(UserControl uc)
        {
            this.panelkhachhang.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            this.panelkhachhang.Controls.Add(uc);
        }

        public void MoDonHang(int maMon, string tenMon, decimal donGia)
        {
            ucDonHang = new ucDonHang();
            ucDonHang.SetData(maMon, tenMon, donGia);  // truyền dữ liệu món
            LoadUserControl(ucDonHang);
        }

        public void MoDonHangVoiDuLieu(DonHang donHang)
        {
            ucDonHang = new ucDonHang();  // tạo mới control
            ucDonHang.SetDonHang(donHang); // truyền model
            LoadUserControl(ucDonHang);
        }

        public void MoHoaDon(string tenMon, decimal donGia, int soLuong, string sdtKH, string hinhAnh)
        {
            ucHoaDon = new HoaDon();
            ucHoaDon.SetData(tenMon, donGia, soLuong, sdtKH, hinhAnh);
            LoadUserControl(ucHoaDon);
        }
    }
}
