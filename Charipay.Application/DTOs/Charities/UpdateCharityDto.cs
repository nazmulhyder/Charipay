using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.DTOs.Charities
{
    public class UpdateCharityDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string ContactEmail { get; set; }
        public bool IsApproved { get; set; }
    }
}
