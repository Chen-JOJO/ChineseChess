using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace _5_21
{
    public class Xiang:Chess
    {
        public ChessBoard board = ChessBoard.GetInstance();
        public Xiang(EnumChessColor  color,MyPoint p):base(color ,p)
        {
            this.Image = new BitmapImage();
            this.Image.BeginInit();
            if (this.Color == EnumChessColor.红)
            {
                this.Image.StreamSource = new MemoryStream(File.ReadAllBytes("images/红象.gif"));
            }
            else
            {
                this.Image.StreamSource = new MemoryStream(File.ReadAllBytes("images/黑象.gif"));
            }
            this.Image.EndInit();
            this.Type = EnumChessType.象;
        }
        public override bool Move(MyPoint p)
        {
            bool res = false;
            if (Hong(p))
            {
                if (HongXiang(p))
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
                if (HeiXiang(p))
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
        /// 判断颜色
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
        public bool HeiXiang(MyPoint p)
        {
            bool res = false;
            if (p.Y <= 4 )
            {
                if (Math.Abs(this.Poit.X - p.X) == 2 && Math.Abs(this.Poit.Y - p.Y) == 2)
                {
                    res = ZuDang(p);
                }
            }
            return res;
        }
        public bool HongXiang(MyPoint p)
        {
            bool res = false;
            if (p.Y >= 5)
            {
                if(Math.Abs(this.Poit.X - p.X) == 2 && Math.Abs(this.Poit.Y - p.Y) == 2)
                {
                    res = ZuDang(p);
                }
            }
            return res;
        }
        /// <summary>
        /// 判断有没有阻挡
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        /// 
        public bool ZuDang(MyPoint p)
        {
            bool res = false;
            int x = (this.Poit.X + p.X) / 2;
            int y = (this.Poit.Y + p.Y) / 2;
            if (board[x,y].CurrentChess == null)
            {
                res = Kong(p);
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
