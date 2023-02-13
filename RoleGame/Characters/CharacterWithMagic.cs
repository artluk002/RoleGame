using System;

namespace RoleGame
{
    public class CharacterWithMagic : Character
    {
        public UInt32 currentMP;
        public UInt32 maxMP;
        public CharacterWithMagic(uint CurrentMp, uint MaxMP, string name, CharacterRace race, CharacterGender gender, UInt32 age) : base(name, race, gender, age)
        {
            currentMP = CurrentMp;
            maxMP = MaxMP;
        }
        public CharacterWithMagic() : base()
        {
            currentMP = 100;
            maxMP = 150;
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
    }
}
