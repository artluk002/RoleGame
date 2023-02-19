using System;

namespace RoleGame
{
    public class Staff : Artifact
    {

        public static string Name = "Staff";
        public Staff(ref Character character) : base(100, true, character) { }
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
    }
}