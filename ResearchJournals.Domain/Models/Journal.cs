using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchJournals.Domain.Models
{
    public class Journal
    {
        public Guid Id { get; set; }
        public Guid ResearcherId { get; set; }
        public string FileName { get; set; }
        public string Title { get; set; }
        public byte[] Content { get; set; }
        public DateTime UploadedAt { get; set; }

        public virtual Researcher Researcher { get; set; }
    }
}
