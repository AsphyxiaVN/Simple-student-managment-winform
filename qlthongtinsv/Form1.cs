using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qlthongtinsv
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LayDSHS();
        }
        private void LayDSHS()
        {
            //khởi tạo các đối tượng SqlConnection, SqlDataAdapter, DataTable
            SqlConnection conn = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            //lấy chuỗi kết nối từ file App.config
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;


            try
            {
                //mở chuỗi kết nối
                conn.Open();
                //khai báo đối tượng SqlCommand trong SqlDataAdapter
                da.SelectCommand = new SqlCommand();
                //gọi thủ tục từ SQL
                da.SelectCommand.CommandText = "SP_Retrieve_Student";
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                //gán chuỗi kết nối
                da.SelectCommand.Connection = conn;
                //sử dụng phương thức fill để điền dữ liệu từ datatable vào SqlDataAdapter
                da.Fill(dt);
                //gán dữ liệu từ datatable vào datagridview
                dtgDSSV.DataSource = dt;
                //đóng chuỗi kết nối
                conn.Close();
                //sử dụng thuộc tính Width và HeaderText để set chiều dài và tiêu đề cho các coloumns
                dtgDSSV.Columns[0].Width = 80;
                dtgDSSV.Columns[0].HeaderText = "Mã Sinh Viên";
                dtgDSSV.Columns[1].Width = 110;
                dtgDSSV.Columns[1].HeaderText = "Họ và tên";
                dtgDSSV.Columns[2].Width = 110;
                dtgDSSV.Columns[2].HeaderText = "Số ĐT";
                dtgDSSV.Columns[3].Width = 90;
                dtgDSSV.Columns[3].HeaderText = "Địa Chỉ";
                dtgDSSV.Columns[4].Width = 90;
                dtgDSSV.Columns[4].HeaderText = "Năm Sinh";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Reset()
        {
            txtDiaChi.Text = "";
            txtMSSV.Text = "";
            txtSDT.Text = "";
            txtTen.Text = "";
            txtNamSinh.Text = "";
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Bạn có chắc muốn thoát?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dg == DialogResult.OK)
            {
                Application.Exit();
            }
        }
        public bool KTThongTin()
        {
            if (txtTen.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên sinh viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTen.Focus();
                return false;
            }
            if (txtDiaChi.Text == "")
            {
                MessageBox.Show("Vui lòng nhập địa chỉ sinh viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChi.Focus();
                return false;
            }
            if (txtSDT.Text == "")
            {
                MessageBox.Show("Vui lòng nhập SĐT sinh viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSDT.Focus();
                return false;
            }
            if (txtNamSinh.Text == "")
            {
                MessageBox.Show("Vui lòng nhập năm sinh của sinh viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNamSinh.Focus();
                return false;
            }
            if (txtMSSV.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã sinh viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMSSV.Focus();
                return false;
            }
            return true;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void btnThem_Click_1(object sender, EventArgs e)
        {
            {
                if (KTThongTin())
                {
                    try
                    {
                        SqlConnection conn = new SqlConnection();
                        conn.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                        SqlCommand cmd = new SqlCommand();

                        cmd.CommandText = "SP_ThemHocSinh";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@MSSV", SqlDbType.VarChar).Value = txtMSSV.Text;
                        cmd.Parameters.Add("@Ten", SqlDbType.NVarChar).Value = txtTen.Text;
                        cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar).Value = txtDiaChi.Text;
                        cmd.Parameters.Add("@Mobile", SqlDbType.VarChar).Value = txtSDT.Text;
                        cmd.Parameters.Add("@NamSinh", SqlDbType.Int).Value = txtNamSinh.Text;

                        cmd.Connection = conn;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        LayDSHS();
                        Reset();
                        MessageBox.Show("Đã thêm mới sinh viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = dtgDSSV.Rows[e.RowIndex];
            txtMSSV.Text = Convert.ToString(row.Cells["MaSV"].Value);
            txtTen.Text = Convert.ToString(row.Cells["Hoten"].Value);
            txtDiaChi.Text = Convert.ToString(row.Cells["DiaChi"].Value);
            txtSDT.Text = Convert.ToString(row.Cells["SDT"].Value);
            txtNamSinh.Text = Convert.ToString(row.Cells["NamSinh"].Value);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMSSV.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã sinh viên cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMSSV.Focus();
            }
            else if (KTThongTin())
            {
                try
                {
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = "SP_SuaHocSinh";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MSSV", SqlDbType.VarChar).Value = txtMSSV.Text;
                    cmd.Parameters.Add("@Ten", SqlDbType.NVarChar).Value = txtTen.Text;
                    cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar).Value = txtDiaChi.Text;
                    cmd.Parameters.Add("@Mobile", SqlDbType.VarChar).Value = txtSDT.Text;
                    cmd.Parameters.Add("@NamSinh", SqlDbType.Int).Value = txtNamSinh.Text;

                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    LayDSHS();
                    Reset();
                    MessageBox.Show("Đã sửa sinh viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMSSV.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã sinh viên cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMSSV.Focus();
            }
            else if (KTThongTin())
            {
                try
                {
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = "SP_XoaHocSinh";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MSSV", SqlDbType.VarChar).Value = txtMSSV.Text;

                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    LayDSHS();
                    Reset();
                    MessageBox.Show("Đã xóa sinh viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
