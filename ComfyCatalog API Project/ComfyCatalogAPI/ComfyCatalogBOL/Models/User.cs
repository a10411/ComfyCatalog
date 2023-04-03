using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ComfyCatalogBOL.Models
{
    public class User
    {
        public int UserID { get; set; }

        public string Username { get; set; }

        public string Password_Hash { get; set; }

        public string Password_Salt { get; set; }

        public User()
        {

        }

        public User(SqlDataReader rdr)
        {
            this.UserID = Convert.ToInt32(rdr["UserID"]);
            this.Username = rdr["username"].ToString() ?? String.Empty;
            this.Password_Hash = rdr["password_hash"].ToString() ?? String.Empty;
            this.Password_Salt = rdr["password_salt"].ToString() ?? String.Empty;
        }


    }



}
