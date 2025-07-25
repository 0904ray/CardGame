﻿using CardGame.Models.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Models.CommonActions
{
    internal interface ITargetableAction : IAction
    {
        Task SetTargetAsync(Func<List<Character>> getAvailableTargets);
    }
}
