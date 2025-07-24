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
    public class ArmStorageCard : Card
    {
        public ArmStorageCard(Character owner) { 
            Name = "武器儲備";
            Description = "Gain 1 SP and draw a card.";
            Cost = 1;

            OnPlayActions = new List<IAction>
            {
                new DrawCardAction(owner, 1),
                new DiscardCardAction(owner,1)
            };

            OnDiscardActions = new List<IAction>
            {
                new DrawCardAction(owner, 1),
                new AddEffectAction(owner, new WealthEffect(), 1)
            };
        }
    }
}
