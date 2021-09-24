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
    public class ResearcherConfiguration : IEntityTypeConfiguration<Researcher>
    {
        public void Configure(EntityTypeBuilder<Researcher> builder)
        {
            builder.HasMany(e => e.Journals)
                .WithOne(e => e.Researcher);
        }
    }
}
