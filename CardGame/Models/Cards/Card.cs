using CardGame.Models.Characters;
using CardGame.Models.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Models.Cards
{
    public class Card
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }

        public List<IEffectAction> OnPlayActions { get; set; } = new();
        public List<IEffectAction> OnDiscardActions { get; set; } = new();

        public virtual async Task PlayAsync(Character source, Func<IEffectAction, Task<Character?>> resolveTarget)
        {
            foreach (var action in OnPlayActions)
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

        public virtual async Task OnDiscard(Character source, Func<IEffectAction, Task<Character?>> resolveTarget)
        {

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

    }
}
