﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleGame.Spells
{
    public class Otomri : Spell
    {
        public Otomri(CharacterWithMagic characterWithMagic) : base(85, false, false, characterWithMagic) { }
        public override void Wiz(ref Character character, int force = 10)
        {
            Console.WriteLine("You cant use this spell with force");
            return;
        }

        public override void Wiz(ref Character character)
        {
            if(character.State != CharacterState.Paralyzed)
            {
                Console.WriteLine($"The {character.Name} isn't Paralyzed!");
            }
            else
            {
                if (character.CurrentHealth < character.MaxHealth * 0.5)
                    character.State = CharacterState.Weakened;
                else
                    character.State = CharacterState.Normal;
                Console.WriteLine($"The {character.Name} is {character.State.ToString()}");
                character.CurrentHealth = 1;
            }
            characterWithMagic.currentMP -= MinMPValue;
        }
    }
}