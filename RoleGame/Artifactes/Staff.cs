using System;

namespace RoleGame
{
    public class Staff : Artifact
    {

        public static string Name = "Staff";
        public Staff(ref Character character) : base(100, true, character)// допилить восстановление мощьности
        {
            Description = " - Deals damage to the enemy. Can be used until its\n " +
                "power runs out, at which point it becomes unusable.";
        }
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
            Console.WriteLine("This artifact can't be used without force!");
            return;
        }
        public override string ToString() => $"{Name}, {Description}";
    }
}