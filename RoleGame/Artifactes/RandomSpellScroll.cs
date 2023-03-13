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

        public RandomSpellScroll() : base(0)
        {
            Description = "- this scroll contains a random spell";
            type = SpellType.Without;
        }

        public override string GetName() => Name;

        public override void Wiz(ref Character character, int force = 10)
        {
            Wiz(ref character);
        }

        public override void Wiz(ref Character character)
        {
            Random r = new Random();
            if (character as CharacterWithMagic != null)
                (character as CharacterWithMagic).LearnSpell((SpellScroll)r.Next(0, 6));
            else
                return;
        }
    }
}
