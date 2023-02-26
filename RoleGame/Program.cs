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

            Character character1 = new Character("Andrew", CharacterRace.Goblin, CharacterGender.Male, 18);
            CharacterWithMagic character2 = new CharacterWithMagic("Vladislave", CharacterRace.Orc, CharacterGender.Male, 20);
            Character character = Character.SummonBoss(110, "Andrew", CharacterGender.Male, CharacterRace.Orc, 400);
            character2.SetLevel(110);
            character2.CreateTeam("AAA");
            character2.team.AddCharacter(character1);
            character.Shield = 0;
            Console.WriteLine(character);
            /*character.TakeDamage(1296);*/
            int count = 0;
            while (character.State != CharacterState.Dead)
            {
                character1.Attak(ref character);
                character2.Attak(ref character);
                count++;
            }
            character2.ForgetSpell(SpellScroll.Armor);
            character2.LearnSpell(SpellScroll.Armor);
            character2.LearnSpell(SpellScroll.Armor);
            /*character2.ForgetSpell(SpellScroll.Armor);*/
            Console.WriteLine(character);
            Console.WriteLine(character2);
            Console.WriteLine(character1);
            Console.WriteLine(count);
            Console.ReadKey();
            
        }
    }
}
