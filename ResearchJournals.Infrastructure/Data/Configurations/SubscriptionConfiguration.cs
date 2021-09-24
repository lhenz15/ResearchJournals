using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResearchJournals.Domain.Models;

namespace ResearchJournals.Infrastructure.Data.Configurations
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.HasKey(e => e.Id);
            
            builder.HasOne(e => e.Researcher)
                .WithMany(e => e.Subscriptors)
                .HasForeignKey(e => e.ResearcherId);

            builder.HasOne(e => e.SubscribedTo)
                .WithMany(e => e.Subscriptions)
                .HasForeignKey(e => e.SubscribedToId);
        }
    }
}
