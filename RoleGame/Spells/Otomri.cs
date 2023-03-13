using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleGame.Spells
{
    public class Otomri : Spell
    {
        public static string Name = "Otomri";
        public Otomri(CharacterWithMagic characterWithMagic) : base(85, false, false, characterWithMagic)
        {
             type = SpellType.Without;
        }
        public override void Wiz(ref Character character, int force = 10)
        {
            Wiz(ref character);
        }

        public override void Wiz(ref Character character)
        {
            if(character.State != CharacterState.Paralyzed && character.ParalazedCount == 0)
            {
                Console.WriteLine($"The {character.Name} isn't Paralyzed!");
            }
            else
            {
                if (character.CurrentHealth < character.MaxHealth * 0.1)
                    character.State = CharacterState.Weakened;
                else
                    character.State = CharacterState.Normal;
                character.ParalazedCount = 0;
                Console.WriteLine($"The {character.Name} is {character.State.ToString()}");
                character.CurrentHealth = 1;
            }
            characterWithMagic.CurrentMP -= MinMPValue;
        }
        public override string ToString() => Name;
    }
}
