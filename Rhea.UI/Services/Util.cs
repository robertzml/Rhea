using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Rhea.UI.Services
{
    /// <summary>
    /// 工具类
    /// </summary>
    public static class Util
    {
        #region Method
        /// <summary>
        /// 检查文件是否图片
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static bool CheckImageExtension(string fileName)
        {
            //获取文件后缀
            string fileExtension = Path.GetExtension(fileName);
            if (fileExtension != null)
                fileExtension = fileExtension.ToLower();
            else
                return false;

            if (fileExtension != ".jpg" && fileExtension != ".gif" && fileExtension != ".png")
                return false;

            return true;
        }

        /// <summary>
        /// 缩放图片
        /// </summary>
        /// <param name="imgPath">原图片</param>
        /// <param name="width">新图片宽度</param>
        /// <param name="height">新图片高度</param>
        /// <returns></returns>
        public static Bitmap ResizeImage(string imgPath, int width, int height)
        {
            Bitmap bitmap = new Bitmap(imgPath);
            Bitmap thumb = new Bitmap(width, height);

            Graphics g = Graphics.FromImage(thumb);

            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(bitmap, new Rectangle(0, 0, width, height), new Rectangle(0, 0, bitmap.Width, bitmap.Height), GraphicsUnit.Pixel);
            g.Dispose();

            return thumb;
        }

        /// <summary>
        /// 缩放图片
        /// </summary>
        /// <param name="fs">原图片</param>
        /// <param name="width">新图片宽度</param>
        /// <param name="height">新图片高度</param>
        /// <returns></returns>
        public static Bitmap ResizeImage(Stream fs, int width, int height)
        {
            Bitmap bitmap = new Bitmap(fs);
            Bitmap thumb = new Bitmap(width, height);

            Graphics g = Graphics.FromImage(thumb);

            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(bitmap, new Rectangle(0, 0, width, height), new Rectangle(0, 0, bitmap.Width, bitmap.Height), GraphicsUnit.Pixel);
            g.Dispose();

            return thumb;
        }
        #endregion //Method
    }
}