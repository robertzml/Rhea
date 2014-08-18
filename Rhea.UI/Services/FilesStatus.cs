using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Rhea.UI.Services
{
    /// <summary>
    /// 上传文件状态
    /// </summary>
    public class FilesStatus
    {
        #region Field
        public const string HandlerPath = "/Services/";
        #endregion //Field

        #region Constructor
        public FilesStatus() { }

        public FilesStatus(FileInfo fileInfo)
        {
            SetValues(fileInfo.Name, (int)fileInfo.Length, fileInfo.FullName);
        }

        public FilesStatus(string fileName, int fileLength, string fullPath)
        {
            SetValues(fileName, fileLength, fullPath);
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
            delete_url = HandlerPath + "UploadHandler.ashx?f=" + fileName;
            delete_type = "DELETE";

            var ext = Path.GetExtension(fullPath);

            var fileSize = ConvertBytesToMegabytes(new FileInfo(fullPath).Length);
            if (fileSize > 3 || !IsImage(ext)) 
                thumbnail_url = "/Content/img/generalFile.png";
            else                 
                thumbnail_url = @"data:image/png;base64," + EncodeFile(fullPath);
        }

        private bool IsImage(string ext)
        {
            return ext == ".gif" || ext == ".jpg" || ext == ".png";
        }

        private string EncodeFile(string fileName)
        {
            return Convert.ToBase64String(System.IO.File.ReadAllBytes(fileName));
        }
        #endregion //Function

        #region Method
        static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }
        #endregion //Method

        #region Property
        public string group { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int size { get; set; }
        public string progress { get; set; }
        public string url { get; set; }
        public string thumbnail_url { get; set; }
        public string delete_url { get; set; }
        public string delete_type { get; set; }
        public string error { get; set; }
        #endregion //Property
    }
}