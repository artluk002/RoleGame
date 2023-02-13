using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleGame
{
    public class Armor : Spell
    {
        public Armor(CharacterWithMagic characterWithMagic) : base(50, false, false, characterWithMagic) { }
        public override void Wiz(ref Character character, int force = 10)
        {
            int crrForce = force >= 100 ? 100 : force;
            UInt32 MpForSpell = (UInt32)(characterWithMagic.currentMP * crrForce / 100);
            if (MpForSpell < 50)
            {
                Console.WriteLine("You can't use Armor spell with current mana");
                return;
            }


        }
        public override void Wiz(ref Character character)
        {
            throw new NotImplementedException();
        }
    }
}
