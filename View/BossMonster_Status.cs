using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week3
{
    public class BossMonster_Status
    {
        public static void ShowBossStatus(BossMonster_Data boss)
        {
            Console.Clear();
            Console.WriteLine($"<매니저 스펙>");
            Console.WriteLine(new string('=', 35));
            Console.WriteLine($"{boss.Name}");
            Console.WriteLine($"공격력: {boss.Atk}");
            Console.WriteLine($"방어력: {boss.Def}");
            Console.WriteLine($"체력: {boss.MaxHP}");
            Console.WriteLine($"정신력: {boss.MaxSpirit}");
            Console.WriteLine($"치명타율: {boss.Critical}%");
            Console.WriteLine($"회피율: {boss.Evasion}%");
            Console.WriteLine(new string('=', 35));
            Console.WriteLine();
            Console.ReadKey(true);
            BossMonster_Battle.PlayerTurn();
        }
    }
}
