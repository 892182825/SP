using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing.Design;

public partial class image : System.Web.UI.Page
{
    private Bitmap validateimage;
    private Graphics g;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.RenderPicture();
    }

    private void RenderPicture()
    {
        Label lb = new System.Web.UI.WebControls.Label();
        validateimage = new Bitmap(60, 24, PixelFormat.Format24bppRgb);
        g = Graphics.FromImage(validateimage);

        DrawValidateCode(this, MakeValidateCode());
    }

    private void DrawValidateCode(Page e, string code)
    {
        //清空图片背景色
        g.Clear(Color.White);

        //生成随机数种子
        Random random = new Random();

        //画图片的背景噪音线 10条
        for (int i = 0; i < 10; i++)
        {
            //噪音线起点坐标(x1,y1),终点坐标(x2,y2)
            int x1 = random.Next(validateimage.Width);
            int x2 = random.Next(validateimage.Width);
            int y1 = random.Next(validateimage.Height);
            int y2 = random.Next(validateimage.Height);

            //用银色画出噪音线
            g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
        }

        //输出图片中校验码的字体: 12号Arial,粗斜体
        Font font = new Font(
         "Arial",
         12,
         (FontStyle.Bold | FontStyle.Italic));

        //线性渐变画刷
        LinearGradientBrush brush = new LinearGradientBrush(
         new Rectangle(0, 0, validateimage.Width, validateimage.Height),
         Color.Blue,
         Color.Purple,
         1.2f,
         true);
        g.DrawString(code, font, brush, 2, 2);

        //画图片的前景噪音点 50个
        for (int i = 0; i < 50; i++)
        {
            int x = random.Next(validateimage.Width);
            int y = random.Next(validateimage.Height);

            validateimage.SetPixel(x, y, Color.FromArgb(random.Next()));
        }

        //画图片的边框线
        g.DrawRectangle(new Pen(Color.SaddleBrown), 0, 0, validateimage.Width - 1, validateimage.Height - 1);

        g.Save();
        MemoryStream ms = new MemoryStream();
        validateimage.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
        e.Response.ClearContent();
        e.Response.ContentType = "image/gif";
        e.Response.BinaryWrite(ms.ToArray());
        e.Response.End();

    }

    private string MakeValidateCode()
    {
        //Write by WangHua 2009-11-20
        //char[] s = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' 
        //                                     ,'a' ,'b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q'
        //                                     ,'r','s','t','u','v','w','x','y','z','A','B','C','D','E','F','G'
        //                                     ,'H','J','K','L','M','N','O','P','Q','R','S','T','U','V','W'
        //                                     ,'X','Y','Z'};

        char[] s = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
                                         
            

        string num = "";
        Random r = new Random();
        for (int i = 0; i < 5; i++)
        {
            num += s[r.Next(0, s.Length)].ToString();
        }

        System.Web.HttpContext.Current.Session["ValidateCode"] = num.Trim();
        return num.Trim();
    }
}
