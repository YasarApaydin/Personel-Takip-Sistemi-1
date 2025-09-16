using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace YuzTanimaTakip.Areas.Admin.Controllers
{    
    [Area("Admin")]
    public class AdminController:Controller
    {


        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
        
}
