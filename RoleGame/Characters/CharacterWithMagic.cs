using MyOwnLib;
using System;
using System.Collections.Generic;

namespace RoleGame
{
    public class CharacterWithMagic : Character
    {
        public UInt32 currentMP;
        public UInt32 maxMP;
        public Dictionary<string, Spell> Spells;
        public CharacterWithMagic(string name, CharacterRace race, CharacterGender gender, UInt32 age) : base(name, race, gender, age)
        {
            Spells = new Dictionary<string, Spell>();
            currentMP = 150;
            maxMP = 150;
        }
        public CharacterWithMagic() : base()
        {
            currentMP = 150;
            maxMP = 150;
            Spells = new Dictionary<string, Spell>();
        }
        public void RestoreMP(UInt32 MP)
        {
            if(currentMP + MP > maxMP)
            {
                Console.WriteLine($"The {Name} has full mana");
                currentMP = maxMP;
            }
            else
            {
                Console.WriteLine($"The {Name} restored {MP} units of mana");
                currentMP += MP;
            }
        }
        
        public void Heal(Character character)
        {
            if (((character.MaxHealth - character.CurrentHealth) * 2) <= currentMP)
                character.Heal(character.MaxHealth - character.CurrentHealth);
            else
                character.Heal(currentMP / 2);
        }
        public override string ToString() => $"==Character: {Name}==\n" +
            $"Id: {Id},state: {State.ToString()}\n" +
            $"race: {Race.ToString()}, gender: {Gender.ToString()}, age: {Age}\n" +
            $"speak: {(CanSpeak == true ? "yes" : "no")}, move: {(CanMove == true ? "yes" : "no")}\n" +
            $"Health: {CurrentHealth}/{MaxHealth}\n" +
            $"Mana {currentMP}/{maxMP}\n" +
            $"Damage: {MinDamage}/{MaxDamage}" +
            $"Level: {Level}\n" +
            $"XP - {CurrXp}/{XpToNextLvl}\n" +
            $"============={Functions.Fill("=", Name.Length)}==";
    }
}
