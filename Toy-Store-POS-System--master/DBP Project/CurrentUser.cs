using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBP_Project
{
    public static class CurrentUser
    {
        public static int MemberID { get; set; }
        public static string Role { get; set; }

        public static int EmpID { get; set; }

        public static string EmpPosition { get; set; } // เก็บตำแหน่งพนักงาน


    }
}
