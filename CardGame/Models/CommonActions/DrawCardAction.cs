using CardGame.Models.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Models.CommonActions
{
    public class DrawCardAction: IAction
    {
        private readonly int _count;
        private Character? _source;

        public DrawCardAction(Character? source, int count)
        {
            this._source = source;
            _count = count;
        }

        public string Description => throw new NotImplementedException();

        public async Task ExecuteAsync()
        {
            _source?.DrawCards(_count);
            Console.WriteLine($"抽 {_count} 張卡");
            await Task.CompletedTask;
        }
    }
}
