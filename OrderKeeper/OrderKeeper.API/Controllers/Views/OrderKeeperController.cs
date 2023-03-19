using Microsoft.AspNetCore.Mvc;

namespace OrderKeeper.API.Controllers.Views;

public class OrderKeeperController : Controller
{
    
    public IActionResult Index()
    {
        return View("~/Views/Home/Index.cshtml");
    }
    
    public IActionResult CreateOrder()
    {
        return View("~/Views/CreateOrder/Index.cshtml");
    }
    
    public IActionResult EditOrder(int orderId)
    {
        ViewData["OrderId"] = orderId;
        return View("~/Views/EditOrder/Index.cshtml");
    }

}