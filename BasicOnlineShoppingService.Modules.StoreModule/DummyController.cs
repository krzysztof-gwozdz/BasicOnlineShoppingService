using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace BasicOnlineShoppingService.Modules.StoreModule;

[ApiController]
[Route("api/v1/store-module")]
internal class DummyController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult Get() => Ok(Assembly.GetExecutingAssembly().FullName);
}