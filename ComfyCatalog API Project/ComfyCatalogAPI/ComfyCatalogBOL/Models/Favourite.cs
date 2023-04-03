using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfyCatalogBOL.Models
{
    public class Favourite
    {
        public int FavouriteID  { get; set; }

        public int UserID { get; set; } 

        public int ProductID { get; set; }

        public Favourite() { }

        public Favourite(SqlDataReader rdr)
        {
            this.FavouriteID = Convert.ToInt32(rdr["favouriteID"]);
            this.UserID = Convert.ToInt32(rdr["userID"]);
            this.ProductID = Convert.ToInt32(rdr["productID"]);
        }


    }
}
