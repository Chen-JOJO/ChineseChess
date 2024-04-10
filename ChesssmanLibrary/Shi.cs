using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace _5_21
{
    public class Shi : Chess
    {
        public ChessBoard board = ChessBoard.GetInstance(); 
        public Shi(EnumChessColor color, MyPoint p) : base(color, p)
        {
            this.Image = new BitmapImage();
            this.Image.BeginInit();
            if (this.Color == EnumChessColor.红)
            {
                this.Image.StreamSource = new MemoryStream(File.ReadAllBytes("images/红士.gif"));
            }
            else
            {
                this.Image.StreamSource = new MemoryStream(File.ReadAllBytes("images/黑士.gif"));
            }
            this.Image.EndInit();
            this.Type = EnumChessType.士;
        }
        public override bool Move(MyPoint p)
        {
            bool res = false;
            if (Hong(p))
            {
                //为红色棋子
                if (HongZuoBiao(p))
                {
                    this.Poit.CurrentChess = null;
                    this.Poit = p;
                    this.Poit.CurrentChess = this;
                    return res = true;
                }
                else
                {
                    return res;
                }
            }
            else
            {
                //为黑色棋子
                if (HeiZuoBiao(p)) {
                    this.Poit.CurrentChess = null;
                    this.Poit = p;
                    this.Poit.CurrentChess = this;
                    return res = true;
                }
                else
                {
                    return res;
                }
            }
        }
        /// <summary>
        /// 判断颜色
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool Hong(MyPoint p)
        {
            bool res = false;
            if (this.Color==EnumChessColor.红)
            {
                res = true;
            }
            return res; 
        }
        public bool HeiZuoBiao(MyPoint p)
        {
            bool res = false;
            List<MyPoint> list = new List<MyPoint>()
            {
                new MyPoint { X = 3,Y=0},
                new MyPoint {X=4,Y=1 },
                new MyPoint {X=3,Y=2},
                new MyPoint {X=5,Y=0},
                new MyPoint {X=5,Y=2},
            };
            if (Math.Abs(this.Poit.X-p.X)==1 && Math.Abs(this.Poit.Y-p.Y)==1)
            {
                foreach(MyPoint stu in list)
                {
                    if (stu.X == p.X && stu.Y == p.Y)
                    {
                        res = Kong(p);
                    }
                }
            }
            return res;
        }
        public bool HongZuoBiao(MyPoint p)
        {
            bool res = false;
            List<MyPoint> list = new List<MyPoint>()
            {
                new MyPoint { X = 3,Y=9},
                new MyPoint {X=4,Y=8 },
                new MyPoint {X=3,Y=7},
                new MyPoint {X=5,Y=9},
                new MyPoint {X=5,Y=7},
            };
            if (Math.Abs(this.Poit.X - p.X) == 1 && Math.Abs(this.Poit.Y - p.Y) == 1)
            {
                foreach (MyPoint stu in list)
                {
                    if (stu.X == p.X && stu.Y == p.Y)
                    {
                        res = Kong(p);
                    }
                }
            }
            return res;
        }
        public bool Kong(MyPoint p)
        {
            bool res = true;
            ChessBoard board = ChessBoard.GetInstance();
            if (board[p.X, p.Y].CurrentChess != null)
            {
                res = Chi(p);
            }
            return res;
        }
        public bool Chi(MyPoint p)
        {
            ChessBoard board = ChessBoard.GetInstance();
            bool res = true;
            if (board[this.Poit.X, this.Poit.Y].CurrentChess.Color == board[p.X, p.Y].CurrentChess.Color)
            {
                res = false;
            }
            return res;
        }
    }
}
