using RoleGame.Artifactes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RoleGame.Plot
{
    public class Game
    {
        List<Character> Characters;
        int CurrFloor;
        string[] BossNames;
        Random r;
        public Game()
        {
            Characters = new List<Character>();
            CurrFloor = 1;
            BossNames = new string[] { "Jhon", "Valera", "Vanya" };
            r = new Random();
        }
        public void Run()
        {
            while (true)
            {

            }
        }
        public void Precondition()
        {
            int PlayersCount = 1;
            Console.Write("Enter count of palyers: ");
            PlayersCount = int.Parse(Console.ReadLine());
            string answer;
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
                        ////
                        break;
                    case "no":
                        switch (r.Next(1, 3))
                        {
                            case 1:
                                Characters.Add(new Character());
                                break;
                            case 2:
                                Characters.Add(new CharacterWithMagic());
                                break;
                            default:
                                Characters.Add(new Character());
                                break;
                        }
                        break;
                }
            }
            Characters[0].CreateTeam("AAA");
            for (int i = 0; i < Characters.Count; i++)
                if (i != 0)
                {
                    Characters[0].team.AddCharacter(Characters[i]);
                    Characters[i].team = Characters[0].team;
                }
        }
        public async void Save()
        {
            if (!File.Exists("Characters.json"))
                File.Create("Characters.json").Close();
            
            using (FileStream fs = new FileStream("Characters.json", FileMode.Append, FileAccess.Write))
            {
                await JsonSerializer.SerializeAsync<List<Character>>(fs, Characters, new JsonSerializerOptions { WriteIndented = true});
                Console.WriteLine("Data has been saved to file");
            }
        }
        public void BossBattle()
        {
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
                        action = int.Parse(Console.ReadLine());
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
                                        break;
                                    case SpellType.Without:
                                        if (id == Characters.Count)
                                            player.Inventory.Items[ItemName].Wiz(ref Boss);
                                        else
                                        {
                                            Character ch = Characters[id];
                                            player.Inventory.Items[ItemName].Wiz(ref ch);
                                        }
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
                                    return;
                                }
                                ((CharacterWithMagic)player).MagicSpell();
                                break;
                            default:
                                break;
                        }
                    }
                    catch (Exception ex) { }
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
                    Save();
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
        public static Artifact[] artifacts = new Artifact[] { new DeadWaterBottle(BottleSize.Low), new DeadWaterBottle(BottleSize.Medium), new DeadWaterBottle(BottleSize.High), new DecoctionOfFrogLegs(), new LivingWaterBottle(BottleSize.Low), new LivingWaterBottle(BottleSize.Medium), new LivingWaterBottle(BottleSize.High), new PoisonousSaliva(), new Staff(), new VasiliskEye() };
        public Artifact GetRandomLoot() => artifacts[r.Next(0, artifacts.Length)];

    }
}
