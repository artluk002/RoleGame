using System;
namespace RoleGame
{
    public class Staff : Artifact
    {
        public UInt32 fforse;
        public Staff(UInt32 Fforse, UInt32 Forse, bool Reventability) : base(Forse, Reventability)
        {
            this.fforse = Fforse;
        }
        public void TakeDamage(Character character) => character.TakeDamage();

        public override void Wiz(ref Character character, int force = 10)
        {
            throw new NotImplementedException();
        }

        public override void Wiz(ref Character character)
        {
            throw new NotImplementedException();
        }
    }
}