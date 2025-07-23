using CardGame.Models.Characters;
using CardGame.Models.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Models.Cards.CommonActions
{
    public class AddEffectAction : IEffectAction
    {
        private Effect effect;
        private int amount;

        public AddEffectAction(Effect effect, int amount)
        {
            this.effect = effect;
            this.amount = amount;
        }

        public void Apply(Character self, Character target)
        {
            var existing = self.effects.FirstOrDefault(e => e.GetType() == effect.GetType());
            if (existing != null)
            {
                existing.Multiplier += amount;
            }
            else
            {
                effect.Multiplier = amount;
                self.effects.Add(effect);
            }
        }
    }

}
