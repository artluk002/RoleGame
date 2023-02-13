using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleGame
{
    public interface IMagic
    {
        void Wiz(ref Character character, int force = 10);
        void Wiz(ref Character character);
    }
}
