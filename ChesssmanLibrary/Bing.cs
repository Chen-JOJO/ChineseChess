using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.IO;

namespace _5_21
{
    public  class Bing:Chess
    {
        public Bing(EnumChessColor color, MyPoint p) : base(color, p)
        {
            this.Image = new BitmapImage();
            this.Image.BeginInit();
            if (this.Color == EnumChessColor.红)
            {
                this.Image.StreamSource = new MemoryStream(File.ReadAllBytes("images/红兵.gif"));
            }
            else
            {
                this.Image.StreamSource = new MemoryStream(File.ReadAllBytes("images/黑兵.gif"));
            }
            this.Image.EndInit();
            this.Type = EnumChessType.卒;
        }
        public override bool Move(MyPoint p)
        {
            bool res = false;
            //先判断棋子颜色
            if (Hong(p) )
            {
                //为红色
                if (HongGuoHe(p))
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
                //为黑色
                if (HeiGuoHe(p)) {
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
            ChessBoard board = ChessBoard.GetInstance();
            bool res = true;
            string ab = board[this.Poit.X, this.Poit.Y].CurrentChess.Color.ToString();
            if (ab=="黑")
            {
                res = false;
            }
            return res;
        }
        /// <summary>
        /// 判断红棋过河没有
        /// </summary>
        /// <returns></returns>
        public bool HongGuoHe(MyPoint p)
        {
            ChessBoard board = ChessBoard.GetInstance();
            bool res = false;
            if (this.Poit.Y <= 4)
            {
                res = HongGuoLeHe(p);
            }
            else
            {
                res = HongMeiGuoHe(p);
            }
            return res;
        }
        /// <summary>
        /// 判断有没有过河
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool HeiGuoHe(MyPoint p)
        {
            ChessBoard board = ChessBoard.GetInstance();
            bool res = false;
            if (this.Poit.Y >= 5)
            {
                res= HeiGuoLeHe(p);
            }
            else
            {
                res= HeiMeiGuoHe(p);
            }
            return res;
        }
        /// <summary>
        /// 判断走的步数是不是大于一并且X坐标不能变
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool HeiMeiGuoHe(MyPoint p)
        {
            int i = p.Y - this.Poit.Y;
            bool res = false;
            if (i==1&&this.Poit.X==p.X)
            {
                res= Kong(p);
            }
            return res;
        }
        /// <summary>
        /// 判断横走还是竖走
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool HeiGuoLeHe(MyPoint p)
        {
            int i = p.Y - this.Poit.Y;
            int j = Math.Abs(p.X - this.Poit.X);
            int k = this.Poit.Y - p.Y;
            bool res = false;
            if (i == 1 && this.Poit.X==p.X)
            {
                res= SuZou(p);
            }
            if ((j ==1||k==1) && this.Poit.Y == p.Y)
            {
                res= HengZou(p);
            }
            return res;
        }
        public bool HongMeiGuoHe(MyPoint p)
        {
            int i =  this.Poit.Y-p.Y ;
            bool res = false;
            if (i == 1 && this.Poit.X == p.X)
            {
                res = SuZou(p);
            }
            return res;
        }
        public bool HongGuoLeHe(MyPoint p)
        {
            int i = this.Poit.Y-p.Y;
            int j = this.Poit.X-p.X ;
            int k = p.X - this.Poit.X;
            bool res = false;
            if (i == 1 && this.Poit.X == p.X)
            {
                res = SuZou(p);
            }
            if ((j == 1 || k == 1) && this.Poit.Y == p.Y)
            {
                res = HengZou(p);
            }
            return res;
        }
        public bool SuZou(MyPoint p)
        {
            return Kong(p);
        }
        public bool HengZou(MyPoint p)
        {
            return Kong(p);
        }
        public bool Kong(MyPoint p)
        {
            bool res = true;
            ChessBoard board = ChessBoard.GetInstance();
            if (board[p.X, p.Y].CurrentChess != null) {
                res= Chi(p);
            }
            return res;
        }
        /// <summary>
        /// 吃子
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool Chi(MyPoint p)
        {
            ChessBoard board = ChessBoard.GetInstance();
            bool res = true;
            if (board[this.Poit.X,this.Poit.Y].CurrentChess.Color == board[p.X,p.Y].CurrentChess.Color)
            {
                res = false;
            }
            return res;
        }
    }
}
