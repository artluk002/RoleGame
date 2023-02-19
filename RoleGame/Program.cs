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

            Character character = new Character("Andrew", CharacterRace.Goblin, CharacterGender.Male, 18);
            CharacterWithMagic character2 = new CharacterWithMagic("Vladislave", CharacterRace.Orc, CharacterGender.Male, 20);
            character = Character.SummonBoss(11, "Andrew", CharacterGender.Male, CharacterRace.Orc, 400);
            /*Console.WriteLine(character);
            Console.WriteLine(character2);
            character2.Spells.Add(Armor.Name, new Armor(character2));
            character2.Spells.Add(Revive.Name, new Revive(character2));
            character2.Spells.Add(AddHealth.Name, new AddHealth(character2));
            character.TakeDamage(100);
            Console.WriteLine(character);
            character2.Spells[Revive.Name].Wiz(ref character);
            Console.WriteLine(character);
            character2.currentMP = 100;
            character2.Spells[AddHealth.Name].Wiz(ref character, 100);
            Console.WriteLine(character);
            Console.WriteLine(character2);*/
            Console.WriteLine(character);
            while (character.State != CharacterState.Dead)
                character2.Attak(ref character);
            Console.WriteLine(character);
            Console.WriteLine(character2);
            Console.ReadKey();
            
        }
    }
}
