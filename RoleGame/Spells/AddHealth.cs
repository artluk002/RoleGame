using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleGame
{
    public class AddHealth : Spell
    {
        public override void Wiz(ref Character character, int force = 10)
        {
            int crrForce = force >= 100 ? 100 : force;
            UInt32 MpForSpell = (UInt32)(characterWithMagic.CurrentMP * crrForce / 100);
            characterWithMagic.CurrentMP -= MpForSpell;
            character.Heal(MpForSpell / 2);
        }
        public override void Wiz(ref Character character)
        {
            Wiz(ref character);
        }
        public AddHealth(CharacterWithMagic characterWithMagic) : base(0, false, false, characterWithMagic)
        {
            type = SpellType.Force;
        }
        public static string Name = "AddHealth";
        public override string ToString() => Name;
        
    }
}
