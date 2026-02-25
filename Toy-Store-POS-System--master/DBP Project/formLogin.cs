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
    public partial class formLogin : Form
    {
        const string strFileName = "ConnectionString.ini";
        string strConnectionString;
        public formLogin()
        {
            InitializeComponent();
        }

        private void buttonBackLogin_Click(object sender, EventArgs e)
        {
            Form newFormIndex = new formIndex(); // เปลี่ยนเป็นฟอร์มที่ต้องการเปิด
            newFormIndex.Show();  // เปิดฟอร์มใหม่ในลักษณะ modal
            this.Hide(); // ซ่อนฟอร์มปัจจุบัน
        }

        private void checkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (checkShowPassword.Checked)
                textPassword.PasswordChar = '\0'; // โชว์รหัสจริง
            else
                textPassword.PasswordChar = '*';  // ซ่อนเป็น *
        }

        private void buttonLoginDone_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(strConnectionString))
            {
                conn.Open();
                string query = "SELECT 'Employee' AS Role,Emp_Position, Emp_Id " +
                               "FROM Employee WHERE Username = @username AND Password = @password";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", textUsername.Text);
                    cmd.Parameters.AddWithValue("@password", textPassword.Text);

                    SqlDataReader reader = cmd.ExecuteReader();
 
                    if (reader.Read())
                    {
                        string role = reader["Role"].ToString();
                        // เก็บข้อมูลพนักงาน
                        CurrentUser.EmpID = Convert.ToInt32(reader["Emp_Id"]);
                        CurrentUser.EmpPosition = reader["Emp_Position"].ToString(); // หากต้องการเก็บตำแหน่งพนักงาน
                                                                                     // แสดงข้อความว่า "ล็อกอินสำเร็จ"
                        MessageBox.Show("ล็อกอินสำเร็จ", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (role == "Employee")
                        {
                            CurrentUser.EmpID = Convert.ToInt32(reader["Emp_ID"]); // เก็บ Member_ID
                            CurrentUser.EmpPosition = Convert.ToString(reader["Emp_Position"]);
                            CurrentUser.Role = role; // เก็บบทบาทผู้ใช้

                            formShop newShop = new formShop(); // ฟอร์มสำหรับพนักงาน
                            newShop.Show();  // เปิดฟอร์มพนักงาน
                            this.Hide(); // ซ่อนฟอร์มปัจจุบัน
                        }
                    }
                    else
                    {
                        MessageBox.Show("คุณกรอกชื่อผู้ใช้งานหรือรหัสผ่านผิด", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }        
        private void formLogin_Load(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(strFileName))
            {
                strConnectionString = System.IO.File.ReadAllText(strFileName);
            }
            else
            {
                MessageBox.Show("Connection string file is missing or invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void formLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); // ปิดโปรแกรม
        }
    }
    
}
