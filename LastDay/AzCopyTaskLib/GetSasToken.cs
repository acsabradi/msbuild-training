using Azure.Storage;
using Azure.Storage.Sas;
using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzCopyTaskLib
{
    public class GetSasToken : Microsoft.Build.Utilities.Task
    {
        [Required]
        public string AccountName { get; set; }

        [Required]
        public string AccountKey { get; set; }

        [Required]
        public string Container { get; set; }

        public int? LifeTimeSeconds { get; set; }

        [Output]
        public string SasToken { get; set; }

        public override bool Execute()
        {
            var blobSasBuilder = new BlobSasBuilder
            {
                StartsOn = DateTime.UtcNow.AddMinutes(-1),
                ExpiresOn = DateTime.UtcNow.AddSeconds(LifeTimeSeconds ?? 5 * 60),
                BlobContainerName = Container,
                Resource = "c",
            };
            blobSasBuilder.SetPermissions(BlobAccountSasPermissions.Write | BlobAccountSasPermissions.Read);
            SasToken = blobSasBuilder.ToSasQueryParameters(new StorageSharedKeyCredential(AccountName, AccountKey)).ToString();
            return true;
        }
    }
}
