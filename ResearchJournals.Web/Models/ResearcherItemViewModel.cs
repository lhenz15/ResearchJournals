using System;

namespace ResearchJournals.Web.Models
{
    public class ResearcherItemViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public bool IsSubscribed { get; set; }
    }
}