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

namespace DBP_Project
{
    public partial class formReportSell : Form
    {
        const string strFileName = "ConnectionString.ini";
        string connectionString = "";
        public formReportSell()
        {
            InitializeComponent();
            LoadConnectionString();
            LoadComboBoxes();  
        }
        private void LoadConnectionString()
        {
            if (System.IO.File.Exists(strFileName))
            {
                connectionString = System.IO.File.ReadAllText(strFileName).Trim();
            }
            else
            {
                MessageBox.Show("ไม่พบไฟล์ ConnectionString.ini", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadSalesData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT o.Order_ID, 
                                CONVERT(DATE, o.Order_Date) AS Date, 
                                p.Product_Name, 
                                pc.Category_Name,
                                od.Quantity,
                                p.Cost_Price,
                                p.Product_Price,
                                od.Total_Price
                         FROM Orders o
                         JOIN Order_Detail od ON o.Order_ID = od.Order_ID
                         JOIN Product p ON od.Product_ID = p.Product_ID
                         JOIN Product_Category pc ON p.Category_ID = pc.Category_ID
                         WHERE 1=1 ";

                if (comboDay.SelectedIndex > 0)
                {
                    query += " AND DAY(o.Order_Date) = @Day ";
                }
                if (comboMonth.SelectedIndex > 0)
                {
                    query += " AND MONTH(o.Order_Date) = @Month ";
                }
                if (comboYear.SelectedItem != null)
                {
                    query += " AND YEAR(o.Order_Date) = @Year ";
                }
                if (comboCategory.SelectedIndex > 0)
                {
                    query += " AND pc.Category_Name = @Category ";
                }

                query += " ORDER BY Date, o.Order_ID, p.Product_Name;";

                SqlCommand cmd = new SqlCommand(query, conn);

                if (comboDay.SelectedIndex > 0)
                {
                    cmd.Parameters.AddWithValue("@Day", comboDay.SelectedItem);
                }
                if (comboMonth.SelectedIndex > 0)
                {
                    cmd.Parameters.AddWithValue("@Month", comboMonth.SelectedIndex);
                }
                if (comboYear.SelectedItem != null)
                {
                    cmd.Parameters.AddWithValue("@Year", comboYear.SelectedItem);
                }
                if (comboCategory.SelectedIndex > 0)
                {
                    cmd.Parameters.AddWithValue("@Category", comboCategory.SelectedItem.ToString());
                }

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable salesTable = new DataTable();
                adapter.Fill(salesTable);

                // ✅ คำนวณผลรวมแต่ละคอลัมน์
                int totalQuantity = 0;
                decimal totalRevenue = 0;

                foreach (DataRow row in salesTable.Rows)
                {
                    totalQuantity += Convert.ToInt32(row["Quantity"]);
                    totalRevenue += Convert.ToDecimal(row["Total_Price"]);
                }

                // ✅ เพิ่มแถวสรุปท้ายตาราง
                DataRow footerRow = salesTable.NewRow();
                footerRow["Product_Name"] = "รวมทั้งหมด";
                footerRow["Quantity"] = totalQuantity;
                footerRow["Total_Price"] = totalRevenue;
                salesTable.Rows.Add(footerRow);

                dataGridViewSales.DataSource = salesTable;
            }
        }

        private void LoadComboBoxes()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // โหลดข้อมูลปีลงใน ComboBox
                for (int year = DateTime.Now.Year; year >= 2000; year--)
                {
                    comboYear.Items.Add(year);
                }
                comboYear.SelectedItem = DateTime.Now.Year;

                // โหลดข้อมูลเดือนลงใน ComboBox
                comboMonth.Items.AddRange(new string[] {
            "ทั้งหมด", "มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน",
            "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม"
        });
                comboMonth.SelectedIndex = 0;

                // โหลดข้อมูลวันที่ 1-31 ลงใน ComboBox
                for (int day = 1; day <= 31; day++)
                {
                    comboDay.Items.Add(day);
                }
                comboDay.Items.Insert(0, "ทั้งหมด");
                comboDay.SelectedIndex = 0;

                // โหลดข้อมูล Category ลงใน ComboBox
                string query = "SELECT DISTINCT Category_Name FROM Product_Category ORDER BY Category_Name";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                comboCategory.Items.Add("ทั้งหมด");
                while (reader.Read())
                {
                    comboCategory.Items.Add(reader["Category_Name"].ToString());
                }
                reader.Close();
                comboCategory.SelectedIndex = 0;
            }
        }

        private void buttonBackLogin_Click(object sender, EventArgs e)
        {
            formShop newShop = new formShop(); // ฟอร์มสำหรับพนักงาน
            newShop.Show();  // เปิดฟอร์มพนักงาน
            this.Hide(); // ซ่อนฟอร์มปัจจุบัน
        }


        private void buttonFind_Click(object sender, EventArgs e)
        {
            LoadSalesData();
        }

        private void formReportSell_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
