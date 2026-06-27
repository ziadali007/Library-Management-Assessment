using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public enum BookStatus
    {
        Available = 1,
        CheckedOut = 2,
        Reserved = 3
    }
}
