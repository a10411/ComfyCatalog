using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ComfyCatalogBLL.Logic;
using ComfyCatalogBLL.Utils;
using ComfyCatalogBOL.Models;
using ComfyCatalogDAL;
using Microsoft.AspNetCore.Authorization;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;
using System.IO;
using System.Linq;
using System.Data.SqlClient;
using System.Data;

namespace ComfyCatalogAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : Controller
    {
        /// <summary>
        /// Construtor e variável que visam permitir a obtenção da connectionString da base de dados, que reside no appsettings.json
        /// </summary>
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public ImageController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        /// <summary>
        /// Request GET relativo às Imagens
        /// </summary>
        /// <returns>Retorna a response obtida pelo BLL para o utilizador. Idealmente, retornará a lista de Imagens</returns> 
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await ImageLogic.GetAllImages(CS);
            if(response.StatusCode != ComfyCatalogBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);
        }



        /// <summary>
        /// Request POST relativo às imagens
        /// </summary>
        /// <returns>Retorna a response obtida pelo BLL para o utilizador.</returns> 
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [Route("/SaveFile")]
        [HttpPost] 
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Product_Images/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {
                return new JsonResult("SockAvatar.png");
            }
        }

        /// <summary>
        /// Request POST relativo às imagens
        /// </summary>
        /// <returns>Retorna a response obtida pelo BLL para o utilizador.</returns> 
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        //[Route("/AddImage")]
        [HttpPost]
        public async Task<IActionResult> AddImageAndAssociateWithProduct(Image imageToAdd, int productId)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await ImageLogic.AddImageAndAssociateWithProduct(CS, imageToAdd, productId);
            if (response.StatusCode != ComfyCatalogBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);
        }



        /*
        public async Task<IActionResult> AddImage(Image imageToAdd)
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");
            Response response = await ImageLogic.AddImage(CS, imageToAdd);
            if (response.StatusCode != ComfyCatalogBLL.Utils.StatusCodes.SUCCESS)
            {
                return StatusCode((int)response.StatusCode);
            }
            return new JsonResult(response);
        }
        */

        /*

        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, Description = "No content was found.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Description = "Api key authentication was not provided or it is not valid.")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, Description = "You do not have permissions to perform the operation.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.")]
        [Route("/AddPhoto")]
        [HttpPost]
        public JsonResult AddPhoto()
        {
            string CS = _configuration.GetConnectionString("WebApiDatabase");

            try
            {
                string fileName = SaveFile().ToString() ?? string.Empty;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    string addPhotoToProduct = $"INSERT INTO Image (photoFileName) VALUES (@photoFileName)";
                    using (SqlCommand queryAddPhotoToProduct = new SqlCommand(addPhotoToProduct))
                    {
                        queryAddPhotoToProduct.Connection = con;
                        queryAddPhotoToProduct.Parameters.Add("@photoFileName", SqlDbType.Char).Value = fileName;
                        
                        con.Open();
                        queryAddPhotoToProduct.ExecuteNonQuery();
                        con.Close();
                        return new JsonResult("photo successfuly added");
                    }

                }

            }
            catch (Exception)
            {
                return new JsonResult("Error associating photo to product!");
            }
        }

        */

    }
}
