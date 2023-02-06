using System;
namespace RoleGame
{
    public abstract class Artifact:IMagic
    {
        public UInt32 power;
        bool rewenability;
        public Artifact(UInt32 Power, bool Reventability)
        {
            this.power= Power;
            this.rewenability = Reventability;
        }

        public abstract void Wiz(Character character, int force = 10);
    }
}