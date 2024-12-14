using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes_Manager.Data;
using Notes_Manager.Models;
using System.Diagnostics;

namespace Notes_Manager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<ApplicationUser> usermanager)
        {
            _userManager = usermanager;
            _logger = logger;
            _context = context;
        }

        public async Task<ActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var model = _context.Users.Include(x => x.Notes).FirstOrDefault(y => y.Id == user.Id);

                return View(model);
            }
            else
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}