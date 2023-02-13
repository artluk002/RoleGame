using System;
namespace RoleGame
{
    public enum BottleSize
    {
        Low = 10,
        Medium = 25, 
        High = 50,
    }
    public class LivingWaterBottle : Artifact
    {
        public BottleSize size;

        public LivingWaterBottle(BottleSize Size, UInt32 Forse, bool Reventability) : base(Forse, Reventability)
        {
            this.size = Size;
        }
        
        public void Heal(Character character) => character.Heal((UInt32)size);
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