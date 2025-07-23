using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Models.Characters
{
    public class Attr
    {
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public int Attack { get; set; }
        public int Sp { get; set; }
        public int MaxSp { get; set; }
        public int Kp { get; set; }
        public int MaxKp { get; set; }
        public Attr(int hp, int attack, int sp, int kp)
        {
            Hp = hp;
            MaxHp = hp;
            Attack = attack;
            Sp = sp;
            MaxSp = sp;
            Kp = kp;
            MaxKp = kp;
        }
    }
}
