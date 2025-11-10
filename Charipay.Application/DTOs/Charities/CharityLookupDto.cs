using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.DTOs.Charities
{
    public class CharityLookupDto
    {
        public Guid CharityId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
