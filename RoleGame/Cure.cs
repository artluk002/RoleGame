using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleGame
{
    public class Cure : Spell
    {
        public Cure(CharacterWithMagic characterWithMagic) : base(20, false, false, characterWithMagic)
        {

        }

        public override void Wiz(ref Character character, int force = 10)
        {
            if(character.State != CharacterState.Painful)
                Console.WriteLine($"The {character.Name} isn't Painful");
            else
            {

                if (character.CurrentHealth < character.MaxHealth * 0.5)
                    character.State = CharacterState.Weakened;
                else
                    character.State = CharacterState.Normal;
                Console.WriteLine($"The {character.Name} is {character.State.ToString()}");
            }
            characterWithMagic.currentMP -= MinMPValue;
        }
    }
}
