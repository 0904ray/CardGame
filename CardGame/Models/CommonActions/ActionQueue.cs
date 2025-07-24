using CardGame.Models.Characters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardGame.Models.CommonActions
{
    public class ActionQueue
    {
        private static readonly ActionQueue _instance = new();
        public static ActionQueue Instance => _instance;

        private readonly LinkedList<IAction> _queue = new();

        public void Enqueue(IAction action) => _queue.AddLast(action);  // 加到後面
        public void EnqueueFront(IAction action) => _queue.AddFirst(action); // 加到前面

        public async Task ProcessActionQueue(Func<List<Character>> getAvailableTargets)
        {
            while (_queue.Count > 0)
            {
                var action = _queue.First.Value;
                _queue.RemoveFirst();

                if (action is ITargetableAction targetable)
                {
                    await targetable.SetTargetAsync(getAvailableTargets);
                }

                await action.ExecuteAsync();
            }
        }

        public bool IsEmpty => _queue.Count == 0;
    }
}
