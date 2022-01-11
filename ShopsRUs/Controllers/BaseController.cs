using Microsoft.AspNetCore.Mvc;
using ShopsRUsApi.ActionDecorators;

namespace ShopsRUs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(ExceptionDecoratorFilter))]
    public class BaseController : ControllerBase
    {
    }
}