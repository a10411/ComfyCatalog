using ComfyCatalogBOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace ComfyCatalogDAL.Services
{
    /// <summary>
    /// Class que visa implementar todos os métodos de Data Access Layer referentes às Imagens
    /// Isto é, todos os acessos à base de dados relativos às Imagens estarão implementados em funções implementadas dentro desta classe
    /// </summary>
    public class ImageService
    {


        #region GET
        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e obter todos os registos de Imagens lá criados (tabela Image)
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "ComfyCatalogAPI", no ficheiro appsettings.json</param>
        /// <returns>Lista de Imagenss</returns>
        public static async Task<List<Image>> GetAllImages(string conString)
        {
            var imageList = new List<Image>();
            using(SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.[Image]", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Image image = new Image(rdr);
                    imageList.Add(image);
                }
                rdr.Close();
                con.Close();
            }
            return imageList;
        }

        #endregion


        #region POST



        #endregion


        #region PATCH/PUT



        #endregion


        #region DELETE



        #endregion


    }
}
