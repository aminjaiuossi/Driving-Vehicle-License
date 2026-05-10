using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer;

namespace Business_Logic_Layer
{
    public class clsUserinfo
    {
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public static clsUserinfo Find(string userName, string password)
        {
            DataTable dt = dalUsers.GetUser(userName, password);

            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];
            return new clsUserinfo
            {
                UserID = Convert.ToInt32(row["UserID"]),
                PersonID = Convert.ToInt32(row["PersonID"]),
                UserName = row["UserName"].ToString(),
                Password = row["Password"].ToString(),
                IsActive = Convert.ToBoolean(row["IsActive"])
            };
        }
    }
}
