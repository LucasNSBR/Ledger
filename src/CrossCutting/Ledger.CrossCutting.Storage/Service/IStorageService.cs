using System.IO;
using System.Threading.Tasks;

namespace Ledger.CrossCutting.Storage.Service
{
    public interface IStorageService
    {
        Task<bool> CheckFileExistsAsync(string name);
        Task<StorageResult> UploadFileToStorageAsync(Stream file, string fileName);
    }
}
