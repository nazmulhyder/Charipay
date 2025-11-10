using Charipay.Application.Common.Models;
using Charipay.Application.DTOs.Charities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Queries.Charities
{
    public class GetCharityLookupQuery : IRequest<ApiResponse<IEnumerable<CharityLookupDto>>>
    {

    }
}
