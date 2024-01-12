using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Journals.DesktopClient.ViewModels;
using Image = System.Drawing.Image;

namespace Journals.DesktopClient.Views
{
    /// <summary>
    /// Interaction logic for ContentView.xaml
    /// </summary>
    public partial class ContentView : UserControl
    {
        private readonly ContentViewModel _viewModel;
        public ContentView(ContentViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            _viewModel = viewModel;
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Document" && _viewModel.Document != null)
            {
                imageMemDC.Source =  RenderPageToMemDC(0, (int) imageMemDC.Width, (int) imageMemDC.Height);

                //imageMemDC.Source = await Task.Run(() => RenderPageToMemDC(0, (int) imageMemDC.Width, (int) imageMemDC.Height));
            }
        }

        private BitmapSource RenderPageToMemDC(int page, int width, int height)
        {
            var image = _viewModel.Document?.Render(page, width, height, 96, 96, false);
            return BitmapHelper.ToBitmapSource(image);
        }
    }

    internal class BitmapHelper
    {

        public static BitmapSource ToBitmapSource(Image image)
        {
            return ToBitmapSource(image as Bitmap);
        }

        /// <summary>
        /// Convert an IImage to a WPF BitmapSource. The result can be used in the Set Property of Image.Source
        /// </summary>
        /// <param name="bitmap">The Source Bitmap</param>
        /// <returns>The equivalent BitmapSource</returns>
        public static BitmapSource ToBitmapSource(System.Drawing.Bitmap bitmap)
        {
            if (bitmap == null) return null;

            using (System.Drawing.Bitmap source = (System.Drawing.Bitmap)bitmap.Clone())
            {
                IntPtr ptr = source.GetHbitmap(); //obtain the Hbitmap

                BitmapSource bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    ptr,
                    IntPtr.Zero,
                    System.Windows.Int32Rect.Empty,
                    System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

                NativeMethods.DeleteObject(ptr); //release the HBitmap
                bs.Freeze();
                return bs;
            }
        }

        public static BitmapSource ToBitmapSource(byte[] bytes, int width, int height, int dpiX, int dpiY)
        {
            var result = BitmapSource.Create(
                            width,
                            height,
                            dpiX,
                            dpiY,
                            PixelFormats.Bgra32,
                            null /* palette */,
                            bytes,
                            width * 4 /* stride */);
            result.Freeze();

            return result;
        }
    }

    internal static class NativeMethods
    {
        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);
    }
}

