using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfyCatalogBOL.Models
{
    public class Brand
    {
        public int BrandID { get; set; }
        public string BrandName { get; set; }

        public Brand() { }

        public Brand(SqlDataReader rdr) 
        {
            this.BrandID = Convert.ToInt32(rdr["brandID"]);
            this.BrandName = rdr["name"].ToString() ?? string.Empty;
        }
    }
}
