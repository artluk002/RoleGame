using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleGame
{
    public interface Magic
    {
        void Wiz(Character character, int force = 10);
    }
}
