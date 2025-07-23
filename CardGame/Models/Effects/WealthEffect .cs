using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Models.Effects
{
    public class WealthEffect : Effect
    {
        public override int Id => 1001;
        public override string Name => "財富";
        public override string Description => "可用於發動[收買]效果";
    }

}
