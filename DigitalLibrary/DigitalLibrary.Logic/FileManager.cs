namespace DigitalLibrary.Data.Logic
{
    using System.IO;
    using System.Linq;
    using System.Web;

    public static class FileManager
    {
        public static void DeleteFile(string path)
        {
            var folders = path.Split('\\').ToList();

            folders.RemoveAt(folders.Count - 1);

            var filePath = string.Join("/", folders);

            var fullPath = HttpContext.Current.Server.MapPath("~/" + filePath);

            var dir = new DirectoryInfo(fullPath);

            if (folders.Count > 4)
            {
                if (dir != null)
                {
                    dir.Delete(true);
                }
            }
        }

        public static bool CheckIfFileExists(string filePath)
        {
            var path = HttpContext.Current.Server.MapPath("~/" + filePath);
            var test2 = @"C:\Users\Dobri\Desktop\GitProjects\Library\DigitalLibrary\DigitalLibrary.Web\UploadedFiles\arheologiq\papuy\works\gfdsdgsd\gfdsdgsd";
            var test = File.Exists(test2);

            return File.Exists(HttpContext.Current.Server.MapPath("~/" + filePath));
        }

        public static void CreateFolderIfDoesntExists(string foderPath)
        {
            bool exists = Directory.Exists(HttpContext.Current.Server.MapPath("~/" + foderPath));
            string test = HttpContext.Current.Server.MapPath("~/" + foderPath);
            if (!exists)
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/" + foderPath));
            }
        }

        public static byte[] DownloadFile(string filePath)
        {
            var dir = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/" + filePath));

            byte[] fileBytes = File.ReadAllBytes(dir.ToString());

            return fileBytes;
        }

        public static void UploadFile(HttpPostedFileBase file, string fileName, string uploadPath)
        {
            var extension = Path.GetExtension(file.FileName);

            if (file != null && file.ContentLength > 0)
            {
                if (CheckIfFileIsPicture(file))
                {
                    var fileSaveName = fileName + extension;

                    var path = Path.Combine(HttpContext.Current.Server.MapPath("~/" + uploadPath + "/"), fileSaveName);

                    file.SaveAs(path);
                }
                else if (CheckIfFileIsZipped(file))
                {
                    var fileSaveName = fileName + extension;

                    var path = Path.Combine(HttpContext.Current.Server.MapPath("~/" + uploadPath + "/"), fileSaveName);

                    file.SaveAs(path);
                }
            }
        }

        public static bool CheckIfFileIsPicture(HttpPostedFileBase file)
        {
            var extension = Path.GetExtension(file.FileName);
            extension = extension.ToLower();

            if (extension == ".jpg"
                || extension == ".png"
                || extension == ".jpeg"
                || extension == ".gif")
            {
                return true;
            }

            return false;
        }

        public static bool CheckIfFile(HttpPostedFileBase file)
        {
            if (file != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckIfFileIsZipped(HttpPostedFileBase file)
        {
            var extension = Path.GetExtension(file.FileName);
            extension = extension.ToLower();

            if (extension == ".zip"
                || extension == ".rar")
            {
                return true;
            }

            return false;
        }
    }
}
