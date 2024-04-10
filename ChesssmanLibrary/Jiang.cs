using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace _5_21
{
    public class Jiang:Chess
    {
        public ChessBoard board = ChessBoard.GetInstance();
        public Jiang(EnumChessColor color, MyPoint p) : base(color, p)
        {
            this.Image = new BitmapImage();
            this.Image.BeginInit();
            if (this.Color == EnumChessColor.红)
            {
                this.Image.StreamSource = new MemoryStream(File.ReadAllBytes("images/红将.gif"));
            }
            else
            {
                this.Image.StreamSource = new MemoryStream(File.ReadAllBytes("images/黑将.gif"));
            }
            this.Image.EndInit();
            this.Type = EnumChessType.将;
        }
        public override bool Move(MyPoint p)
        {
            bool res = false;
            if (Hong(p))
            {
                if (FeiJiang(p))
                {
                    this.Poit.CurrentChess = null;
                    this.Poit = p;
                    this.Poit.CurrentChess = this;
                    return res = true;
                }
                if (Handsome(p))
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
                if (FeiJiang(p))
                {
                    this.Poit.CurrentChess = null;
                    this.Poit = p;
                    this.Poit.CurrentChess = this;
                    return res = true;
                }
                if (Take(p))
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
        }
        /// <summary>
        /// 判断其颜色
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool Hong(MyPoint p)
        {
            bool res = false;
            if (this.Color == EnumChessColor.红)
            {
                res = true;
            }
            return res;
        }
        //黑将
        public bool Take(MyPoint p)
        {
            bool res = false;
            if ((p.X>2&&p.X<6)&&p.Y<3)
            {
                res = Zou(p);
            }
            return res;
        }
        //红帅
        public bool Handsome(MyPoint p)
        {
            bool res = false; 
            if (p.X > 2 && p.X < 6 && p.Y > 6)
            {
               res = Zou(p);
            }
            return res;
        }
        public bool Zou(MyPoint p)
        {
            bool res = false;
            //判断是横走还是竖走
            if (Math.Abs(this.Poit.X - p.X) == 1 && this.Poit.Y==p.Y)
            {
                res = Kong(p);
            }
            if(Math.Abs(this.Poit.Y - p.Y) == 1 && this.Poit.X == p.X)
            {
                res = Kong(p);
            }
            return res;
        }
        public bool FeiJiang(MyPoint p)
        {
            bool res = false;
            bool r = false ;
            Chess ch = board[p.X, p.Y].CurrentChess;
            int startx, endx;
            startx = this.Poit.Y < p.Y ? this.Poit.Y + 1 : p.Y + 1;
            endx = this.Poit.Y > p.Y ? this.Poit.Y : p.Y;
            for (int i = startx; i < endx; i++)
            {
                if (board[p.X, i].CurrentChess == null)
                {
                    //没有阻挡并都是帅
                    if (board[p.X, p.Y].CurrentChess != null&&ch.Type==EnumChessType.将)
                    {
                        r = true;
                    }
                
                }
                else
                {
                    r = false;
                    break;
                }
            }
            if (r)
            {
                res = Chi(p);
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
