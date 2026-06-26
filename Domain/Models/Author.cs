using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Author : BaseEntity, IHasName
    {
        public string Name { get; set; } = string.Empty;

        public string? Biography { get; set; }

        public ICollection<Book> Books { get; set; } = new HashSet<Book>();
    }
}
