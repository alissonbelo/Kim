using Microsoft.AspNetCore.Mvc;

namespace WebAPI;

public class Teste : Controller
{
    // GET
    public IActionResult Index()
    {
        turn View();
    }
}