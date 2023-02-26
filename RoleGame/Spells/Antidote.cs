using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleGame
{
    internal class Antidote : Spell
    {
        public static string Name = "Antidote";
        public Antidote(CharacterWithMagic characterWithMagic) : base(30, false, false, characterWithMagic)
        {
             type = SpellType.Without;
        }
        public override void Wiz(ref Character character, int force = 10)
        {
            Wiz(ref character);
        }
        public override void Wiz(ref Character character)
        {
            if (character.State != CharacterState.Poisoned)
                Console.WriteLine($"The {character.Name} isn't {CharacterState.Poisoned.ToString()}");
            else
            {
                if (character.CurrentHealth < character.MaxHealth * 0.1)
                    character.State = CharacterState.Weakened;
                else
                    character.State = CharacterState.Normal;
                Console.WriteLine($"The {character.Name} is {character.State.ToString()}");
            }
            characterWithMagic.currentMP -= MinMPValue;
        }
        public override string ToString() => Name;
    }
}
