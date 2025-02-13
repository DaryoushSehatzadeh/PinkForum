using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PinkForum.Data;
using PinkForum.Models;
using Microsoft.EntityFrameworkCore;


namespace PinkForum.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly PinkForumContext _context;

        public HomeController(PinkForumContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // get all discussions
            var discussions = await _context.Discussion.Include(m => m.Comments).ToListAsync();

            return View(discussions);
        }

        // Get discussion details by id
        public async Task<IActionResult> DiscussionDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // get discussion by id
            var discussion = await _context.Discussion.Include(m => m.Comments).FirstOrDefaultAsync(m => m.DiscussionId == id);

            if (discussion == null)
            {
                return NotFound();
            }

            return View(discussion);
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
