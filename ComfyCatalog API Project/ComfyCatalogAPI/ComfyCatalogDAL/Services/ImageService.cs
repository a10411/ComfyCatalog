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
        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e obter todos os registos de Imagens lá criados (tabela Observation)
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "ComfyCatalogAPI", no ficheiro appsettings.json</param>
        /// <returns>Lista de Imagens</returns>

        #region GET
        public static async Task<List<Image>> GetAllImages(string conString)
        {
            var imgList = new List<Image>();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [Image]", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Image img = new Image(rdr);
                    imgList.Add(img);
                }
                rdr.Close();
                con.Close();
            }
            return imgList;
        }

       public static async Task<Image> GetImageByID(string conString, int imageID)
        {
            Image img = new Image();
            using(SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM [Image] WHERE imageID = {imageID}", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    img = new Image();
                }
                rdr.Close();
                con.Close();
            }
            return img;
            // retorna uma imagem com id = 0 caso não encontre nenhum com este ID
        }



        #endregion


        #region POST
        public static async Task<Boolean> AddImage(string conString, Image imageToAdd)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string addImage = "INSERT INTO [Image] ( imageData) VALUES ( @imageData)";
                    using (SqlCommand queryAddImage = new SqlCommand(addImage))
                    {
                        queryAddImage.Connection = con;
                        queryAddImage.Parameters.Add("@imageData", SqlDbType.VarBinary).Value = imageToAdd.ImageData;
                        con.Open();
                        queryAddImage.ExecuteNonQuery();
                        con.Close();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Método que visa aceder à base de dados via SQL Query e adicionar um novo registo na tabela Product_Image consoante o ID de produto e imagem 
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "ComfyCatalogAPI", no ficheiro appsettings.json</param>
        /// <param name="imageID">Id da imagem a associar ao producto</param>
        /// <param name="productID">Id do producto associar à Imagem</param>
        /// <returns>True se adicionar, False se não adicionar</returns>
        public static async Task<Boolean> SetImageToProduct(string conString, int imageID, int productID)
        {
            try
            {
                Product product = await ProductService.GetProduct(conString, productID);
                Image image = await GetImageByID(conString, productID);

                using(SqlConnection con = new SqlConnection(conString))
                {
                    string addProduct_Image = "INSERT INTO Product_Image(imageID, productID) VALUES (@imageID , @productID)";
                    using (SqlCommand queryAddProduct_Image = new SqlCommand(addProduct_Image))
                    {
                        queryAddProduct_Image.Connection = con;
                        queryAddProduct_Image.Parameters.Add("@imageID", SqlDbType.Int).Value = imageID;
                        queryAddProduct_Image.Parameters.Add("@productID", SqlDbType.Int).Value =productID;

                        con.Open();
                        queryAddProduct_Image.ExecuteNonQuery();
                        con.Close();
                        return true;
                    }
                }
            }
            catch
            {
                throw;
            }
        }



        #endregion


        #region PATCH/PUT



        #endregion


        #region DELETE
        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e apagar uma imagem existente na mesma
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "ComfyCatalogAPI", no ficheiro appsettings.json</param>
        /// <param name="obsID">ID da imagem a apagar</param>
        /// <returns>True caso tudo tenha corrido bem (imagem removida), algum erro caso a imagem não tenha sido removida.</returns>
        public static async Task<Boolean> DeleteImage(string conString, int imageID)
        {
            try
            {
                using(SqlConnection con = new SqlConnection(conString))
                {
                    string deleteImage = $"DELETE FROM [Image] WHERE imageID = {imageID}";
                    using(SqlCommand queryDeleteImage = new SqlCommand(deleteImage))
                    {
                        queryDeleteImage.Connection = con;
                        con.Open();
                        queryDeleteImage.ExecuteNonQuery ();
                        con.Close();
                        return true;
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        #endregion


    }
}
