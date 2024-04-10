using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.IO;
using System.Media;

using _5_21;
using System.Reflection;
using Path = System.IO.Path;

namespace MyGame
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool res = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            bool r = false;
            this.Width = 919;
            this.Height = 620;
            ChessBoard board = ChessBoard.GetInstance();
            this.Fimg.Visibility = Visibility.Collapsed;
            this.Fimg1.Visibility = Visibility.Collapsed;
            board.GameImage = this.imgGame;
            this.ZouText.Visibility = Visibility.Visible;
            this.HouText.Visibility = Visibility.Collapsed;
            board.Show();
            board.Shengyu();
            ShenYu();
            res = true;
            Xianshi();
            Music(r);
        }

        private void imgGame_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ChessBoard board = ChessBoard.GetInstance();
            ChessBoard.GetInstance().Mouse_Click(this,e);
            ShenYu();
            Zou();
        }

        private void BtnStar_Click(object sender, RoutedEventArgs e)
        {
            ChessBoard board = ChessBoard.GetInstance();
            if (MessageBox.Show("是否重新开始?", "重新开始",MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                board.Fupan();
                board.Shengyu();
                ShenYu();
                Zou();
            }
        }

        private void BtnEsc_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            bool r = true;
            Xianshi();
            this.ResizeMode = ResizeMode.NoResize;
            Music(r);
        }
        public void Xianshi()
        {
            if (res)
            {
                this.BtnStar.IsEnabled = true;
            }
            else
            {
                this.BtnStar.IsEnabled = false;
            }
        }
        public void Zou()
        {
            ChessBoard board = ChessBoard.GetInstance();
            if (board.XingZou())
            {
                this.ZouText.Visibility = Visibility.Visible;
                this.HouText.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.HouText.Visibility = Visibility.Visible;
                this.ZouText.Visibility = Visibility.Collapsed;
            }
        }
        public void ShenYu()
        {
            ChessBoard board = ChessBoard.GetInstance();
            SyuHei.Text = board.Hei.ToString();
            SyuHong.Text = board.Hong.ToString();
        }
        public void Music(bool r)
        {
            SoundPlayer sp = new SoundPlayer();
            sp.SoundLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"/主题曲.wav";
            if (r)
            {
                sp.PlayLooping();
            }
            else
            {
                sp.Stop();
            }
            
        }
    }
}
