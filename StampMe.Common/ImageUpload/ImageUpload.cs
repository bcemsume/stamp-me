using CoreFtp;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System;

namespace StampMe.Common.ImageUpload
{
    public class ImageUpload
    {

        public async static  Task Upload(string img, string fileName)
        {
            try
            {
                using (var ftpClient = new FtpClient(new FtpClientConfiguration
                {
                    Host = @"ftp://185.187.186.41",
                    Username = "anonymous"
                }))
                {
                    // var fileinfo = new FileInfo("C:\\test.png");
                    await ftpClient.LoginAsync();

                    using (var writeStream = await ftpClient.OpenFileWriteStreamAsync(fileName))
                    {
                        var fileReadStream = new MemoryStream(Convert.FromBase64String(img));
                        await fileReadStream.CopyToAsync(writeStream);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
