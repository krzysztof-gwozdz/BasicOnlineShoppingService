using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace BasicOnlineShoppingService.App.Api;

internal class InternalControllerFeatureProvider : ControllerFeatureProvider
{
    protected override bool IsController(TypeInfo typeInfo) => 
        !typeInfo.IsAbstract && typeof(ControllerBase).IsAssignableFrom(typeInfo) || base.IsController(typeInfo);
}