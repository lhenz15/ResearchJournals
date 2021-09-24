using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ResearchJournals.Domain.Models
{
    public class Researcher : IdentityUser<Guid>
    {
        public virtual ICollection<Journal> Journals { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
        public virtual ICollection<Subscription> Subscriptors { get; set; }
    }
}
