using API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Game_EntryPoint : EntryPoint
    {
        static void Main(string[] args)
        {
            var game = new TestGame(new API_Application());
            exec(game.Application);
        }
    }
}
