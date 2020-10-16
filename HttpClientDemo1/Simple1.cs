using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientDemo1
{
    public class Simple1
    {

        public void GetForecast()
        {
            string serviceURL = "http://www.weather.com.cn/forecast/";
            HttpClient client = new HttpClient();
            Task<HttpResponseMessage> rsp = client.GetAsync(serviceURL);
            HttpResponseMessage message = rsp.Result;

            var strContent= message.Content.ReadAsStringAsync().Result;


        }
    }
}
