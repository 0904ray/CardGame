using CardGame.Models.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Models.Cards.CommonActions
{
    public class DiscardCardAction : IEffectAction
    {
        public int Count { get; set; }

        public DiscardCardAction(int count)
        {
            Count = count;
        }

        public void Apply(Character source, Character target)
        {
            var index;
            // 假設這裡會有一個方法讓玩家選擇要丟棄的卡牌索引
            source.DiscardCard(index); // 假設會讓玩家選擇
        }
    }

}
