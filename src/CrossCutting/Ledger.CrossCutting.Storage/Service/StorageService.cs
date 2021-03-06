﻿using Ledger.CrossCutting.Storage.Configuration;
using Ledger.Shared.Notifications;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Ledger.CrossCutting.Storage.Service
{
    public class StorageService : IStorageService
    {
        private readonly IOptions<StorageOptions> _options;

        public StorageService(IOptions<StorageOptions> options)
        {
            _options = options;
        }

        private CloudBlobContainer GetContainer()
        {
            StorageCredentials credentials = new StorageCredentials(_options.Value.AccountName, _options.Value.AccountKey);

            CloudBlobContainer container = new CloudStorageAccount(credentials, true)
                .CreateCloudBlobClient()
                .GetContainerReference(_options.Value.ContainerName);

            return container;
        }

        public async Task<bool> CheckFileExistsAsync(string name)
        {
            CloudBlockBlob block = GetContainer()
                .GetBlockBlobReference(name);

            return await block.ExistsAsync();
        }

        public async Task<StorageResult> UploadFileToStorageAsync(Stream file, string name)
        {
            string fileName = name + Guid.NewGuid().ToString();

            CloudBlockBlob block = GetContainer()
                .GetBlockBlobReference(fileName);

            try
            {
                await block.UploadFromStreamAsync(file);
                return StorageResult.Ok(fileName);
            }
            catch
            {
                return StorageResult.Failure(
                    new DomainNotification("Blob não encontrado", "Não foi possível fazer o o upload do arquivo para o servidor."));
            }
        }
    }
}
