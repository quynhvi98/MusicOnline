using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace MusicOnline
{
 public   class CutDataHtml
    {
        private readonly CookieContainer cookie = new CookieContainer();
        private static HttpClient httpClient;
        private static HttpClientHandler handler;

        public async Task<String> getdulieu(String url)
        {
            handler = new HttpClientHandler
            {
                CookieContainer = cookie,
                ClientCertificateOptions = ClientCertificateOption.Automatic,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                AllowAutoRedirect = true,
                UseCookies = true,
                UseDefaultCredentials = false
            };
            httpClient = new HttpClient(handler);
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Language", "vi,en-US;q=0.8,en;q=0.6");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Host", "mp3.zing.vn");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.101 Safari/537.36");
            httpClient.BaseAddress = new Uri("http://mp3.zing.vn");

            httpClient.GetStringAsync("http://mp3.zing.vn").Wait();
            var conten = httpClient.GetStringAsync(url).Result;
            return conten.ToString();



        }
        public async Task<List<Song>> cutDataHtmlAddList(List<String> list)
        {
        
            List<Song> listAlbum = new List<Song>();

            for (int i = 0; i < list.Count-1; i++)
            {
                String content = await getdulieu("http://mp3.zing.vn/bai-hat/"+ list[i].ToString()+ ".html");
                var getHtmlHasJson = Regex.Matches(content, @"<div id=""zplayerjs-wrapper""(.*?)</div>", RegexOptions.Singleline);
                String LinkJson = getHtmlHasJson[0].ToString();
                String[] a = LinkJson.Split('"');
                List<Song> listSong = new List<Song>();
                listSong = await getListSong("http://mp3.zing.vn" + a[5]);
                Song song = listSong[0];
                listAlbum.Add(song);
            }
              
                       

            return listAlbum;

        }
        private async Task<List<Song>> getListSong(string url)
        {
            handler = new HttpClientHandler
            {
                CookieContainer = cookie,
                ClientCertificateOptions = ClientCertificateOption.Automatic,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                AllowAutoRedirect = true,
                UseCookies = true,
                UseDefaultCredentials = false
            };
            httpClient = new HttpClient(handler);
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Language", "vi,en-US;q=0.8,en;q=0.6");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Host", "mp3.zing.vn");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.101 Safari/537.36");
            httpClient.BaseAddress = new Uri("http://mp3.zing.vn");

            httpClient.GetStringAsync("http://mp3.zing.vn").Wait();
            var content = httpClient.GetStringAsync(url).Result;
            string newCont = content.Substring(16, content.Length - 17);
            List<Song> listSong = new List<Song>();
            listSong = JsonConvert.DeserializeObject<List<Song>>(newCont);

            return listSong;
        }
    }

}
