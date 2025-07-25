using CardGame.Models.Characters;
using CardGame.Models.CommonActions;
using CardGame.Models.Effects;


namespace CardGame.Models.Cards.SavannahCards
{
    public class ArmsDealCard : Card
    {
        public ArmsDealCard(Character owner)
        {
            Name = "軍火交易";
            Cost = 1;

            OnPlayActions = new List<IAction>
            {
                new DiscardCardAction(owner,1),
                new AddEffectAction(owner, new WealthEffect(), 1),
                new AttackAction(owner)
            };

            OnDiscardActions = new List<IAction>
            {
                new AttackAction(owner),
                new DrawCardAction(owner, 1)
            };
        }
        public override async Task OnDiscard(Character source)
        {
            var wealthEffect = source.effects
            .OfType<WealthEffect>()
            .FirstOrDefault(e => e.Multiplier >= 1);
            if (wealthEffect != null)
            {
                wealthEffect.Multiplier -= 1;
                if (wealthEffect.Multiplier <= 0)
                {
                    source.effects.Remove(wealthEffect);
                }
                Console.WriteLine("排進棄排動作");
                foreach (var action in OnDiscardActions)
                {
                    ActionQueue.Instance.Enqueue(action);
                }

            }
            await Task.CompletedTask;
        }
    }

}
