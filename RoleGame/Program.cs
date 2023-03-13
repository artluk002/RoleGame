using RoleGame.Artifactes;
using RoleGame.Characters;
using RoleGame.Plot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleGame
{
    public class Program
    {
        static void Main(string[] args)
        {
            /*Character character1 = new Character("Andrew", CharacterRace.Goblin, CharacterGender.Male, 18);
            CharacterWithMagic character2 = new CharacterWithMagic("Vladislave", CharacterRace.Orc, CharacterGender.Male, 20);
            character1.CreateTeam("BUFA");
            character1.team.AddCharacter(character2);*/
            /*Character character = Character.SummonBoss(110, "Andrew", CharacterGender.Male, CharacterRace.Orc, 400);
            character2.SetLevel(110);
            character2.CreateTeam("AAA");
            character2.team.AddCharacter(character1);
            character2.LearnSpell(SpellScroll.Armor);
            character2.LearnSpell(SpellScroll.Revive);
            character.Shield = 0;
            Console.WriteLine(character);
            character.TakeDamage(1296);
            int count = 0;
            character2.MagicSpell();
            while (character.State != CharacterState.Dead)
            {
                character1.Attak(ref character);
                character2.Attak(ref character);
                count++;
            }
            character2.ForgetSpell(SpellScroll.Armor);
            Console.WriteLine(character);
            Console.WriteLine(character2);
            Console.WriteLine(character1);
            Console.WriteLine(count);*/

            /*character1.Inventory.AddItem(new DeadWaterBottle(BottleSize.High));
            character1.Inventory.AddItem(new LivingWaterBottle(BottleSize.Low));
            character1.Inventory.AddItem(new LivingWaterBottle(BottleSize.Medium));
            character1.Inventory.AddItem(new LivingWaterBottle(BottleSize.High));
            character1.Inventory.AddItem(new LivingWaterBottle(BottleSize.High));
            character1.Inventory.AddItem(new DeadWaterBottle(BottleSize.Medium));
            character1.Inventory.AddItem(new DeadWaterBottle(BottleSize.Low));
            character1.Inventory.AddItem(new DeadWaterBottle(BottleSize.High));
            character1.Inventory.AddItem(new PoisonousSaliva());
            character1.Inventory.AddItem(new DeadWaterBottle(BottleSize.Medium));
            character1.Inventory.AddItem(new Staff());
            character1.Inventory.AddItem(new Staff());
            character1.Inventory.PrintItems();
            character1.PassItem();
            Console.WriteLine("===============");
            character2.Inventory.PrintItems();
            Console.WriteLine("===============");
            character1.Inventory.PrintItems();
            Console.WriteLine("===============");*/
            /*Character an = new Character(true);
            JsonOperations json = new JsonOperations();
            json.SaveCharacters(an);
            List<Character> a = json.ReadCharacters();
            foreach (Character c in a)
            {
                if(c as CharacterWithMagic != null)
                    Console.WriteLine(true);
                else
                    Console.WriteLine(false);
            
            }*/
            Game g = new Game();
            g.Run();
            /*g.Precondition();
            g.BossBattle();*/
            Console.ReadKey();
        }
    }
}
