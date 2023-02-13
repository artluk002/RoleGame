using System;
namespace RoleGame
{
    public abstract class Artifact:IMagic
    {
        public UInt32 forse;
        bool rewenability;
        public Artifact(UInt32 forse, bool Reventability)
        {
            d
            this.forse= forse;
            this.rewenability = Reventability;
        }

        public abstract void Wiz(ref Character character, int force = 10);
    }
}