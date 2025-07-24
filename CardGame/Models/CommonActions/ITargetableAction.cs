using CardGame.Models.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Models.CommonActions
{
    internal interface ITargetableAction : IEffectAction
    {
        bool RequiresTarget { get; }
        void SetTarget(Character target);      // 預設 UI 用這個設定
    }
}
