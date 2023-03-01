using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace StringToBase64.API.Controllers
{
    [EnableCors("AllowOrigin")]
    public class BaseController<T> : ControllerBase where T : BaseController<T>
    {
        //private ILogger<T> _logger;
        //protected ILogger<T> Logger => _logger ?? (_logger = HttpContext.RequestServices.GetService<ILogger<T>>());
    }
}
  