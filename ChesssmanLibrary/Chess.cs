using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace _5_21
{
    public enum EnumChessType
    {
        车,
        马,
        炮,
        士,
        象,
        卒,
        将
    }
    public enum EnumChessColor
    {
        红,
        黑
    }
    public abstract class Chess
    {
        public BitmapImage Image { get; set; }
        public EnumChessType Type { get; set; }
        public Chess(EnumChessColor color,MyPoint p)
        {
            this.Color = color;
            this.Poit = p;
            p.CurrentChess = this;
        }
        public MyPoint Poit { get; set; }
        public EnumChessColor Color { get; set; }
        public abstract bool Move(MyPoint p); 
    }
}
