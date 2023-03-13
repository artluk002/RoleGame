using System;

namespace RoleGame
{
    public class Staff : Artifact
    {

        public static string Name = "Staff";
        public Staff() : base(100)// допилить восстановление мощьности
        {
            Description = "- Deals damage to the enemy. Can be used until its\n " +
                "power runs out, at which point it becomes unusable.";
            type = SpellType.Force;
        }
        public override string GetName() => Name;
        public override void Wiz(ref Character character, int force = 10)
        {
            if (Forse - force < 0)
            {
                character.TakeDamage(Forse);
                Forse = 0;
            }
            else
            {
                Forse -= (UInt32)force;
                character.TakeDamage((UInt32)force);
            }
        }
        public override void Wiz(ref Character character)
        {
            Wiz(ref character, 10);
        }
        public override string ToString() => $"- {Count}.\n{Name}, {Description}";
    }
}