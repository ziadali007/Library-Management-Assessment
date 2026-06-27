using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class BorrowBookDto
    {
        public int BookId { get; set; }
        public string MemberId { get; set; } = string.Empty;
        public string IssuedById { get; set; } = string.Empty; 
        public int DaysToBorrow { get; set; } = 14;
    }
}
