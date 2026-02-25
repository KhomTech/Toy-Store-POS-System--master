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

namespace DBP_Project
{
    public partial class formBillingAddress : Form
    {
        private int _memberId;  // ตัวแปรเก็บค่า Member_ID
        const string strFileName = "ConnectionString.ini";
        private string strConnectionString = "";
        SqlConnection dbConnection;
        SqlCommand addressCommand;
        SqlDataAdapter addressAdapter;
        DataTable addressTable;
        CurrencyManager addressManager;
        string currentState;
        public formBillingAddress(int memberId)
        {
            InitializeComponent();
            _memberId = memberId;
        }

        private void formBillingAddress_Load(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(strFileName))
                    strConnectionString = File.ReadAllText(strFileName, Encoding.GetEncoding("Windows-874"));

                if (string.IsNullOrEmpty(strConnectionString))
                {
                    MessageBox.Show("ไม่สามารถอ่านค่า Connection String ได้", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (SqlConnection dbConnection = new SqlConnection(strConnectionString))
                {
                    dbConnection.Open();

                    // ดึงข้อมูลที่อยู่สำหรับการออกใบกำกับภาษี
                    string query = "SELECT * FROM Billing_Address WHERE Member_ID = @Member_ID";
                    SqlCommand addressCommand = new SqlCommand(query, dbConnection);
                    addressCommand.Parameters.AddWithValue("@Member_ID", _memberId);

                    SqlDataAdapter addressAdapter = new SqlDataAdapter(addressCommand);
                    addressTable = new DataTable();
                    addressAdapter.Fill(addressTable);

                    if (addressTable.Rows.Count > 0)
                    {
                        // มีข้อมูลที่อยู่เดิม ให้แสดงใน TextBox
                        DataRow row = addressTable.Rows[0];
                        txtTaxID.Text = row["Tax_ID"].ToString();
                        txtAddress.Text = row["Address"].ToString();
                        txtSubdistrict.Text = row["Subdistrict"].ToString();
                        txtDistrict.Text = row["District"].ToString();
                        txtProvince.Text = row["Province"].ToString();
                        txtPostalCode.Text = row["Postal_Code"].ToString();
                        txtPhone.Text = row["Phone"].ToString();
                    }
                    else
                    {
                        //  กรณีลูกค้ายังไม่มีที่อยู่ ให้ขึ้นข้อความแจ้งเตือน
                        MessageBox.Show("ลูกค้ารายนี้ยังไม่มีที่อยู่สำหรับออกใบกำกับภาษี กรุณากรอกข้อมูลใหม่", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                SetState("View");
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();

                    if (addressTable.Rows.Count > 0)
                    {
                        // ✅ กรณีมีข้อมูลอยู่แล้ว: ทำการอัปเดต
                        string updateQuery = "UPDATE Billing_Address SET  Tax_ID = @TaxID, Address = @Address, " +
                                             "Subdistrict = @Subdistrict, District = @District, Province = @Province, " +
                                             "Postal_Code = @PostalCode, Phone = @Phone WHERE Member_ID = @Member_ID";

                        SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                        updateCmd.Parameters.AddWithValue("@TaxID", txtTaxID.Text);
                        updateCmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                        updateCmd.Parameters.AddWithValue("@Subdistrict", txtSubdistrict.Text);
                        updateCmd.Parameters.AddWithValue("@District", txtDistrict.Text);
                        updateCmd.Parameters.AddWithValue("@Province", txtProvince.Text);
                        updateCmd.Parameters.AddWithValue("@PostalCode", txtPostalCode.Text);
                        updateCmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                        updateCmd.Parameters.AddWithValue("@Member_ID", _memberId);

                        updateCmd.ExecuteNonQuery();
                    }
                    else
                    {
                        // ✅ กรณีไม่มีข้อมูล: ทำการเพิ่มที่อยู่ใหม่
                        string insertQuery = "INSERT INTO Billing_Address (Member_ID, Tax_ID, Address, Subdistrict, District, Province, Postal_Code, Phone) " +
                                             "VALUES (@Member_ID, @TaxID, @Address, @Subdistrict, @District, @Province, @PostalCode, @Phone)";

                        SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                        insertCmd.Parameters.AddWithValue("@Member_ID", _memberId);
                        insertCmd.Parameters.AddWithValue("@TaxID", txtTaxID.Text);
                        insertCmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                        insertCmd.Parameters.AddWithValue("@Subdistrict", txtSubdistrict.Text);
                        insertCmd.Parameters.AddWithValue("@District", txtDistrict.Text);
                        insertCmd.Parameters.AddWithValue("@Province", txtProvince.Text);
                        insertCmd.Parameters.AddWithValue("@PostalCode", txtPostalCode.Text);
                        insertCmd.Parameters.AddWithValue("@Phone", txtPhone.Text);

                        insertCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("บันทึกข้อมูลที่อยู่เรียบร้อยแล้ว!", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    SetState("View");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("คุณแน่ใจหรือไม่ว่าต้องการลบข้อมูลนี้?", "ลบข้อมูล", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM Billing_Address WHERE Member_ID = @Member_ID";
                    SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn);
                    deleteCmd.Parameters.AddWithValue("@Member_ID", _memberId);
                    deleteCmd.ExecuteNonQuery();

                    MessageBox.Show("ลบข้อมูลที่อยู่เรียบร้อยแล้ว!", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void SetState(string state)
        {
            currentState = state;
            switch (state)
            {
                case "View":

                    txtAddress.ReadOnly = true;
                    txtSubdistrict.ReadOnly = true;
                    txtDistrict.ReadOnly = true;
                    txtProvince.ReadOnly = true;
                    txtPostalCode.ReadOnly = true;
                    txtPhone.ReadOnly = true;
                    txtTaxID.ReadOnly = true;
                    buttonEdit.Enabled = true;
                    buttonDelete.Enabled = true;
                    buttonSave.Enabled = false;
                    buttonCancel.Enabled = false;
                    buttonDone.Enabled = true;
                    break;

                default:
                    txtAddress.ReadOnly = false;
                    txtSubdistrict.ReadOnly = false;
                    txtDistrict.ReadOnly = false;
                    txtProvince.ReadOnly = false;
                    txtPostalCode.ReadOnly = false;
                    txtPhone.ReadOnly = false;
                    txtTaxID.ReadOnly= false;
                    buttonEdit.Enabled = false;
                    buttonDelete.Enabled = false;
                    buttonSave.Enabled = true;
                    buttonCancel.Enabled = true;
                    buttonDone.Enabled = false;
                    break;
            }
        }
        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            SetState("Edit");
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            addressManager.CancelCurrentEdit();
            SetState("View");
        }
        private void buttonBackMCS_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void formBillingAddress_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
