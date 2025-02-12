using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace BasicOnlineShoppingService.Modules.Report;

[ApiController]
[Route("api/v1/report-module")]
internal class DummyController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult Get() => Ok(Assembly.GetExecutingAssembly().FullName);
}