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
using System.Drawing.Printing;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace DBP_Project
{
    public partial class formTaxinvoice : Form
    {
        const string strFileName = "ConnectionString.ini";
        string strConnectionString;
        private int _orderId;
        public formTaxinvoice(int orderId)
        {
            InitializeComponent();
            _orderId = orderId; // เก็บ OrderID ที่ได้รับมา

        }

        private void formTaxinvoice_Load(object sender, EventArgs e)
        {
            if (File.Exists(strFileName))
            {
                strConnectionString = File.ReadAllText(strFileName).Trim();
            }
            else
            {
                MessageBox.Show("ไม่พบไฟล์ ConnectionString.ini", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(strConnectionString))
            {
                MessageBox.Show("Connection String ว่างเปล่า", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LoadSellerInfo();
            LoadCustomerInfo();
            LoadInvoiceDetails();
            LoadInvoiceItems();
        }
        private void LoadSellerInfo()
        {
            lblCompanyName.Text = "บริษัท Toy Store 888 จำกัด";
            lblComapanyAddress.Text = "ถนน วงศ์สว่าง 11 แขวง วงศ์สว่าง เขต บางซื่อ \nกรุงเทพฯ 10800";
            lblCompanyTaxID.Text = "TAX629017768";
        }
        private void LoadCustomerInfo()
        {
            using (SqlConnection conn = new SqlConnection(strConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT c.Member_FName + ' ' + c.Member_LName AS CustomerName, 
                               b.Tax_ID, b.Address, b.Subdistrict, b.District, b.Province, b.Postal_Code, b.Phone
                        FROM Orders o
                        JOIN Member c ON o.Member_ID = c.Member_ID
                        LEFT JOIN Billing_Address b ON c.Member_ID = b.Member_ID
                        WHERE o.Order_ID = @OrderID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@OrderID", _orderId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        lblCustomerName.Text = reader["CustomerName"].ToString();
                        lblCustomerAddress.Text = $"{reader["Address"]}, {reader["Subdistrict"]}, \n{reader["District"]}, {reader["Province"]}, {reader["Postal_Code"]}";
                        lblCustomerID.Text = reader["Tax_ID"].ToString();
                        lblCustomerInvoiceNo.Text = reader["Phone"].ToString();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการโหลดข้อมูลลูกค้า: " + ex.Message);
                }
            }
        }

        private void LoadInvoiceDetails()
        {
            using (SqlConnection conn = new SqlConnection(strConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT Order_ID, Order_Date
                        FROM Orders WHERE Order_ID = @OrderID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@OrderID", _orderId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        lblCustomerInvoiceNo.Text = $"INV{DateTime.Now:yyyyMMdd}-{reader["Order_ID"]:D4}";
                        lblInvoiceDate.Text = Convert.ToDateTime(reader["Order_Date"]).ToString("dd/MM/yyyy");
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการโหลดข้อมูลใบกำกับภาษี: " + ex.Message);
                }
            }
        }

        private void LoadInvoiceItems()
        {
            using (SqlConnection conn = new SqlConnection(strConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT p.Product_Name, od.Quantity, p.Product_Price AS Unit_Price, 
                               (od.Quantity * p.Product_Price) AS TotalPrice
                        FROM Order_Detail od
                        JOIN Product p ON od.Product_ID = p.Product_ID
                        WHERE od.Order_ID = @OrderID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@OrderID", _orderId);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dt.Columns.Add("No", typeof(int));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["No"] = i + 1;
                    }

                    dt.Columns["No"].SetOrdinal(0);
                    dgvInvoiceItems.DataSource = dt;
                    CalculateTotal();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการโหลดรายการสินค้า: " + ex.Message);
                }
            }
        }

        private void CalculateTotal()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvInvoiceItems.Rows)
            {
                if (row.Cells["TotalPrice"].Value != null)
                {
                    total += Convert.ToDecimal(row.Cells["TotalPrice"].Value);
                }
            }

            lblTotalAmount.Text = total.ToString("N2");
            decimal vat = total * 0.07m;
            lblVatAmount.Text = vat.ToString("N2");
            lblSubTotal.Text = (total - vat).ToString("N2");
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(PrintInvoice);

            PrintPreviewDialog previewDialog = new PrintPreviewDialog();
            previewDialog.Document = printDocument;

            if (previewDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }
        private void PrintInvoice(object sender, PrintPageEventArgs e)
        {
            // กำหนดฟอนต์
            System.Drawing.Font titleFont = new System.Drawing.Font("Arial", 18, System.Drawing.FontStyle.Bold);
            System.Drawing.Font headerFont = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
            System.Drawing.Font contentFont = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Regular);

            // ตำแหน่งเริ่มต้นของการพิมพ์
            float x = 50;
            float y = 50;
            float lineSpacing = 40; // เพิ่มระยะห่างมากขึ้น

            // พิมพ์หัวข้อ
            e.Graphics.DrawString("ใบกำกับภาษี (Tax Invoice)", titleFont, Brushes.Black, x + 200, y);
            y += lineSpacing * 2; // เพิ่มระยะห่าง

            // พิมพ์ข้อมูลบริษัท
            e.Graphics.DrawString("Company Name:", headerFont, Brushes.Black, x, y);
            e.Graphics.DrawString(lblCompanyName.Text, contentFont, Brushes.Black, x + 180, y);
            y += lineSpacing;

            e.Graphics.DrawString("Company Address:", headerFont, Brushes.Black, x, y);
            e.Graphics.DrawString(lblComapanyAddress.Text, contentFont, Brushes.Black, x + 180, y);
            y += lineSpacing;

            e.Graphics.DrawString("Tax ID:", headerFont, Brushes.Black, x, y);
            e.Graphics.DrawString(lblCompanyTaxID.Text, contentFont, Brushes.Black, x + 180, y);
            y += lineSpacing * 2;

            // พิมพ์ข้อมูลลูกค้า
            e.Graphics.DrawString("Customer Name:", headerFont, Brushes.Black, x, y);
            e.Graphics.DrawString(lblCustomerName.Text, contentFont, Brushes.Black, x + 180, y);
            y += lineSpacing;

            e.Graphics.DrawString("Customer Address:", headerFont, Brushes.Black, x, y);
            e.Graphics.DrawString(lblCustomerAddress.Text, contentFont, Brushes.Black, x + 180, y);
            y += lineSpacing;

            e.Graphics.DrawString("Tax ID:", headerFont, Brushes.Black, x, y);
            e.Graphics.DrawString(lblCustomerID.Text, contentFont, Brushes.Black, x + 180, y);
            y += lineSpacing;

            e.Graphics.DrawString("Invoice No. :", headerFont, Brushes.Black, x, y);
            e.Graphics.DrawString(lblCustomerInvoiceNo.Text, contentFont, Brushes.Black, x + 180, y);
            y += lineSpacing * 2;

            // พิมพ์ตารางรายการสินค้า
            float tableX = x;
            float tableY = y;

            string[] headers = { "No", "Product Name", "Quantity", "Unit Price", "Total Price" };
            int[] colWidths = { 50, 250, 80, 100, 100 };

            // วาดเส้นหัวตาราง
            float currentX = tableX;
            foreach (var header in headers)
            {
                e.Graphics.DrawRectangle(Pens.Black, currentX, tableY, colWidths[Array.IndexOf(headers, header)], lineSpacing);
                e.Graphics.DrawString(header, headerFont, Brushes.Black, currentX + 5, tableY + 5);
                currentX += colWidths[Array.IndexOf(headers, header)];
            }
            tableY += lineSpacing;

            // วาดข้อมูลตาราง
            foreach (DataGridViewRow row in dgvInvoiceItems.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    currentX = tableX;
                    for (int i = 0; i < headers.Length; i++)
                    {
                        e.Graphics.DrawRectangle(Pens.Black, currentX, tableY, colWidths[i], lineSpacing);
                        e.Graphics.DrawString(row.Cells[i].Value.ToString(), contentFont, Brushes.Black, currentX + 5, tableY + 5);
                        currentX += colWidths[i];
                    }
                    tableY += lineSpacing;
                }
            }

            y = tableY + lineSpacing * 3; // เพิ่มระยะห่างจากตาราง

            // พิมพ์ยอดรวม
            e.Graphics.DrawString("Subtotal:", headerFont, Brushes.Black, x + 350, y);
            e.Graphics.DrawString(lblSubTotal.Text, contentFont, Brushes.Black, x + 480, y);
            y += lineSpacing;

            e.Graphics.DrawString("Vat Amount:", headerFont, Brushes.Black, x + 350, y);
            e.Graphics.DrawString(lblVatAmount.Text, contentFont, Brushes.Black, x + 480, y);
            y += lineSpacing;

            e.Graphics.DrawString("Total Amount:", headerFont, Brushes.Black, x + 350, y);
            e.Graphics.DrawString(lblTotalAmount.Text, contentFont, Brushes.Black, x + 480, y);
            y += lineSpacing * 2;

            // พิมพ์ช่องลงนาม
            e.Graphics.DrawString("Authorized Sign: ____________________", headerFont, Brushes.Black, x, y);
        }


        private void buttonBackLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void formTaxinvoice_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
        }
    }

}