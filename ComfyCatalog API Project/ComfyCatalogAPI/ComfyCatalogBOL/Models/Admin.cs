using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfyCatalogBOL.Models
{
    public class Admin
    {
        public int AdminID { get; set; }

        public string Username { get; set; }

        public string Password_Hash { get; set; }

        public string Password_Salt { get; set; }

        public Admin() { }

        public Admin(SqlDataReader rdr)
        {
            this.AdminID = Convert.ToInt32(rdr["adminID"]);
            this.Username = rdr["username"].ToString() ?? String.Empty;
            this.Password_Hash = rdr["password_hash"].ToString() ?? String.Empty;
            this.Password_Salt = rdr["password_salt"].ToString() ?? String.Empty;
        }
    }
    
}
