using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;
using System.Web;
using System.Media;
using System.Reflection;

namespace _5_21
{
    public class ChessBoard
    {
        public Chess CurrentChess { get; set; }
        public Image GameImage { get; set; }
        public int Hong { get; set; }
        public int Hei { get; set; }
        public  string Zou { get; set; }
        public BitmapImage GameBitmapImag { get; set; }
        private static ChessBoard Board;
        public int num = 1;

        public static ChessBoard GetInstance()
        {
            if (Board == null)
            {
                Board = new ChessBoard();
                //构造棋子
                Board.InitChessBoard();
            }
            return Board;
        }
        public MyPoint[,] Points { get; set; }
        private ChessBoard()
        {
            this.GameBitmapImag = new BitmapImage();
            this.GameBitmapImag.BeginInit();
            this.GameBitmapImag.StreamSource = new MemoryStream(File.ReadAllBytes("images/棋盘.jpg"));
            this.GameBitmapImag.EndInit();
            //构造棋盘，构造坐标点
            Points = new MyPoint[10, 11];
            for (int i=0;i<10;i++)
            {
                for (int j=0;j<11;j++)
                {
                    this.Points[i,j] = new MyPoint() { X = i, Y = j };
                }
            }
        }
        public void InitChessBoard() 
        {
            //车
            _ = Factory.GetInstanee(EnumChessColor.黑, EnumChessType.车, Points[0, 0]);
            _ = Factory.GetInstanee(EnumChessColor.黑, EnumChessType.车, Points[8, 0]);
            _ = Factory.GetInstanee(EnumChessColor.红, EnumChessType.车, Points[0, 9]);
            _ = Factory.GetInstanee(EnumChessColor.红, EnumChessType.车, Points[8, 9]);
            //马
            _ = Factory.GetInstanee(EnumChessColor.黑, EnumChessType.马, Points[1, 0]);
            _ = Factory.GetInstanee(EnumChessColor.黑, EnumChessType.马, Points[7, 0]);
            _ = Factory.GetInstanee(EnumChessColor.红, EnumChessType.马, Points[1, 9]);
            _ = Factory.GetInstanee(EnumChessColor.红, EnumChessType.马, Points[7, 9]);
            //象
            _ = Factory.GetInstanee(EnumChessColor.黑, EnumChessType.象, Points[2, 0]);
            _ = Factory.GetInstanee(EnumChessColor.黑, EnumChessType.象, Points[6, 0]);
            _ = Factory.GetInstanee(EnumChessColor.红, EnumChessType.象, Points[2, 9]);
            _ = Factory.GetInstanee(EnumChessColor.红, EnumChessType.象, Points[6, 9]);
            //士
            _ = Factory.GetInstanee(EnumChessColor.黑, EnumChessType.士, Points[3, 0]);
            _ = Factory.GetInstanee(EnumChessColor.黑, EnumChessType.士, Points[5, 0]);
            _ = Factory.GetInstanee(EnumChessColor.红, EnumChessType.士, Points[3, 9]);
            _ = Factory.GetInstanee(EnumChessColor.红, EnumChessType.士, Points[5, 9]);
            //将
            _ = Factory.GetInstanee(EnumChessColor.黑, EnumChessType.将, Points[4, 0]);
            _ = Factory.GetInstanee(EnumChessColor.红, EnumChessType.将, Points[4, 9]);
            //炮
            _ = Factory.GetInstanee(EnumChessColor.黑, EnumChessType.炮, Points[1, 2]);
            _ = Factory.GetInstanee(EnumChessColor.黑, EnumChessType.炮, Points[7, 2]);
            _ = Factory.GetInstanee(EnumChessColor.红, EnumChessType.炮, Points[1, 7]);
            _ = Factory.GetInstanee(EnumChessColor.红, EnumChessType.炮, Points[7, 7]);
            //兵
            _ = Factory.GetInstanee(EnumChessColor.黑, EnumChessType.卒, Points[0, 3]);
            _ = Factory.GetInstanee(EnumChessColor.黑, EnumChessType.卒, Points[2, 3]);
            _ = Factory.GetInstanee(EnumChessColor.黑, EnumChessType.卒, Points[4, 3]);
            _ = Factory.GetInstanee(EnumChessColor.黑, EnumChessType.卒, Points[6, 3]);
            _ = Factory.GetInstanee(EnumChessColor.黑, EnumChessType.卒, Points[8, 3]);
            _ = Factory.GetInstanee(EnumChessColor.红, EnumChessType.卒, Points[0, 6]);
            _ = Factory.GetInstanee(EnumChessColor.红, EnumChessType.卒, Points[2, 6]);
            _ = Factory.GetInstanee(EnumChessColor.红, EnumChessType.卒, Points[4, 6]);
            _ = Factory.GetInstanee(EnumChessColor.红, EnumChessType.卒, Points[6, 6]);
            _ = Factory.GetInstanee(EnumChessColor.红, EnumChessType.卒, Points[8, 6]);
        }
        public void Show()
        {
            //this.GameImage.Source = this.GameBitmapImag;
            //展示棋盘
            var target = new RenderTargetBitmap(517,562,96,96,PixelFormats.Pbgra32);
            var visual = new DrawingVisual();
            using (var r=visual.RenderOpen())
            {
                //先画背景图
                r.DrawImage(this.GameBitmapImag, new Rect(0, 0, 517,562));
                //画所有的棋子
                for(int i = 0; i < 9; i++)
                {
                    for(int j = 0; j < 10; j++)
                    {
                        if (this[i, j].CurrentChess != null)
                        {
                            Chess ch = this[i, j].CurrentChess;
                            r.DrawImage(ch.Image, new Rect(i * 53 + 46 - 26, j * 52 + 41 - 19, 51, 52));

                        }
                    }
                }
                if (CurrentChess != null)
                {
                    r.DrawImage(CurrentChess.Image, new Rect(this.CurrentChess.Poit.X * 53 + 46 - 26, this.CurrentChess.Poit.Y * 52 + 41 - 19, 55, 58));
                }
            }
              target.Render(visual);
              this.GameImage.Source = target;
        }
        public void Mouse_Click(object sender, MouseButtonEventArgs e)
        {
            var p = e.GetPosition((IInputElement)this.GameImage.Parent);
            int x, y;
            x = (int)((p.X-20)/53);
            y = (int)((p.Y-21)/52);
            Mouse_Click(x,y);
        }
        public void Mouse_Click(int x,int y)
        {
            //为单数红方走
            if (num%2!=0)
            {
                if (this.CurrentChess == null)
                {
                    if (this[x, y].CurrentChess != null&& this[x, y].CurrentChess.Color == EnumChessColor.红)
                    {
                        this.CurrentChess = this[x, y].CurrentChess;
                    }
                }
                else
                {
                    if (this.CurrentChess.Move(this[x, y]))
                    {
                        this.CurrentChess = null;
                        Shengyu();
                        Music();
                        num++;
                    }
                    else
                    {
                        this.CurrentChess = null;
                    }
                }
            }
            else
            {
                if (this.CurrentChess == null)
                {
                    if (this[x, y].CurrentChess != null && this[x,y].CurrentChess.Color == EnumChessColor.黑)
                    {
                        this.CurrentChess = this[x, y].CurrentChess;
                    }
                }
                else
                {
                    if (this.CurrentChess.Move(this[x, y]))
                    {
                        this.CurrentChess = null;
                        Shengyu();
                        Music();
                        num++;
                    }
                    else
                    {
                        this.CurrentChess = null;
                    }
                }
            }
            Show();
            Panduan();
        }
        public MyPoint this[int x,int y]
        {
            get
            {
                return this.Points[x,y];
            }
        }
        public void Panduan()
        {
            int num = 0;
            int num1 = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (this[i, j].CurrentChess != null)
                    {
                        Chess ch = this[i, j].CurrentChess;
                        if (ch.Type == EnumChessType.将 && ch.Color == EnumChessColor.黑)
                        {
                            num++;
                        }
                        if (ch.Type == EnumChessType.将 && ch.Color == EnumChessColor.红)
                        {
                            num1++;
                        }
                    }
                }
            }
            if (num == 0)
            {
                MessageBox.Show("黑方输了,胜败乃兵家常事");
                Shenli();
            }
            if (num1 == 0)
            {
                MessageBox.Show("红方输了,胜败乃兵家常事");
                Shenli();
            }
        }
        public void Fupan()
        {
            for (int i = 0; i <9; i++)
                for (int j =0 ; j < 10; j++)
                {
                    //初始化棋子
                    this[i,j].CurrentChess = null;
                }
            Board.InitChessBoard ();
            Show();
            num = 1;
        }
        public bool XingZou()
        {
            bool res = false;
            if (num%2!=0)
            {
                res = true;
            }
            else
            {
                res = false;
            }
            return res;
        }
        public void Shengyu()
        {
            Hong = 0;
            Hei = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (this[i, j].CurrentChess != null)
                    {
                        Chess ch = this[i, j].CurrentChess;
                        if (ch.Color == EnumChessColor.红)
                        {
                            Hong++;
                        }
                        if (ch.Color == EnumChessColor.黑)
                        {
                            Hei++;
                        }
                    }
                }
            }
        }
        public void Music()
        {
            SoundPlayer sp = new SoundPlayer();
            sp.SoundLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"/象棋.wav";
            sp.Play();
        }
        public void Shenli()
        {
            SoundPlayer sp = new SoundPlayer();
            sp.SoundLocation= Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"/胜利.wav";
            sp.Play();
        }
    }
}
