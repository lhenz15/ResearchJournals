using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchJournals.Domain.Models
{
    public class Subscription
    {
        public Guid Id { get; set; }
        public Guid ResearcherId { get; set; }
        public Guid SubscribedToId { get; set; }
        public DateTime SubscribedAt { get; set; }

        public virtual Researcher Researcher { get; set; }
        public virtual Researcher SubscribedTo { get; set; }

    }
}
