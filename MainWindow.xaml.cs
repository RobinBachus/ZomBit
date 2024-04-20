using System.Reactive.Linq;
using System.Windows;

namespace ZomBit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
	    private double _initialHeight;
		private double _aspectRatio;

	    public MainWindow()
        {

			InitializeComponent();
	        Game.Initialize(Frame);

			Frame.Loaded += (_, _) =>
	        {
		        // Set aspect ratio on initialization
		        _initialHeight = Frame.ActualHeight;
		        _aspectRatio = Frame.ActualHeight / Frame.ActualWidth;
			};


	        // Keep aspect ratio on resize (16:9)
	        _ = Observable.FromEventPattern<SizeChangedEventArgs>(this, nameof(SizeChanged))
		        .Throttle(TimeSpan.FromMilliseconds(300)) // Avoids flickering
		        .ObserveOn(SynchronizationContext.Current ?? throw new Exception("SynchronizationContext.Current is null"))
		        .Subscribe(e => Resize_End(e.EventArgs));

		}

        private void Resize_End(SizeChangedEventArgs e)
        {
			if (_aspectRatio == 0 || _initialHeight == 0) return;
            if (e.HeightChanged) Width = e.NewSize.Height / _aspectRatio;
            else Height = e.NewSize.Width * _aspectRatio;
			Game.Scale = e.NewSize.Height / _initialHeight;
            UpdateLayout();
        }
    }
}