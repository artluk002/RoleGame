using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace RoleGame
{
    public class Armor : Spell
    {
        public static string Name = "Armor";
        public Armor(CharacterWithMagic characterWithMagic) : base(50, false, false, characterWithMagic)
        {
            type = SpellType.Double;
        }
        public override void Wiz(ref Character character, int force = 10)
        {
            int crrForce = force >= 100 ? 100 : force;
            UInt32 MpForSpell = (UInt32)(characterWithMagic.CurrentMP * crrForce / 100);
            if (MpForSpell < 50)
            {
                Console.WriteLine("You can't use Armor spell with current mana");
                return;
            }
            character.Shield += (int)Math.Round((double)MpForSpell / (double)MinMPValue);
            Console.WriteLine($"The {character.Name} shield has been increased to {character.Shield}");
            characterWithMagic.CurrentMP -= MpForSpell;
        }
        public override void Wiz(ref Character character)
        {
            if(characterWithMagic.CurrentMP < 50)
            {
                Console.WriteLine("You can't use Armor spell with current mana");
                return;
            }
            character.Shield++;
            Console.WriteLine($"The {character.Name} shield has been increased to {character.Shield}");
            characterWithMagic.CurrentMP -= MinMPValue;
        }
        public override string ToString() => Name;
    }
}
