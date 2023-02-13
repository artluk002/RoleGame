using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleGame.Artifactes
{
    public class DecoctionOfFrogLegs : Artifact
    {
        public DecoctionOfFrogLegs(Character character) : base(0, false, character) { }
        public override void Wiz(ref Character character, int force = 10)
        {
            Console.WriteLine("This artifact can't be used with force");
            return;
        }
        public override void Wiz(ref Character character)
        {
            if(character.State != CharacterState.Poisoned)
            {
                Console.WriteLine($"The {character.Name} hasn't been poisoned!");
                return;
            }
            else
            {
                if (character.CurrentHealth < character.MaxHealth * 0.1)
                    character.State = CharacterState.Weakened;
                else
                    character.State = CharacterState.Normal;
                Console.WriteLine($"The {character.Name} is {character.State.ToString()}");
            }
        }
    }
}
