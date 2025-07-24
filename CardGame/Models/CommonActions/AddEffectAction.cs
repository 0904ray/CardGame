using CardGame.Models.Characters;
using CardGame.Models.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardGame.Models.CommonActions
{
    public class AddEffectAction : ITargetableAction
    {
        private Character? _target;
        private readonly Effect _effect;
        private readonly int _amount;

        public string Description => throw new NotImplementedException();

        public AddEffectAction(Character target, Effect effect, int amount)
        {
            _target = target;
            _effect = effect;
            _amount = amount;
        }

        public async Task SetTargetAsync(Func<List<Character>> getAvailableTargets)
        {
            if (_target != null)
            {
                await Task.CompletedTask;
                return;
            }
            var targets = getAvailableTargets();
            Console.WriteLine("請選擇效果目標：");
            for (int i = 0; i < targets.Count; i++)
                Console.WriteLine($"{i}: {targets[i].Name}");

            while (true)
            {
                var input = Console.ReadLine();
                if (int.TryParse(input, out int idx) && idx >= 0 && idx < targets.Count)
                {
                    _target = targets[idx];
                    break;
                }
                Console.WriteLine("輸入錯誤，請重試");
            }

            await Task.CompletedTask;
        }

        public async Task ExecuteAsync()
        {
            if (_target == null)
                throw new InvalidOperationException("Target not set.");

            var existing = _target.effects.FirstOrDefault(e => e.GetType() == _effect.GetType());

            if (existing != null)
            {
                existing.Multiplier += _amount;
            }
            else
            {
                var newEffect = (Effect)Activator.CreateInstance(_effect.GetType())!;
                newEffect.Multiplier = _amount;
                _target.effects.Add(newEffect);
            }

            await Task.CompletedTask;
        }
    }
}
