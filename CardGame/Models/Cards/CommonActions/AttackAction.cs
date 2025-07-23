using CardGame.Models.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Models.Cards.CommonActions
{
    public class AttackAction : ITargetableAction
    {
        private Character? target;
        public bool RequiresTarget => true; // 這個動作需要目標
        public void SetTarget(Character target)
        {
            this.target = target;
        }

        public void Apply(Character source, Character target)
        {
            if (target == null)
                throw new InvalidOperationException("No target set for AttackAction.");
            target.TakeDamage(source.GetFinalAttack());
        }
    }

}
