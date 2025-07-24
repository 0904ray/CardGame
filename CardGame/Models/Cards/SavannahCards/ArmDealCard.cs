using CardGame.Models.Cards.CommonActions;
using CardGame.Models.Characters;
using CardGame.Models.CommonActions;
using CardGame.Models.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Models.Cards.SavannahCards
{
    public class ArmsDealCard : Card
    {
        public ArmsDealCard()
        {
            Name = "軍火交易";
            Cost = 1;

            OnPlayActions = new List<IEffectAction>
            {
                new DiscardCardAction(1),                             // 自身棄1
                new AddEffectAction(new WealthEffect(), 1),           // 加1財富
                new AttackAction()                                   // 打擊1目標
            };

            OnDiscardActions = new List<IEffectAction>
            {
                new AttackAction(),
                new DrawCardAction(1)
            };
        }
        public override async Task OnDiscard(Character source, Func<IEffectAction, Task<Character?>> resolveTarget)
        {
            var wealth = source.effects.OfType<WealthEffect>().FirstOrDefault();
            if (wealth != null && wealth.Multiplier >= 1)
            {
                wealth.Multiplier -= 1; // 減少財富效果
                if (wealth.Multiplier <= 0)
                {
                    source.effects.Remove(wealth); // 如果財富效果歸零，則移除
                }

                foreach (var action in OnDiscardActions)
                {
                    Character? target = null;

                    // 如果是 ITargetableAction 就等 UI 指定目標
                    if (action is ITargetableAction)
                    {
                        target = await resolveTarget(action); // UI 自己彈窗回傳
                    }

                    action.Apply(source, target ?? source); // 預設 target 是自己
                }
            }
            else
            {
                // 如果沒有財富效果，則不執行 OnDiscardActions
                Console.WriteLine("沒有足夠的財富效果，無法執行棄牌後動作。");
            }
        }

    }
}
