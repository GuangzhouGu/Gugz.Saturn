using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.AutoPosition.ImageProcess
{
    public class HTImageProcessWrapper
    {
        private TemplateMatching templateMatching = new TemplateMatching();
        public HTImageProcessWrapper()
        {

        }

        /// <summary>
        /// 更新参数
        /// </summary>
        /// <param name="values"></param>
        public void UpdateParameter(double values)
        {
            //匹配率范围0.1-0.99
            if (values > 10)
            {
                values = values / 10;
            }
            templateMatching.MinScore = values;
        }
        
        /// <summary>
        /// 初始化模板
        /// </summary>
        /// <param name="bmp"></param>
        public void InitTemplate(Bitmap bmp)
        {
            HObject image = null;
            image = BitmapToHImage(bmp);
            templateMatching.CreateShapeModel(image);
        }

        /// <summary>
        /// 开始匹配
        /// </summary>
        /// <param name="srcImage"></param>
        /// <param name="rowCheck"></param>
        /// <param name="columnCheck"></param>
        /// <param name="angleCheck"></param>
        /// <param name="score"></param>
        public void Match(Bitmap srcImage, out double rowCheck, out double columnCheck, out double angleCheck, out double score)
        {
            try
            {               
                HObject image = null;
                image = BitmapToHImage(srcImage);
                HTuple RowCheck, ColumnCheck, AngleCheck, Score;
                templateMatching.Search(image, out RowCheck, out ColumnCheck, out AngleCheck, out Score);
                rowCheck = RowCheck.D;
                columnCheck = ColumnCheck.D;
                angleCheck = AngleCheck.D;
                angleCheck = angleCheck / Math.PI * 180;
                score = Score.D;
            }
            catch (Exception ex)
            {
                rowCheck = 0;
                columnCheck = 0;
                angleCheck = - 400;
                score = 0;
            }
           
            
        }

        public void Match(IntPtr data, int Width, int Height)
        {

        }


        private void Bitmap2HObjectBpp8(Bitmap bmp, out HObject image)
        {
            try
            {
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                BitmapData srcBmpData = bmp.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
                HOperatorSet.GenImage1(out image, "byte", bmp.Width, bmp.Height, srcBmpData.Scan0);
                bmp.UnlockBits(srcBmpData);
            }
            catch (Exception ex)
            {
                image = null;
            }
        }

        private static HObject BitmapToHImage(Bitmap SrcImage)
        {
            HObject Hobj;
            HOperatorSet.GenEmptyObj(out Hobj);

            Point po = new Point(0, 0);
            Size so = new Size(SrcImage.Width, SrcImage.Height);//template.Width, template.Height
            Rectangle ro = new Rectangle(po, so);

            Bitmap DstImage = new Bitmap(SrcImage.Width, SrcImage.Height, PixelFormat.Format8bppIndexed);
            DstImage = SrcImage.Clone(ro, PixelFormat.Format8bppIndexed);

            int width = DstImage.Width;
            int height = DstImage.Height;

            Rectangle rect = new Rectangle(0, 0, width, height);
            System.Drawing.Imaging.BitmapData dstBmpData =
                DstImage.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);//pImage.PixelFormat
            int PixelSize = Bitmap.GetPixelFormatSize(dstBmpData.PixelFormat) / 8;
            int stride = dstBmpData.Stride;

            //重点在此
            unsafe
            {
                int count = height * width;
                byte[] data = new byte[count];
                byte* bptr = (byte*)dstBmpData.Scan0;
                fixed (byte* pData = data)
                {
                    for (int i = 0; i < height; i++)
                        for (int j = 0; j < width; j++)
                        {
                            data[i * width + j] = bptr[i * stride + j];
                        }
                    HOperatorSet.GenImage1(out Hobj, "byte", width, height, new IntPtr(pData));
                }
            }

            DstImage.UnlockBits(dstBmpData);

            return Hobj;
        }
    }
}
