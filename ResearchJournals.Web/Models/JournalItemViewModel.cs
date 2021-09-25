using System;

namespace ResearchJournals.Web.Models
{
    public class JournalItemViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public byte[] Content { get; set; }
        public string ContentBase64 { get; set; }
        public string Owner { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}