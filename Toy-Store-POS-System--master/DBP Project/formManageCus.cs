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
using System.IO;
using System.Text.RegularExpressions;

namespace DBP_Project
{
    public partial class formManageCus : Form
    {
        const string strFileName = "ConnectionString.ini";
        private string strConnectionString = "";  // ประกาศตัวแปรสำหรับ Connection String

        SqlConnection storeConnection;
        SqlCommand customerCommand;
        SqlDataAdapter customerAdapter;
        DataTable customerTable;
        CurrencyManager customerManager;

        string myState;
        int myCus;

        public formManageCus()
        {
            InitializeComponent();
        }

        private void formManageCus_Load(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(strFileName))
                    strConnectionString = File.ReadAllText(strFileName, Encoding.GetEncoding("Windows-874"));

                if (string.IsNullOrEmpty(strConnectionString))
                {
                    MessageBox.Show("ไม่สามารถอ่านค่า Connection String ได้จากไฟล์", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Connect to books database
                storeConnection = new SqlConnection(strConnectionString);
                storeConnection.Open();

                // Establish command object
                customerCommand = new SqlCommand("SELECT * FROM Member ORDER BY Member_ID", storeConnection);

                // Establish data adapter/data table
                customerAdapter = new SqlDataAdapter(customerCommand);
                customerTable = new DataTable();
                customerAdapter.Fill(customerTable);

                // Bind controls to data table
                txtFirstName.DataBindings.Add("Text", customerTable, "Member_FName");
                txtLastName.DataBindings.Add("Text", customerTable, "Member_LName");
                dateTimePickerBirthdate.DataBindings.Add("Text", customerTable, "Birthdate");
                comboBoxGender.DataBindings.Add("Text", customerTable, "Gender");
                txtEmail.DataBindings.Add("Text", customerTable, "Member_Email");
                comboStatus.DataBindings.Add("Text", customerTable, "Status");
                txtPhone.DataBindings.Add("Text", customerTable, "Member_Phone");
                if (comboBoxGender.SelectedItem == null)
                {
                    comboBoxGender.SelectedIndex = 0; // ตั้งค่าให้เลือกค่าแรก (ถ้ามี)
                }
                if (comboBoxGender.SelectedItem == null) { comboBoxGender.SelectedIndex = 0; }

                // Establish currency manager
                customerManager = (CurrencyManager)this.BindingContext[customerTable];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "เกิดข้อผิดพลาดในการสร้างการทำงานกับตารางผู้แต่ง", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Show();
            SetState("View");
        }
        private void SetState(string appState)
        {
            myState = appState;
            switch (appState)
            {
                case "View":
                    txtFirstName.ReadOnly = true;
                    txtLastName.ReadOnly = true;
                    dateTimePickerBirthdate.Enabled = false;
                    comboBoxGender.Enabled = false;
                    txtEmail.ReadOnly = true;
                    txtPhone.ReadOnly = true;
                    buttonFirst.Enabled = true;
                    buttonPrevious.Enabled = true;
                    buttonNext.Enabled = true;
                    buttonLast.Enabled = true;
                    buttonAddNew.Enabled = true;
                    buttonSave.Enabled = false;
                    buttonCancel.Enabled = false;
                    buttonEdit.Enabled = true;
                    buttonDelete.Enabled = true;
                    buttonDone.Enabled = true;
                    comboStatus.Enabled = false;
                    break;
                default: // Add or Edit if not View
                    txtFirstName.ReadOnly = false;
                    txtLastName.ReadOnly = false;
                    dateTimePickerBirthdate.Enabled = true;
                    comboBoxGender.Enabled = true;
                    txtEmail.ReadOnly = false;
                    txtPhone.ReadOnly = false;
                    comboStatus.Enabled = true;
                    buttonFirst.Enabled = false;
                    buttonPrevious.Enabled = false;
                    buttonNext.Enabled = false;
                    buttonLast.Enabled = false;
                    buttonAddNew.Enabled = false;
                    buttonSave.Enabled = true;
                    buttonCancel.Enabled = true;
                    buttonEdit.Enabled = false;
                    buttonDelete.Enabled = false;
                    buttonDone.Enabled = false;
                    break;
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            SetState("Edit");
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            { // ตรวจสอบข้อมูลก่อนบันทึก
                if (!ValidateData())
                {
                    return; // ถ้าข้อมูลไม่ถูกต้องจะไม่ทำการบันทึก
                }

                // ตรวจสอบสถานะว่าอยู่ในโหมดแก้ไขหรือไม่
                if (myState == "Edit")
                {
                    using (SqlConnection conn = new SqlConnection(strConnectionString))
                    {
                        conn.Open();

                        // คำสั่ง SQL สำหรับการอัปเดตข้อมูล
                        string updateQuery = "UPDATE Member SET Member_FName = @FirstName, Member_LName = @LastName, " +
                                             "Birthdate = @Birthdate, Gender = @Gender, Member_Email = @Email, " +
                                             "Member_Phone = @Phone, Status = @Status WHERE Member_ID = @MemberID";

                        SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                        updateCmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                        updateCmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                        updateCmd.Parameters.AddWithValue("@Birthdate", dateTimePickerBirthdate.Value.Date);
                        updateCmd.Parameters.AddWithValue("@Gender", comboBoxGender.Text);  // ตรวจสอบว่า ComboBox เลือกค่า
                        updateCmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        updateCmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                        updateCmd.Parameters.AddWithValue("@MemberID", Convert.ToInt32(customerTable.Rows[customerManager.Position]["Member_ID"]));  // ใช้ Member_ID จากตำแหน่งปัจจุบัน
                        updateCmd.Parameters.AddWithValue("@Status", comboStatus.SelectedItem.ToString());

                        int rowsAffected = updateCmd.ExecuteNonQuery();  // ส่งคำสั่ง UPDATE ไปยังฐานข้อมูล

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("ข้อมูลถูกอัปเดตแล้ว", "บันทึก", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // รีเฟรชข้อมูลใน customerTable
                            customerTable.Clear();  // เคลียร์ข้อมูลเดิม
                            customerAdapter.Fill(customerTable);  // โหลดข้อมูลใหม่จากฐานข้อมูล

                            SetState("View");  // เปลี่ยนสถานะกลับเป็น "View"
                        }
                        else
                        {
                            MessageBox.Show("ไม่พบข้อมูลที่ต้องการอัปเดต", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else // ในกรณีที่ไม่ได้อยู่ในโหมดแก้ไขหรือเพิ่ม (ใช้สำหรับการเพิ่มข้อมูลใหม่)
                {
                    using (SqlConnection conn = new SqlConnection(strConnectionString))
                    {
                        conn.Open();

                        // ตรวจสอบว่า Email มีในฐานข้อมูลแล้วหรือไม่
                        string checkEmailQuery = "SELECT COUNT(*) FROM Member WHERE Member_Email = @Email";
                        SqlCommand checkEmailCmd = new SqlCommand(checkEmailQuery, conn);
                        checkEmailCmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        int emailExists = (int)checkEmailCmd.ExecuteScalar();

                        if (emailExists > 0)
                        {
                            MessageBox.Show("อีเมลนี้ถูกใช้แล้วในระบบ กรุณาลองใหม่อีกครั้ง.", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // คำสั่ง SQL สำหรับการเพิ่มข้อมูลใหม่
                        string insertQuery = "INSERT INTO Member (Member_FName, Member_LName, Birthdate, Gender, Member_Email, Member_Phone, Status) " +
                                             "VALUES (@FirstName, @LastName, @Birthdate, @Gender, @Email, @Phone, @Status); " +
                                             "SELECT SCOPE_IDENTITY();";

                        SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                        insertCmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                        insertCmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                        insertCmd.Parameters.AddWithValue("@Birthdate", dateTimePickerBirthdate.Value);
                        insertCmd.Parameters.AddWithValue("@Gender", comboBoxGender.SelectedItem.ToString());  // ตรวจสอบว่า ComboBox เลือกค่า
                        insertCmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        insertCmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                        insertCmd.Parameters.AddWithValue("@Status", comboStatus.SelectedItem.ToString());

                        int memberId = Convert.ToInt32(insertCmd.ExecuteScalar());  // รับ Member_ID ที่ถูกสร้างใหม่

                        MessageBox.Show("ข้อมูลสมาชิกใหม่ถูกบันทึกแล้ว", "บันทึก", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // รีเฟรชข้อมูลใน customerTable
                        customerTable.Clear();
                        customerAdapter.Fill(customerTable);

                        // ค้นหาตำแหน่งของข้อมูลใหม่ใน customerTable
                        int newPosition = customerTable.Rows.IndexOf(customerTable.Select("Member_ID = " + memberId)[0]);

                        // กำหนดตำแหน่งของ customerManager ให้ชี้ไปยังข้อมูลใหม่
                        customerManager.Position = newPosition;

                        SetState("View"); // เปลี่ยนสถานะกลับไปที่ View
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการบันทึกข้อมูล: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ValidateData()
        {
            string message = "";
            bool allOK = true;

            // ตรวจสอบชื่อจริง
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                message += "คุณต้องป้อนชื่อจริง\n";
                txtFirstName.Focus();
                allOK = false;
            }

            // ตรวจสอบนามสกุล
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                message += "คุณต้องป้อนนามสกุล\n";
                txtLastName.Focus();
                allOK = false;
            }

            // ตรวจสอบอีเมล
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !ValidateEmail(txtEmail.Text))
            {
                message += "คุณต้องป้อนอีเมลที่ถูกต้อง\n";
                txtEmail.Focus();
                allOK = false;
            }

            // ตรวจสอบหมายเลขโทรศัพท์
            if (string.IsNullOrWhiteSpace(txtPhone.Text) || txtPhone.Text.Length < 10 || txtPhone.Text.Length > 10)
            {
                message += "คุณต้องป้อนหมายเลขโทรศัพท์ที่ถูกต้อง\n";
                txtPhone.Focus();
                allOK = false;
            }

            // ตรวจสอบสถานะสมาชิก
            if (comboStatus.SelectedItem == null)
            {
                message += "คุณต้องเลือกสถานะสมาชิก\n";
                comboStatus.Focus();
                allOK = false;
            }

            // หากตรวจสอบไม่ผ่าน จะขึ้นข้อความแจ้งเตือน
            if (!allOK)
            {
                MessageBox.Show(message, "ข้อผิดพลาดในการตรวจสอบ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return allOK;
        }
        private bool ValidateEmail(string email)
        {
            // Regular Expression pattern สำหรับตรวจสอบรูปแบบอีเมล
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(emailPattern);

            return regex.IsMatch(email);
        }


        private void buttonCancel_Click(object sender, EventArgs e)
        {
            customerManager.CancelCurrentEdit();
            if (myState.Equals("Add"))
            {
                customerManager.Position = myCus;
            }
            SetState("View");
        }

        private void buttonAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                myCus = customerManager.Position;
                customerManager.AddNew();
                SetState("Add");
            }
            catch
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการเพิ่มข้อมูล", "ข้อผิดพลาด",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DialogResult response;
            response = MessageBox.Show("คุณแน่ใจหรือไม่ว่าต้องการลบข้อมูลนี้", "ลบ",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button2);
            if (response == DialogResult.No)
            {
                return;
            }
            try
            {
                // ลบข้อมูลจากฐานข้อมูล
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM Member WHERE Member_ID = @MemberID";
                    SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn);
                    deleteCmd.Parameters.AddWithValue("@MemberID", Convert.ToInt32(customerTable.Rows[customerManager.Position]["Member_ID"]));  // ใช้ Member_ID จากตำแหน่งปัจจุบัน

                    int rowsAffected = deleteCmd.ExecuteNonQuery();  // ส่งคำสั่ง DELETE ไปยังฐานข้อมูล

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("ลบข้อมูลเรียบร้อยแล้ว", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // รีเฟรชข้อมูลใน customerTable
                        customerTable.Clear();
                        customerAdapter.Fill(customerTable);

                        // ปรับตำแหน่งใน customerManager
                        if (customerManager.Position >= customerManager.Count)
                        {
                            customerManager.Position = customerManager.Count - 1;  // กรณีที่ลบรายการสุดท้าย
                        }
                    }
                    else
                    {
                        MessageBox.Show("ไม่พบข้อมูลที่ต้องการลบ", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการลบข้อมูล: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            Form newShop = new formShop(); // เปลี่ยนเป็นฟอร์มที่ต้องการเปิด
            newShop.Show();  // เปิดฟอร์มใหม่ในลักษณะ modal
            this.Hide(); // ซ่อนฟอร์มปัจจุบัน
        }

        private void formManageCus_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (myState.Equals("Edit") || myState.Equals("Add"))
            {
                MessageBox.Show("คุณต้องแก้ไขข้อมูลปัจจุบันให้เสร็จก่อนที่จะหยุดโปรแกรม", "",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                e.Cancel = true;
            }
            else
            {
                try
                {
                    SqlCommandBuilder productAdapterCommands
                        = new SqlCommandBuilder(customerAdapter);
                    customerAdapter.Update(customerTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการบันทึกฐานข้อมูล: \r\n" + ex.Message,
                        "ข้อผิดพลาดในการบันทึก",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            // Close the connection
            storeConnection.Close();
            // Dispose of the objects
            storeConnection.Dispose();
            customerCommand.Dispose();
            customerAdapter.Dispose();
            customerTable.Dispose();

            Application.Exit();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (customerManager.Position == customerManager.Count - 1)
            {
                return;
            }
            customerManager.Position++;
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            if (customerManager.Position == 0)
            {
                return;
            }
            customerManager.Position--;
        }

        private void buttonFirst_Click(object sender, EventArgs e)
        {
            customerManager.Position = 0;
  
        }

        private void buttonLast_Click(object sender, EventArgs e)
        {
            customerManager.Position = customerManager.Count - 1;

        }

        private void buttonEditAddress_Click(object sender, EventArgs e)
        {
            if (customerManager.Count == 0)
            {
                MessageBox.Show("ไม่มีข้อมูลลูกค้าให้แก้ไขที่อยู่", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ดึงค่า Member_ID จาก customerTable
            int memberId = Convert.ToInt32(customerTable.Rows[customerManager.Position]["Member_ID"]);

            // เปิดฟอร์ม formManageCusShip และส่งค่า Member_ID
            formManageCusShip addressForm = new formManageCusShip(memberId);
            addressForm.ShowDialog();
        }

        private void buttonEditBillingAddress_Click(object sender, EventArgs e)
        {
            if (customerManager.Count == 0)
            {
                MessageBox.Show("ไม่มีข้อมูลลูกค้าให้แก้ไขที่อยู่", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ดึงค่า Member_ID จาก customerTable
            int memberId = Convert.ToInt32(customerTable.Rows[customerManager.Position]["Member_ID"]);

            // เปิดฟอร์ม formManageCusShip และส่งค่า Member_ID
            formBillingAddress BillingForm = new formBillingAddress(memberId);
            BillingForm.ShowDialog();
        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            if (textFind.Text.Equals(""))
            {
                return; // ถ้าไม่ได้กรอกข้อความให้หยุดการทำงาน
            }
            int savedRow = customerManager.Position; // บันทึกตำแหน่งของแถวที่เลือกในตาราง
            DataRow[] foundRows; // ตัวแปรสำหรับเก็บแถวที่พบ

            // เรียงข้อมูลใน DataTable ตาม Member_ID ก่อนค้นหา
            customerTable.DefaultView.Sort = "Member_ID ASC";

            // ค้นหาชื่อสมาชิกที่มี textFind.Text ปรากฏอยู่ที่ไหนก็ได้
            foundRows = customerTable.Select("Member_FName LIKE '%" + textFind.Text + "%'");

            // ถ้าไม่พบข้อมูลใดๆ ให้รีเซ็ตตำแหน่งกลับไปยังตำแหน่งเดิม
            if (foundRows.Length == 0)
            {
                customerManager.Position = savedRow; // กำหนดตำแหน่งกลับไปที่แถวเดิม
            }
            else
            {
                // ถ้าพบข้อมูล ให้เลือกแถวที่พบ (เลือกตาม Member_ID ลำดับแรกที่เจอ)
                int foundRowIndex = customerTable.Rows.IndexOf(foundRows[0]);

                // กำหนดตำแหน่งของ customerManager
                customerManager.Position = foundRowIndex;
            }
        }
    }
}
