using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class BorrowingTransaction : BaseEntity, IHasName
    {

        public int BookId { get; set; }
        public Book Book { get; set; }

      
        public string MemberId { get; set; } = string.Empty; 

        public string IssuedById { get; set; } = string.Empty; 

        public string? ProcessedById { get; set; }

        public DateTime BorrowDate { get; set; } = DateTime.UtcNow;
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
