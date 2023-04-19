using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfyCatalogBOL.Models
{
    /// <summary>
    /// Business Object Layer relativa a uma Imagem
    /// Implementa a class (ou modelo) Image e os seus construtores
    /// </summary>
    public class Image
    {
        public int ImageID { get; set; }
        public string PhotoFileName { get; set; }
        public string PhotoPath { get; set; }
        public IFormFile Photo { get; set; }


        public Image() { }

        /// <summary>
        /// Construtor que visa criar uma Image convertendo os dados obtidos a partir de um SqlDataReader
        /// Construtor presente na layer DAL, que recebe dados para converter num objecto.
        /// </summary>
        /// <param name="rdr">SqlDataReader</param>
        public Image(SqlDataReader rdr)
        {
            this.ImageID = Convert.ToInt32(rdr["imageID"]);
            this.PhotoFileName = rdr["photoFileName"].ToString() ?? string.Empty;
            this.PhotoPath = rdr["photoPath"].ToString() ?? string.Empty;
            this.Photo = null;
        }
    }
}
