using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ComfyCatalogBLL.Logic;
using ComfyCatalogBLL.Utils;
using ComfyCatalogBOL.Models;
using ComfyCatalogDAL;
using Microsoft.AspNetCore.Authorization;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;



namespace ComfyCatalogAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class EstadoController : Controller
    {
        private readonly IConfiguration _configuration;
        
        public EstadoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


    }
}
