using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComfyCatalogBLL.Utils;
using ComfyCatalogBOL.Models;
using ComfyCatalogDAL;
using ComfyCatalogDAL.Services;

namespace ComfyCatalogBLL.Logic
{
    /// <summary>
    /// Esta classe implementa todas as funções que, por sua vez, implementam a parte lógica de cada request relativo às Imagens
    /// Nesta classe, abstraímo-nos de rotas, autorizações, links, etc. que dizem respeito à API
    /// Porém, a API consome esta classe no sentido em que esta é responsável por transformar objetos vindos do DAL em responses.
    /// Esta classe é a última a lidar com objetos (models) e visa abstrair a API dos mesmos
    /// Gera uma response com um status code e dados
    /// </summary>
    public class ImageLogic
    {
        public static async Task<Response> GetAllImages(string conString)
        {
            Response response = new Response();
            List<Image> imageList = await ImageService.GetAllImages(conString);
            if (imageList.Count != 0)
            {
                response.StatusCode = StatusCodes.SUCCESS;
                response.Message = "Sucesso na obtenção dos dados";
                response.Data = imageList;
            }
            return response;
        }

        /*
        public static async Task<Response> AddImage(string conString, Image imageToAdd)
        {
            Response response = new Response();
            try
            {
                if(await ImageService.AddImage(conString, imageToAdd) != 0)
                {
                    response.StatusCode = StatusCodes.SUCCESS;
                    response.Message = "Image was added to Catalog";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.INTERNALSERVERERROR;
                response.Message = ex.ToString();
            }
            return response;
        }
        */

        public static async Task<Response> AddImageAndAssociateWithProduct(string conString, Image imageToAdd, int productId)
        {
            Response response = new Response();
            try
            {
                int imageId = await ImageService.AddImage(conString, imageToAdd);
                await ImageService.AddProductImage(conString, productId, imageId);
                response.StatusCode = StatusCodes.SUCCESS;
                response.Message = "Image was added to Catalog and associated with product";
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.INTERNALSERVERERROR;
                response.Message = ex.ToString();
            }
            return response;
        }

    }
}
