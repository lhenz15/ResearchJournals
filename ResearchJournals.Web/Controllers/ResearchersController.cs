using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResearchJournals.Domain.Models;
using ResearchJournals.Infrastructure.Data;
using ResearchJournals.Web.Models;

namespace ResearchJournals.Web.Controllers
{
    public class ResearchersController : BaseController
    {
        public ResearchersController(ApplicationDbContext context, UserManager<Researcher> userManager) 
            : base(context, userManager)
        {
            
        }
        
        // GET
        public async Task<IActionResult> Index()
        {
            var results = _context.Users
                .Include(u => u.Subscriptors)
                .Where(u => u.Id != CurrentUserId)
                .Select(u => new ResearcherItemViewModel
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    IsSubscribed = u.Subscriptors.Any(s => s.ResearcherId == CurrentUserId)
                });

            return View(await results.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(SubscriptionViewModel model)
        {
            var currentUser = await _context.Users
                .Include(u => u.Subscriptions)
                .FirstOrDefaultAsync(u => u.Id == CurrentUserId);

            var subscribeTo = await _context.Users.FirstOrDefaultAsync(d => d.Id == model.ResearcherId);

            if (subscribeTo == null)
            {
                return NotFound();
            }

            if (currentUser.Subscriptions.Any(s => s.SubscribedToId == subscribeTo.Id))
            {
                return RedirectToAction(nameof(Index));
            }
            
            currentUser.Subscriptions.Add(new Subscription
            {
                SubscribedToId = subscribeTo.Id,
                SubscribedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        public async Task<IActionResult> Unsubscribe(SubscriptionViewModel model)
        {
            var subscription = await _context.Subscriptions
                .FirstOrDefaultAsync(s => s.ResearcherId == CurrentUserId && s.SubscribedToId == model.ResearcherId);
            
            if (subscription == null)
            {
                return NotFound();
            }

            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}