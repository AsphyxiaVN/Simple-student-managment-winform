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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
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
                da.SelectCommand.CommandText = "SP_Retrieve_Diem";
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
                dtgDSSV.Columns[1].Width = 80;
                dtgDSSV.Columns[1].HeaderText = "Mã Môn Học";
                dtgDSSV.Columns[2].Width = 80;
                dtgDSSV.Columns[2].HeaderText = "Tên Môn Học";
                dtgDSSV.Columns[3].Width = 80;
                dtgDSSV.Columns[3].HeaderText = "Số Tín Chỉ";
                dtgDSSV.Columns[4].Width = 80;
                dtgDSSV.Columns[4].HeaderText = "Điểm";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Reset()
        {
            txtMSSV.Text = "";
            txtMaMH.Text = "";
            txtMon.Text = "";
            txtSoTC.Text = "";
            txtDiem.Text = "";
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Bạn có chắc muốn thoát?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dg == DialogResult.OK)
            {
                Application.Exit();
            }
        }
        public bool KTThongTin()
        {
            if (txtDiem.Text == "")
            {
                MessageBox.Show("Vui lòng nhập điểm cần thêm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiem.Focus();
                return false;
            }
            if (txtMaMH.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã môn học", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaMH.Focus();
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
        private void btnThem_Click(object sender, EventArgs e)
        {
            {
                if (KTThongTin())
                {
                    try
                    {
                        SqlConnection conn = new SqlConnection();
                        conn.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                        SqlCommand cmd = new SqlCommand();

                        cmd.CommandText = "SP_ThemDiem";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@MSSV", SqlDbType.Char).Value = txtMSSV.Text;
                        cmd.Parameters.Add("@MaMH", SqlDbType.Char).Value = txtMaMH.Text;
                        cmd.Parameters.Add("@Diem", SqlDbType.Int).Value = txtDiem.Text;

                        cmd.Connection = conn;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        LayDSHS();
                        Reset();
                        MessageBox.Show("Đã thêm mới điểm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void dtgDSSV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = dtgDSSV.Rows[e.RowIndex];
            txtMSSV.Text = Convert.ToString(row.Cells["MaSV"].Value);
            txtMaMH.Text = Convert.ToString(row.Cells["MaMH"].Value);
            txtMon.Text = Convert.ToString(row.Cells["TenMH"].Value);
            txtSoTC.Text = Convert.ToString(row.Cells["SoTC"].Value);
            txtDiem.Text = Convert.ToString(row.Cells["SoDiem"].Value);
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (KTThongTin())
            {
                try
                {
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = "SP_SuaDiem";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Diem", SqlDbType.Int).Value = txtDiem.Text;

                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    LayDSHS();
                    Reset();
                    MessageBox.Show("Đã sửa điểm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                    cmd.CommandText = "SP_XoaDiem";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MSSV", SqlDbType.Char).Value = txtMSSV.Text;
                    cmd.Parameters.Add("@MaMH", SqlDbType.Char).Value = txtMaMH.Text;

                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    LayDSHS();
                    Reset();
                    MessageBox.Show("Đã xóa điểm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
