using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MusicOnline
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        CutDataHtml cutDataHtml = new CutDataHtml();
        private List<LinkAndTitleSong> listLinkAndTitleSong = new List<LinkAndTitleSong>();
        private readonly CookieContainer cookie = new CookieContainer();
        private static HttpClient httpClient;
        private static HttpClientHandler handler;
        List<Song> listPresent = new List<Song>();
        List<Song> listAllSong = new List<Song>();
        //List<String> listFile;
        string NewLanguage = "vi-VN";
        int idPage = 0;

        //fixed
        public MainPage()
        {
            this.InitializeComponent();
            demo();
            Rank_VNHOT_btnHandle();
        }
        private async void demo()
        {
            ussermain.LikeForeg();
            vn_Rank.btnHandle += Vn_Rank_btnHandle;
            //vn_Rank.setbtn_content("BXH VIỆT NAM");
            Rank_UK.btnHandle += Rank_UK_btnHandle;
            // Rank_UK.setbtn_content("BXH ÂU MỸ");
            Rank_HQ.btnHandle += Rank_HQ_btnHandle;
            // Rank_HQ.setbtn_content("BXH HÀN QUỐC");
            Rank_RealTime.btnHandle += Rank_RealTime_btnHandle;
            // Rank_RealTime.setbtn_content("REAL TIME");
            Rank_VNHOT.btnHandle += Rank_VNHOT_btnHandle;
            //  Rank_VNHOT.setbtn_content("NHẠC VIỆT HOT");
            //ussermain.SetNameIDListView(await getListSong("http://mp3.zing.vn/json/playlist/get-source/playlist/LmcGtLGiZDWFhvQtkbxyDmLH"));
            listAllSong = await getAllSong();
        }

        private async Task<List<Song>> getAllSong()
        {
            List<Song> list1 = await getListSong("http://mp3.zing.vn/json/playlist/get-source/playlist/LmcGtLGiZDWFhvQtkbxyDmLH");
            List<Song> list2 = await getListSong("http://mp3.zing.vn/json/playlist/get-source/playlist/LmcmTkmsdEFEbEFtLFJyvGkn");
            List<Song> list3 = await getListSong("http://mp3.zing.vn/json/playlist/get-source/playlist/ZmxHyLHukzRhEVCyZbJtvHkm");
            List<Song> list4 = await getListSong("http://mp3.zing.vn/json/playlist/get-source/playlist/ZHcmyZHikWdDCWmyLFcTDGZH");
            List<Song> list5 = await getListSong("http://mp3.zing.vn/json/playlist/get-source/playlist/ZnJGtLGRkScgJAZTZFcyvnkn");
            List<Song> list6 = await getListSong("http://mp3.zing.vn/json/playlist/get-source/playlist/knJHTLGRZWlCNHutZFxtDmLH");

            listAllSong.AddRange(list1);
            listAllSong.AddRange(list2);
            listAllSong.AddRange(list3);
            listAllSong.AddRange(list4);
            listAllSong.AddRange(list5);
            listAllSong.AddRange(list6);


            return listAllSong;
        }

        private async void Rank_VNHOT_btnHandle()
        {
            idPage = 1;
            var resource = new Windows.ApplicationModel.Resources.ResourceLoader();
            ussermain.senameMusic("ms-appx:///Assets/nhacvietmoi.png", resource.GetString("title_nhac_viet_moi"), "Various Artists", "2017", resource.GetString("type_vn"), resource.GetString("tb_type"), resource.GetString("tb_realease"));
            ussermain.SetNameIDListView(await getListSong("http://mp3.zing.vn/json/playlist/get-source/playlist/LmcmTkmsdEFEbEFtLFJyvGkn"));
            ussermain.LikeForeg();
        }
        //Fixed
        private async void Rank_RealTime_btnHandle()
        {
            var resource = new Windows.ApplicationModel.Resources.ResourceLoader();
            idPage = 4;
            ussermain.senameMusic("ms-appx:///Assets/realtime.jpg", resource.GetString("title_real_time"), "Various Artists", "2017", resource.GetString("type_real_time"), resource.GetString("tb_type"), resource.GetString("tb_realease"));

            ussermain.SetNameIDListView(await getListSong("http://mp3.zing.vn/json/playlist/get-source/playlist/ZmxHyLHukzRhEVCyZbJtvHkm"));
            ussermain.LikeForeg();
        }
        //Fixed
        private async void Rank_HQ_btnHandle()
        {
            var resource = new Windows.ApplicationModel.Resources.ResourceLoader();
            idPage = 3;
            ussermain.senameMusic("ms-appx:///Assets/bxh-hanquoc.jpg", resource.GetString("title_han_quoc"), "Various Artists", "2017", resource.GetString("type_han_quoc"), resource.GetString("tb_type"), resource.GetString("tb_realease"));
            ussermain.SetNameIDListView(await getListSong("http://mp3.zing.vn/json/playlist/get-source/playlist/ZHcmyZHikWdDCWmyLFcTDGZH"));
            ussermain.LikeForeg();
        }
        //Fixed
        private async void Rank_UK_btnHandle()
        {
            var resource = new Windows.ApplicationModel.Resources.ResourceLoader();
            idPage = 2;

            ussermain.senameMusic("ms-appx:///Assets/bxh-aumi.jpg", resource.GetString("title_au_my"), "Various Artists", "2017", resource.GetString("type_au_my"), resource.GetString("tb_type"), resource.GetString("tb_realease"));
            ussermain.LikeForeg();
            ussermain.SetNameIDListView(await getListSong("http://mp3.zing.vn/json/playlist/get-source/playlist/ZnJGtLGRkScgJAZTZFcyvnkn"));
        }
        //Fixed
        private async void Vn_Rank_btnHandle()
        {
            var resource = new Windows.ApplicationModel.Resources.ResourceLoader();
            idPage = 5;
            ussermain.senameMusic("ms-appx:///Assets/nhacviethot.jpg", resource.GetString("title_vn_hot"), "Various Artists", "2017", resource.GetString("type_vn"), resource.GetString("tb_type"), resource.GetString("tb_realease"));
            ussermain.LikeForeg();
            ussermain.SetNameIDListView(await getListSong("http://mp3.zing.vn/json/playlist/get-source/playlist/knJHTLGRZWlCNHutZFxtDmLH"));
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
            listSong= JsonConvert.DeserializeObject<List<Song>>(newCont);

            return listSong;
        }


        private void TextBox_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            bool found = false;
            var border = (resultStack.Parent as ScrollViewer).Parent as Border;

            string query = (sender as TextBox).Text;

            if (query.Length == 0)
            {
                // Clear

                border.Visibility = Visibility.Collapsed;
                stackTimkiem.Visibility = Visibility.Collapsed;
            }
            else
            {
                border.Visibility = Visibility.Visible;

                stackTimkiem.Visibility = Visibility.Visible;
                resultStack.Background = new SolidColorBrush(Colors.White);

            }
            List<Song> modell = new List<Song>();
            // Add the result
            foreach (var obj in listAllSong)
            {
                if (obj.name.ToLower().Contains(query.ToLower()))
                {
                    
                    Song model = obj;
                    if (modell.IndexOf(model)>=0)
                    {

                    }
                    else
                    {
                        modell.Add(model);
                        found = true;
                    }
                      
                }
            }
            if (query == null)
            {
                border.Visibility = Visibility.Collapsed;
                stackTimkiem.Visibility = Visibility.Collapsed;
            }
            else
            {
                resultStack.ItemsSource = modell;
            }
            if (!found)
            {
                border.Visibility = Visibility.Collapsed;
                stackTimkiem.Visibility = Visibility.Collapsed;
            }
        }

        private void resultStack_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var border = (resultStack.Parent as ScrollViewer).Parent as Border;
            border.Visibility = Visibility.Collapsed;
            stackTimkiem.Visibility = Visibility.Collapsed;
            Song model = resultStack.SelectedItem as Song;
            if (model != null)
            {
                ussermain.getSong(model.id, model.name, model.artist, model.cover, model.source_list[0]);
            }
        }
        private void Album(object sender, RoutedEventArgs e)
        {
            ussermain.ListAlbum();
        }
        
        private async Task<Boolean> cutDataHtmlAddList(String content, int type)
        {
            switch (type)
            {
                case 0:
                    {
                        List<Song> listSongFull = new List<Song>();
                        var srt = Regex.Matches(content, @"<h3 class=""title-item ellipsis"">(.*?)</h3>", RegexOptions.Singleline);
                        foreach (var srt1 in srt)
                        {
                            var srt2 = Regex.Matches(srt1.ToString(), @"<a href=""(.*?)order", RegexOptions.Singleline);
                            foreach (var srt3 in srt2)
                            {
                                LinkAndTitleSong linkAndTitleSong = new LinkAndTitleSong();
                                String[] a = srt3.ToString().Split('"');
                                String link = "http://mp3.zing.vn" + a[1].ToString();
                                linkAndTitleSong.link = link;
                                linkAndTitleSong.title = a[7].ToString();
                                listLinkAndTitleSong.Add(linkAndTitleSong);
                                String Conten = await cutDataHtml.getdulieu(link);
                                var getHtmlHasJson = Regex.Matches(Conten, @"<div id=""zplayerjs-wrapper""(.*?)</div>", RegexOptions.Singleline);
                                String LinkJson = getHtmlHasJson[0].ToString();
                                a=LinkJson.Split('"');
                                List<Song> listSong = new List<Song>();
                                listSong = await getListSong("http://mp3.zing.vn" + a[5]);
                                Song song = listSong[0];
                                listSongFull.Add(song);
                                break;
                            }
                        }
                        ussermain.SetNameIDListView(listSongFull);
                        break;
                    }
                case 1:
                    {                          
                        break;
                    }
            }
            return true;
        }

        private async void TimKiemOnline(object sender, RoutedEventArgs e)
        {
            String timkiem = NhapTimkiem.Text.Replace(" ", "+");
            String a = await cutDataHtml.getdulieu("http://mp3.zing.vn/tim-kiem/bai-hat.html?q=" + timkiem);
          await  cutDataHtmlAddList(a, 0);
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbLanguage.SelectedIndex == 1)
                NewLanguage = "en-US";
            else
                NewLanguage = "vi-VN";

            Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = NewLanguage;
            Windows.ApplicationModel.Resources.Core.ResourceManager.Current.DefaultContext.Reset();
            var resource = new Windows.ApplicationModel.Resources.ResourceLoader();
            vn_Rank.setbtn_content(resource.GetString("button_vn"));
            Rank_UK.setbtn_content(resource.GetString("button_au_my"));
            Rank_HQ.setbtn_content(resource.GetString("button_han_quoc"));
            Rank_RealTime.setbtn_content(resource.GetString("button_real_time"));
            Rank_VNHOT.setbtn_content(resource.GetString("button_vn_hot"));
            NhapTimkiem.PlaceholderText = resource.GetString("search");
            switch (idPage)
            {
                case 1:

                    Rank_VNHOT_btnHandle();
                    break;
                case 2:
                    Rank_UK_btnHandle();
                    break;
                case 3:
                    Rank_HQ_btnHandle();
                    break;
                case 4:
                    Rank_RealTime_btnHandle();
                    break;
                case 5:
                    Vn_Rank_btnHandle();
                    break;
                default:
                    break;
            }
        }

        private void btnDownload(object sender, RoutedEventArgs e)
        {
            ussermain.ListOffAlbum();
        }
    }
}
