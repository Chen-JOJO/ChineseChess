using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;

namespace _5_21
{
    public class Che:Chess
    {
        public Che(EnumChessColor color,MyPoint p):base(color,p)
        {
            this.Image = new BitmapImage();
            this.Image.BeginInit();
            if (this.Color==EnumChessColor.红)
            {
                this.Image.StreamSource = new MemoryStream(File.ReadAllBytes("images/红车.gif"));
            }
            else
            {
                this.Image.StreamSource = new MemoryStream(File.ReadAllBytes("images/黑车.gif"));
            }
            this.Image.EndInit();
            this.Type = EnumChessType.车;
        }
        public override bool Move(MyPoint p)
        {
            bool res = false;
            //先判断走的二是不是直线
            if (this.Poit.X==p.X||this.Poit.Y==p.Y)
            {
                //如果没有阻挡就可以走
                if (Zudang(p))
                {
                    if (Kong(p))
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
            else
            {
                return res;
            }
            
        }
        public bool Zudang(MyPoint p)
        {
            //判断是横着走，还是竖着走
            if (this.Poit.X==p.X)
            {
                //竖走
                return Shuzou(p);
            }
            else
            {
                //横走
                return Hengzou(p);
            }
        }
        /// <summary>
        /// 横着有阻挡，返回为true
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool Hengzou(MyPoint p)
        {
            ChessBoard board = ChessBoard.GetInstance();
            int startx,endx;
            startx = this.Poit.X < p.X ? this.Poit.X+1 : p.X+1;
            endx = this.Poit.X > p.X ? this.Poit.X : p.X ;
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
        /// <summary>
        /// 竖着走
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool Shuzou(MyPoint p)
        {
            ChessBoard board = ChessBoard.GetInstance();
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
        /// <summary>
        /// 判断目标的是否不同颜色的棋子
        /// </summary>
        /// <returns></returns>
        public bool Chi(MyPoint p)
        {
            ChessBoard board = ChessBoard.GetInstance();
            bool res = false;
            if (board[p.X, p.Y].CurrentChess.Color!=board[this.Poit.X,this.Poit.Y].CurrentChess.Color)
            {
                return res=true;
            }
            return res;
        }
        /// <summary>
        /// 判断是否有棋子
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
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
    }
}
