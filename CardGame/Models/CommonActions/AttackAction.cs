using CardGame.Models.Characters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardGame.Models.CommonActions
{
    public class AttackAction : ITargetableAction
    {
        private readonly Character _source;
        private Character? _target;
        private readonly int _damage;

        public string Description => throw new NotImplementedException();

        public AttackAction(Character source)
        {
            _source = source;
            _damage = source.GetFinalAttack();
        }

        public async Task SetTargetAsync(Func<List<Character>> getAvailableTargets)
        {
            var targets = getAvailableTargets();
            // 用 Console 模擬選目標
            Console.WriteLine("請選擇攻擊目標：");
            for (int i = 0; i < targets.Count; i++)
                Console.WriteLine($"{i}: {targets[i].Name} (HP: {targets[i].Attr.Hp})");

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
            {
                Console.WriteLine("攻擊取消，無目標");
                return;
            }
            _target.TakeDamage(_damage);
            Console.WriteLine($"{_source.Name} 攻擊了 {_target.Name} 造成 {_damage} 傷害");
            await Task.CompletedTask;
        }
    }

}
