using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBP_Project
{
    internal class ClassProduct
    {
        public int ProdID { get; set; }
        public string ProdName { get; set; }

        public string ProdDescript { get; set; }
        public decimal ProdPrice { get; set; }
        public int ProdStock { get; set; }
        public int CateID { get; set; }
        public int CostPrice { get; set; }
        public string ProdStatus { get; set; }
        public byte[] Product_Image { get; set; } // เก็บรูปเป็น Binary Data
                                                  // 
    }
}
