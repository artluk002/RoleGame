using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleGame.Artifactes
{
    public class DecoctionOfFrogLegs : Artifact
    {
        public static string Name = "DecoctionOfFrogLegs";
        public override string GetName() => Name;
        public DecoctionOfFrogLegs() : base(0, false)
        {
            Description = "- Stops the effect of the poison. Can be\n " +
                "used on yourself or teammates, non-renewable.";
            type = SpellType.Without;
        }
        public override void Wiz(ref Character character, int force = 10)
        {
            Wiz(ref character);
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
        public override string ToString() => $"- {Count}.\n{Name}, {Description}";
    }
}
