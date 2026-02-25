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
    public partial class formOrder : Form
    {
        const string strFileName = "ConnectionString.ini";
        string strConnectionString;

        public formOrder()
        {
            InitializeComponent();
        }

        private void LoadOrders()
        {
            dataGridViewOrders.Rows.Clear(); // ล้างข้อมูลเดิม

            try
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();
                    string query = @"
                    SELECT o.Order_ID, o.Order_Date, m.Member_FName, 
                           SUM(d.Total_Price) AS Total_Price
                    FROM Orders o
                    JOIN Order_Detail d ON o.Order_ID = d.Order_ID
                    JOIN Member m ON o.Member_ID = m.Member_ID
                    GROUP BY o.Order_ID, o.Order_Date, m.Member_FName
                    ORDER BY o.Order_Date DESC"; // เรียงตามวันที่ใหม่สุด

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int orderId = Convert.ToInt32(reader["Order_ID"]);
                                string orderDate = Convert.ToDateTime(reader["Order_Date"]).ToString("dd/MM/yyyy HH:mm");
                                string memberName = reader["Member_FName"].ToString();
                                decimal totalPrice = Convert.ToDecimal(reader["Total_Price"]);

                                // เพิ่มข้อมูลลง DataGridView
                                dataGridViewOrders.Rows.Add(orderId, memberName, orderDate, totalPrice.ToString("C"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการโหลดข้อมูล: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void formOrder_Load(object sender, EventArgs e)
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

            // ตั้งค่าการแสดงผลใน DataGridView
            dataGridViewOrders.ColumnCount = 4;
            dataGridViewOrders.Columns[0].Name = "Order ID";
            dataGridViewOrders.Columns[1].Name = "Member Name";
            dataGridViewOrders.Columns[2].Name = "Order Date";
            dataGridViewOrders.Columns[3].Name = "Total Price";

            // เพิ่มปุ่ม Cancel
            DataGridViewButtonColumn cancelButtonColumn = new DataGridViewButtonColumn();
            cancelButtonColumn.Name = "Cancel";
            cancelButtonColumn.HeaderText = "";
            cancelButtonColumn.Text = "Cancel";
            cancelButtonColumn.UseColumnTextForButtonValue = true;
            dataGridViewOrders.Columns.Add(cancelButtonColumn);

            // เพิ่มปุ่มออกใบกำกับภาษี
            DataGridViewButtonColumn taxInvoiceButtonColumn = new DataGridViewButtonColumn();
            taxInvoiceButtonColumn.Name = "TaxInvoice";
            taxInvoiceButtonColumn.HeaderText = "";
            taxInvoiceButtonColumn.Text = "Issue tax invoice";
            taxInvoiceButtonColumn.UseColumnTextForButtonValue = true;
            dataGridViewOrders.Columns.Add(taxInvoiceButtonColumn);

            LoadOrders(); // โหลดข้อมูลออเดอร์
        }

        private void buttonBackLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void CancelOrder(int orderId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        // Step 1: Get the details of the order to know how many products to return
                        string selectQuery = @"
                        SELECT d.Product_ID, d.Quantity 
                        FROM Order_Detail d
                        WHERE d.Order_ID = @OrderID";
                        SqlCommand selectCmd = new SqlCommand(selectQuery, conn, transaction);
                        selectCmd.Parameters.AddWithValue("@OrderID", orderId);
                        SqlDataReader reader = selectCmd.ExecuteReader();

                        List<(int ProductID, int Quantity)> productsToReturn = new List<(int, int)>();
                        while (reader.Read())
                        {
                            int productId = Convert.ToInt32(reader["Product_ID"]);
                            int quantity = Convert.ToInt32(reader["Quantity"]);
                            productsToReturn.Add((productId, quantity));
                        }
                        reader.Close();

                        // Step 2: Update Product quantity
                        foreach (var product in productsToReturn)
                        {
                            string updateProductQuery = @"
                            UPDATE Product
                            SET Product_Stock = Product_Stock + @Quantity
                            WHERE Product_ID = @ProductID";
                            SqlCommand updateProductCmd = new SqlCommand(updateProductQuery, conn, transaction);
                            updateProductCmd.Parameters.AddWithValue("@ProductID", product.ProductID);
                            updateProductCmd.Parameters.AddWithValue("@Quantity", product.Quantity);
                            updateProductCmd.ExecuteNonQuery();
                        }

                        // Step 3: Delete from Order_Detail
                        string deleteOrderDetailQuery = @"
                        DELETE FROM Order_Detail
                        WHERE Order_ID = @OrderID";
                        SqlCommand deleteOrderDetailCmd = new SqlCommand(deleteOrderDetailQuery, conn, transaction);
                        deleteOrderDetailCmd.Parameters.AddWithValue("@OrderID", orderId);
                        deleteOrderDetailCmd.ExecuteNonQuery();

                        // Step 4: Delete from Orders
                        string deleteOrderQuery = @"
                        DELETE FROM Orders
                        WHERE Order_ID = @OrderID";
                        SqlCommand deleteOrderCmd = new SqlCommand(deleteOrderQuery, conn, transaction);
                        deleteOrderCmd.Parameters.AddWithValue("@OrderID", orderId);
                        deleteOrderCmd.ExecuteNonQuery();

                        // Commit the transaction
                        transaction.Commit();

                        // After successful cancellation, reload orders
                        LoadOrders();
                    }
                    catch (Exception ex)
                    {
                        // Rollback transaction if any error occurs
                        transaction.Rollback();
                        MessageBox.Show("เกิดข้อผิดพลาดในการยกเลิกออเดอร์: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการเชื่อมต่อฐานข้อมูล: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void formOrder_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void dataGridViewOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        { 
            // ตรวจสอบว่าเป็นการคลิกที่ปุ่ม Cancel หรือไม่
            if (e.ColumnIndex == dataGridViewOrders.Columns["Cancel"].Index && e.RowIndex >= 0)
            {
                int orderId = Convert.ToInt32(dataGridViewOrders.Rows[e.RowIndex].Cells["Order ID"].Value);
                CancelOrder(orderId); // ยกเลิกออเดอร์
            }
            // ตรวจสอบว่าเป็นการคลิกที่ปุ่มออกใบกำกับภาษีหรือไม่
            if (e.ColumnIndex == dataGridViewOrders.Columns["TaxInvoice"].Index && e.RowIndex >= 0)
            {
                int orderId = Convert.ToInt32(dataGridViewOrders.Rows[e.RowIndex].Cells["Order ID"].Value);

                // สร้างฟอร์มใหม่สำหรับออกใบกำกับภาษี
                formTaxinvoice formInvoice = new formTaxinvoice(orderId);
                formInvoice.Show();  // เปิดฟอร์มใหม่
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string memberName = textBoxSearch.Text.Trim(); // รับค่าจาก TextBox
            if (string.IsNullOrEmpty(memberName))
            {
                MessageBox.Show("กรุณากรอกชื่อผู้ซื้อเพื่อทำการค้นหา", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SearchOrdersByMemberName(memberName); // เรียกใช้ฟังก์ชันค้นหาผู้ซื้อ
        }
        private void SearchOrdersByMemberName(string memberName)
        {
            dataGridViewOrders.Rows.Clear(); // ล้างข้อมูลเดิม

            try
            {
                using (SqlConnection conn = new SqlConnection(strConnectionString))
                {
                    conn.Open();
                    string query = @"
                    SELECT o.Order_ID, o.Order_Date, m.Member_FName, 
                           SUM(d.Total_Price) AS Total_Price
                    FROM Orders o
                    JOIN Order_Detail d ON o.Order_ID = d.Order_ID
                    JOIN Member m ON o.Member_ID = m.Member_ID
                    WHERE m.Member_FName LIKE @MemberName
                    GROUP BY o.Order_ID, o.Order_Date, m.Member_FName
                    ORDER BY o.Order_Date DESC"; // เรียงตามวันที่ใหม่สุด

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberName", "%" + memberName + "%"); // ใช้ % เพื่อค้นหาชื่อที่มีคีย์เวิร์ด

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int orderId = Convert.ToInt32(reader["Order_ID"]);
                                string orderDate = Convert.ToDateTime(reader["Order_Date"]).ToString("dd/MM/yyyy HH:mm");
                                string memberNameResult = reader["Member_FName"].ToString();
                                decimal totalPrice = Convert.ToDecimal(reader["Total_Price"]);

                                // เพิ่มข้อมูลลง DataGridView
                                dataGridViewOrders.Rows.Add(orderId, memberNameResult, orderDate, totalPrice.ToString("C"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการโหลดข้อมูล: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadOrders();
        }
    }
}
