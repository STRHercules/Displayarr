using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Displayarr
{
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer _timer;
        private readonly List<BitmapImage> _posters = new();
        private int _currentIndex = 0;
        private readonly PlexClient _client;

        public MainWindow()
        {
            InitializeComponent();
            _client = new PlexClient("http://localhost:32400", "YOUR_TOKEN_HERE");
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMinutes(1); // change this for poster duration
            _timer.Tick += OnTick;
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var posterUrls = await _client.GetRecentlyAddedPosterUrlsAsync();
            foreach (var url in posterUrls)
            {
                var bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.UriSource = new Uri(url);
                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.EndInit();
                _posters.Add(bmp);
            }

            if (_posters.Count > 0)
            {
                PosterImage.Source = _posters[0];
                _timer.Start();
            }
        }

        private void OnTick(object? sender, EventArgs e)
        {
            if (_posters.Count == 0) return;

            _currentIndex = (_currentIndex + 1) % _posters.Count;
            PosterImage.Source = _posters[_currentIndex];
        }
    }
}
