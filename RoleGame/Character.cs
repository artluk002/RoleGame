using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleGame
{
    public delegate void HealthHandler(object sender, PersonArgs e);
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
    public class Character : IComparable<Character>
    {
        public event HealthHandler Health = (sender, e) =>
        {
            double HealthPercent = ((double)e.Health / (double)e.MaxHealth) * 100;
            if (HealthPercent >= 10)
                (sender as Character).State = CharacterState.Normal;
            else if (HealthPercent < 10)
                (sender as Character).State = CharacterState.Weakened;
            else if (HealthPercent == 0)
                (sender as Character).State = CharacterState.Dead;
        };
        public UInt32 Id { get; private set; }
        public String Name { get; private set; }
        public CharacterState State { get; set; }
        private bool CanSpeak { get; set; }
        private bool CanMove {get; set;}
        public CharacterRace Race { get; private set; }
        public CharacterGender Gender { get; private set; }
        public UInt32 Age { get; private set; }
        public UInt32 CurrentHealth { get; set; }
        public UInt32 MaxHealth { get; private set; }
        public UInt32 XP { get; set; }
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
            XP = 0;
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
                        case "non":
                            Gender = CharacterGender.Non;
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
                XP = 0;
                CanSpeak = false;
                CanMove = false;
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); }
        }
        /// <summary>
        /// Сравнение персонажей
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Character other) => this.XP.CompareTo(other.XP);
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
        public void TakeDamage(UInt32 HP)
        {
            if (CurrentHealth - HP <= 0)
            {
                Console.WriteLine($"The character {Name} is Died!");
                CurrentHealth = 0;
            }
            else
                CurrentHealth -= HP;
            if (Health != null)
                Health(this, new PersonArgs(CurrentHealth, MaxHealth));
        }
        public override string ToString() => $"Id: {Id}\nnick - {Name}, state - {State.ToString()}\nrace - {Race.ToString()}, gender - {Gender.ToString()}, age - {Age}\nspeak - {(CanSpeak == true?"yes":"no")}, move - {(CanMove == true?"yes":"no")}\nHealth {CurrentHealth}/{MaxHealth}\nXP - {XP}";
    }
}
