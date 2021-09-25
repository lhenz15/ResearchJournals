using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResearchJournals.Domain.Models;
using ResearchJournals.Infrastructure.Data;
using ResearchJournals.Web.Models;

namespace ResearchJournals.Web.Controllers
{
    [Authorize]
    public class JournalsController : BaseController
    {

        public JournalsController(ApplicationDbContext context, 
            UserManager<Researcher> userManager) : base(context, userManager)
        {

        }
        
        // GET 
        // List all my Journals
        public async Task<IActionResult> Index()
        {
            var researcher = await _context.Users
                .Include(d => d.Journals)
                .FirstOrDefaultAsync(t => t.Id == CurrentUserId);

            return View(researcher);
        }

        [HttpPost]
        public async Task<IActionResult> Index(JournalUploadViewModel model)
        {
            var newJournal = new Journal
            {
                Title = model.Title,
                FileName = model.File.Name,
                ResearcherId = CurrentUserId,
                UploadedAt = DateTime.UtcNow
            };
            
            var content = await GetFileContentByteArray(model.File);

            if (content == null)
            {
                return RedirectToAction(nameof(Index));
            }

            newJournal.Content = content;
            _context.Journals.Add(newJournal);

            await _context.SaveChangesAsync();

            return View("UploadCompleted");
        }

        private async Task<byte[]> GetFileContentByteArray(IFormFile formFile)
        {
            await using var memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream);

            // Upload the file if less than 2 MB
            if (memoryStream.Length > 2097152)
            {
                ModelState.AddModelError("File", "The file is too large.");
                return null;
            }

            return memoryStream.ToArray();
        }
    }
}