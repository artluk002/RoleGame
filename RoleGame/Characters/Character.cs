using MyOwnLib;
using Newtonsoft.Json;
using NJsonSchema.Converters;
using RoleGame.Characters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace RoleGame
{
    public delegate void HealthHandler(object sender, PersonArgs e);
    public delegate void XPHandler(object sender, XpArgs e);
    /// <summary>
    /// поле с состоянием персонажа
    /// </summary>
    public enum CharacterState
    {
        Normal, // нормально
        Weakened, // ослаблен
        Painful, // болен
        Poisoned, // отравлен
        Paralyzed, // парализован
        Dead, // мёртв
    }
    /// <summary>
    /// поле с рассами персонажа
    /// </summary>
    public enum CharacterRace
    {
        Person,
        Gnome,
        Elf,
        Orc,
        Goblin,
    }
    public enum CharacterGender
    {
        Male,
        Female,
        Non,
    }
    /// <summary>
    /// класс Персонаж
    /// </summary>
    [JsonConverter(typeof(JsonInheritanceConverter), "type")]
    public class Character : IComparable<Character>
    {
        public event HealthHandler Health = (sender, e) =>
        {
            double HealthPercent = ((double)e.Health / (double)e.MaxHealth) * 100;
            if ((sender as Character).ParalazedCount == 0)
            { 
                (sender as Character).State = CharacterState.Normal;
                (sender as Character).CanMove= true;
            }
            if (HealthPercent >= 10)
            {
                if ((sender as Character).State != CharacterState.Poisoned & (sender as Character).State != CharacterState.Painful & (sender as Character).State != CharacterState.Paralyzed)
                {
                    (sender as Character).State = CharacterState.Normal;
                    (sender as Character).CanMove = true;
                }
            }
            else if (HealthPercent < 10 & HealthPercent > 0)
            {
                if ((sender as Character).State != CharacterState.Poisoned & (sender as Character).State != CharacterState.Painful & (sender as Character).State != CharacterState.Paralyzed)
                {
                    (sender as Character).State = CharacterState.Weakened;
                    (sender as Character).CanMove = true;
                }
            }
            else if (HealthPercent <= 0)
            {
                (sender as Character).State = CharacterState.Dead;
                (sender as Character).CanMove= false;
            }
        };
        public event XPHandler AddXP = (sender, e) =>
        {
            Character character = sender as Character;
            if (e.CurrXp + e.AddedXp >= e.XpToNextLvl)
            {
                int buf = e.CurrXp + e.AddedXp;
                while (buf > ((Character)sender).XpToNextLvl)
                {
                    buf -= ((Character)sender).XpToNextLvl;
                    ((Character)sender).Level++;
                    ((Character)sender).XpToNextLvl += (int)(((Character)sender).XpToNextLvl * 0.05);
                    ((Character)sender).MaxHealth += (uint)(((Character)sender).MaxHealth * 0.05);
                    ((Character)sender).MinDamage += ((Character)sender).MinDamage * 0.05;
                    ((Character)sender).MaxDamage += ((Character)sender).MaxDamage * 0.05;
                    if (sender as CharacterWithMagic != null)
                        ((CharacterWithMagic)sender).MaxMP += (uint)(((CharacterWithMagic)sender).MaxMP * 0.05);
                }
                ((Character)sender).CurrXp = buf;
                //Console.WriteLine($"The {((Character)sender).Name} has {((Character)sender).Level} level");
            }
            else
            {
                ((Character)sender).CurrXp += e.AddedXp;
            }
        };
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public CharacterState State { get; set; }
        public bool CanSpeak { get; set; }
        public bool CanMove { get; set; }
        public CharacterRace Race { get; set; }
        public CharacterGender Gender { get; set; }
        public UInt32 Age { get; set; }
        public UInt32 CurrentHealth { get; set; }
        public UInt32 MaxHealth { get; set; }
        public double MinDamage { get; set; }
        public double MaxDamage { get; set; }
        public int Level { get; set; }
        public int XpToNextLvl { get; set; }
        public int CurrXp { get; set; }
        public int Shield { get; set; }
        public Inventory Inventory { get; set; }
        [JsonIgnore]
        public Team team { get; set; }
        [JsonIgnore]
        public int ParalazedCount { get; set; }
        private Random r;

        /// <summary>
        /// конструктор с пользовательскими параметрами для создания персонажа
        /// </summary>
        /// <param name="name">Имя персонажа</param>
        /// <param name="race">Раса персонажа</param>
        /// <param name="gender">пол персонажа</param>
        /// <param name="age">возраст персонажа</param>
        public Character(String name, CharacterRace race, CharacterGender gender, UInt32 age)
        {
            r = new Random();
            Id = (Int32)(name.GetHashCode() / r.Next());
            Name = name;
            State = CharacterState.Normal;
            Race = race;
            Gender = gender;
            Age = age;
            CurrentHealth = 100;
            MaxHealth = 100;
            MinDamage = 10;
            MaxDamage = 15;
            Level = 1;
            XpToNextLvl = 100;
            CurrXp = 0;
            CanSpeak = true;
            CanMove = true;
            Inventory = new Inventory();
            ParalazedCount = 0;
        }
        public Character() { }
        public Character(bool i)
        {
            r = new Random();
            try
            {
                Console.Write("Enter character Name: ");
                Name = Console.ReadLine();
                Id = (Int32)(Name.GetHashCode() * r.Next());
                bool validDate = false;
                State = CharacterState.Normal;
                do
                {
                    Console.Write($"Enter race you want to play (Person, Gnome, Elf, Orc, Goblin): ");
                    String race = Console.ReadLine().ToLower();
                    switch (race)
                    {
                        case "person":
                            Race = CharacterRace.Person;
                            validDate = true;
                            break;
                        case "gnome":
                            Race = CharacterRace.Gnome;
                            validDate = true;
                            break;
                        case "elf":
                            Race = CharacterRace.Elf;
                            validDate = true;
                            break;
                        case "orc":
                            Race = CharacterRace.Orc;
                            validDate = true;
                            break;
                        case "goblin":
                            Race = CharacterRace.Goblin;
                            validDate = true;
                            break;
                        default:
                            Console.WriteLine("This Race isn't exist!");
                            break;
                    }
                } while (!validDate);
                validDate = false;
                do
                {
                    Console.Write("Enter character gender (Male, Female, Non): ");
                    String gender = Console.ReadLine().ToLower();
                    switch (gender)
                    {
                        case "male":
                            Gender = CharacterGender.Male;
                            validDate = true;
                            break;
                        case "female":
                            Gender = CharacterGender.Female;
                            validDate = true;
                            break;
                        default:
                            Console.WriteLine("This Gender isn't exist");
                            break;
                    }
                } while (!validDate);
                validDate = false;
                Console.Write("Enter character age: ");
                Age = UInt32.Parse(Console.ReadLine());
                CurrentHealth = 100;
                MaxHealth = 100;
                Level = 1;
                XpToNextLvl = 100;
                CurrXp = 0;
                CanSpeak = true;
                CanMove = true;
                MinDamage = 10;
                MaxDamage = 15;
                Inventory = new Inventory();
                ParalazedCount = 0;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        /// <summary>
        /// Сравнение персонажей
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Character other) => this.CurrXp.CompareTo(other.CurrXp);
        public void Heal(UInt32 HP)
        {
            if (CurrentHealth == 0)
            {
                Console.WriteLine($"The character {Name} is Dead");
            }
            if (CurrentHealth + HP >= MaxHealth)
            {
                CurrentHealth = MaxHealth;
                Console.WriteLine($"The character {Name} was healing on full");
            }
            else
            {
                CurrentHealth += HP;
                Console.WriteLine($"The character {Name} was healing on {HP} HP");
            }
            if (Health != null)
                Health(this, new PersonArgs(CurrentHealth, MaxHealth));
        }
        public void Attak(ref Character character)
        {
            if (r == null)
                r = new Random();
            if (State == CharacterState.Paralyzed || ParalazedCount > 0)
            {
                Console.WriteLine($"The {Name} is paralyzed and can't attak");
                return;
            }
            else if (State == CharacterState.Weakened)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"The {Name} is Weakened, attack is weakened by 30%");
                Console.ForegroundColor = ConsoleColor.White;
                character.TakeDamage((UInt32)r.Next((int)(MinDamage * 0.7), (int)(MaxDamage * 0.7 + 1)));
            }
            else if (State == CharacterState.Painful)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"The {Name} Painful, attack is weakened by 50%");
                Console.ForegroundColor = ConsoleColor.White;
                character.TakeDamage((UInt32)r.Next((int)(MinDamage * 0.5), (int)(MaxDamage * 0.5 + 1)));
            }
            else if (State == CharacterState.Poisoned)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"The {Name} Poisoned, attack is weakened by 40%");
                Console.ForegroundColor = ConsoleColor.White;
                character.TakeDamage((UInt32)r.Next((int)(MinDamage * 0.6), (int)(MaxDamage * 0.6 + 1)));
            }
            else
            {
                character.TakeDamage((UInt32)r.Next((int)MinDamage, (int)MaxDamage + 1));
            }
            if (character.State == CharacterState.Dead)
            {
                Console.WriteLine($"The {character.Name} was defeated");
                if (AddXP != null)
                    AddXP(this, new XpArgs(CurrXp, XpToNextLvl, character.Level * 10));
                if (team != null)
                    if (team.Characters.Count > 1)
                        foreach (Character player in team.Characters)
                        {
                            if (player == this)
                                continue;
                            AddXP(player, new XpArgs(player.CurrXp, player.XpToNextLvl, character.Level * 10));
                        }
            }
        }
        public void TakeDamage(UInt32 HP)
        {
            while (HP != 0 && State != CharacterState.Dead)
            {

                if (Shield > 0)
                {
                    if (HP > MaxHealth)
                    {
                        HP -= MaxHealth;
                        Shield--;
                    }
                    else
                    {
                        HP = 0;
                        Shield--;
                    }
                }
                else
                {
                    if (HP >= CurrentHealth)
                    {
                        Console.WriteLine($"The character {Name} is Died!");
                        CurrentHealth = 0;
                        HP = 0;
                    }
                    else
                    {
                        CurrentHealth -= HP;
                        HP = 0;
                    }
                }
            }
            if (ParalazedCount > 0)
                ParalazedCount--;
            if (Health != null)
                Health(this, new PersonArgs(CurrentHealth, MaxHealth));
        }
        public void SetLevel(int level)
        {
            while (level > Level)
            {
                if (AddXP != null)
                    AddXP(this, new XpArgs(CurrXp, XpToNextLvl, XpToNextLvl));
            }

        }
        public static Character SummonBoss(int level, string name, CharacterGender gender, CharacterRace race, uint age)
        {
            Character Boss;
            Boss = new Character(name, race, gender, age);
            Boss.MaxHealth = 300;
            Boss.MinDamage = 30;
            Boss.MaxDamage = 40;
            Boss.SetLevel(level);
            Boss.CurrentHealth = Boss.MaxHealth;
            return Boss;

        }
        public void CreateTeam(string name)
        {
            team = new Team(name);
            team.AddCharacter(this);
        }
        public void PassItem()
        {
            int i = 0;
            foreach (var item in Inventory.Items)
                Console.WriteLine($"{item.Key} - {item.Value.Count}");
            Console.Write("Enter name of item you want to pass: ");
            string ItemName = Console.ReadLine();
            if (!Inventory.Items.ContainsKey(ItemName))
            {
                Console.WriteLine("Item with such name isn't exist in your inventory!");
                return;
            }
            else
            {
                do
                {
                    for (int j = 0; j < team.Characters.Count; j++)
                        Console.WriteLine($"{j}: {team.Characters[j]}");

                    Console.Write("Enter number of your teammate: ");
                    i = int.Parse(Console.ReadLine());
                } while (i > team.Characters.Count | i < 1);
                team.Characters[i].Inventory.AddItem(Inventory.Items[ItemName]);
                Inventory.RemoveItem(ItemName);
            }
        }
        public override string ToString() => $"==Character: {Name}==\n" +
            $"Id: {Id},state: {State.ToString()}\n" +
            $"race: {Race.ToString()}, gender: {Gender.ToString()}, age: {Age}\n" +
            $"speak: {(CanSpeak == true ? "yes" : "no")}, move: {(CanMove == true ? "yes" : "no")}\n" +
            $"Health: {CurrentHealth}/{MaxHealth}\n" +
            $"Damage: {MinDamage}/{MaxDamage}\n" +
            $"Level: {Level}\n" +
            $"XP: {CurrXp}/{XpToNextLvl}\n" +
            $"Shields: {Shield}\n" +
            $"Inventory: {$"{Inventory.GetItems()}"}\n" +
            $"============={Functions.Fill("=", Name.Length)}==";
    }
}
