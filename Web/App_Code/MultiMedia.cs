using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// MultiMedia 的摘要说明
/// </summary>
public class MultiMedia
{
	public MultiMedia()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    private int id;

    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    private string mp3Name;

    public string Mp3Name
    {
        get { return mp3Name; }
        set { mp3Name = value; }
    }
    private string mp3Artist;

    public string Mp3Artist
    {
        get { return mp3Artist; }
        set { mp3Artist = value; }
    }
    private string mp3Url;

    public string Mp3Url
    {
        get { return mp3Url; }
        set { mp3Url = value; }
    }
}
