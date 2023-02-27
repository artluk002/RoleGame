using System;
namespace RoleGame
{
    public class DeadWaterBottle : Artifact
    {
        public static string Name = "DeadWaterBottle";
        public BottleSize Size { get; set; }
        public override string GetName() => Size + Name;
        public DeadWaterBottle(BottleSize size) : base(0, false)
        {
            this.Size = size;
            Description = "- Restores the mana of a character who owns magic,\n" +
                "but is useless for a normal character, and the larger the bottle,\n " +
                "the more it will restore, non-renewable.";
            type = SpellType.Without;
        }
        public override void Wiz(ref Character character, int force = 10)
        {
            Wiz(ref character);
        }
        public override void Wiz(ref Character character)
        {
            if(character as CharacterWithMagic != null)
            {
                (character as CharacterWithMagic).RestoreMP((UInt32)Size);
                Console.WriteLine($"Mana of {character.Name} has been increased to {(character as CharacterWithMagic).currentMP}");
            }
        }
        public override string ToString() => $"- {Count}.\n{Name}, {Size}, {Description}";
    }
}