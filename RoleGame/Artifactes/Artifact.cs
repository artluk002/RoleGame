using System;
namespace RoleGame
{
    public abstract class Artifact:IMagic
    {
        public UInt32 forse;
        bool rewenability;
        public Artifact(UInt32 Forse, bool Reventability)
        {
            this.forse= Forse;
            this.rewenability = Reventability;
        }
        public Artifact(UInt32 Forse, bool Reventability)
        {
            Forse = 100;
        }
        public abstract void Wiz(ref Character character, int force = 10);
        public abstract void Wiz(ref Character character);
    }
}