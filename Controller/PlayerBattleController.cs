using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace week3.Model
{
    public class PlayerBattleController
    {
        Player player;
        Random random = new Random();

        // 몬스터 데미지
        public void TakeDamageWithDefense(int damage)
        {
            int damaged = damage - player.Defense;
            int reducedDamage = Math.Max(1, damaged); //최소 1 데미지
            player.TakeDamage(reducedDamage);
            Console.WriteLine($"[전투] {player.Name}이(가) {reducedDamage} 데미지 입음 (HP: {player.CurrentHp})");
        }

        // 보스 대미지 계산 메서드
        public void TakeDamageWithChance(int damage, int critRate, int evasionRate)
        {

            int rand = random.Next(100);

            if (rand < evasionRate)
            {
                Console.WriteLine("다행히 매니저의 공격을 피했다.");
                return;
            }

            if (rand < evasionRate + critRate)
            {
                damage *= 2;
                Console.WriteLine("매니저의 눈이 번뜩이며 평소보다 강한 공격이 들어온다!");
            }
            player.TakeDamage(damage);
        }

        public void ApplyEquipmentStats(Item item)
        {
            if(item.Type == "무기")
            {
                int value = item.Value;
                player.AddAttackPower(value);
            }
            else if(item.Type == "방어구")
            {
                int value = item.Value;
                player.AddDefensePower(value);
            }
        }

        public void RemoveEquipmentStats(Item item)
        {
            if (item.Type == "무기")
            {
                int value = item.Value;
                player.RemoveAttackPower(value);
            }
            else if (item.Type == "방어구")
            {
                int value = item.Value;
                player.RemoveDefensePower(value);
            }
        }

    }
}
