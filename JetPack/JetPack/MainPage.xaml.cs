using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JetPack
{
    public partial class MainPage : ContentPage
    {
        bool pageIsActive;
        int fps = 30;

        public MainPage()
        {
            InitializeComponent();

            canvasView.PaintSurface += OnPaintSample;
            canvasView.Touch += OnTouch;
            Content = canvasView;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            pageIsActive = true;
            AnimationLoop();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            pageIsActive = false;
        }

        async Task AnimationLoop()
        {
            while (pageIsActive)
            {
                canvasView.InvalidateSurface();
                await Task.Delay(TimeSpan.FromSeconds(1.0 / fps));
            }
        }

        private void OnPaintSample(object sender, SKPaintSurfaceEventArgs e)
        {
            float surfaceWidth = e.Info.Width;
            float surfaceHeight = e.Info.Height;
            SKCanvas canvas = e.Surface.Canvas;
            Player player = new Player(10, 10, 50, 50);
            player.Draw(canvas, surfaceWidth, surfaceHeight);
        }

        private void OnTouch(object sender, SKTouchEventArgs e)
        {
            
        }
    }
}
