#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.Extension
* 项目描述 ：通用扩展工具
* 类 名 称 ：ImageHelper
* 类 描 述 ：图像类扩展
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.Extension
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/10/16 23:21:26
* 更新时间 ：2020/03/20 13:21:26
* 版 本 号 ：v1.0.3.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace Kane.Extension
{
    /// <summary>
    /// 图像类扩展
    /// </summary>
    public static class ImageHelper
    {
        #region 检查该文件是否图片的后缀
        /// <summary>
        /// 常见的图像文件后缀
        /// </summary>
        internal static string[] ImageExt = new string[] { ".png", ".jpg", ".jepg", ".gif", ".bmp" };
        /// <summary>
        /// 可扩展的图像文件后缀，如【.tif】
        /// </summary>
        public static List<string> ImageExtEx = new List<string>(4);
        #endregion

        #region 检查该文件是否为图片文件，可设置ImageExtEx进行全局扩展
        /// <summary>
        /// 检查该文件是否为图片文件，可设置ImageExtEx进行全局扩展
        /// </summary>
        /// <param name="path">文件完整路径</param>
        /// <returns></returns>
        public static bool IsImageFile(this string path)
        {
            var ext = Path.GetExtension(path).ToLower();
            return ImageExt.Any(k => k == ext) || ImageExtEx.Any(k => k.ToLower() == ext);
        }
        #endregion

        #region 检查该文件是否为图片文件，可临时进行扩充比较 + IsImageFile(this string path, params string[] ex)
        /// <summary>
        /// 检查该文件是否为图片文件，可临时进行扩充比较，如【.tif】
        /// </summary>
        /// <param name="path">文件完整路径</param>
        /// <param name="ex">补充的后缀名，，如【.tif】</param>
        /// <returns></returns>
        public static bool IsImageFile(this string path, params string[] ex)
        {
            var ext = Path.GetExtension(path).ToLower();
            return ImageExt.Any(k => k == ext) || ImageExtEx.Any(k => k.ToLower() == ext) || ex.Any(k => k.ToLower() == ext);
        }
        #endregion

        #region 多个Bitmap叠加 + BitmapOverlay(Bitmap original, params Bitmap[] overlays)
        /// <summary>
        /// 多个Bitmap叠加
        /// </summary>
        /// <param name="original">原始的Bitmap</param>
        /// <param name="overlays">要叠加的Bitmap</param>
        /// <returns></returns>
        public static Bitmap BitmapOverlay(Bitmap original, params Bitmap[] overlays)
        {
            Bitmap bitmap = new Bitmap(original.Width, original.Height, PixelFormat.Format64bppArgb);
            Graphics canvas = Graphics.FromImage(bitmap);
            canvas.DrawImage(original, new Point(0, 0));
            foreach (Bitmap overlay in overlays)
            {
                canvas.DrawImage(new Bitmap(overlay, original.Size), new Point(0, 0));
            }
            canvas.Save();
            return bitmap;
        }
        #endregion

        #region 替换Bitmap的底色 + ChangeBitmapColor(Bitmap original, Color colorMask)
        /// <summary>
        /// 替换Bitmap的底色
        /// </summary>
        /// <param name="original">原始的Bitmap</param>
        /// <param name="colorMask">要替换的颜色</param>
        /// <returns></returns>
        public static Bitmap ChangeBitmapColor(Bitmap original, Color colorMask)
        {
            Bitmap newBitmap = new Bitmap(original);

            for (int x = 0; x < newBitmap.Width; x++)
            {
                for (int y = 0; y < newBitmap.Height; y++)
                {
                    Color color = original.GetPixel(x, y);
                    if (color.A != 0)
                    {
                        int red = color.R * colorMask.R / 255;
                        int green = color.G * colorMask.G / 255;
                        int blue = color.B * colorMask.B / 255;
                        int alpha = color.A * colorMask.A / 255;
                        newBitmap.SetPixel(x, y, Color.FromArgb(alpha, red, green, blue));
                    }
                    else
                    {
                        newBitmap.SetPixel(x, y, color);
                    }
                }
            }
            return newBitmap;
        }
        #endregion

        #region 调整Bitmap大小 + ResizeBitmap(Bitmap original, int width, int height)
        /// <summary>
        /// 调整Bitmap大小
        /// </summary>
        /// <param name="original">原始的Bitmap</param>
        /// <param name="width">调整后的宽</param>
        /// <param name="height">调整后的高</param>
        /// <returns></returns>
        public static Bitmap ResizeBitmap(Bitmap original, int width, int height)
        {
            Bitmap newBitmap = new Bitmap(width, height);
            using Graphics g = Graphics.FromImage(newBitmap);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.DrawImage(original, new Rectangle(0, 0, width, height));
            return newBitmap;
        }
        #endregion

        #region 将Image转成指定格式的Base64字符串 + ToBase64(this Image image, ImageFormat format)
        /// <summary>
        /// 将Image转成指定格式的Base64字符串
        /// </summary>
        /// <param name="image">要转换的Image</param>
        /// <param name="format">要转换的格式</param>
        /// <returns></returns>
        public static string ToBase64(this Image image, ImageFormat format)
        {
            try
            {
                using MemoryStream ms = new MemoryStream();
                image.Save(ms, format);
                byte[] byteArr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(byteArr, 0, (int)ms.Length);
                ms.Close();
                return Convert.ToBase64String(byteArr);
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion

        #region 将Image转成Png格式的Base64字符串 + ToBase64(this Image image)
        /// <summary>
        /// 将Image转成Png格式的Base64字符串
        /// </summary>
        /// <param name="image">要转换的Image</param>
        /// <returns></returns>
        public static string ToBase64(this Image image) => image.ToBase64(ImageFormat.Png);
        #endregion

        #region 将Base64字符串转成Image,自动去除CssBase64格式 + Base64ToImage(this string value)
        /// <summary>
        /// 将Base64字符串转成Image,自动去除CssBase64格式
        /// </summary>
        /// <param name="value">要转换的Base64字符串</param>
        /// <returns></returns>
        public static Image Base64ToImage(this string value)
        {
            try
            {
                var imageBytes = value.Replace("data:image/png;base64,", "").Replace("data:image/jpg;base64,", "")
                    .Replace("data:image/jpeg;base64,", "").Base64ToBytes();//如果包含CssBase64头部信息，则去掉
                using MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                return Image.FromStream(ms, true);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 将图像文件转成Base64字符串，注意未增加CssBase64头信息 + FileToBase64(this string path)
        /// <summary>
        /// 将图像文件转成Base64字符串，注意未增加CssBase64头信息
        /// </summary>
        /// <param name="path">图像文件路径</param>
        /// <returns></returns>
        public static string FileToBase64(this string path)
        {
            if (!path.IsImageFile()) throw new Exception("文件不是有效的图像文件");
            return new Bitmap(path, true).ToBase64();
        }
        #endregion

        #region 将图像文件转成Base64字符串，可扩展后缀，如【.tif】，注意未增加CssBase64头信息 + FileToBase64(this string path) + FileToBase64(this string path,params string[] ext)
        /// <summary>
        /// 将图像文件转成Base64字符串，可扩展后缀，如【.tif】，注意未增加CssBase64头信息
        /// </summary>
        /// <param name="path">图像文件路径</param>
        /// <param name="ext">扩展后缀</param>
        /// <returns></returns>
        public static string FileToBase64(this string path, params string[] ext)
        {
            if (!path.IsImageFile(ext)) throw new Exception("文件不是有效的图像文件");
            try
            {
                return new Bitmap(path, true).ToBase64();
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion
    }
}