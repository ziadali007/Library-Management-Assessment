using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Book : BaseEntity,IHasName
    {

        public string Title { get; set; } = string.Empty;

        public string ISBN { get; set; } = string.Empty;

        public string? Edition { get; set; }

        public int PublicationYear { get; set; }

        public string? Summary { get; set; }

        public string? CoverImageUrl { get; set; }

        public BookStatus Status { get; set; } = BookStatus.Available;

        // Foreign Keys
        public int PublisherId { get; set; }
        public int LanguageId { get; set; }

        // Navigation Properties
        public Publisher Publisher { get; set; } = null!;
        public Language Language { get; set; } = null!;

        public ICollection<Author> Authors { get; set; } = new HashSet<Author>();
        public ICollection<Category> Categories { get; set; } = new HashSet<Category>();
        public ICollection<BorrowingTransaction> BorrowingTransactions { get; set; } = new HashSet<BorrowingTransaction>();
        [NotMapped]
        public string Name
        {
            get => Title;
            set => Title = value ?? string.Empty;
        }
    }
}
