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
        public static string Name = "LivingWaterBottle";
        public BottleSize Size { get; set; }

        public LivingWaterBottle(BottleSize size, Character character) : base(0, false, character) 
        {
            this.Size = size;
            Description = " - Restores character's health, can be used on yourself or \n " +
                "on teammates, and the larger the bottle, the more restores, non-renewable.";
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
        public override string ToString() => $"{Name}, {Size}, {Description}";
    }
}