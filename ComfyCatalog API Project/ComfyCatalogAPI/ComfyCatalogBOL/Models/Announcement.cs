using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfyCatalogBOL.Models
{
    public class Announcement
    {
        public int AnnouncementID { get; set; }

        public int ProductID { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime Date_Hour { get; set; }


        public Announcement() { }

        public Announcement(int announcementID, int productID, string title, string body, DateTime date_Hour)
        {
            AnnouncementID = announcementID;
            ProductID = productID;
            Title = title;
            Body = body;
            Date_Hour = date_Hour;
        }

        public Announcement(SqlDataReader rdr)
        {
            this.AnnouncementID = Convert.ToInt32(rdr["obsID"]);
            this.ProductID = Convert.ToInt32(rdr["productID"]);
            this.Title = rdr["title"].ToString() ?? String.Empty;
            this.Body = rdr["body"].ToString() ?? String.Empty;
            this.Date_Hour = Convert.ToDateTime(rdr["date_hour"].ToString());
        }
    }
}
