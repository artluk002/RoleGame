using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleGame
{
    public abstract class Spell : IMagic
    {
        public UInt32 MinMPValue;
        bool IsVerbalSpell;//#6,#2
        bool IsMotorSpell;
        public CharacterWithMagic characterWithMagic;
        public abstract void Wiz(ref Character character, int force = 10);
        public Spell(UInt32 minMPValue, bool isVerbalSpell, bool isMotorSpell, CharacterWithMagic characterWithMagic)
        {
            MinMPValue = minMPValue;
            IsVerbalSpell = isVerbalSpell;
            IsMotorSpell = isMotorSpell;
            this.characterWithMagic = characterWithMagic;
        }
    }
}
