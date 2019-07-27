using MessagingToolkit.QRCode.Codec;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FileManager
{
    public partial class _Default : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                refreshfile();
            }
        }

        private void refreshfile()
        {
            var localdir = Server.MapPath("/upload/");
            if (!Directory.Exists(localdir))
            {
                Directory.CreateDirectory(localdir);
            }
            List<FileItem> fileItems = new List<FileItem>();
            foreach (var file in Directory.GetFiles(localdir))
            {
                FileInfo f = new FileInfo(file);
                fileItems.Add(new FileItem { name = f.Name, fileurl = "Handler1.ashx?filename=" + f.Name });
            }
            GridView1.DataSource = fileItems;
            GridView1.DataBind();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                var localdir = Server.MapPath("/upload/");
                FileUpload1.SaveAs(localdir + "//" + FileUpload1.FileName);
                refreshfile();
            }
        }

        public class FileItem
        {
            public string name { get; set; }
            public string fileurl { get; set; }

        }
    }
}