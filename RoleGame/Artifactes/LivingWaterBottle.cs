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
        public override string GetName() => Size + Name;

        public LivingWaterBottle(BottleSize size) : base(0, false) 
        {
            this.Size = size;
            Description = "- Restores character's health, can be used on yourself or \n " +
                "on teammates, and the larger the bottle, the more restores, non-renewable.";
            type = SpellType.Without;   
        }
        public override void Wiz(ref Character character, int force = 10)
        {
            Wiz(ref character);
        }
        public override void Wiz(ref Character character)
        {
            character.Heal((UInt32)Size);
        }
        public override string ToString() => $"- {Count}.\n{Name}, {Size}, {Description}";
    }
}