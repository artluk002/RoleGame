using RoleGame.Artifactes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RoleGame.Plot
{
    public class Game
    {
        private List<Character> Characters;
        public string Name { get; set; }
        public int CurrFloor { get; set; }
        private string[] BossNames;
        private Random r;
        private bool heal;
        public Game()
        {
            Characters = new List<Character>();
            CurrFloor = 1;
            BossNames = new string[] { "Agent Smith", "Voldemort", "Witch", "Dragon Master", "Swamp Golem", "Fire lord", "Big serpent", "Aragog", "Tentalaklone", "Dungeon Master", "Ice Dragon", "Stonebird", "Fallen Warrior", "King of the dead", "Zeus", "Hydra", "Peregarych", "Cannibal", "Tony Montana", "Jack Torrent", "Hannibal Lecter", "Darth Vader", "Evil Witch", "Cast iron master", "Black Tolik", "Satan", "Scribbler", "Waido", "Fomenochka", "Gopnik", "Madara", "The Dark Knight", "Mafia", "Dark Ripper", "Cyclops", "Devil", "Plant-eater", "Dinosaur", "Alien invader" };
            r = new Random();
        }
        public void Run()
        {
            Precondition();
            while (true)
            {
                foreach (Character character in Characters)
                    Console.WriteLine(character);
                Console.Write("1 - Boss fighting\n" +
                "2 - Use Item\n" +
                "3 - Pass Item" +
                "4 - Use Magic\n" +
                "5 - Save Characters\n" +
                "6 - Save Game\n" +
                "7 - Healing room\n" +
                "Enter action: ");
                Menu();
            }
        }
        public void Precondition()
        {
            int PlayersCount = 1;
            Console.Write("Enter count of palyers: ");
            PlayersCount = int.Parse(Console.ReadLine());
            string answer;
            JsonOperations json = new JsonOperations();
            List<Character> list = json.ReadCharacters();
            for (int i = 0; i < PlayersCount; i++)
            {
                do
                {
                    Console.WriteLine("Do you have an account?");
                    Console.Write("yes/no: ");
                    answer = Console.ReadLine().ToLower();
                } while (answer != "yes" && answer != "no");

                switch (answer)
                {
                    case "yes":

                        foreach (Character character in list)
                            Console.WriteLine(character);

                        bool validChoice = false;
                        string Char_name;
                        do
                        {
                            Console.Write("Enter name: ");
                            Char_name = Console.ReadLine();
                            for (int j = 0; j < list.Count; j++)
                            {
                                if (list[j].Name == Char_name)
                                {
                                    if (list[j] as CharacterWithMagic != null)
                                        (list[j] as CharacterWithMagic).SpellsComing();
                                    Characters.Add(list[j]);
                                    list.RemoveAt(j);
                                    validChoice = true;
                                    break;
                                }
                            }
                        } while (!validChoice);
                        break;
                    case "no":
                        switch (r.Next(1, 3))
                        {
                            case 1:
                                Characters.Add(new Character(true));
                                break;
                            case 2:
                                Characters.Add(new CharacterWithMagic(true));
                                break;
                            default:
                                Characters.Add(new Character(true));
                                break;
                        }
                        //if()
                        break;
                }
                Console.Clear();
            }
            do
            {
                Console.WriteLine("Do you have a map?");
                Console.Write("yes/no: ");
                answer = Console.ReadLine().ToLower();
            } while (answer != "yes" && answer != "no");
            switch (answer)
            {
                case "yes":
                    List<Game> games = json.ReadGame();
                    foreach (Game game in games)
                        Console.WriteLine($"{game.Name}, {game.CurrFloor}");
                    Console.Write("Enter name of game");
                    string gameName = Console.ReadLine();
                    foreach (Game game in games)
                        if (game.Name == gameName)
                        {
                            this.Name = game.Name;
                            this.CurrFloor = game.CurrFloor;
                            break;
                        }
                    break;
                case "no":
                default:
                    break;
            }
            JsonOperations json1 = new JsonOperations();
            for (int j = 0; j < Characters.Count; j++)
                json1.SaveCharacters(Characters[j]);
            Characters[0].CreateTeam("AAA");
            for (int i = 0; i < Characters.Count; i++)
                if (i != 0)
                {
                    Characters[0].team.AddCharacter(Characters[i]);
                    Characters[i].team = Characters[0].team;
                }

        }
        public void BossBattle()
        {
            Console.Clear();
            Character Boss = Character.SummonBoss(CurrFloor, BossNames[r.Next(0, BossNames.Length)], CharacterGender.Non, (CharacterRace)r.Next(0, 5), (UInt32)r.Next(100, 1001));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("!!! Boss !!!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(Boss);
            while (Boss.CurrentHealth > 0)
            {
                foreach (var player in Characters)
                {
                    try
                    {
                        if (player.State == CharacterState.Dead) continue;
                        int action = 0;
                        Console.WriteLine($"== {player.Name} ==");
                        Console.WriteLine("1 - Attack Boss\n" +
                            "2 - Use Artifact\n" +
                            "3 - Pass item\n" +
                            $"{(player as CharacterWithMagic != null ? "4 - Use Spell" : "")}");
                        Console.Write("Enter action: ");
                        3action = int.Parse(Console.ReadLine());
                        switch (action)
                        {
                            case 1:
                                player.Attak(ref Boss);
                                break;
                            case 2:
                                player.Inventory.PrintItems();
                                Console.Write("Enter name of artifact you want to use: ");
                                string ItemName = Console.ReadLine();
                                if (!player.Inventory.IsItIn(ItemName))
                                {
                                    Console.WriteLine("You haven't this Artifact");
                                    return;
                                }
                                Console.WriteLine("Select the entity on which you want to use an artifact");
                                for (int i = 0; i < Characters.Count; i++)
                                    Console.WriteLine($"{i}: {Characters[i]}");
                                Console.WriteLine($"{Characters.Count}: {Boss}");
                                Console.WriteLine("Enter num: ");
                                int id = int.Parse(Console.ReadLine());
                                int force = 0;
                                switch (player.Inventory.Items[ItemName].type)
                                {
                                    case SpellType.Force:
                                        Console.Write("Enter force: ");
                                        force = int.Parse(Console.ReadLine());
                                        if (id == Characters.Count)
                                            player.Inventory.Items[ItemName].Wiz(ref Boss, force);
                                        else
                                        {
                                            Character ch = Characters[id];
                                            player.Inventory.Items[ItemName].Wiz(ref ch, force);
                                        }
                                        if (player.Inventory.Items[ItemName].Forse == 0)
                                            player.Inventory.RemoveItem(ItemName);
                                        break;
                                    case SpellType.Without:
                                        if (id == Characters.Count)
                                            player.Inventory.Items[ItemName].Wiz(ref Boss);
                                        else
                                        {
                                            Character ch = Characters[id];
                                            player.Inventory.Items[ItemName].Wiz(ref ch);
                                        }
                                        player.Inventory.RemoveItem(ItemName);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case 3:
                                player.PassItem();
                                break;
                            case 4:
                                if (player as CharacterWithMagic == null)
                                {
                                    Console.WriteLine("you aren't Wizard!");
                                    break;
                                }
                                ((CharacterWithMagic)player).MagicSpell();
                                break;
                            default:
                                break;
                        }
                    }
                    catch (Exception) { }
                    Console.Clear();
                }
                int DeadCount = 0;
                for (int i = 0; i < Characters.Count; i++)
                    if (Characters[i].State == CharacterState.Dead)
                        DeadCount++;
                if (DeadCount == Characters.Count)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You Lose this Battle");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                }
                if (Boss.State == CharacterState.Dead)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"!!! Victory !!!\nYou are defeate {Boss.Name}");
                    Console.ForegroundColor = ConsoleColor.White;
                    foreach (var player in Characters)
                        player.Inventory.AddItem(GetRandomLoot());
                    JsonOperations json = new JsonOperations();
                    for (int j = 0; j < Characters.Count; j++)
                        json.SaveCharacters(Characters[j]);
                    CurrFloor++;
                    return;
                }
                Character attakedCH;
                do
                {
                    attakedCH = Characters[r.Next(0, Characters.Count)];
                } while (attakedCH.State == CharacterState.Dead);
                Boss.Attak(ref attakedCH);
                Console.WriteLine("Charecters states: ");
                Console.WriteLine(string.Join("\n", Characters));
                Console.WriteLine("=============");
                Console.WriteLine("Boss states");
                Console.WriteLine(Boss);
                Console.ReadKey();
                Console.Clear();
            }
        }
        public void Menu()
        {
            try
            {
                JsonOperations json = new JsonOperations();
                int action;
                action = int.Parse(Console.ReadLine());

                switch (action)
                {
                    case 1:
                        BossBattle();
                        break;
                    case 2:
                        Console.WriteLine("Choose character: ");
                        for (int i = 0; i < Characters.Count; i++)
                            Console.WriteLine($"{i}: {Characters[i]}");
                        int num;
                        do
                        {
                            num = int.Parse(Console.ReadLine());
                        } while (num < 0 & num >= Characters.Count);

                        Characters[num].Inventory.PrintItems();
                        Console.Write("Enter name of artifact you want to use: ");
                        string ItemName = Console.ReadLine();
                        if (!Characters[num].Inventory.IsItIn(ItemName))
                        {
                            Console.WriteLine("You haven't this Artifact");
                            break;
                        }
                        Console.WriteLine("Select the entity on which you want to use an artifact");
                        for (int i = 0; i < Characters.Count; i++)
                            Console.WriteLine($"{i}: {Characters[i]}");
                        Console.WriteLine("Enter num: ");
                        int id = int.Parse(Console.ReadLine());
                        int force = 0;
                        switch (Characters[num].Inventory.Items[ItemName].type)
                        {
                            case SpellType.Force:
                                Console.Write("Enter force: ");
                                force = int.Parse(Console.ReadLine());
                                Character ch = Characters[id];
                                Characters[num].Inventory.Items[ItemName].Wiz(ref ch, force);
                                if (Characters[num].Inventory.Items[ItemName].Forse == 0)
                                    Characters[num].Inventory.RemoveItem(ItemName);
                                break;
                            case SpellType.Without:
                                ch = Characters[id];
                                Characters[num].Inventory.Items[ItemName].Wiz(ref ch);
                                Characters[num].Inventory.RemoveItem(ItemName);
                                break;
                            default:
                                break;
                        }
                        break;
                    case 3:
                        Console.WriteLine("Choose character: ");
                        for (int i = 0; i < Characters.Count; i++)
                            Console.WriteLine($"{i}: {Characters[i]}");
                        do
                        {
                            num = int.Parse(Console.ReadLine());
                        } while (num < 0 & num >= Characters.Count);
                        Console.Clear();
                        Characters[num].PassItem();
                        break;
                    case 4:
                        Console.WriteLine("Choose character: ");
                        for (int i = 0; i < Characters.Count; i++)
                            Console.WriteLine($"{i}: {Characters[i]}");
                        do
                        {
                            num = int.Parse(Console.ReadLine());
                        } while (num < 0 & num >= Characters.Count);
                        if (Characters[num] as CharacterWithMagic == null)
                        {
                            Console.WriteLine("you aren't Wizard!");
                            break;
                        }
                        ((CharacterWithMagic)Characters[num]).MagicSpell();
                        break;
                    case 5:
                        for (int j = 0; j < Characters.Count; j++)
                            json.SaveCharacters(Characters[j]);
                        break;
                    case 6:
                        if (Name == null)
                        {
                            Console.Write("Enter name of save: ");
                            Name = Console.ReadLine();
                        }
                        json.SaveGame(this);
                        break;
                    case 7:
                        ConsoleKey key;
                        heal = true;
                        while (heal)
                        {
                            Console.WriteLine("Tap `Enter` to leave Healing room");
                            foreach (Character character in Characters)
                                Console.WriteLine(character);
                            WaitClick();
                            Thread.Sleep(2000);
                            Healing();
                            Console.Clear();
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        async Task WaitClick()
        {
            Task.Run(() =>
            {
                ConsoleKey key = Console.ReadKey().Key;
                switch(key)
                {
                    case ConsoleKey.Enter:
                        heal = false;
                        break;
                    default:
                        break;
                }
            });
        }
        public void Healing()
        {
            for (int i = 0; i < Characters.Count; i++)
            {
                Characters[i].Heal(2);
                if (Characters[i] as CharacterWithMagic != null)
                    (Characters[i] as CharacterWithMagic).RestoreMP(2);
            }
        }
        public static Artifact[] artifacts = new Artifact[] { new DeadWaterBottle(BottleSize.Low), new DeadWaterBottle(BottleSize.Medium), new DeadWaterBottle(BottleSize.High), new DecoctionOfFrogLegs(), new LivingWaterBottle(BottleSize.Low), new LivingWaterBottle(BottleSize.Medium), new LivingWaterBottle(BottleSize.High), new PoisonousSaliva(), new Staff(), new VasiliskEye(), new RandomSpellScroll() };
        public Artifact GetRandomLoot() => artifacts[r.Next(0, artifacts.Length)];

    }
}
