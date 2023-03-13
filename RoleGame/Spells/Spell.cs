using Newtonsoft.Json;
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
        public UInt32 MinMPValue { get; set; }
        public static string Name = "Spell";
        public SpellType type { get; set; }
        bool IsVerbalSpell;
        bool IsMotorSpell;
        [JsonIgnore]
        public CharacterWithMagic characterWithMagic { get; set; }
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
