using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class AddLanguageDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public ICollection<string> Books { get; set; } = new HashSet<string>();
    }
}
