using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncCon
{
    class Program
    {
        static void Main(string[] args)
        {
            DealLock();
        }

        private static void DealLock()
        {
            var bll = new AsyncMethon();
            Console.WriteLine("start");
            Task<string> t = bll.DoSomething();
            t.Wait();
            Console.WriteLine("ret");

            Task<string> tt= Task.FromResult("132");
            Console.ReadKey();
        }
    }


    public class AsyncMethon
    {
        public async Task<string> DoSomething()
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            return "ok";
        }




        public async Task<string> Thing1(string src)
        {
            HelperHttp http = new HelperHttp();
            return await http.GetAsync(src);

        }

        public async Task<string> Thing2(string src)
        {
            HelperHttp http = new HelperHttp();
            return await http.GetPicimageAsync(src);
        }

        public async Task<string> Thing3(string src)
        {
            HelperHttp http = new HelperHttp();
            return await http.GetAsync(src);
        }
    }



}
