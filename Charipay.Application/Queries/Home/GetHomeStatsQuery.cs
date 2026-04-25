using Charipay.Application.Common.Models;
using Charipay.Application.DTOs.Home;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Queries.Home
{
    public class GetHomeStatsQuery : IRequest<ApiResponse<HomeStatsDto>>
    {
    }
}
