namespace week3
{
    public class Player
    {
        UIManager uiManager;
        public InventoryManager inventoryManager;
        public static volatile bool stopFlicker = false;
        public static volatile bool stopLED = false;
        public SoundManager soundManager;
        public Thread? flickerThread;
        public bool isFlickering = false;
        public TextEffect textEffect = new TextEffect();

        

        public string Name { get; private set; }
        public int Attack { get; private set; }
        public int Defense { get; private set; }
        public int AdditionalAttackPower { get; private set; }
        public int AdditionalDefensePower { get; private set; }
        public int CurrentHp { get; private set; }
        public int MaxHp { get; private set; }
        public int Gold { get; private set; }
        public int ZebCoin { get; set; }
        public bool IsPlayerAlive { get; private set; }

        //초기 스탯 설정
        public Player(UIManager uiManager)
        {
            this.uiManager = uiManager;
            inventoryManager = new InventoryManager(this);

            Attack = 10 + AdditionalAttackPower;
            Defense = 5 + AdditionalDefensePower;
            AdditionalAttackPower = 0;
            AdditionalDefensePower = 0;
            CurrentHp = 100;
            MaxHp = 100;
            Gold = 20000;
            IsPlayerAlive = true;
            ZebCoin = 0;
        }
        // 스탯 창 들고오는 함수
        public void ShowPlayerStat()
        {
            while (true)
            {
                Console.Clear();
                uiManager.ShowStatus(this);
                int inputNum = InputManager.GetInt(0, 0);
                break;
            }
        }

        //이름 들고오는 함수
        public void GetPlayerName()
        {
            TextEffect textEffect = new TextEffect();
            SoundManager soundManager = new SoundManager();

            Console.Clear();
            Console.SetWindowSize(120, 40);
            Console.SetBufferSize(120, 40);
            Console.CursorVisible = true;

            textEffect.BeginTextSet();

            soundManager.PlayLoopExWithCut("musicbox", "musicbox.wav", 7.2, 0.6f); // ✅ 7.2초 루프 배경음

            string prompt = "당신의 이름은?";
            int promptX = (Console.WindowWidth - textEffect.GetVisualWidth(prompt)) / 2;
            int promptY = Console.WindowHeight / 2 - 1;

            Console.SetCursorPosition(promptX, promptY);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(prompt);
            Console.ResetColor();

            string nameInput = "";
            int inputY = promptY + 2;

            while (true)
            {
                int inputX = (Console.WindowWidth - textEffect.GetVisualWidth(nameInput)) / 2;
                Console.SetCursorPosition(inputX, inputY);

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                soundManager.PlayOnce("enterKey.wav"); // ✅ 키 누를 때마다 효과음

                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    if (!string.IsNullOrWhiteSpace(nameInput))
                    {
                        break; // 입력 완료
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && nameInput.Length > 0)
                {
                    nameInput = nameInput[..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    nameInput += keyInfo.KeyChar;
                }

                // 입력창 다시 그리기 (이름 재중앙정렬)
                int clearLineX = (Console.WindowWidth - textEffect.GetVisualWidth(new string(' ', 30))) / 2;
                Console.SetCursorPosition(clearLineX, inputY);
                Console.Write(new string(' ', 30)); // 최대 30칸 클리어

                inputX = (Console.WindowWidth - textEffect.GetVisualWidth(nameInput)) / 2;
                Console.SetCursorPosition(inputX, inputY);
                Console.Write(nameInput);
            }

            soundManager.StopAllLoopEx(); // ✅ 배경음 끄기
            Name = nameInput.Trim();

            Console.Clear();
        }



        // 보상 시스템
        public void AddZebCoin(int value)
        {
            ZebCoin += value;
            Console.WriteLine($"[시스템] {Name}의 ZebCoin이 {value} 증가! (현재 : {ZebCoin})");
        }

        public void AddGold(int value)
        {
            Gold += value;
            Console.WriteLine($"[시스템] {Name}의 Gold가 {value}G 증가! (현재: {Gold})");
        }

        // 재화 차감 시스템
        public void SpendGold(int value)
        {
            Gold -= value;
            if (Gold < 0) { Gold = 0; }
            Console.WriteLine($"[시스템] {Name}의 Gold가 {value}G 감소! (현재: {Gold})");
        }

        //스탯 증가 메서드
        public void Heal(int value)
        {
            CurrentHp += value;

            if (CurrentHp > MaxHp) { CurrentHp = MaxHp; }
        }

        public void AddAttackPower(int value)
        {
            Attack += value;
            AdditionalAttackPower += value;
        }
        public void AddDefensePower(int value)
        {
            Defense += value;
            AdditionalDefensePower += value;
        }

        public void TakeDamage(int value)
        {
            CurrentHp -= value;

            if (CurrentHp < 0)
            {
                CurrentHp = 0;
                Die();
            }
        }

        public void RemoveAttackPower(int value)
        {
            Attack -= value;
            AdditionalAttackPower -= value;
        }

        public void RemoveDefensePower(int value)
        {
            Defense -= value;
            AdditionalDefensePower -= value;
        }

        public void Die()
        {
            Console.WriteLine("... 몸이 점점 무거워지고 눈 앞이 흐려진다... 출석... 해야하는데...");
            Console.WriteLine("안타깝게도 당신은 영원히 Zeb세상에 살게되었습니다..."); //이거 나중에 UI함수 호출해서 출력하면 좋을듯
            Console.WriteLine("GAME OVER");
            Thread.Sleep(700);
            IsPlayerAlive = false;
            Environment.Exit(0);
        }

        public void ApplyEquipmentStats(Item item)
        {
            if(item.Type == "무기")
            {
                int value = item.Value;
                AddAttackPower(value);
            }
            else if(item.Type == "방어구")
            {
                int value = item.Value;
                AddDefensePower(value);
            }
        }

        public void RemoveEquipmentStats(Item item)
        {
            if (item.Type == "무기")
            {
                int value = item.Value;
                RemoveAttackPower(value);
            }
            else if (item.Type == "방어구")
            {
                int value = item.Value;
                RemoveDefensePower(value);
            }
        }
    }
}
