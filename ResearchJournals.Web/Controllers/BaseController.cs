using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ResearchJournals.Domain.Models;
using ResearchJournals.Infrastructure.Data;
using ResearchJournals.Web.Models;

namespace ResearchJournals.Web.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {
        protected readonly ApplicationDbContext _context;
        protected readonly UserManager<Researcher> _userManager;
        
        protected Guid CurrentUserId => Guid.Parse(_userManager.GetUserId(User));

        protected BaseController(ApplicationDbContext context, UserManager<Researcher> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}