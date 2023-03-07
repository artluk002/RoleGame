﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleGame.Artifactes
{
    public class PoisonousSaliva : Artifact
    {
        public static string Name = "PoisonousSaliva";
        public override string GetName() => Name;
        public PoisonousSaliva() : base(20, true)
        {
            Description = "- Poisons and damages the character";
            type = SpellType.Force;
        }
        public override void Wiz(ref Character character, int force = 10)
        {
            if (character.State == CharacterState.Dead)
            {
                Console.WriteLine("This artifact can't be used because the enemy is dead"); 
                return;
            }
            else
            {
                if (character.State == CharacterState.Weakened || character.State == CharacterState.Normal)
                {
                    character.State = CharacterState.Poisoned;
                    character.TakeDamage((UInt32)force);
                    Console.WriteLine($"The {character.Name} is {character.State.ToString()} and current health is {character.CurrentHealth}");
                }
            }
        }
        public override void Wiz(ref Character character)
        {
            Wiz(ref character, 10);
        }
        public override string ToString() => $"- {Count}.\n{Name}, {Description}";
    }

}