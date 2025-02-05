namespace Text_Rpg
{
    // 플레이어
    public class Player
    {
        public int Level { get; set; } // 레벨
        public string Name { get; set; } // 이름
        public string Job { get; set; } // 직업
        public float AttackPower { get; set; } // 공격력
        public float DefensePower { get; set; } //방어력
        public int Hp { get; set; } // 체력
        public int Gold { get; set; } // 골드
        public int OwnEquipmentIdx { get; set; } // 소유 장비 번호
        public Player() // 플레이어 클래스 생성자
        {
            Level = 1;
            Name = "Name";
            Job = "job";
            AttackPower = 10f;
            DefensePower = 5f;
            Hp = 100;
            Gold = 100000;
            Gold = 1500;
            OwnEquipmentIdx = 0;
        }
    }
    // 아이템 클래스
    public class Item
    {
        public string ItemName { get; }  // 이름
        public string ItemInfo { get; }   // 설명
        public int AttackPower { get; set; }   // 공격력
        public int DefensePower { get; set; }  // 방어력
        public int Price { get; }    // 가격
        public int OwnEquipmentIdx { get; set; } // 소유 장비 번호
        public bool PurchaseItem { get; set; } // 구매 여부 
        public bool EquippedItem { get; set; } // 장착 여부

        public Item(string itemName, string itemInfo, int attackPower, int defensePower, int price) // 생성자
        {
            ItemName = itemName;
            ItemInfo = itemInfo;
            AttackPower = attackPower;
            DefensePower = defensePower;
            Price = price;
            OwnEquipmentIdx = -1;
            PurchaseItem = false;
            EquippedItem = false;
        }
    }
    // 스테이지 클레스
    public class Stage
    {
        public void PlayerSelect(Player player) // 플레이어 행동 선택
        {
            //아이템 리스트 생성
            List<Item> items = new List<Item>
            {
            new Item("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", 0, 5, 1000),
            new Item("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, 9, 1800),
            new Item("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 0, 15, 3500),
            new Item("낡은 검", "수련에 도움을 주는 갑옷입니다.", 2, 0, 600),
            new Item("청동 도끼", "수련에 도움을 주는 갑옷입니다.", 5, 0, 1500),
            new Item("스파르타의 창", "수련에 도움을 주는 갑옷입니다.", 7, 0, 2700),
            new Item("삼위일체", "공격력과 방어력이 올라가는 완벽한 아이템입니다.", 15, 20, 5000)
            };
            // 시작 화면
            while (true)
            {
                Console.Clear();
                Console.WriteLine("스파르타 마을에 오신 {0}님 환영합니다.", player.Name);
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("4. 던전입장");
                Console.WriteLine("5. 휴식하기");
                Console.Write("\n원하시는 행동을 입력해주세요. \n>>");
                try
                {
                    int selectInput = int.Parse(Console.ReadLine());
                    switch (selectInput)
                    {
                        case 1:
                            StatusWindow(player, items);
                            break;
                        case 2:
                            Inventory(player, items);
                            break;
                        case 3:
                            Store(player, items);
                            break;
                        case 4:
                            Dungeon(player);
                            break;
                        case 5:
                            Rest(player);
                            break;
                        case 6:
                            SaveGame(player);
                            break;
                        default:
                            GoBack();
                            break;
                    }
                }
                catch
                {
                    GoBack();
                }
            }
        }

        //오타 안내문
        public void GoBack()
        {
            Console.WriteLine("잘못된 입력입니다. 다시 진행하시려면 아무키나 입력해주세요.");
            Console.ReadKey();
        }

        // 상태창 
        public void StatusWindow(Player player, List<Item> items)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("상태 보기");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
                Console.WriteLine("LV {0}", string.Format("{0:D2}", player.Level));
                Console.WriteLine("이름 : {0}, ( {1} )", player.Name, player.Job);
                Console.WriteLine("공격력 :  {0}(+{1})", player.AttackPower, player.AttackPower - 10f);
                Console.WriteLine("방어력 :  {0}(+{1})", player.DefensePower, player.DefensePower - 5f);
                Console.WriteLine("체 력 :  {0}", player.Hp);
                Console.WriteLine("Gold :  {0} G\n", player.Gold);
                Console.WriteLine("0. 나가기");
                Console.Write("\n원하시는 행동을 입력해주세요. \n>>");
                try
                {
                    int SWInput = int.Parse(Console.ReadLine());
                    if (SWInput == 0) break;
                    else GoBack();
                }
                catch
                {
                    GoBack();
                }
            }
        }

        //인벤토리
        public void Inventory(Player player, List<Item> items)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
                Console.WriteLine("[아이템 목록]\n");
                for (int j = 0; j < player.OwnEquipmentIdx; j++) // 소유 장비 갯수만큼 반복
                {
                    for (int i = 0; i < items.Count; i++) // 전체 장비 갯수만큼 반복
                    {
                        if (items[i].PurchaseItem) // 장비를 소유하고 있는지 확인
                        {
                            if (items[i].OwnEquipmentIdx == j) // 획득 아이템 순서가 맞는지 확인
                            {
                                if (items[i].EquippedItem) // 장착중인지 확인
                                {
                                    if (items[i].AttackPower > 0 && items[i].DefensePower > 0) Console.WriteLine($"- [E]{items[i].ItemName} | 공격력 +{items[i].AttackPower} | 방어력 +{items[i].DefensePower} | {items[i].ItemInfo}"); // 공격력 O 방어력 O
                                    else if (items[i].AttackPower > 0 && items[i].DefensePower <= 0) Console.WriteLine($"- [E]{items[i].ItemName} | 공격력 +{items[i].AttackPower}  | {items[i].ItemInfo}"); // 공격력 O 방어력 X
                                    else Console.WriteLine($"- [E]{items[i].ItemName} | 방어력 +{items[i].DefensePower}  | {items[i].ItemInfo}");
                                }
                                else
                                {
                                    if (items[i].AttackPower > 0 && items[i].DefensePower > 0) Console.WriteLine($"- {items[i].ItemName} | 공격력 +{items[i].AttackPower} | 방어력 +{items[i].DefensePower} | {items[i].ItemInfo}"); // 공격력 O 방어력 O
                                    else if (items[i].AttackPower > 0 && items[i].DefensePower <= 0) Console.WriteLine($"- {items[i].ItemName} | 공격력 +{items[i].AttackPower}  | {items[i].ItemInfo}"); // 공격력 O 방어력 X
                                    else Console.WriteLine($"- {items[i].ItemName} | 방어력 +{items[i].DefensePower}  | {items[i].ItemInfo}");
                                }
                            }
                        }
                    }


                }
                Console.WriteLine("\n1. 장착 관리");
                Console.WriteLine("0. 나가기");
                Console.Write("\n원하시는 행동을 입력해주세요. \n>>");
                try
                {
                    int IvnInput = int.Parse(Console.ReadLine());
                    switch (IvnInput)
                    {
                        case 0: break;

                        case 1:
                            EquipmentManagement(player, items);
                            break;

                        default:
                            GoBack();
                            break;
                    }
                    if (IvnInput == 0) break;
                }
                catch
                {
                    GoBack();
                }
            }
        }

        //장착 관리
        public void EquipmentManagement(Player player, List<Item> items)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("인벤토리 - 장착 관리");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
                Console.WriteLine("[ 아이템 목록 ]\n");
                for (int j = 0; j < player.OwnEquipmentIdx; j++) // 소유 장비 갯수만큼 반복
                {
                    for (int i = 0; i < items.Count; i++) // 전체 장비 갯수만큼 반복
                    {
                        if (items[i].PurchaseItem) // 장비를 소유하고 있는지 확인
                        {
                            if (items[i].OwnEquipmentIdx == j) // 획득 아이템 순서가 맞는지 확인
                            {
                                if (items[i].EquippedItem)
                                {
                                    if (items[i].AttackPower > 0 && items[i].DefensePower > 0) Console.WriteLine($"- {j + 1}. [E]{items[i].ItemName} | 공격력 +{items[i].AttackPower} | 방어력 +{items[i].DefensePower} | {items[i].ItemInfo}"); // 공격력 O 방어력 O
                                    else if (items[i].AttackPower > 0 && items[i].DefensePower <= 0) Console.WriteLine($"- {j + 1}. [E]{items[i].ItemName} | 공격력 +{items[i].AttackPower}  | {items[i].ItemInfo}"); // 공격력 O 방어력 X
                                    else Console.WriteLine($"- {j + 1}. [E]{items[i].ItemName} | 방어력 +{items[i].DefensePower}  | {items[i].ItemInfo}");
                                }
                                else
                                {
                                    if (items[i].AttackPower > 0 && items[i].DefensePower > 0) Console.WriteLine($"- {j + 1}. {items[i].ItemName} | 공격력 +{items[i].AttackPower} | 방어력 +{items[i].DefensePower} | {items[i].ItemInfo}"); // 공격력 O 방어력 O
                                    else if (items[i].AttackPower > 0 && items[i].DefensePower <= 0) Console.WriteLine($"- {j + 1}. {items[i].ItemName} | 공격력 +{items[i].AttackPower}  | {items[i].ItemInfo}"); // 공격력 O 방어력 X
                                    else Console.WriteLine($"- {j + 1}. {items[i].ItemName} | 방어력 +{items[i].DefensePower}  | {items[i].ItemInfo}");
                                }
                            }
                            else continue;

                        }
                        else continue;
                    }

                }
                Console.WriteLine("\n0. 나가기");
                Console.Write("\n원하시는 행동을 입력해주세요. \n>>");
                try
                {
                    int IvnInput = int.Parse(Console.ReadLine());
                    switch (IvnInput)
                    {
                        case 0: break;

                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                            if (IvnInput - 1 < player.OwnEquipmentIdx) // 선택번호가 소유 장비보다 적은지 확인
                            {
                                for (int i = 0; i < items.Count; i++) // 전체 장비 갯수만큼 반복
                                {
                                    if (items[i].OwnEquipmentIdx == IvnInput - 1)// 소유장비 번호 비교
                                        // 장비 해제
                                        if (items[i].EquippedItem)
                                        {
                                            if (items[i].AttackPower > 0 && items[i].DefensePower > 0)
                                            {
                                                player.AttackPower -= items[i].AttackPower;
                                                player.DefensePower -= items[i].DefensePower;
                                                items[i].EquippedItem = false;
                                                break;
                                            }
                                            else if (items[i].AttackPower > 0 && items[i].DefensePower <= 0)
                                            {
                                                player.AttackPower -= items[i].AttackPower;
                                                items[i].EquippedItem = false;
                                                break;
                                            }

                                            else
                                            {
                                                player.DefensePower -= items[i].DefensePower;
                                                items[i].EquippedItem = false;
                                                break;
                                            }
                                        }
                                        // 장비 장착
                                        else
                                        {
                                            
                                            if (items[i].AttackPower > 0 && items[i].DefensePower > 0)
                                            {
                                                player.AttackPower += items[i].AttackPower;
                                                player.DefensePower += items[i].DefensePower;
                                                items[i].EquippedItem = true;
                                            }
                                            else if (items[i].AttackPower > 0 && items[i].DefensePower <= 0)
                                            {
                                                player.AttackPower += items[i].AttackPower;
                                                items[i].EquippedItem = true;
                                            }

                                            else
                                            {
                                                player.DefensePower += items[i].DefensePower;
                                                items[i].EquippedItem = true;
                                            }
                                        }
                                    else continue;
                                }
                            }
                            else GoBack();
                            break;

                        default:
                            GoBack();
                            break;
                    }
                    if (IvnInput == 0) break;
                }
                catch
                {
                    GoBack();
                }
            }
        }

        //상점
        public void Store(Player player, List<Item> items)
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 얻거나 팔 수 있는 상점입니다.\n");
                Console.WriteLine("[ 보유 골드 ]\n{0}G\n", player.Gold);
                Console.WriteLine("[ 아이템 목록 ]\n");
                for (int i = 0; i < items.Count; i++) // 아이템 목록 출력
                {
                    if (items[i].PurchaseItem) // 아이템 소유
                    {
                        if (items[i].AttackPower > 0 && items[i].DefensePower > 0) Console.WriteLine($"- {items[i].ItemName} | 공격력 +{items[i].AttackPower} | 방어력 +{items[i].DefensePower} | {items[i].ItemInfo} | 구매 완료"); // 공격력 O 방어력 O
                        else if (items[i].AttackPower > 0 && items[i].DefensePower <= 0) Console.WriteLine($"- {items[i].ItemName} | 공격력 +{items[i].AttackPower}  | {items[i].ItemInfo} | 구매 완료"); // 공격력 O 방어력 X
                        else Console.WriteLine($"- {items[i].ItemName} | 방어력 +{items[i].DefensePower}  | {items[i].ItemInfo} | 구매 완료");
                    }
                    else // 아이템 미소유
                    {
                        if (items[i].AttackPower > 0 && items[i].DefensePower > 0) Console.WriteLine($"- {items[i].ItemName} | 공격력 +{items[i].AttackPower} | 방어력 +{items[i].DefensePower} | {items[i].ItemInfo} | {items[i].Price}G"); // 공격력 O 방어력 O
                        else if (items[i].AttackPower > 0 && items[i].DefensePower <= 0) Console.WriteLine($"- {items[i].ItemName} | 공격력 +{items[i].AttackPower}  | {items[i].ItemInfo} | {items[i].Price}G"); // 공격력 O 방어력 X
                        else Console.WriteLine($"- {items[i].ItemName} | 방어력 +{items[i].DefensePower}  | {items[i].ItemInfo} | {items[i].Price}G");
                    }

                }
                Console.WriteLine("\n1. 구매하기");
                Console.WriteLine("2. 판매하기");
                Console.WriteLine("0. 나가기");
                Console.Write("\n원하시는 행동을 입력해주세요. \n>>");
                try
                {
                    int STInput = int.Parse(Console.ReadLine());
                    if (STInput == 0) break;
                    else if (STInput == 1) BuyEquipment(player, items);
                    else if (STInput == 2) SaleEquipment(player, items);
                    else GoBack();
                }
                catch
                {
                    GoBack();
                }

            }
        }

        //구매하기
        public void BuyEquipment(Player player, List<Item> items)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("인벤토리 - 구매하기");
                Console.WriteLine("필요한 아이템을 구매합니다.\n");
                Console.WriteLine("[ 보유 골드 ]\n{0}G\n", player.Gold);
                Console.WriteLine("[ 아이템 목록 ]\n");
                for (int i = 0; i < items.Count; i++) // 아이템 목록 출력
                {
                    if (items[i].PurchaseItem) // 아이템 소유
                    {
                        if (items[i].AttackPower > 0 && items[i].DefensePower > 0) Console.WriteLine($"- {i + 1}. {items[i].ItemName} | 공격력 +{items[i].AttackPower} | 방어력 +{items[i].DefensePower} | {items[i].ItemInfo} | 구매 완료"); // 공격력 O 방어력 O
                        else if (items[i].AttackPower > 0 && items[i].DefensePower <= 0) Console.WriteLine($"- {i + 1}. {items[i].ItemName} | 공격력 +{items[i].AttackPower}  | {items[i].ItemInfo} | 구매 완료"); // 공격력 O 방어력 X
                        else Console.WriteLine($"- {i + 1}. {items[i].ItemName} | 방어력 +{items[i].DefensePower}  | {items[i].ItemInfo} | 구매 완료");
                    }
                    else // 아이템 미소유
                    {
                        if (items[i].AttackPower > 0 && items[i].DefensePower > 0) Console.WriteLine($"- {i + 1}. {items[i].ItemName} | 공격력 +{items[i].AttackPower} | 방어력 +{items[i].DefensePower} | {items[i].ItemInfo} | {items[i].Price}G"); // 공격력 O 방어력 O
                        else if (items[i].AttackPower > 0 && items[i].DefensePower <= 0) Console.WriteLine($"- {i + 1}. {items[i].ItemName} | 공격력 +{items[i].AttackPower}  | {items[i].ItemInfo} | {items[i].Price}G"); // 공격력 O 방어력 X
                        else Console.WriteLine($"- {i + 1}. {items[i].ItemName} | 방어력 +{items[i].DefensePower}  | {items[i].ItemInfo} | {items[i].Price}G");
                    }
                }
                Console.WriteLine("\n0. 나가기");
                Console.Write("\n원하시는 행동을 입력해주세요. \n>>");
                try
                {
                    int BuyInput = int.Parse(Console.ReadLine());
                    switch (BuyInput)
                    {
                        case 0: break;

                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                            if (items[BuyInput - 1].PurchaseItem)
                            {
                                Console.WriteLine("이미 보유중인 아이템입니다 .");
                                Console.ReadKey();
                            }
                            else if (items[BuyInput - 1].Price <= player.Gold)
                            {
                                items[BuyInput - 1].PurchaseItem = true;
                                player.Gold -= items[BuyInput - 1].Price;
                                items[BuyInput - 1].OwnEquipmentIdx = player.OwnEquipmentIdx;
                                player.OwnEquipmentIdx++;
                                Console.WriteLine($"{items[BuyInput - 1].ItemName}를(을) 구매했습니다.");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine("보유 Gold가 부족합니다.");
                                Console.ReadKey();

                            }
                            break;
                        default: GoBack(); break;
                    }
                    if (BuyInput == 0) break;
                }
                catch
                {
                    GoBack();
                }
            }
        }

        //판매하기
        public void SaleEquipment(Player player, List<Item> items)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("상점 - 판매하기");
                Console.WriteLine("불필요한 아이템을 판매합니다..\n");
                Console.WriteLine("[ 보유 골드 ]\n{0}G\n", player.Gold);
                Console.WriteLine("[ 아이템 목록 ]\n");
                for (int j = 0; j < player.OwnEquipmentIdx; j++) // 소유 장비 갯수만큼 반복
                {
                    for (int i = 0; i < items.Count; i++) // 전체 장비 갯수만큼 반복
                    {
                        if (items[i].PurchaseItem) // 장비를 소유하고 있는지 확인
                        {
                            if (items[i].OwnEquipmentIdx == j) // 선택한 장비가 맞을때
                            {
                                if (items[i].EquippedItem)
                                {
                                    if (items[i].AttackPower > 0 && items[i].DefensePower > 0) Console.WriteLine($"- {j + 1}. [E]{items[i].ItemName} | 공격력 +{items[i].AttackPower} | 방어력 +{items[i].DefensePower} | {items[i].ItemInfo} | {items[i].Price} G"); // 공격력 O 방어력 O
                                    else if (items[i].AttackPower > 0 && items[i].DefensePower <= 0) Console.WriteLine($"- {j + 1}. [E]{items[i].ItemName} | 공격력 +{items[i].AttackPower}  | {items[i].ItemInfo} | {items[i].Price} G"); // 공격력 O 방어력 X
                                    else Console.WriteLine($"- {j + 1}. [E]{items[i].ItemName} | 방어력 +{items[i].DefensePower}  | {items[i].ItemInfo} | {items[i].Price} G");
                                }
                                else
                                {
                                    if (items[i].AttackPower > 0 && items[i].DefensePower > 0) Console.WriteLine($"- {j + 1}. {items[i].ItemName} | 공격력 +{items[i].AttackPower} | 방어력 +{items[i].DefensePower} | {items[i].ItemInfo} | {items[i].Price} G"); // 공격력 O 방어력 O
                                    else if (items[i].AttackPower > 0 && items[i].DefensePower <= 0) Console.WriteLine($"- {j + 1}. {items[i].ItemName} | 공격력 +{items[i].AttackPower}  | {items[i].ItemInfo} | {items[i].Price} G"); // 공격력 O 방어력 X
                                    else Console.WriteLine($"- {j + 1}. {items[i].ItemName} | 방어력 +{items[i].DefensePower}  | {items[i].ItemInfo} | {items[i].Price} G");
                                }
                            }

                        }
                    }

                }
                Console.WriteLine("\n0. 나가기");
                Console.Write("\n원하시는 행동을 입력해주세요. \n>>");
                try
                {
                    int SaleInput = int.Parse(Console.ReadLine());
                    switch (SaleInput)
                    {
                        case 0: break;

                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                            if ((SaleInput - 1) < player.OwnEquipmentIdx) // 선택번호가 소유 장비보다 적은지 확인
                            {
                                for (int i = 0; i < items.Count; i++) // 전체 장비 갯수만큼 반복
                                {
                                    if ((SaleInput - 1) == items[i].OwnEquipmentIdx) // 선택한 장비가 맞을때
                                    {
                                        if (items[i].EquippedItem) //장비를 끼고있다면 장비 해제
                                        {
                                            if (items[i].AttackPower > 0 && items[i].DefensePower > 0)
                                            {
                                                player.AttackPower -= items[i].AttackPower;
                                                player.DefensePower -= items[i].DefensePower;
                                                items[i].EquippedItem = false;
                                            }
                                            else if (items[i].AttackPower > 0 && items[i].DefensePower < 0)
                                            {
                                                player.AttackPower -= items[i].AttackPower;
                                                items[i].EquippedItem = false;
                                            }
                                            else
                                            {
                                                player.DefensePower -= items[i].DefensePower;
                                                items[i].EquippedItem = false;
                                            }
                                            items[i].PurchaseItem = false;
                                            player.Gold += items[SaleInput - 1].Price / 100 * 85;
                                            player.OwnEquipmentIdx--;
                                            for (int j = 0; j < items.Count; j++)
                                            {
                                                if (items[i].OwnEquipmentIdx < items[j].OwnEquipmentIdx) items[j].OwnEquipmentIdx -= 1;
                                            }
                                            items[i].OwnEquipmentIdx = -1;
                                            Console.WriteLine($"{items[i].ItemName}를(을) 판매했습니다.");
                                            Console.ReadKey();
                                            break;
                                        }
                                        else
                                        {
                                            items[i].PurchaseItem = false;
                                            player.Gold += items[SaleInput - 1].Price / 100 * 85;
                                            player.OwnEquipmentIdx--;
                                            for (int j = 0; j < items.Count; j++)
                                            {
                                                if (items[i].OwnEquipmentIdx < items[j].OwnEquipmentIdx) items[j].OwnEquipmentIdx -= 1;
                                            }
                                            items[i].OwnEquipmentIdx = -1;
                                            Console.WriteLine($"{items[i].ItemName}를(을) 판매했습니다.");
                                            Console.ReadKey();
                                            break;
                                        }
                                    }
                                }
                            }
                            break;
                        
                        default:
                            GoBack();
                            break;
                    }
                    if (SaleInput == 0) break;
                }
                catch
                {
                    GoBack();
                }
            }
        }

        //던전
        public void Dungeon(Player player)
        {

        }

        //휴식
        public void Rest(Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("휴식하기");
                Console.WriteLine("500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {0} G \n))", player.Gold);
                Console.WriteLine("1. 휴식하기");
                Console.WriteLine("0. 나가기");
                Console.Write("\n원하시는 행동을 입력해주세요.\n>>");
                try
                {
                    int RestInput = int.Parse(Console.ReadLine());
                    switch (RestInput)
                    {
                        case 0: break;

                        case 1:
                            if (player.Hp < 100)
                            {
                                player.Gold = -500;
                                player.Hp = 100;
                                Console.WriteLine("회복이 완료되었습니다.");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine("체력이 최대치입니다.");
                                Console.ReadKey();
                            }
                            break;

                        default:
                            GoBack();
                            break;
                    }
                    if (RestInput == 0) break;
                }
                catch
                {
                    GoBack();
                }
            }
        }

        //게임저장
        public void SaveGame(Player player)
        {

        }
    }
    internal class Program
    {

        static void Main(string[] args)
        {
            Player player = new Player();

            // 캐릭터 이름 설정
            while (true)
            {
                Console.Clear();
                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
                Console.WriteLine("원하시는 이름을 설정해주세요.");
                Console.WriteLine();
                string Name = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine("입력하신 이름은 {0} 입니다\n", Name);
                Console.WriteLine("1. 저장");
                Console.WriteLine("2. 취소");
                Console.Write("\n원하시는 행동을 입력해주세요. \n>>");
                try
                {
                    int NameSelect = int.Parse(Console.ReadLine());
                    if (NameSelect == 1)
                    {
                        player.Name = Name;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ReadKey();
                    }
                }
                catch
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadKey();
                }
            }

            // 캐릭터 직업 설정
            while (true)
            {
                Console.Clear();
                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
                Console.WriteLine("원하시는 직업을 선택해주세요.\n");
                Console.WriteLine("1. 전사");
                Console.WriteLine("2. 도적");
                Console.Write("\n원하시는 행동을 입력해주세요. \n>>");
                int JobSelect = int.Parse(Console.ReadLine());
                if (JobSelect == 1)
                {
                    player.Job = "전사";
                    break;
                }
                else if (JobSelect == 2)
                {
                    player.Job = "도적";
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadKey();
                }
            }



            Stage stage = new Stage();
            stage.PlayerSelect(player);
        }

    }
}


