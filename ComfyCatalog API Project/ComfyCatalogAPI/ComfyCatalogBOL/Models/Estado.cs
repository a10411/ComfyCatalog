using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfyCatalogBOL.Models
{
    public class Estado
    {
        public int EstadoID { get; set; }

        public string EstadoName { get; set; }

        public Estado() { }

        public Estado(SqlDataReader rdr) 
        {
            this.EstadoID = Convert.ToInt32(rdr["estadoID"]);
            this.EstadoName = rdr["estado"].ToString() ?? String.Empty;

        }
    }
}
