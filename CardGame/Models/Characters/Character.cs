using CardGame.Models.Cards;
using CardGame.Models.CommonActions;
using CardGame.Models.Effects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Models.Characters
{
    public abstract class Character
    {
        public string Name { get; set; }
        // 原始值（最大值）
        public int Hp { get; protected set; }
        public int Sp { get;  set; }
        public int Kp { get; protected set; }
        public int Attack { get; protected set; }

        public Attr Attr { get; set; }

        public List<Effect> effects { get; set; } = new List<Effect>();

        public List<Card> Hand { get; private set; } = new List<Card>();
        public List<Card> Deck { get; private set; } = new List<Card>();
        public List<Card> DiscardPile { get; private set; } = new List<Card>();

        public Character(string name, int hp, int sp, int kp, int attack)
        {
            Name = name;
            Hp = hp;
            Sp = sp;
            Kp = kp;
            Attack = attack;
            Attr = new Attr(hp, attack, sp, kp);
        }

        public bool IsDead => Attr.Hp <= 0;
        public async Task PlayCard(Card card)
        {
            if (Hand.Contains(card))
            {
                Attr.Sp -= card.Cost; // 扣除 SP
                await card.PlayAsync(this);
                Hand.Remove(card);
                DiscardPile.Add(card);
            }
            else
            {
                throw new InvalidOperationException("Card not in hand.");
            }
        }
        public void DiscardCard(Card c)
        {
            Hand.Remove(c);
            DiscardPile.Add(c);
        }


        public bool CanPlay(Card card)
        {
            return Sp >= card.Cost; // SP 足夠才可以打
        }

        public void TakeDamage(int amount)
        {
            int remaining = amount;
            // 扣 HP
            if (remaining > 0 && Attr.Hp > 0)
            {
                Attr.Hp -= remaining;
                remaining = 0;

                if (Attr.Hp <= 0)
                {
                    Attr.Hp = 0;
                    // TODO: 重創判定
                }
            }

            // TODO: 播放受傷動畫、觸發事件等
        }
        public void DrawCards(int count)
        {
            for (int i = 0; i < count; i++)
            {
                if (Deck.Count == 0)
                {
                    // 洗牌重整
                    if (DiscardPile.Count > 0)
                    {
                        Deck.AddRange(DiscardPile);
                        DiscardPile.Clear();
                        Shuffle(Deck);
                    }
                    else
                    {
                        // 無牌可抽
                        break;
                    }
                }

                var card = Deck[0];
                Deck.RemoveAt(0);
                Hand.Add(card);
            }
        }

        // 工具方法：洗牌
        protected void Shuffle(List<Card> cards)
        {
            Random rng = new Random();
            int n = cards.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                var value = cards[k];
                cards[k] = cards[n];
                cards[n] = value;
            }
        }

        public int GetFinalAttack()
        {
            int result = Attr.Attack;

            foreach (var effect in effects)
            {
                if (effect.IsActive)
                    result = effect.ModifyAttack(this, result);
            }

            return result;
        }
        public async Task<List<Card>> ChooseCardsAsync(int count)
        {
            // 示意：列出可選卡片
            Console.WriteLine($"{Name} 的手牌：");
            for (int i = 0; i < Hand.Count; i++)
                Console.WriteLine($"{i}: {Hand[i].Name}");

            var selected = new List<Card>();

            while (selected.Count < count)
            {
                Console.WriteLine($"請選擇第 {selected.Count + 1} 張牌（輸入索引）:");
                if (int.TryParse(Console.ReadLine(), out int index) &&
                    index >= 0 && index < Hand.Count &&
                    !selected.Contains(Hand[index]))
                {
                    selected.Add(Hand[index]);
                }
                else
                {
                    Console.WriteLine("無效選擇，請再試一次。");
                }
            }

            return selected;
        }


    }
}
