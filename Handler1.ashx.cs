using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MessagingToolkit.QRCode.Codec;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
namespace FileManager
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            var filename = context.Request.QueryString["filename"];
            if (!string.IsNullOrEmpty(filename))
            {
                //设置客户端缓冲时间过期时间为0，即立即过期
                context.Response.Expires = 0;
                //清空服务器端为此会话开启的输出缓存
                context.Response.Clear();
                //设置输出文件类型
                context.Response.ContentType = "image/jpg";
                QRCodeEncoder endocder = new QRCodeEncoder();
                //二维码背景颜色
                endocder.QRCodeBackgroundColor = System.Drawing.Color.White;
                //二维码编码方式
                endocder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                //每个小方格的宽度
                endocder.QRCodeScale = 10;
                //二维码版本号
                endocder.QRCodeVersion = 5;
                //纠错等级
                endocder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;

                var url = "http://" + context.Request.Url.Authority + "/upload/" + filename;

                //将json川做成二维码
                Bitmap bitmap = endocder.Encode(url, System.Text.Encoding.UTF8);
                var bytes = Bitmap2Byte(bitmap);
                //将请求文件写入到输出缓存中
                context.Response.BinaryWrite(bytes);
                //将输出缓存中的信息传送到客户端
                context.Response.End();
            }
        }

        public static byte[] Bitmap2Byte(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Jpeg);
                byte[] data = new byte[stream.Length];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(data, 0, Convert.ToInt32(stream.Length));
                return data;
            }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}