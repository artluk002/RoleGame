using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace RoleGame
{
    public class Revive : Spell
    {
        public static string Name = "Revive";
        public Revive(CharacterWithMagic characterWithMagic) : base(150, false, false, characterWithMagic)
        {
        }
        public override void Wiz(ref Character character, int force = 10)
        {
            Wiz(ref character);
        }
        public override void Wiz(ref Character character)
        {
            if (character.State != CharacterState.Dead)
                Console.WriteLine($"The {character.Name} isn't {CharacterState.Dead.ToString()}");
            else
            {
                character.State = CharacterState.Weakened;
                character.CurrentHealth = 1;
                Console.WriteLine($"The {character.Name} is {character.State.ToString()}");
            }
            characterWithMagic.currentMP -= MinMPValue;
        }
        public override string ToString() => Name;
    }
}
