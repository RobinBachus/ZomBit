using System.Reactive.Linq;
using System.Windows;

namespace ZomBit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Game? Game { get; private set; }
        private const double ASPECT_RATIO = 0.5625;

        public MainWindow()
        {
            InitializeComponent();
			Game.Initialize(Frame);

            // Keep aspect ratio on resize (16:9)
            _ = Observable.FromEventPattern<SizeChangedEventArgs>(this, nameof(SizeChanged))
                .Throttle(TimeSpan.FromMilliseconds(300)) // Avoids flickering
                .ObserveOn(SynchronizationContext.Current ?? throw new Exception(""))
                .Subscribe(e => Resize_End(e.EventArgs));
        }

        private void Resize_End(SizeChangedEventArgs e)
        {
            if (e.HeightChanged) Width = e.NewSize.Height / ASPECT_RATIO;
            else Height = e.NewSize.Width * ASPECT_RATIO;
            UpdateLayout();
        }
    }
}