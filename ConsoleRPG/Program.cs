﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG
{
    class Program
    {
        static void Main(string[] args)
        {

            Game game = new Game();
            game.initGame();
            game.playing = true;
            game.activeCharacter = 0;
            game.fileName = "characters.txt";

            while (game.playing == true)
            {
                game.mainMenu();
            }
        }
    }
}
