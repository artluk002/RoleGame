using System;
namespace RoleGame
{
    public class DeadWaterBottle : Artifact
    {
        public BottleSize size;

        public DeadWaterBottle(BottleSize Size, UInt32 Forse, bool Reventability) : base(Forse, Reventability)
        {
            this.size = Size;
        }

        public void RestoreMP(CharacterWithMagic wizard) => wizard.RestoreMP((UInt32)size);
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