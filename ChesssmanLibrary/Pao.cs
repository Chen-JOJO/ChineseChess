using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace _5_21
{
    public class Pao:Chess
    {
        ChessBoard board = ChessBoard.GetInstance();
        public Pao(EnumChessColor color, MyPoint p) : base(color, p)
        {
            this.Image = new BitmapImage();
            this.Image.BeginInit();
            if (this.Color == EnumChessColor.红)
            {
                this.Image.StreamSource = new MemoryStream(File.ReadAllBytes("images/红炮.gif"));
            }
            else
            {
                this.Image.StreamSource = new MemoryStream(File.ReadAllBytes("images/黑炮.gif"));
            }
            this.Image.EndInit();
            this.Type = EnumChessType.炮;
        }
        public override bool Move(MyPoint p)
        {
            bool res = false;
            //判断走的是不是直线
            if (this.Poit.X==p.X||this.Poit.Y==p.Y)
            {
                if (Dudang(p))
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
                return res;
            }
        }
        //先判断他有没有阻挡,有阻挡为吃，没阻挡为走
        public bool Dudang(MyPoint p)
        {
            bool res = false;
            if (this.Poit.X == p.X)
            {
                //竖走
                if (Shuzou(p))
                {
                    //没有阻挡
                    res = PaoZou(p);
                }
                else
                {
                    //有阻挡
                    res=SubaoChi(p);
                }
            }
            else
            {
                //横走
                if (Hengzou(p))
                {
                    //没有阻挡
                    res = PaoZou(p);
                }
                else
                {
                    //有阻挡
                    res = HengbaoChi(p);
                }
            }
            return res;
        }
        public bool Shuzou(MyPoint p)
        {
            int startx, endx;
            startx = this.Poit.Y < p.Y ? this.Poit.Y + 1 : p.Y + 1;
            endx = this.Poit.Y > p.Y ? this.Poit.Y : p.Y;
            bool res = true;
            for (int i = startx; i < endx; i++)
            {
                if (board[p.X, i].CurrentChess != null)
                {
                    //有阻挡
                    res = false;
                    break;
                }
            }
            return res;
        }
        public bool Hengzou(MyPoint p)
        {
            int startx, endx;
            startx = this.Poit.X < p.X ? this.Poit.X + 1 : p.X + 1;
            endx = this.Poit.X > p.X ? this.Poit.X : p.X;
            bool res = true;
            for (int i = startx; i < endx; i++)
            {
                if (board[i, p.Y].CurrentChess != null)
                {
                    //有阻挡
                    res = false;
                    break;
                }
            }
            return res;
        }
        public bool SubaoChi(MyPoint p)
        {
            int startx, endx, num=0;
            startx = this.Poit.Y < p.Y ? this.Poit.Y + 1 : p.Y + 1;
            endx = this.Poit.Y > p.Y ? this.Poit.Y : p.Y;
            bool res = false;
            for (int i = startx; i < endx; i++)
            {
                if (board[p.X, i].CurrentChess != null)
                {
                    //有阻挡+1
                    num=num+1;
                }
            }
            if (num == 1)
            {
                res = Kong(p);
            }
            return res;
        }
        public bool HengbaoChi(MyPoint p)
        {
            int startx, endx, num = 0;
            startx = this.Poit.X < p.X ? this.Poit.X + 1 : p.X + 1;
            endx = this.Poit.X > p.X ? this.Poit.X : p.X;
            bool res = false ;
            for (int i = startx; i < endx; i++)
            {
                if (board[i, p.Y].CurrentChess != null)
                {
                    //有阻挡+1
                    num = num + 1;
                }
            }
            if (num == 1)
            {
                res = Kong(p);
            }
            return res;
        }
        public bool PaoChi(MyPoint p)
        {
            bool res = false;
            if (board[this.Poit.X, this.Poit.Y].CurrentChess.Color != board[p.X, p.Y].CurrentChess.Color)
            {
                res = true;
            }
            return res;
        }
        public bool Kong(MyPoint p)
        {
            bool res = false;
            if(board[p.X, p.Y].CurrentChess != null)
            {
                res=PaoChi(p);
            }
            return res;
        }
        public bool PaoZou(MyPoint p)
        {
            bool res = false;
            if (board[p.X, p.Y].CurrentChess == null)
            {
                res = true;
            }
            return res;
        }

    }
}
