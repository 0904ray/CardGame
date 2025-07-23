using CardGame.Models.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Models.Cards.CommonActions
{
    public class DrawCardAction: IEffectAction
    {
        public int Count { get; set; }

        public DrawCardAction(int count)
        {
            Count = count;
        }

        public void Apply(Character source, Character _)
        {
            source.DrawCards(Count); 
        }
    }
}
