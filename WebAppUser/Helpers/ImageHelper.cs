using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppUser.Helpers
{
    public class ImageHelper 
    {

        //save the Image in a directory phisical and return url for save in the database
        public async Task<string> UploadImageDirectoryAsync(IFormFile imageFile, string folder)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var path = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot\\img\\" + folder,
                    file);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                return $"~/img/{folder}/{file}";
            }
            return string.Empty;
        }

        //Convert Image to a Array byte, for save in a database
        public byte[] UploadImageDB(IFormFile Image)
        {
            byte[] p1 = null;
            if (Image != null && Image.Length > 0)
                {
                        using (var fs1 = Image.OpenReadStream())
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    return   p1;
                }            
                return p1;
        }
    }

}
