using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleGame.Characters
{
    public class XpArgs
    {
        public int CurrXp { get; }
        public int XpToNextLvl { get; }
        public int AddedXp { get; }
        public XpArgs(int currXp, int xpToNextLvl, int addedXp)
        {
            CurrXp = currXp;
            XpToNextLvl = xpToNextLvl;
            AddedXp = addedXp;
        }
    }
}
