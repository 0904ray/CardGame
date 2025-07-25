using CardGame.Models.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Models.CommonActions
{
    public class DiscardCardAction : IAction
    {
        private readonly int _count;
        private Character? _source;

        public DiscardCardAction(Character source,int count)
        {
            _count = count;
            _source = source;
        }

        public string Description => throw new NotImplementedException();

        public async Task ExecuteAsync()
        {
            if (_source == null) return;
            if (_source.Hand.Count < _count)
            {
                Console.WriteLine($"{_source.Name} 的手牌不足 {_count} 張，無法執行棄牌動作。");
                return;
            }
            // 讓使用者選擇要丟哪些牌
            var cardsToDiscard = await _source.ChooseCardsAsync(_count); // List<Card>

            foreach (var card in cardsToDiscard)
            {
                // 棄牌
                _source.DiscardCard(card);

                Console.WriteLine($"{_source.Name} 棄掉了 {card.Name}");

                // 問是否要發動棄牌效果
                if (card.OnDiscardActions != null)
                {
                    bool shouldActivate = true;

                    Console.WriteLine($"要發動 {card.Name} 的棄牌效果嗎？(y/n)");
                    shouldActivate = Console.ReadLine()?.ToLower() == "y";

                    await card.OnDiscard(_source);
                }
            }
        }

    }


}
