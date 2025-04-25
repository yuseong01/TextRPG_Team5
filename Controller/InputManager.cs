using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week3
{
    internal static class InputManager
    {
        public static int GetInt(int min, int max)
        {
            while (true)
            {
                Thread.Sleep(700);

                Console.Write("번호를 선택해주세요. >");
                string input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out int value);

                if (isNumber)
                {
                    if (value >= min && value <= max)
                    {
                        return value;
                    }
                    else
                    {
                        Console.WriteLine($"{min}부터 {max} 사이의 숫자를 입력해주세요.");
                    }
                }
                else
                {
                    Console.WriteLine("숫자를 입력해주세요.");
                }
            }
        }

    }
}
