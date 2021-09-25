using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ResearchJournals.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ResearchJournals.Domain.Models;
using ResearchJournals.Infrastructure.Data;

namespace ResearchJournals.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,
            ApplicationDbContext context, UserManager<Researcher> userManager) :
            base(context, userManager)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var journals = _context.Users
                .Include(u => u.Subscriptions)
                .ThenInclude(s => s.SubscribedTo)
                .Include(s => s.Journals)
                .Where(u => u.Id == CurrentUserId)
                .SelectMany(e => e.Subscriptions.SelectMany(s => s.SubscribedTo.Journals))
                .Select(j => new JournalItemViewModel
                {
                    Id = j.Id,
                    Title = j.Title,
                    FileName = j.FileName,
                    Owner = j.Researcher.UserName,
                    UploadedAt = j.UploadedAt
                })
                .OrderByDescending(j => j.UploadedAt);
            
            return View(await journals.ToListAsync());
        }
    }
}
