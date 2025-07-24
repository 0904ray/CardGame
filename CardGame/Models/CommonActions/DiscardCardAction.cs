using CardGame.Models.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Models.CommonActions
{
    public class DiscardCardAction : IAction
    {
        private readonly int _count;
        private Character? _source;

        public DiscardCardAction(Character source,int count)
        {
            _count = count;
            _source = source;
        }

        public string Description => throw new NotImplementedException();

        public async Task ExecuteAsync()
        {
            _source.DiscardCardAtIndex(_count - 1);
            Console.WriteLine($"棄掉 {_count} 張卡");
            await Task.CompletedTask;
        }
    }


}
