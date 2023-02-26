using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleGame.Artifactes
{
    public class PoisonousSaliva : Artifact
    {
        public static string Name = "PoisonousSaliva";
        public override string GetName() => Name;
        public PoisonousSaliva(Character character) : base(20, true, character)
        {
            Description = "- Poisons and damages the character";
        }
        public override void Wiz(ref Character character, int force = 10)
        {
            if (character.State == CharacterState.Dead)
            {
                Console.WriteLine("This artifact can't be used because the enemy is dead"); //Console.WriteLine($"The {character.Name} can't be poisoned because he is dead!");
                return;
            }
            else
            {
                if (character.State == CharacterState.Weakened || character.State == CharacterState.Normal)
                {
                    character.State = CharacterState.Poisoned;
                    character.TakeDamage((UInt32)force);
                    Console.WriteLine($"The {character.Name} is {character.State.ToString()} and current health is {character.CurrentHealth}");
                }
            }
        }
        public override void Wiz(ref Character character)
        {
            Wiz(ref character, 10);
        }
        public override string ToString() => $"- {Count}.\n{Name}, {Description}";
    }

}