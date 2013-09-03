using System;
using System.IO;

namespace Rhea.UI.Services
{
    public class FilesStatus
    {
        #region Field
        public const string HandlerPath = "/Services/";

        public string group { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int size { get; set; }
        public string progress { get; set; }
        public string url { get; set; }
        public string thumbnailUrl { get; set; }
        public string deleteUrl { get; set; }
        public string deleteType { get; set; }
        public string error { get; set; }
        #endregion //Field

        #region Constructor
        public FilesStatus() { }

        public FilesStatus(FileInfo fileInfo) { SetValues(fileInfo.Name, (int)fileInfo.Length, fileInfo.FullName); }

        public FilesStatus(string fileName, int fileLength, string fullPath) { SetValues(fileName, fileLength, fullPath); }

        public FilesStatus(string fileName, int fileLength, string fullPath, string contentType)
        {
            SetValues(fileName, fileLength, fullPath, contentType);
        }
        #endregion //Constructor

        #region Function
        private void SetValues(string fileName, int fileLength, string fullPath)
        {
            name = fileName;
            type = "image/png";
            size = fileLength;
            progress = "1.0";
            url = HandlerPath + "UploadHandler.ashx?f=" + fileName;
            deleteUrl = HandlerPath + "UploadHandler.ashx?f=" + fileName;
            deleteType = "DELETE";

            var ext = Path.GetExtension(fullPath);
            
            var fileSize = ConvertBytesToMegabytes(new FileInfo(fullPath).Length);
            if (fileSize > 3 || !IsImage(ext)) thumbnailUrl = "/Content/img/generalFile.png";
            else thumbnailUrl = @"data:image/png;base64," + EncodeFile(fullPath);
        }

        private void SetValues(string fileName, int fileLength, string fullPath, string contentType)
        {
            name = fileName;
            type = contentType;
            size = fileLength;
            progress = "1.0";
            url = HandlerPath + "UploadHandler.ashx?f=" + fileName;
            deleteUrl = HandlerPath + "UploadHandler.ashx?f=" + fileName;
            deleteType = "DELETE";

            var ext = Path.GetExtension(fullPath);

            var fileSize = ConvertBytesToMegabytes(new FileInfo(fullPath).Length);
            if (fileSize > 3 || !IsImage(ext)) thumbnailUrl = "/Content/img/generalFile.png";
            else thumbnailUrl = @"data:image/png;base64," + EncodeFile(fullPath);
        }

        private bool IsImage(string ext)
        {
            return ext == ".gif" || ext == ".jpg" || ext == ".png";
        }

        private string EncodeFile(string fileName)
        {
            return Convert.ToBase64String(System.IO.File.ReadAllBytes(fileName));
        }

        static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }
        #endregion //Function
    }
}