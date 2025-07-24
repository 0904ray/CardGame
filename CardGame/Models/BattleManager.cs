using CardGame.Models.Characters;
using CardGame.Models.CommonActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Models
{
    public class BattleManager
    {
        private Player _player1;
        private Player _player2;

        public BattleManager(Player player1, Player player2)
        {
            _player1 = player1;
            _player2 = player2;
        }

        public async Task StartGameAsync()
        {
            while (!_player1.IsDefeated && !_player2.IsDefeated)
            {
                await _player1.TakeTurnAsync(_player2);
                if (_player2.IsDefeated) break;

                await _player2.TakeTurnAsync(_player1);
            }

            Console.WriteLine(_player1.IsDefeated ? $"{_player2.Name} 獲勝！" : $"{_player1.Name} 獲勝！");
        }

        public List<Character> GetAllCharacters()
        {
            return _player1.Characters.Concat(_player2.Characters).ToList();
        }

    }

}
