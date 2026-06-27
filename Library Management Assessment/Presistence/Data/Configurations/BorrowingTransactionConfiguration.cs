using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Data.Configurations
{
    public class BorrowingTransactionConfiguration : IEntityTypeConfiguration<BorrowingTransaction>
    {
        public void Configure(EntityTypeBuilder<BorrowingTransaction> builder)
        {
            builder.HasOne(bt => bt.Book)
                .WithMany(b => b.BorrowingTransactions)
                .HasForeignKey(bt => bt.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(bt => bt.BorrowDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(bt => bt.DueDate)
                   .IsRequired();

            builder.Property(bt => bt.ReturnDate)
                   .IsRequired(false);

            builder.Property(bt => bt.MemberId)
                   .IsRequired();

            builder.Property(bt => bt.IssuedById)
                   .IsRequired();

            builder.Property(bt => bt.ProcessedById)
                   .IsRequired(false);

            builder.HasIndex(bt => bt.MemberId)
                   .HasDatabaseName("IX_BorrowingTransaction_MemberId");

            builder.HasIndex(bt => bt.BookId)
                   .HasDatabaseName("IX_BorrowingTransaction_BookId");

            builder.HasIndex(bt => new { bt.ReturnDate, bt.DueDate })
                   .HasDatabaseName("IX_BorrowingTransaction_ActiveLoans");
        }
    }
}
