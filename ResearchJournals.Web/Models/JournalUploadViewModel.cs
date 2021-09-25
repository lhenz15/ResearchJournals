using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ResearchJournals.Web.Models
{
    public class JournalUploadViewModel
    {
        [Required]
        public string Title { get; set; }
        public string FileName { get; set; }
        public IFormFile File { get; set; }    
    }
}