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
    public class JournalsController : BaseController
    {

        private const int MAX_FILE_SIZE = 2097152; // 2mb
        
        public JournalsController(ApplicationDbContext context, 
            UserManager<Researcher> userManager) : base(context, userManager)
        {

        }
        
        // GET 
        // List all my Journals
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var researcher = await _context.Users
                .Include(d => d.Journals)
                .FirstOrDefaultAsync(t => t.Id == CurrentUserId);

            return View(researcher);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<JournalItemViewModel>> GetJournal(Guid id)
        {
            var journal = await _context.Journals.FirstOrDefaultAsync(j => j.Id == id);
            
            if (journal == null)
            {
                return NotFound();
            }

            var result = new JournalItemViewModel();
            
            result.Id = journal.Id;
            result.Title = journal.Title;
            result.FileName = journal.FileName;
            result.ContentBase64 = Convert.ToBase64String(journal.Content);
            result.UploadedAt = journal.UploadedAt;
            
            return Json(result);
        }

        [HttpPost("")]
        public async Task<IActionResult> Index(JournalUploadViewModel model)
        {
            var newJournal = new Journal
            {
                Title = model.Title,
                FileName = model.File.FileName,
                ResearcherId = CurrentUserId,
                UploadedAt = DateTime.UtcNow
            };
            
            var content = await GetFileContentByteArray(model.File);

            if (content == null)
            {
                // Add error message
                return RedirectToAction(nameof(Index));
            }

            newJournal.Content = content;
            _context.Journals.Add(newJournal);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task<byte[]> GetFileContentByteArray(IFormFile formFile)
        {
            await using var memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream);

            // Upload the file if less than 2 MB
            if (memoryStream.Length > MAX_FILE_SIZE)
            {
                ModelState.AddModelError("File", "The file is too large.");
                return null;
            }

            return memoryStream.ToArray();
        }
    }
}