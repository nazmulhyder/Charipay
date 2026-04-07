using Charipay.Application.Common.Models;
using Charipay.Application.DTOs.Dashboard;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Queries.Dashboard
{
    public class GetDashboardQuery : IRequest<ApiResponse<DashboardDto>>
    {
    }
}
