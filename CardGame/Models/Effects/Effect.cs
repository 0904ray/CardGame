using CardGame.Models.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Models.Effects
{
    public abstract class Effect
    {
        public abstract int Id { get; }
        public abstract string Name { get; }
        public abstract string Description { get; }
        public virtual int Multiplier { get; set; } = 1;
        public virtual bool IsActive { get; } = true;
        public int ModifyAttack(Character character, int baseAttack)
        {
            return baseAttack;
        }
        public void ModifyMaxHp(Character character, int MaxHp)
        {
            character.Attr.MaxHp = MaxHp;
        }
        public void ModifyMaxSp(Character character, int MaxSp)
        {
            character.Attr.MaxSp = MaxSp;
        }
        public void ModifyMaxKp(Character character, int MaxKp)
        {
            character.Attr.MaxKp = MaxKp;
        }
    }
}
