using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace GameOfLife
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Pola i wlasciwosci   
        private DispatcherTimer dispatcherTimer { get; set; }
        private ViewModel vm { get; set; }
        public String liveRules { get; set; }
        public String deadRules { get; set; }
        public int gridSize { get; set; }
        public int maxCycleAmount { get; set; }
        public int delay { get; set; }
        public int currentCycleAmount { get; set; }
        public ImageBrush ib { get; set; }
        #endregion

        #region Konstructor
        /// <summary>
        ///  Konstruktor klasy MainWindow    
        /// </summary>      
        public MainWindow()
        {
            InitializeComponent();
            vm = null;
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(timer_Tick);
        }
        #endregion
        #region Metody
        /// <summary>
        ///  Metoda klasy MainWindow jako calback metody obiekyu dispatcherTimer.Tick  
        /// </summary>  
        private void timer_Tick(object sender, EventArgs e)
        {
            if (maxCycleAmount <= currentCycleAmount)
            {
                dispatcherTimer.Stop();
            }
            vm.Next();
            currentCycleAmount++;
            CycleAmount.Text = currentCycleAmount.ToString();
        }

        /// <summary>
        ///  Metoda klasy MainWindow odpowiedzialna za obsluge eventu klikniecia lewim klawiszem myszy. 
        /// </summary> 
        private void Canvas_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            vm.mouseClick();
        }

        /// <summary>
        ///  Metoda klasy MainWindow odpowiedzialna za obsluge eventu ruchu myszy.  
        ///  Plus rozroznienie wcisnietego klawisza.
        /// </summary>       
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                vm.mouseMoveLeft();
            }
            if (e.RightButton == MouseButtonState.Pressed)
            {
                vm.mouseMoveRight();
            }
        }

        /// <summary>
        ///  Metoda klasy MainWindow odpowiedzialna korekte rozmiaru canvas.        
        /// </summary>   
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Plansza.Children.Count > 0)
            {
                vm.gameOfLifeModel.PaintGrid();
            }
        }

        /// <summary>
        ///  Metoda klasy MainWindow odpowiedzialna za obsluge nacisniecia buttona (create/start/stop),
        ///  oraz walidacje inputow
        /// </summary>  
        public void Start_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                gridSize = Int32.Parse(GridSize.Text);
                if (gridSize >= 100)
                {
                    gridSize = 100;
                }
                else if (gridSize < 0)
                {
                    gridSize = 0;
                }
                Regex rulesReg = new Regex(@"[0-9]+\/[0-9]+");
                Match rulesMatch = rulesReg.Match((String)Rules.Text);

                if (rulesMatch.Success)
                {
                    liveRules = (String)Rules.Text.Split('/').GetValue(1);
                    deadRules = (String)Rules.Text.Split('/').GetValue(0);

                }
                maxCycleAmount = Int32.Parse(maxCycle.Text);
                if (maxCycleAmount <= 0)
                {
                    maxCycleAmount = 0;
                }
                delay = Int32.Parse(cycleDelay.Text);
                if (delay <= 0)
                {
                    delay = 1;
                }
                dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, delay);

                if (vm == null)
                {
                    vm = new ViewModel(gridSize, liveRules, deadRules, Plansza);
                    vm.gameOfLifeModel.PaintGrid();
                    startBtn.Content = "Start";
                    return;
                }
                if (dispatcherTimer.IsEnabled)
                {
                    dispatcherTimer.Stop();
                    startBtn.Content = "Start";
                }
                else
                {
                    dispatcherTimer.Start();
                    startBtn.Content = "Stop";
                }
            }
            catch { }
        }

        /// <summary>
        ///  Metoda klasy MainWindow odpowiedzialna za obsluge nacisniecia buttona (reset)        
        /// </summary> 
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                vm.resetGrid();
                Plansza.Background = ib;
                currentCycleAmount = 0;
                CycleAmount.Text = currentCycleAmount.ToString();
                dispatcherTimer.Stop();
                startBtn.Content = "Create";
                vm = null;
            }
            catch { }
        }

        /// <summary>
        ///  Metoda klasy MainWindow obslugujaca event window loaded.
        ///  Oraz dodanie backgound.
        /// </summary> 
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri(@"resource/bg.jpg", UriKind.Relative));
            Plansza.Background = ib;
        }

        #endregion
    }
}


