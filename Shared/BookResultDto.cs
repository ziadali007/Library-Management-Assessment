using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class BookResultDto
    {
        public string Title { get; set; } = string.Empty;

        public string ISBN { get; set; } = string.Empty;

        public string? Edition { get; set; }

        public int PublicationYear { get; set; }

        public string? Summary { get; set; }

        public string? CoverImageUrl { get; set; }

        public string Status { get; set; }

        public string Publisher { get; set; } = null!;
        public string Language { get; set; } = null!;

        public ICollection<string> Authors { get; set; } = new HashSet<string>();
        public ICollection<string> Categories { get; set; } = new HashSet<string>();
    }
}
