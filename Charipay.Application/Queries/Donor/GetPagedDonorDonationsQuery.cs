using Charipay.Application.Common.Models;
using Charipay.Application.Common.Pagination;
using Charipay.Application.DTOs.Donor.Donation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Queries.Donor
{
    public class GetPagedDonorDonationsQuery : PagedRequest, IRequest<ApiResponse<PageResult<DonationResponseDto>>>
    {
        public string? search {  get; set; }
    }
}
