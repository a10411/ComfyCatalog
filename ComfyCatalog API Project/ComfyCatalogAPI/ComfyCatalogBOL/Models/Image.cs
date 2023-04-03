using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfyCatalogBOL.Models
{
    public class Image
    {
        public int ImageID { get; set; }
        public string Path { get; set; }

        public Image() { }

        public Image(SqlDataReader rdr)
        {
            this.ImageID = Convert.ToInt32(rdr["imageID"]);
            this.Path = rdr["path"].ToString() ?? string.Empty;
        }
    }
}
