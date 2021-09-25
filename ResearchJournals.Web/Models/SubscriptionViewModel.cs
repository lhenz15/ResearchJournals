using System;
using System.ComponentModel.DataAnnotations;

namespace ResearchJournals.Web.Models
{
    public class SubscriptionViewModel
    {
        [Required]
        public Guid ResearcherId { get; set; }
    }
}