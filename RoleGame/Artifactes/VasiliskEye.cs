using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleGame.Artifactes
{
    public class VasiliskEye : Artifact
    {
        public static string Name = "PoisonousSaliva";
        public override string GetName() => Name;
        public VasiliskEye() : base(0, false)
        {
            Description = "- Paralyzes the enemy, non-renewable.";
        }
        public override void Wiz(ref Character character, int force = 10)
        {
            Wiz(ref character);
        }
        public override void Wiz(ref Character character)
        {
           
            if (character.State == CharacterState.Dead)
            {
                Console.WriteLine("This artifact can't be used because the enemy is dead"); //Console.WriteLine($"The {character.Name} can't be used because he is dead!");////////////////////////Console.WriteLine("This artifact can't be used because the enemy is dead");
                return;
            }
            else
            {
                if (character.State != CharacterState.Paralyzed)
                {
                    character.State = CharacterState.Paralyzed;
                    Console.WriteLine($"The {character.Name} is {character.State.ToString()} ");
                }
            }
        }
        public override string ToString() => $"- {Count}.\n{Name}, {Description}";
    }

}