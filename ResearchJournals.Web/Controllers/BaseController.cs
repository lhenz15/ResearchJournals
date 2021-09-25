using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ResearchJournals.Domain.Models;
using ResearchJournals.Infrastructure.Data;

namespace ResearchJournals.Web.Controllers
{
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
    }
}