using CardGame.Models.Cards;
using CardGame.Models.Cards.SavannahCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Models.Characters
{
    public class Savannah : Character
    {
        public Savannah() : base("Savannah", 35, 6, 5, 1)
        {
            // 初始化卡組（Deck）
            Deck.AddRange(new List<Card>
            {
                new ArmsDealCard(this), // 軍火交易
                new ArmsDealCard(this),
                new ArmsDealCard(this),
                new ArmsDealCard(this),

                new ArmStorageCard(this), // 武器商店
                new ArmStorageCard(this),
                new ArmStorageCard(this),
                // 可依照設計加入更多卡片
            });

            Shuffle(Deck);
            // 起手抽牌
            DrawCards(5);
        }

        public override string ToString()
        {
            return $"{Name} HP: {Attr.Hp}/{Attr.MaxHp} SP: {Attr.Sp}/{Attr.MaxSp} KP: {Attr.Kp}/{Attr.MaxKp}";
        }

    }
}
