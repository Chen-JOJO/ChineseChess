using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows;

namespace _5_21
{
    public class Ma:Chess
    {
        public ChessBoard board = ChessBoard.GetInstance();     
        public Ma(EnumChessColor color,MyPoint p) : base(color, p)
        {
            this.Image = new BitmapImage();
            this.Image.BeginInit();
            if (this.Color == EnumChessColor.红)
            {
                this.Image.StreamSource = new MemoryStream(File.ReadAllBytes("images/红马.gif"));
            }
            else
            {
                this.Image.StreamSource = new MemoryStream(File.ReadAllBytes("images/黑马.gif"));
            }
            this.Image.EndInit();
            this.Type = EnumChessType.马;
        }
        public override bool Move(MyPoint p)
        {
            bool res = false;
            //判断走法时候合格
            if (ZouFa(p))
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
        public bool ZouFa(MyPoint p)
        {
            bool res = false;
            //横走
            if (Math.Abs(Poit.X - p.X) == 2 && Math.Abs(Poit.Y - p.Y) == 1)
            {
                res = HengBieJiao(p);
            }
            //竖走
            if (Math.Abs(Poit.Y - p.Y) == 2 && Math.Abs(Poit.X - p.X) == 1)
            {
                res = SuBeiJiao(p);
            }
            return res;
        }
        public bool HengBieJiao(MyPoint p)
        {
            bool res = false;
            if (board[(Poit.X+p.X)/2,Poit.Y].CurrentChess==null)
            {
                res = Kong(p);
            }
            return res;
        }
        public bool SuBeiJiao(MyPoint p)
        {
            bool res = false;
            if (board[Poit.X , (Poit.Y+p.Y)/2].CurrentChess == null)
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
