using MyOwnLib;
using Newtonsoft.Json;
using RoleGame.Spells;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace RoleGame
{
    public enum SpellScroll
    {
        AddHealth,
        Antidote,
        Armor,
        Cure,
        Otomri,
        Revive,
    }
    public class CharacterWithMagic : Character
    {
        public UInt32 CurrentMP { get; set; }
        public UInt32 MaxMP { get; set; }
        public Dictionary<string, Spell> Spells { get; set; }
        public CharacterWithMagic(CharacterWithMagic clone) : base(clone)
        {
            CurrentMP = clone.CurrentMP;
            MaxMP = clone.MaxMP;
            Spells = clone.Spells;
        }
        public CharacterWithMagic(string name, CharacterRace race, CharacterGender gender, UInt32 age) : base(name, race, gender, age)
        {
            Spells = new Dictionary<string, Spell>();
            CurrentMP = 150;
            MaxMP = 150;
        }
        public CharacterWithMagic() : base() { }
        public CharacterWithMagic(bool i) : base(true)
        {
            CurrentMP = 150;
            MaxMP = 150;
            Spells = new Dictionary<string, Spell>();
        }

        public void RestoreMP(UInt32 MP)
        {
            if(CurrentMP + MP > MaxMP)
            {
                Console.WriteLine($"The {Name} has full mana");
                CurrentMP = MaxMP;
            }
            else
            {
                Console.WriteLine($"The {Name} restored {MP} units of mana");
                CurrentMP += MP;
            }
        }
        
        public void Heal(Character character)
        {
            if (((character.MaxHealth - character.CurrentHealth) * 2) <= CurrentMP)
                character.Heal(character.MaxHealth - character.CurrentHealth);
            else
                character.Heal(CurrentMP / 2);
        }

        public void LearnSpell(SpellScroll spell)
        {
            Spell addedSpell;
            switch(spell)
            {
                case SpellScroll.AddHealth:
                    addedSpell = new AddHealth(this);
                    break;
                case SpellScroll.Antidote:
                    addedSpell = new Antidote(this);
                    break;
                case SpellScroll.Armor:
                    addedSpell = new Armor(this);
                    break;
                case SpellScroll.Cure:
                    addedSpell = new Cure(this);
                    break;
                case SpellScroll.Otomri:
                    addedSpell = new Otomri(this);
                    break;
                case SpellScroll.Revive:
                    addedSpell = new Revive(this);
                    break;
                default:
                    addedSpell = null;
                    break;
            }
            if (addedSpell == null)
                return;
            if (Spells.ContainsKey(spell.ToString()))
            {
                Console.WriteLine($"The {Name} know this spell");
                return;
            }
            Spells.Add(spell.ToString().ToLower(), addedSpell);
            Console.WriteLine($"The {Name} learned {spell.ToString()} spell");
        }

        public void ForgetSpell(SpellScroll spell)
        {
            Spell addedSpell;
            switch (spell)
            {
                case SpellScroll.AddHealth:
                    addedSpell = new AddHealth(this);
                    break;
                case SpellScroll.Antidote:
                    addedSpell = new Antidote(this);
                    break;
                case SpellScroll.Armor:
                    addedSpell = new Armor(this);
                    break;
                case SpellScroll.Cure:
                    addedSpell = new Cure(this);
                    break;
                case SpellScroll.Otomri:
                    addedSpell = new Otomri(this);
                    break;
                case SpellScroll.Revive:
                    addedSpell = new Revive(this);
                    break;
                default:
                    addedSpell = null;
                    break;
            }
            if (addedSpell == null)
                return;
            if (!Spells.ContainsKey(spell.ToString().ToLower()))
                Console.WriteLine($"The {Name} doesn't know this spell");
            else
            {
                Console.WriteLine($"The {Name} forgot {spell.ToString()} spell");
                Spells.Remove(spell.ToString());
            }
        }
        public void MagicSpell()
        {
            Console.WriteLine($"({string.Join(", ", Spells.Keys)})");
            Console.Write("Cast a spell: ");
            string spell = Console.ReadLine();
            if (!Spells.ContainsKey(spell.ToLower()))
            {
                Console.WriteLine("You doesn't know this spell");
                return;
            }
            switch(Spells[spell.ToLower()].type)
            {
                case SpellType.Force:
                    Console.WriteLine("Enter force of spell: ");
                    int force = int.Parse(Console.ReadLine());
                    for(int i = 0; i < team.Characters.Count; i++)
                        Console.WriteLine($"{i}: {team.Characters[i]}");
                    Console.WriteLine("Enter id of teammate: ");
                    int id = int.Parse(Console.ReadLine());
                    Character character = team.Characters[id];
                    Spells[spell.ToLower()].Wiz(ref character, force);
                    break;
                case SpellType.Without:
                    for (int i = 0; i < team.Characters.Count; i++)
                        Console.WriteLine($"{i}: {team.Characters[i]}");
                    Console.WriteLine("Enter id of teammate: ");
                    id = int.Parse(Console.ReadLine());
                    character = team.Characters[id];
                    Spells[spell.ToLower()].Wiz(ref character);
                    break;
                case SpellType.Double:
                    Console.WriteLine("Would you like to use this spell with or without power?");
                    Console.Write("Enter with or without: ");
                    string choose = Console.ReadLine().ToLower();
                    switch(choose)
                    {
                        case "with":
                            Console.WriteLine("Enter force of spell: ");
                            force = int.Parse(Console.ReadLine());
                            for (int i = 0; i < team.Characters.Count; i++)
                                Console.WriteLine($"{i}: {team.Characters[i]}");
                            Console.WriteLine("Enter id of teammate: ");
                            id = int.Parse(Console.ReadLine());
                            character = team.Characters[id];
                            Spells[spell.ToLower()].Wiz(ref character, force);
                            break;
                        case "without":
                        default:
                            for (int i = 0; i < team.Characters.Count; i++)
                                Console.WriteLine($"{i}: {team.Characters[i]}");
                            Console.WriteLine("Enter id of teammate: ");
                            id = int.Parse(Console.ReadLine());
                            character = team.Characters[id];
                            Spells[spell.ToLower()].Wiz(ref character);
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
        public override string ToString() => $"==Character: {Name}==\n" +
            $"Id: {Id},state: {State.ToString()}\n" +
            $"race: {Race.ToString()}, gender: {Gender.ToString()}, age: {Age}\n" +
            $"speak: {(CanSpeak == true ? "yes" : "no")}, move: {(CanMove == true ? "yes" : "no")}\n" +
            $"Health: {CurrentHealth}/{MaxHealth}\n" +
            $"Mana {CurrentMP}/{MaxMP}\n" +
            $"Damage: {MinDamage}/{MaxDamage}\n" +
            $"Level: {Level}\n" +
            $"XP: {CurrXp}/{XpToNextLvl}\n" +
            $"Shields: {Shield}\n" +
            $"Spells: ({string.Join(", ", Spells.Values)})\n" +
            $"============={Functions.Fill("=", Name.Length)}==";
    }
}
