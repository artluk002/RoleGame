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
        public VasiliskEye(Character character) : base(0, false, character) { }
        public override void Wiz(ref Character character, int force = 10)
        {
            Console.WriteLine("This artifact uses without force");
            return;
        }
        public override void Wiz(ref Character character)
        {
           
            if (character.State == CharacterState.Dead)
            {
                Console.WriteLine($"The {character.Name} can't be used because he is dead!");////////////////////////
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
    }

}