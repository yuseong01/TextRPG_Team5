using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using week3;


namespace week3
{
    public static class MonsterManager
    {
        private static List<Monster> normalMonster { get; } = new List<Monster>(); //-lv.2 lv4 lv10 lv6  <-한전투에 계속 등장할 애들 (랜덤값으로 1-4마리가 나와요)  
        private static List<Monster> mimicMonster { get; } = new List<Monster>();
        private static Random random = new Random();

        public static void InitializeMonsters()
        {
            // 일반 몬스터
            normalMonster.Add(new Monster(
            name: "",
            type: MonsterType.Normal,
            question: "",
            correctAnswer: "",
            maxHealth: 50,
            attackPower: 5,
            expReward: 10
             
            ));

            normalMonster.Add(new Monster(
            name: "",
            type: MonsterType.Normal,
            question: "",
            correctAnswer: "",
            maxHealth: 50,
            attackPower: 5,
            expReward: 10

            ));
            normalMonster.Add(new Monster(
            name: "",
            type: MonsterType.Normal,
            question: "",
            correctAnswer: "",
            maxHealth: 50,
            attackPower: 5,
            expReward: 10

            ));
            normalMonster.Add(new Monster(
            name: "",
            type: MonsterType.Normal,
            question: "",
            correctAnswer: "",
            maxHealth: 50,
            attackPower: 5,
            expReward: 10


            // 미믹 몬스터
            ));
            normalMonster.Add(new Monster(
            name: "",
            type: MonsterType.Mimic,
            question: "",
            correctAnswer: "",
            maxHealth: 50,
            attackPower: 5,
            expReward: 10

            ));
            normalMonster.Add(new Monster(
            name: "",
            type: MonsterType.Mimic,
            question: "",
            correctAnswer: "",
            maxHealth: 50,
            attackPower: 5,
            expReward: 10

            ));
            normalMonster.Add(new Monster(
            name: "",
            type: MonsterType.Mimic,
            question: "",
            correctAnswer: "",
            maxHealth: 50,
            attackPower: 5,
            expReward: 10

            ));
            normalMonster.Add(new Monster(
            name: "",
            type: MonsterType.Mimic,
            question: "",
            correctAnswer: "",
            maxHealth: 50,
            attackPower: 5,
            expReward: 10

            ));
        }

        //public static List<Monster> GetRandomMonsters()
        //{
        //    int count = random.Next(1, 8); // 1~8마리
        //    var group = new List<Monster>();

        //    for (int i = 0; i < count; i++)
        //    {
        //        group.Add(GetRandomMonsters());
        //    }
        //    return group;
        //}
    }
}
