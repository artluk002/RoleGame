using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Character character = new Character("Andrew", CharacterRace.Goblin, CharacterGender.Non, 18);
            Console.WriteLine(character);
            character.Heal(200);
            Console.WriteLine(character);
            character.TakeDamage(95);
            Console.WriteLine(character);
            Console.ReadKey();
            
        }
    }
}
