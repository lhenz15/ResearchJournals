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
    public class JournalConfiguration : IEntityTypeConfiguration<Journal>
    {
        public void Configure(EntityTypeBuilder<Journal> builder)
        {
            builder.HasKey(e => e.Id);
            
            builder.Property(e => e.FileName)
                .HasMaxLength(256);
            
            builder.Property(e => e.Title)
                .HasMaxLength(256);

            builder.HasOne(e => e.Researcher)
                .WithMany(e => e.Journals)
                .HasForeignKey(e => e.ResearcherId);
        }
    }
}
