using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.SessionState;
using ShopOnline.Common;
using System.Drawing.Text;
using System.IO;
using System.Drawing.Drawing2D;

namespace ShopOnline.Controllers
{
    public class CaptchaController : Controller
    {
        public ActionResult getCaptchaImage(string captchaString)
        {
            var rand = new Random((int)DateTime.Now.Ticks);

            //generate new question
            int a = rand.Next(10, 99);
            int b = rand.Next(0, 9);

            captchaString = new SecurityManager().DecodeString(captchaString);
            var captcha = captchaString;



            //image stream
            FileContentResult img = null;

            using (var mem = new MemoryStream())
            using (var bmp = new Bitmap(Constants.CAPTCHA_WIDTH, Constants.CAPTCHA_HIGHT))
            using (var gfx = Graphics.FromImage((Image)bmp))
            {
                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gfx.SmoothingMode = SmoothingMode.AntiAlias;

                PointF oPoint = new PointF(2f, 2f);
                SolidBrush oBrushWrite = new SolidBrush(Color.Red);
                SolidBrush oBrush = new SolidBrush(Color.White);
                gfx.FillRectangle(oBrush, 0, 0, bmp.Width, bmp.Height);

                //gfx.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, bmp.Width, bmp.Height));

                //add noise
                // if (noisy)
                // {
                int i, r, x, y;
                var pen = new Pen(Color.Yellow);
                for (i = 1; i < 100; i++)
                {
                    pen.Color = Color.FromArgb(
                    (rand.Next(0, 255)),
                    (rand.Next(0, 255)),
                    (rand.Next(0, 255)));

                    r = rand.Next(0, (Constants.CAPTCHA_WIDTH / 4));
                    x = rand.Next(0, Constants.CAPTCHA_WIDTH);
                    y = rand.Next(0, Constants.CAPTCHA_HIGHT);
                    gfx.DrawEllipse(pen, x, y, 2, 2);

                }
                //  }





                //add question
                Font oFont = new Font("Tahoma", Constants.CAPTCHA_FONT_SIZE, FontStyle.Regular);
                gfx.DrawString(captcha, oFont, Brushes.Red, 2, 3);

                //render as Jpeg
                bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
                img = this.File(mem.GetBuffer(), "image/Jpeg");
            }

            return img;
        }

    }
}
