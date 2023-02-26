using System;

namespace RoleGame
{
    public abstract class Artifact:IMagic
    {
        public static string Name = "Artifact";
        public int Count { get; set; }
        public UInt32 Forse { get; set; }
        public bool Rewenability { get; set; }
        public Character character { get; set; }
        public string Description { get; protected set; }
        public abstract void Wiz(ref Character character, int force = 10);
        public abstract void Wiz(ref Character character);
        public Artifact(uint forse, bool rewenability, Character character)
        {
            Forse = forse;
            Rewenability = rewenability;
            this.character = character;
        }
    }
}