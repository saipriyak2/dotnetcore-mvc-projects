using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathApp.Models
{
    internal class Game
    {
        public DateTime Date { get;   set;}
        public int Score { get; set; }
        public GameType Type { get; set; }

    }

    internal enum GameType
    {
        Addititon,
        Substraction,
        Multiplication,
        Division
    }
}
