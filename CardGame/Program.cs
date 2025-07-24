using CardGame.Models;
using CardGame.Models.Characters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardGame
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var player1 = new Player("玩家1", new List<Character>
            {
                new Savannah(),
                new Savannah()
            });

            var player2 = new Player("玩家2", new List<Character>
            {
                new Savannah(),
                new Savannah()
            });

            var battleManager = new BattleManager(player1, player2);
            await battleManager.StartGameAsync();
        }
    }
}
