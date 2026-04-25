using Charipay.Application.DTOs.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Interfaces.Repositories
{
    public interface IPublicRepository
    {
        Task<HomeStatsDto> GetHomeStatsAsync();
    }
}
