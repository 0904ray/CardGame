using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Models.CommonActions
{
    public interface IAction
    {
        Task ExecuteAsync(); 
        string Description { get; } 
    }

}
