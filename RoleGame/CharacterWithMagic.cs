using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleGame
{
    public class CharacterWithMagic : Character
    {
        public UInt32 CurrentMP;
        public UInt32 MaxMP;
        public CharacterWithMagic(uint CurrentMp, uint MaxMP)
        {
            CurrentMP = CurrentMp;
            MaxMP = MaxMP;
        }
        public Heal()
        {
            if(CurrentHealth<MaxHealth)
            {  

            }
        }
    }//never gonna give you up
}
