using System.Net;

namespace ConsoleApplication1
{
    /// <summary>
    /// 下载图片的类
    /// </summary>

    public class ImageDownloader
    {
        public void Download(string url, string path)
        {
            WebClient mywebclient = new WebClient();
            mywebclient.DownloadFile(url, path);
        }
    }
}