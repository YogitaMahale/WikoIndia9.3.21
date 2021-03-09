using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Autofocus.Utility
{
   public  class SaveImageinFolder
    {
        public string uploadImage(string oldImageName,string saveimagePath, string base64)
        {
            var rootFolder = Directory.GetCurrentDirectory();
            rootFolder = rootFolder.ToString().Replace(".API", "");
            var path = rootFolder.ToString().Replace(".Utility", "");
            var folderPath1 = path + @"\wwwroot"+ saveimagePath;
            var deleteImagePath = "";
            if(!string.IsNullOrEmpty(oldImageName))
            {
                  deleteImagePath = path + @"\wwwroot" + oldImageName.Replace(@"/", @"\");
            }
            

            var imagePath = folderPath1;
            var test = folderPath1.Replace("\\", "/");

            string fileName = Guid.NewGuid().ToString();           
           fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + ".jpg";
            try
            {
                if (deleteImagePath != null)
                {                   
                    if (System.IO.File.Exists(deleteImagePath))
                    {
                        System.IO.File.Delete(deleteImagePath);
                    }
                }                            
              
                System.IO.File.WriteAllBytes(Path.Combine(folderPath1, fileName), Convert.FromBase64String(base64));
               

            }
            catch { }
            string res = (saveimagePath.ToString().Replace("\\", "/") + "/" + fileName);
            return res;
               
        }
    }
}
