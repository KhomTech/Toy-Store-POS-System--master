using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBP_Project
{
    public partial class formRegister : Form
    {
        const string strFileName = "ConnectionString.ini";
        string strConnectionString;
        public formRegister()
        {
            InitializeComponent();
        }

        private void buttonBackRegis_Click(object sender, EventArgs e)
        {
            Form newFormIndex = new formIndex(); // เปลี่ยนเป็นฟอร์มที่ต้องการเปิด
            newFormIndex.Show();
            this.Hide(); // ซ่อนฟอร์มปัจจุบัน
        }

        private void buttonNextRegi_Click(object sender, EventArgs e)
        { // ตรวจสอบข้อมูลก่อนบันทึก
            if (!ValidateData())
            {
                return; // ถ้าข้อมูลไม่ถูกต้องจะหยุดการทำงาน
            }
            string email = txtEmail.Text;

            using (SqlConnection conn = new SqlConnection(strConnectionString))
            {
                try
                {
                    conn.Open();

                    // ตรวจสอบว่าอีเมลหรือชื่อผู้ใช้มีอยู่ในฐานข้อมูลแล้วหรือไม่
                    string checkEmailQuery = "SELECT COUNT(*) FROM Member WHERE Member_Email = @Email";
                    SqlCommand checkEmailCmd = new SqlCommand(checkEmailQuery, conn);
                    checkEmailCmd.Parameters.AddWithValue("@Email", email);
                    int emailExists = (int)checkEmailCmd.ExecuteScalar();

                    if (emailExists > 0)
                    {
                        MessageBox.Show("อีเมลนี้ถูกใช้แล้วในระบบ กรุณาลองใหม่อีกครั้ง.", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // เพิ่มข้อมูลลงในตาราง Member
                    string insertQuery = "INSERT INTO Member (Member_FName, Member_LName, Birthdate, Gender, Member_Email, Member_Phone, Status) " +
                                         "VALUES (@FirstName, @LastName, @Birthdate, @Gender, @Email, @Phone, 'Active'); " +
                                         "SELECT SCOPE_IDENTITY();";

                    SqlCommand cmd = new SqlCommand(insertQuery, conn);
                    cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                    cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                    cmd.Parameters.AddWithValue("@Birthdate", dateTimePickerBirthdate.Value);
                    cmd.Parameters.AddWithValue("@Gender", comboBoxGender.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);

                    int memberId = Convert.ToInt32(cmd.ExecuteScalar());

                    // เปิดฟอร์ม ShippingAddress และส่ง Member_ID ไป
                    FormShippingAddress shippingForm = new FormShippingAddress(memberId, this);
                    shippingForm.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        private bool ValidateData()
        {
            string message = "";
            bool allOK = true;

            // ตรวจสอบชื่อจริง
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                message += "กรุณากรอกชื่อจริง\n";
                txtFirstName.Focus();
                allOK = false;
            }

            // ตรวจสอบนามสกุล
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                message += "กรุณากรอกนามสกุล\n";
                txtLastName.Focus();
                allOK = false;
            }
            // ตรวจสอบวันเกิด
            if (dateTimePickerBirthdate.Value >= DateTime.Now)
            {
                message += "กรุณากรอกวันเกิดที่ถูกต้อง\n";
                dateTimePickerBirthdate.Focus();
                allOK = false;
            }

            // ตรวจสอบเพศ
            if (comboBoxGender.SelectedItem == null)
            {
                message += "กรุณาเลือกเพศ\n";
                comboBoxGender.Focus();
                allOK = false;
            }
            // ตรวจสอบอีเมล
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !ValidateEmail(txtEmail.Text))
            {
                message += "กรุณากรอกอีเมลที่ถูกต้อง\n";
                txtEmail.Focus();
                allOK = false;
            }

            // ตรวจสอบหมายเลขโทรศัพท์
            if (string.IsNullOrWhiteSpace(txtPhone.Text) || txtPhone.Text.Length < 10 || txtPhone.Text.Length > 10)
            {
                message += "กรุณากรอกหมายเลขโทรศัพท์ที่ถูกต้อง\n";
                txtPhone.Focus();
                allOK = false;
            }
            // หากตรวจสอบไม่ผ่าน จะขึ้นข้อความแจ้งเตือน
            if (!allOK)
            {
                MessageBox.Show(message, "ข้อผิดพลาดในการตรวจสอบ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return allOK;
        }

        // ตรวจสอบรูปแบบอีเมล
        private bool ValidateEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(emailPattern);
            return regex.IsMatch(email);
        }


        private void formRegister_Load(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(strFileName))
                strConnectionString = System.IO.File.ReadAllText(strFileName);
        }

        private void formRegister_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
