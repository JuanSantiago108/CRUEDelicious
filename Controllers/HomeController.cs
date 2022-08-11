using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRUEDelicious.Models;

namespace CRUEDelicious.Controllers;

public class HomeController : Controller
{

    public IActionResult Privacy()
    {
        return View();
    }

}
