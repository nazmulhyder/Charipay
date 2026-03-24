using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Interfaces.Storage
{
    public interface IFileStorageService
    {
       Task<string> UploadAsync(
       Stream stream,
       string contentType,
       string fileName,
       string folder,
       CancellationToken token);

        Task DeleteAsync(string fileUrl, CancellationToken token);
    }
}
