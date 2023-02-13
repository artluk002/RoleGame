using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleGame.Spells
{
    public class Otomri : Spell
    {
        public Otomri(CharacterWithMagic characterWithMagic) : base(85, false, false, characterWithMagic) { }
        public override void Wiz(ref Character character, int force = 10)
        {
            Console.WriteLine("You cant use this spell with force")
        }

        public override void Wiz(ref Character character)
        {
            if()
        }
    }
}
