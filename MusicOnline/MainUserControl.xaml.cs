using MusicOnline.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace MusicOnline
{
    public sealed partial class MainUserControl : UserControl
    {
        private readonly CookieContainer cookie = new CookieContainer();
        private static HttpClient httpClient;
        private static HttpClientHandler handler;
        List<Song> listAllSong = new List<Song>();
        List<Song> listAlbum = new List<Song>();
        List<SongDownloaded> listOffAlbum = new List<SongDownloaded>();
        string idSong = "";
        int checkSongNumber = 0;
        public MainUserControl()
        {
            this.InitializeComponent();
            getAllSong();
        }
        private List<Song> getListSong(string url)
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
            List<Song> listSong = JsonConvert.DeserializeObject<List<Song>>(newCont);
            return listSong;
        }
        private List<Song> getAllSong()
        {
            List<Song> list1 = getListSong("http://mp3.zing.vn/json/playlist/get-source/playlist/LmcGtLGiZDWFhvQtkbxyDmLH");
            List<Song> list2 = getListSong("http://mp3.zing.vn/json/playlist/get-source/playlist/LmcmTkmsdEFEbEFtLFJyvGkn");
            List<Song> list3 = getListSong("http://mp3.zing.vn/json/playlist/get-source/playlist/ZmxHyLHukzRhEVCyZbJtvHkm");
            List<Song> list4 = getListSong("http://mp3.zing.vn/json/playlist/get-source/playlist/ZHcmyZHikWdDCWmyLFcTDGZH");
            List<Song> list5 = getListSong("http://mp3.zing.vn/json/playlist/get-source/playlist/ZnJGtLGRkScgJAZTZFcyvnkn");
            List<Song> list6 = getListSong("http://mp3.zing.vn/json/playlist/get-source/playlist/knJHTLGRZWlCNHutZFxtDmLH");
            listAllSong.AddRange(list1);
            listAllSong.AddRange(list2);
            listAllSong.AddRange(list3);
            listAllSong.AddRange(list4);
            listAllSong.AddRange(list5);
            listAllSong.AddRange(list6);
            return listAllSong;
        }
        private void PreviousSong(object sender, TappedRoutedEventArgs e)
        {
            int index = listView.SelectedIndex;

            if (index == 0)
            {
            }
            else
            {
                try
                {
                    listView.SelectedIndex = index - 1;
                    Song song = listView.SelectedItem as Song;
                    mediaElement.Source = new Uri(song.source_list[0]);
                    nameSong.Text = song.name;
                    artis.Text = song.artist;
                    image.ImageSource = new BitmapImage(new Uri(song.cover));
                }
                catch (Exception ee)
                {

                }

            }


        }
        private void NextSong(object sender, TappedRoutedEventArgs e)
        {
            int index = listView.SelectedIndex;
            try
            {
                listView.SelectedIndex = index + 1;
                Song song = listView.SelectedItem as Song;
                mediaElement.Source = new Uri(song.source_list[0]);
                nameSong.Text = song.name;
                artis.Text = song.artist;
                image.ImageSource = new BitmapImage(new Uri(song.cover));
            }
            catch (Exception ee)
            {

            }
        }
        public void SetNameIDListView(List<Song> a)
        {
            for (int i = 0; i < a.Count; i++)
            {
                a[i].name = i + 1 + ". " + a[i].name;//trèn số ở trước để xem theo thứ tự
            }
            listView.ItemsSource = a;

        }
        public void senameMusic(String Url, String titledanhdmuc, String artisdanhmuc, String timedanhmuc, String categorydanhmuc, String type, String realease)
        {
            imgDanhMuc.Source = new BitmapImage(new Uri(Url));
            titleDanhMuc.Text = titledanhdmuc;
            artisDanhMuc.Text = artisdanhmuc;
            timeDanhMuc.Text = timedanhmuc;
            categoryDanhMuc.Text = categorydanhmuc;
            theloai.Text = type;
            phathanh.Text = realease;
        }
        public void getSong(String id, String name, String artist, String cover, String LinkMusic)
        {
            idSong = id;
            nameSong.Text = name;
            artis.Text = artist;
            image.ImageSource = new BitmapImage(new Uri(cover));
            mediaElement.Source = new Uri(LinkMusic);
            checkSongAlbum(idSong);
            checkSongOffAlbum(idSong);
        }

        private async void listView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                brightness_reduce.Color = Colors.Black;
                Song song = listView.SelectedItem as Song;
                mediaElement.Source = new Uri(song.source_list[0]);
                checkSongNumber = listAllSong.IndexOf(song);
                image.ImageSource = new BitmapImage(new Uri(song.cover));
                string cutname = song.name.Substring(song.name.IndexOf('.') + 1);
                idSong = song.id;
                nameSong.Text = cutname.Trim();
                artis.Text = song.artist;
                checkSongAlbum(idSong);
                checkSongOffAlbum(idSong);
            }
            catch (Exception ee)
            {
                try
                {
                    brightness_reduce.Color = Colors.Black;
                    SongDownloaded song = listView.SelectedItem as SongDownloaded;
                    var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(song.source_list, CreationCollisionOption.OpenIfExists);
                    mediaElement.Source = new Uri(file.Path);
                    string cutname = song.name.Substring(song.name.IndexOf('.') + 1);
                    nameSong.Text = cutname;
                    artis.Text = file.Attributes.ToString();
                }catch(Exception eee)
                {

                }
               
            }
        }
       
        private async void addAlbumFile()
        {
            try
            {
                string a = "";
                Song song = listView.SelectedItem as Song;
                listAlbum.Add(song);
                var file = await ApplicationData.Current.LocalFolder.CreateFileAsync("album.txt", CreationCollisionOption.ReplaceExisting);
                for (int i = 0; i < listAlbum.Count; i++)
                {
                    a += listAlbum[i].id + ";";
                }
                FileIO.AppendTextAsync(file, a);
                like.Foreground = new SolidColorBrush(Colors.Red);
            }
            catch (Exception ee)
            {

            }

        }

        private async void removeAlbumFile()
        {
            string a = "";
            Song song = listView.SelectedItem as Song;
            listAlbum.Remove(song);
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync("album.txt", CreationCollisionOption.ReplaceExisting);
            for (int i = 0; i < listAlbum.Count; i++)
            {
                a += listAlbum[i].id + ";";
            }
            FileIO.AppendTextAsync(file, a);
            like.Foreground = new SolidColorBrush(Colors.White);
            if (artisDanhMuc.Text.Equals("TQT^^"))
            {
                listView.ItemsSource = null;
                listView.ItemsSource = listAlbum;
            }
        }
        public async void getListAlbum()
        {
            CutDataHtml CutDataHtml = new CutDataHtml();

            listAlbum.Clear();

            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync("album.txt", CreationCollisionOption.OpenIfExists);
            string strings = await FileIO.ReadTextAsync(file);
            string[] idArr = strings.Split(';');
            List<String> list = new List<string>();
            for (int i = 0; i < idArr.Length; i++)
            {
                list.Add(idArr[i]);
            }
            listAlbum = await CutDataHtml.cutDataHtmlAddList(list);
            listView.ItemsSource = null;
            listView.ItemsSource = listAlbum;
        }
        public void ListAlbum()
        {
            getListAlbum();
            senameMusic("ms-appx:///Assets/myalbum.jpg", "MY ALBUM", "TQT^^", "", "","","");
            theloai.Text = "";
            phathanh.Text = "";
        }
        private bool checkSongAlbum(string id)
        {
            try
            {
                foreach (Song song in listAlbum)
                {
                    if (song.id.Equals(id))
                    {
                        like.Foreground = new SolidColorBrush(Colors.Red);
                        return true;
                        break;
                    }
                    else
                    {
                        like.Foreground = new SolidColorBrush(Colors.White);
                    }
                }

            }
            catch (Exception e)
            {

            }
            return false;
        }

        private void Album(object sender, RoutedEventArgs e)
        {
            ListAlbum();
        }

        public void LikeForeg()
        {
            like.Foreground = new SolidColorBrush(Colors.White);
        }

        private void mediaElement_MediaOpened()
        {

        }

        private void like_Click(object sender, RoutedEventArgs e)
        {
            int a = listView.SelectedIndex;
            if (a != -1)
            {
                if (checkSongAlbum(idSong))
                    removeAlbumFile();
                else
                    addAlbumFile();
            }

        }

        //download
        public void DownLoadForeg()
        {
            downLoad.Foreground = new SolidColorBrush(Colors.Black);
        }

        private async Task<Boolean> downLoadMP3(String url, String name)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var data = await httpClient.GetByteArrayAsync(url);
                var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(name + ".mp3", CreationCollisionOption.ReplaceExisting);
                using (var targetStream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    await targetStream.AsStreamForWrite().WriteAsync(data, 0, data.Length);
                    await targetStream.FlushAsync();
                }
            }
            return true;
        }

        private async void addOffAlbumFile()
        {
            if (await isFileDowload(nameSong.Text))
            {

            }else
            {
                DateTime date = DateTime.Now;
                string a = "";

                try
                {
                    SongDownloaded songDownLoad = listView.SelectedItem as SongDownloaded;
                    if (songDownLoad.id != null)
                    {
                        downLoadMP3(songDownLoad.source_list, nameSong.Text);
                    }
                }
                catch (NullReferenceException ee)
                {
                    Song song = listView.SelectedItem as Song;
                    string cutname = song.name.Substring(song.name.IndexOf('.') + 1);
                    downLoadMP3(song.source_list[0], nameSong.Text);
                }
            }
        }

        private async void removeOffAlbumFile()
        {
            try
            {
                SongDownloaded songDownLoad = listView.SelectedItem as SongDownloaded;
                if (songDownLoad.id != null)
                {
                    mediaElement.Source = null;            
                    var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(nameSong.Text+".mp3", CreationCollisionOption.OpenIfExists);
                    MessageDialog a = new MessageDialog(nameSong.Text);
                    a.ShowAsync();
                    file.DeleteAsync();
                    nameSong.Text = "";
                    artis.Text = "";
                }
            }
            catch (NullReferenceException ee)
            {
                Song song = listView.SelectedItem as Song;
                var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(nameSong.Text + ".mp3", CreationCollisionOption.OpenIfExists);
                file.DeleteAsync();
            }
            if (artisDanhMuc.Text.Equals("TQT^^"))
            {
                getListOffAlbum();
                listView.ItemsSource = null;
                listView.ItemsSource = listOffAlbum;
            }
        }
        // list file music offline khi ấn vào button offline
        public async void getListOffAlbum()
        {
            var file = await ApplicationData.Current.LocalFolder.GetFilesAsync();

            List<SongDownloaded> litSongDownloaded = new List<SongDownloaded>();

            foreach (var item in file)
            {               
                if (item.FileType.Equals(".mp3"))
                {
                    SongDownloaded songDownloaded = new SongDownloaded();
                    songDownloaded.id = item.Name.ToString();
                    songDownloaded.name = item.DisplayName.ToString();
                    songDownloaded.artist = item.Attributes.ToString();
                    songDownloaded.source_list= item.Name.ToString();
                    litSongDownloaded.Add(songDownloaded);
                }
                  
            }
            listView.ItemsSource = litSongDownloaded;
        }
        public void ListOffAlbum()
        {
            getListOffAlbum();
            senameMusic("ms-appx:///Assets/myalbum.jpg", "MY OFFLINE ALBUM", "TQT^^", "", "","","");
            theloai.Text = "";
            phathanh.Text = "";
        }
        private bool checkSongOffAlbum(string id)
        {
            foreach (SongDownloaded song in listOffAlbum)
            {
                if (song.id.Equals(id))
                {
                    return true;
                    break;
                }
                else
                {
                }
            }
            return false;
        }

        private async void btnDownload(object sender, RoutedEventArgs e)
        {
            ListOffAlbum();
        }
        private async Task<Boolean> isFileDowload(String Name)
        {
            var file = await ApplicationData.Current.LocalFolder.GetFilesAsync();
            foreach (var item in file)
            {
                if (item.Name.Equals(Name+".mp3"))
                {                  
                    return true;
                }

            }
            return false;
        }

        private async void DownloadSong(object sender, RoutedEventArgs e)
        {
            Button btn = new Button();

            if (await isFileDowload(nameSong.Text))
            {
                var dialog = new ContentDialog()
                {
                    Title = "Download song",
                    //RequestedTheme = ElementTheme.Dark,
                    //FullSizeDesired = true,
                    MaxWidth = this.ActualWidth
                };

                // Setup Content
                var panel = new StackPanel();

                panel.Children.Add(new TextBlock
                {
                    Text = "Bạn có muốn xóa bài hát này không?",
                    //TextWrapping = TextWrapping.Wrap,
                });

                var cb = new CheckBox
                {
                    Content = "Đồng ý"
                };

                cb.SetBinding(CheckBox.IsCheckedProperty,
                    new Windows.UI.Xaml.Data.Binding
                    {
                        Source = dialog,
                        Path = new PropertyPath("IsPrimaryButtonEnabled"),
                        Mode = BindingMode.TwoWay,
                    });

                panel.Children.Add(cb);
                dialog.Content = panel;

                // Add Buttons
                dialog.PrimaryButtonText = "OK";
                dialog.IsPrimaryButtonEnabled = false;
                dialog.PrimaryButtonClick += delegate
                {
                    btn.Content = "Result: OK";
                    removeOffAlbumFile();
                    downLoad.Foreground = new SolidColorBrush(Colors.White);
                };

                dialog.SecondaryButtonText = "Cancel";
                dialog.SecondaryButtonClick += delegate
                {
                    btn.Content = "Result: Cancel";
                };

                // Show Dialog
                var result = await dialog.ShowAsync();
                if (result == ContentDialogResult.None)
                {
                    btn.Content = "Result: NONE";
                }           
                
            }
            else
            {
             
                var dialog = new ContentDialog()
                {
                    Title = "Download song",
                    //RequestedTheme = ElementTheme.Dark,
                    //FullSizeDesired = true,
                    MaxWidth = this.ActualWidth
                };

                // Setup Content
                var panel = new StackPanel();

                panel.Children.Add(new TextBlock
                {
                    Text = "Bạn có muốn download bài hát này không?",
                    //TextWrapping = TextWrapping.Wrap,
                });

                var cb = new CheckBox
                {
                    Content = "Đồng ý"
                };

                cb.SetBinding(CheckBox.IsCheckedProperty,
                    new Windows.UI.Xaml.Data.Binding
                    {
                        Source = dialog,
                        Path = new PropertyPath("IsPrimaryButtonEnabled"),
                        Mode = BindingMode.TwoWay,
                    });

                panel.Children.Add(cb);
                dialog.Content = panel;

                // Add Buttons
                dialog.PrimaryButtonText = "OK";
                dialog.IsPrimaryButtonEnabled = false;
                dialog.PrimaryButtonClick += delegate
                {
                    btn.Content = "Result: OK";
                    addOffAlbumFile();
                    downLoad.Foreground = new SolidColorBrush(Colors.Black);
                };
                dialog.SecondaryButtonText = "Cancel";
                dialog.SecondaryButtonClick += delegate
                {
                    btn.Content = "Result: Cancel";
                };
                // Show Dialog
                var result = await dialog.ShowAsync();
                if (result == ContentDialogResult.None)
                {
                    btn.Content = "Result: NONE";
                }
                
            }
        }
    }

}


