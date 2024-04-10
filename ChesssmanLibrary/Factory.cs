using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_21
{
    public class Factory
    {
        public static Chess GetInstanee(EnumChessColor color,EnumChessType type,MyPoint p)
        {
            Chess ch = null;
            switch (type)
            {
                case EnumChessType.车:
                    ch = new Che(color,p);
                        break;
                case EnumChessType.卒:
                    ch = new Bing(color, p);
                    break;
                case EnumChessType.士:
                    ch = new Shi(color, p);
                    break;
                case EnumChessType.象:
                    ch = new Xiang(color, p);
                    break;
                case EnumChessType.将:
                    ch = new Jiang(color, p);
                    break;
                case EnumChessType.炮:
                    ch = new Pao(color, p);
                    break;
                case EnumChessType.马:
                    ch = new Ma(color, p);
                    break;
                default:
                    break;
            }
            return ch;
        }
    }
}
