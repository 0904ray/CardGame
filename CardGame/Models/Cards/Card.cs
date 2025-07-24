using CardGame.Models.Characters;
using CardGame.Models.CommonActions;
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

        public List<IAction> OnPlayActions { get; set; } = new();
        public List<IAction> OnDiscardActions { get; set; } = new();

        public virtual async Task PlayAsync(Character source)
        {
            foreach (var action in OnPlayActions)
            {
                ActionQueue.Instance.Enqueue(action);
            }
            await Task.CompletedTask;
        }


        public virtual async Task OnDiscard(Character source)
        {

            foreach (var action in OnDiscardActions)
            {
                ActionQueue.Instance.Enqueue(action);
            }
            await Task.CompletedTask;

        }

    }
}
