using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleGame
{
    internal interface Magic
    {
        public static void Wiz(Character character, int force = 10)
        {
            if (force < 0 | force > 100)
                throw new ArgumentException("The force of wiz can't be ")
        }
    }
}
