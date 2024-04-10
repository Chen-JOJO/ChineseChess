using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_21
{
    class Program
    {
        static void Main(string[] args)
        {
            
            ChessBoard.GetInstance().Show();
            Console.ReadLine();
            Console.WriteLine("走棋");
            ChessBoard.GetInstance().Points[6,0].CurrentChess.Move(ChessBoard.GetInstance().Points[6, 1]);
            ChessBoard.GetInstance().Show();
            Console.ReadLine();
        }
    }
}
