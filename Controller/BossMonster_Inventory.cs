using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace week3
{
    public class BossMonster_Inventory
    {
        InventoryManager inventory;
        Player player;
        public void ConsumableItemMenu()
        {
            while (true)
            {
                // 회복 아이템만 필터링
                //var recoveryItems = inventory.ItemData
                //    .Where(kvp => kvp.Value.Type == "회복") //&& inventory.Items.Any(invItem => invItem.Name == kvp.Key))
                //    .ToList();

                List<Item> item = inventory.ItemData[].Type

                if (recoveryItems.Count == 0)
                {
                    Console.WriteLine("현재 가진 회복 아이템이 없다.");
                    Console.ReadKey(true);
                    return;
                }

                for (int i = 0; i < recoveryItems.Count; i++)
                {
                    var item = recoveryItems[i];
                    Console.WriteLine($"{i + 1}. {item.Key} : {item.Value.Description}");
                }

                Console.WriteLine("0. 돌아가기");
                string input = Console.ReadLine();

                if (input == "0") return;

                if (int.TryParse(input, out int selected) && selected >= 1 && selected <= recoveryItems.Count)
                {
                    var item = recoveryItems[selected - 1].Value;
                    Console.WriteLine($"{item.Name}을 사용했다.");
                    //player.HP += item.Point;
                    //if (player.HP > player.MaxHP) player.HP = player.MaxHP;
                    Console.ReadKey(true);
                    return;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadKey(true);
                }
            }
        }
        public void BattleItemMenu(bool isAttackItemUsed)
        {
            while (true)
            {
                // 회복 아이템만 필터링
                var recoveryItems = inventory.ItemData
                    .Where(kvp => kvp.Value.Type != "회복") //&& inventory.Items.Any(invItem => invItem.Name == kvp.Key))
                    .ToList();

                if (recoveryItems.Count == 0)
                {
                    Console.WriteLine("현재 사용할 수 있는 아이템이 없다.");
                    Console.ReadKey(true);
                    return;
                }

                for (int i = 0; i < recoveryItems.Count; i++)
                {
                    var item = recoveryItems[i];
                    Console.WriteLine($"{i + 1}. {item.Key} : {item.Value.Description}");
                }

                Console.WriteLine("0. 돌아가기");
                string input = Console.ReadLine();

                if (input == "0") return;

                if (int.TryParse(input, out int selected) && selected >= 1 && selected <= recoveryItems.Count)
                {
                    var item = recoveryItems[selected - 1].Value;
                    Console.WriteLine($"{item.Name}을 사용했다.");
                    isAttackItemUsed = true;
                    player.Heal(inventory.ItemData[8].Value);
                    //player.HP += item.Point;
                    //if (player.HP > player.MaxHP) player.HP = player.MaxHP;
                    Console.ReadKey(true);
                    return;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadKey(true);
                }
            }
        }
    }
}
