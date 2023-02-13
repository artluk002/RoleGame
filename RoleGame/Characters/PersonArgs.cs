using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleGame
{
    public class PersonArgs
    {
        public UInt32 Health { get; }
        public UInt32 MaxHealth { get; }
        public PersonArgs(UInt32 health, UInt32 maxHealth)
        {
            Health = health;
            MaxHealth = maxHealth;
        }

    }
}
