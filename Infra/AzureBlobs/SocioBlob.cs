using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;

namespace Infra.AzureBlobs
{
    public class SocioBlob
    {
        private BaseBlob _baseBlob = new BaseBlob();

        public string SalvarFotoPerfil(string idSocio, Stream foto)
        {
            CloudBlockBlob blockBlob = _baseBlob.socios.GetBlockBlobReference(idSocio);
            blockBlob.UploadFromStream(foto);

            return blockBlob.StorageUri.PrimaryUri.AbsoluteUri;
        }
    }
}
