using MyOwnLib;
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
    }
    /// <summary>
    /// класс Персонаж
    /// </summary>
    public class Character : IComparable<Character>
    {
        public event HealthHandler Health = (sender, e) =>
        {
            double HealthPercent = ((double)e.Health / (double)e.MaxHealth) * 100;
            if (HealthPercent >= 10)
                (sender as Character).State = CharacterState.Normal;
            else if (HealthPercent < 10 & HealthPercent > 0)
                (sender as Character).State = CharacterState.Weakened;
            else if (HealthPercent <= 0)
                (sender as Character).State = CharacterState.Dead;
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
                    ((Character)sender).MinDamage += (int)(((Character)sender).MinDamage * 0.05);
                    ((Character)sender).MaxDamage += (int)(((Character)sender).MaxDamage * 0.05);
                    if (sender as CharacterWithMagic != null)
                        ((CharacterWithMagic)sender).maxMP += (uint)(((CharacterWithMagic)sender).maxMP * 0.05);
                }
                ((Character)sender).CurrXp = buf;
                //Console.WriteLine($"The {((Character)sender).Name} has {((Character)sender).Level} level");
            }
            else
            {
                ((Character)sender).CurrXp += e.AddedXp;
            }
        };
        public UInt32 Id { get; private set; }
        public String Name { get; private set; }
        public CharacterState State { get; set; }
        public bool CanSpeak { get; set; }
        public bool CanMove { get; set; }
        public CharacterRace Race { get; private set; }
        public CharacterGender Gender { get; private set; }
        public UInt32 Age { get; private set; }
        public UInt32 CurrentHealth { get; set; }
        public UInt32 MaxHealth { get; private set; }
        public int MinDamage { get; protected set; }
        public int MaxDamage { get; protected set; }
        public int Level { get; protected set; }
        public int XpToNextLvl { get; protected set; }
        public int CurrXp { get; set; }
        public int Shield { get; set; }
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
            Id = (UInt32)(name.GetHashCode() / r.Next());
            Name = name;
            State = CharacterState.Normal;
            Race = race;
            Gender = gender;
            Age = age;
            CurrentHealth = 100;
            MaxHealth = 100;
            MinDamage = 5;
            MaxDamage = 10;
            Level = 1;
            XpToNextLvl = 100;
            CurrXp = 0;
            CanSpeak = false;
            CanMove = false;
        }
        /// <summary>
        /// конструктор без параметров для создания персонажа
        /// </summary>
        public Character()
        {
            r = new Random();
            try
            {
                Console.Write("Enter character Name: ");
                Name = Console.ReadLine();
                Id = (UInt32)(Name.GetHashCode() / r.Next());
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
                            validDate = false;
                            break;
                        case "elf":
                            Race = CharacterRace.Elf;
                            validDate = true;
                            break;
                        case "orc":
                            Race = CharacterRace.Orc;
                            validDate = false;
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
                UInt32 age = UInt32.Parse(Console.ReadLine());
                CurrentHealth = 100;
                MaxHealth = 100;
                Level = 1;
                XpToNextLvl = 100;
                CurrXp = 0;
                CanSpeak = false;
                CanMove = false;
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
            if (State == CharacterState.Paralyzed)
            {
                Console.WriteLine($"You are paralyzed, you can't attak");
                return;
            }
            else if (State == CharacterState.Weakened)
            {
                Console.WriteLine($"You are Weakened, your attack is weakened by 30%");
                character.TakeDamage((UInt32)r.Next((int)(MinDamage * 0.7), (int)(MaxDamage * 0.7 + 1)));
            }
            else if (State == CharacterState.Painful)
            {
                Console.WriteLine($"You are Painful, your attack is weakened by 50%");
                character.TakeDamage((UInt32)r.Next((int)(MinDamage * 0.5), (int)(MaxDamage * 0.5 + 1)));
            }
            else if (State == CharacterState.Poisoned)
            {
                Console.WriteLine($"You are Weakened, your attack is weakened by 40%");
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
            }
        }
        public void TakeDamage(UInt32 HP)
        {
            if (Shield > 0)
            {
                Shield--;
                Console.WriteLine($"The Shield is take damege, current shield count is {Shield}");
                return;
            }
            if ((int)CurrentHealth - (int)HP <= 0)
            {
                Console.WriteLine($"The character {Name} is Died!");
                CurrentHealth = 0;
            }
            else
                CurrentHealth -= HP;
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
            Boss.MaxHealth = 400;
            Boss.MinDamage = 10;
            Boss.MaxDamage = 30;
            Boss.SetLevel(level);
            Boss.CurrentHealth = Boss.MaxHealth;
            return Boss;

        }
        public override string ToString() => $"==Character: {Name}==\n" +
            $"Id: {Id},state: {State.ToString()}\n" +
            $"race: {Race.ToString()}, gender: {Gender.ToString()}, age: {Age}\n" +
            $"speak: {(CanSpeak == true ? "yes" : "no")}, move: {(CanMove == true ? "yes" : "no")}\n" +
            $"Health: {CurrentHealth}/{MaxHealth}\n" +
            $"Damage: {MinDamage} - {MaxDamage}\n" +
            $"Level: {Level}\n" +
            $"XP: {CurrXp}/{XpToNextLvl}\n" +
            $"============={Functions.Fill("=", Name.Length)}==";
    }
}
