using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Petadmin.Core.Models;

namespace Petadmin.Storage
{
    public interface IStorageManager<T>
    {
        Task<PetadminStorageFile> Upload(HttpRequest request, string pathToUpload);
        Task<PetadminStorageFile> RestoreUpload(HttpRequest request, string pathToUpload, IProgress<T> progress);
        void DeleteVisitFile(PetadminStorageFile file);
    }
}
