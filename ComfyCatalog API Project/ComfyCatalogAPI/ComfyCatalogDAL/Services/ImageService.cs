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

        public static async Task<int> GetLastImageId(string conString)
        {
            int lastID = 0;
            using (SqlConnection con = new SqlConnection(conString))
            {
                string getLastImageId = "SELECT TOP 1 imageID FROM dbo.[Image] ORDER BY imageID DESC";
                using (SqlCommand cmd = new SqlCommand(getLastImageId))
                {
                    cmd.Connection = con;
                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Image id = new Image(rdr);
                        lastID = id.ImageID;
                    }
                    rdr.Close();
                    con.Close();
                }
                return lastID;
            }
        }

        #endregion


        #region POST
        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e adicionar um registo de uma imagem a um produto
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "ComfyCatalogAPI", no ficheiro appsettings.json</param>
        /// <returns>True caso tenha adicionado ou retorna a exceção para a camada lógica caso tenha havido algum erro</returns>
        public static async Task<int> AddImage(string conString, Image imageToAdd)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string addImage = "INSERT INTO dbo.[Image] (photoFileName, photoPath) VALUES (@photoFileName, @photoPath); SELECT SCOPE_IDENTITY();";
                    using (SqlCommand queryAddImage = new SqlCommand(addImage))
                    {
                        queryAddImage.Connection = con;
                        queryAddImage.Parameters.Add("@photoFileName", SqlDbType.NVarChar).Value = imageToAdd.PhotoFileName;
                        queryAddImage.Parameters.Add("@photoPath", SqlDbType.NVarChar).Value = imageToAdd.PhotoPath;
                        con.Open();
                        var imageId = await queryAddImage.ExecuteScalarAsync();
                        con.Close();
                        return Convert.ToInt32(imageId);

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static async Task AddProductImage(string conString, int productId, int imageId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string addProductImage = "INSERT INTO dbo.[Product_Image] (productID, imageID) VALUES (@productId, @imageId)";
                    using (SqlCommand queryAddProductImage = new SqlCommand(addProductImage))
                    {
                        queryAddProductImage.Connection = con;
                        queryAddProductImage.Parameters.Add("@productId", SqlDbType.Int).Value = productId;
                        queryAddProductImage.Parameters.Add("@imageId", SqlDbType.Int).Value = imageId;
                        con.Open();
                        await queryAddProductImage.ExecuteNonQueryAsync();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        #endregion


        #region PATCH/PUT



        #endregion


        #region DELETE



        #endregion


    }
}
