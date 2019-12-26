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
* 更新时间 ：2019/12/20 23:21:26
* 版 本 号 ：v1.0.0.0
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
using System.Text;

namespace Kane.Extension
{
    /// <summary>
    /// 图像类扩展
    /// </summary>
    public static class ImageHelper
    {
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
    }
}