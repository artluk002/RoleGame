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
        public BottleSize Size { get; set; }

        public LivingWaterBottle(BottleSize size, Character character) : base(0, false, character) 
        {
            this.Size = Size;
        }
        public override void Wiz(ref Character character, int force = 10)
        {
            Console.WriteLine("This artifact can't be used with force!");
            return;
        }
        public override void Wiz(ref Character character)
        {
            character.Heal((UInt32)Size);
        }
    }
}