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
            SKPaint paint = new SKPaint();
            paint.Color = Color.Beige.ToSKColor();
            paint.Style = SKPaintStyle.Fill;
            canvas.DrawCircle(new SKPoint(surfaceWidth / 2, surfaceHeight / 2), surfaceWidth/2 - 50, paint);
        }

        private void OnTouch(object sender, SKTouchEventArgs e)
        {
            
        }
    }
}
