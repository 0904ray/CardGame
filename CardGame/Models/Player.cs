using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGame.Models.Characters;

namespace CardGame.Models
{
    public class Player
    {
        public List<Character> Characters { get; private set; } = new List<Character>();

        public void PlayCard(Character source, Card card, Character target)
        {
            source.PlayCard(card, target);
        }
    }
}
