using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AsyncCon
{
    public class HelperHttp
    {
        public async Task<string> GetAsync(string url)
        {
            int failedTimes = _tryTimes;
            while (failedTimes-- > 0)
            {
                try
                {
                    if (_delayTime > 0)
                    {
                        await Task.Delay(failedTimes);

                    }
                    var req = (HttpWebRequest)WebRequest.Create(new Uri(url));
                    req.CookieContainer = _cc;
                    req.Method = "GET";
                    req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.17 (KHTML, like Gecko) Chrome/24.0.1312.57 Safari/537.17";
                    req.Timeout = _timeout;
                    req.KeepAlive = false;
                    req.ProtocolVersion = HttpVersion.Version10;
                    if (null != _proxy && null != _proxy.Credentials)
                    {
                        req.UseDefaultCredentials = true;
                    }
                    req.Proxy = _proxy;
                    var res = (HttpWebResponse)await req.GetResponseAsync().ConfigureAwait(false);
                    StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
                    String stHTML = sr.ReadToEnd();
                    req.Abort();
                    res.Close();
                    sr.Close();
                    return stHTML;
                }
                catch (Exception e)
                {
                    return "[ExceptionERROR]" + e.Message;
                }
            }
            return null;
        }


        public async Task<string> GetPicimageAsync(string url)
        {
            int failedTimes = _tryTimes;
            while (failedTimes-- > 0)
            {
                try
                {
                    if (_delayTime > 0)
                    {
                        await Task.Delay(failedTimes);
                    }
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new Uri(url));
                    req.CookieContainer = _cc;


                    req.Method = "GET";
                    req.Timeout = _timeout;
                    if (null != _proxy && null != _proxy.Credentials)
                    {
                        req.UseDefaultCredentials = true;
                    }
                    req.Proxy = _proxy;
                    //req.Connection = "Keep-Alive";

                    req.ContentType = "application/x-www-form-urlencoded";
                    req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";
                    //接收返回字串
                    var res = (HttpWebResponse)await req.GetResponseAsync().ConfigureAwait(false);
                    //StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);

                    //Image bitmap1 = (Image)Image.FromStream(res.GetResponseStream());

                    req.Abort();
                    res.Close();
                    //sr.Close();

                    return "123456";
                }
                catch (Exception ex)
                {
                    var ssp = new Exception(ex.Message);
                    Console.Write(ex.Message);
                }
            }

            return null;
        }


        #region Member Fields
        private CookieContainer _cc = new CookieContainer();
        private WebProxy _proxy;

        private int _delayTime = 0;
        private int _timeout = 120000; // The default is 120000 milliseconds (120 seconds).
        private int _tryTimes = 1; // 默认重试3次
        #endregion

    }
}
