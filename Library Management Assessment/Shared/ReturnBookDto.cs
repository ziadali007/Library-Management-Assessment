using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ReturnBookDto
    {
        public int TransactionId { get; set; }
        public string ProcessedById { get; set; } = string.Empty;
    }
}
