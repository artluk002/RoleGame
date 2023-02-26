using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleGame
{
    public enum SpellType
    {
        Force,
        Without,
        Double,
    }

    public abstract class Spell : IMagic
    {
        public UInt32 MinMPValue;
        public static string Name = "Spell";
        public SpellType type;
        bool IsVerbalSpell;
        bool IsMotorSpell;
        public CharacterWithMagic characterWithMagic;
        public abstract void Wiz(ref Character character, int force = 10);
        public abstract void Wiz(ref Character character);
        public Spell(UInt32 minMPValue, bool isVerbalSpell, bool isMotorSpell, CharacterWithMagic characterWithMagic)
        {
            MinMPValue = minMPValue;
            IsVerbalSpell = isVerbalSpell;
            IsMotorSpell = isMotorSpell;
            this.characterWithMagic = characterWithMagic;
        }
        public override string ToString() => Name;
    }
}
