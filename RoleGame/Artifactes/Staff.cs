using System;
namespace RoleGame
{
    public class Staff : Artifact
    {
        public Staff(UInt32 Forse, bool Reventability) : base(Forse, Reventability)
        {
            
        }
        //public void RestoreMP(CharacterWithMagic wizard) => wizard.RestoreMP((UInt32)size);
        public override void Wiz(ref Character character, int force = 10)
        {
            throw new NotImplementedException();
        }
    }
}