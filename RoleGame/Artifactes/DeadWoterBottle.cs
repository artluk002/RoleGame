using System;
namespace RoleGame
{
    public class DeadWaterBottle : Artifact
    {
        public static string Name = "DeadWaterBottle";
        public BottleSize Size { get; set; } 

        public DeadWaterBottle(BottleSize size, Character character) : base(0, false, character)
        {
            this.Size = size;
        }
        public override void Wiz(ref Character character, int force = 10)
        {
            Console.WriteLine("This artifact can't be used with force!");
            return;
        }
        public override void Wiz(ref Character character)
        {
            if(character as CharacterWithMagic != null)
            {
                (character as CharacterWithMagic).RestoreMP((UInt32)Size);
                Console.WriteLine($"Mana of {character.Name} has been increased to {(character as CharacterWithMagic).currentMP}");
            }
        }
    }
}