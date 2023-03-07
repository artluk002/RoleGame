using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleGame.Artifactes
{
    /*public enum SpellName
    {
        AddHealth,
        Antidote,
        Armor,
        Cure,
        Otomri,
        Revive,
        Spell
    }*/
    public class RandomSpellScroll : Artifact
    {
        public static string Name = "RandomSpellScroll";
        public SpellScroll SpellType { get; set; }

        public RandomSpellScroll() : base(0, false)
        {
            Random r = new Random();
            SpellType = (SpellScroll)r.Next(0, 6);
        }

        public override string GetName() => Name;

        public override void Wiz(ref Character character, int force = 10)
        {
            Wiz(ref character);
        }

        public override void Wiz(ref Character character)
        {
            if (character as CharacterWithMagic != null)
                (character as CharacterWithMagic).LearnSpell(SpellType);
            else
                return;
        }
    }
}
