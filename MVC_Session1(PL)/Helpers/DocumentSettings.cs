using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.Language;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MVC_Session1_PL_.Helpers
{
    public static class DocumentSettings
    {
        public static async Task<string> UploadFile(IFormFile file, string FolderName)
        {
            // 1.get located folder path
            // string Folderpath = $"E:\\.thumbnails{FolderName}";
            // string Folderpath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\files\\{FolderName}";
            string Folderpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", FolderName);
            if (Directory.Exists(Folderpath))
                Directory.CreateDirectory(Folderpath);

            // 2.get file name and make it unique
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            // 3.GetFilePath
            string filepath = Path.Combine(Folderpath, fileName);

            // 4.Save File As stream [Data Per Time]
            using var FileStream = new FileStream(filepath,FileMode.Create);

            file.CopyTo(FileStream);

            return fileName;
        }

        public static void DeleteFile(string fileNmae , string folderName)
        {
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);
            if (!File.Exists(filepath))
                File.Delete(filepath);
            

        }
    }
}
