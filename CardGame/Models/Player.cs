using CardGame.Models.Cards;
using CardGame.Models.Characters;
using CardGame.Models.CommonActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardGame.Models
{
    public class Player
    {
        public string Name { get; private set; }
        public List<Character> Characters { get; private set; } = new();
        public Player _opponent;

        public Player(string name, List<Character> characters)
        {
            Name = name;
            Characters = characters;
        }

        public async Task TakeTurnAsync(Player opponent)
        {
            _opponent = opponent;
            Console.WriteLine($"\n--- {Name} 的回合開始 ---");

            foreach (var character in Characters)
            {
                //character.StartTurn(); // 重設 KP、回合狀態等
            }

            bool turnEnded = false;
            while (!turnEnded)
            {
                Console.WriteLine($"\n{Name} 的角色：");
                for (int i = 0; i < Characters.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {Characters[i]}");
                }

                Console.WriteLine("請選擇角色編號（或輸入 0 結束回合）：");
                if (!int.TryParse(Console.ReadLine(), out int charIndex) || charIndex < 0 || charIndex > Characters.Count)
                {
                    Console.WriteLine("輸入錯誤，請重試。");
                    continue;
                }

                if (charIndex == 0)
                {
                    turnEnded = true;
                    break;
                }

                var selectedChar = Characters[charIndex - 1];

                Console.WriteLine($"\n選擇的角色：{selectedChar.Name}");
                var hand = selectedChar.Hand;

                if (hand.Count == 0)
                {
                    Console.WriteLine("此角色沒有可用卡牌。");
                    continue;
                }

                Console.WriteLine("手牌：");
                for (int i = 0; i < hand.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {hand[i].Name} (成本: {hand[i].Cost})");
                }

                Console.WriteLine("請選擇要打出的卡（輸入 0 放棄）：");
                if (!int.TryParse(Console.ReadLine(), out int cardIndex) || cardIndex < 0 || cardIndex > hand.Count)
                {
                    Console.WriteLine("輸入錯誤，請重試。");
                    continue;
                }

                if (cardIndex == 0) continue;

                var selectedCard = hand[cardIndex - 1];
                if (selectedChar.CanPlay(selectedCard))
                {
                    await selectedChar.PlayCard(selectedCard);
                    await ActionQueue.Instance.ProcessActionQueue(() =>
                    {
                        // 你提供可以選擇的目標清單
                        return _opponent.Characters.Where(c => !c.IsDead).ToList();
                    });
                }
                else
                {
                    Console.WriteLine("無法打出這張卡，可能因為 SP 不足或其他限制。");
                }
            }

            Console.WriteLine($"\n--- {Name} 的回合結束 ---");
        }

        public async Task PlayCard(Character source, Card card)
        {
            await source.PlayCard(card);
        }
        public async Task DiscardCardsInteractivelyAsync(Character character, int count)
        {
            var hand = character.Hand;
            if (hand.Count == 0)
            {
                Console.WriteLine($"{character.Name} 沒有手牌可棄置。");
                return;
            }

            count = Math.Min(count, hand.Count);
            for (int discarded = 0; discarded < count; discarded++)
            {
                Console.WriteLine($"\n請選擇要棄置的第 {discarded + 1} 張卡片：");
                for (int i = 0; i < hand.Count; i++)
                {
                    var c = hand[i];
                    Console.WriteLine($"{i}: {c.Name} (費用: {c.Cost}) {(c.hasDiscardAbility ? "[有棄置效果]" : "")}");
                }

                int index;
                while (true)
                {
                    Console.Write("輸入卡片編號: ");
                    var input = Console.ReadLine();
                    if (int.TryParse(input, out index) && index >= 0 && index < hand.Count)
                        break;
                    Console.WriteLine("輸入錯誤，請重試。");
                }

                var card = hand[index];
                hand.RemoveAt(index);
                character.DiscardPile.Add(card);
                Console.WriteLine($"你棄置了：{card.Name}");

                if (card.hasDiscardAbility)
                {
                    Console.Write("此卡有棄置效果，是否要發動？(y/n): ");
                    var confirm = Console.ReadLine();
                    if (confirm?.Trim().ToLower() == "y")
                    {
                        await card.OnDiscard(character);
                        await ActionQueue.Instance.ProcessActionQueue(() =>
                        {
                            return _opponent.Characters.Where(c => !c.IsDead).ToList();
                        });
                    }
                }
            }
        }


        public bool IsDefeated => Characters.All(c => c.IsDead);
    }
}
