using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace StockOpen
{
    internal class StockSerive
    {
        public List<StockInfo> GetData()
        {
            string arg = DateTime.Now.AddYears(-1).ToShortDateString().ToString();
            string arg2 = DateTime.Now.ToShortDateString().ToString();
            string requestUriString = $"http://fund.eastmoney.com/data/rankhandler.aspx?op=ph&dt=kf&ft=all&rs=&gs=0&sc=dm&st=asc&sd={arg}&ed={arg2}&qdii=&tabSubtype=,,,,,&pi=1&pn=2000&dx=1";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUriString);
            httpWebRequest.Method = "GET";
            httpWebRequest.Referer = "http://fund.eastmoney.com/data/fundranking.html";
            string input;
            using (WebResponse webResponse = httpWebRequest.GetResponse())
            {
                using (Stream stream = webResponse.GetResponseStream())
                {
                    StreamReader streamReader = new StreamReader(stream);
                    input = streamReader.ReadToEnd();
                }
            }

            var result = Regex.Match(input, "(\\{.*?\\})").Value;
            Stockobject stockobject = JsonConvert.DeserializeObject<Stockobject>(result);

            List<StockInfo> list = new List<StockInfo>();
            foreach (var item in stockobject.datas)
            {
                var a = item.Split(',');
                StockInfo info = new StockInfo();
                info.基金代码 = a[0];
                info.基金简称 = a[1];
                info.日期 = a[3].Substring(5);
                info.单位净值 = a[4];
                info.累计净值 = a[5];
                info.日增长率 = a[6];
                info.近1周 = a[7];
                info.近1月 = a[8];
                info.近3月 = a[9];
                info.近6月 = a[10];
                info.近1年 = a[11];
                info.近2年 = a[12];
                info.近3年 = a[13];
                info.今年来 = a[14];
                info.成立来 = a[15];
                info.自定义 = a[18];
                info.手续费 = a[22];

                list.Add(info);
            }

            return list;
        }
    }
}