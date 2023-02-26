using RoleGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleGame.Characters
{
    public class Inventory
    {
        public Dictionary<string,Artifact> Items;

        public Inventory()
        {
            Items = new Dictionary<string, Artifact>();
        }

        public void AddItem(Artifact item)
        {
            if (Items.ContainsKey())
                return;
        }
        public void RemoveItem(Artifact item)
        {
            if (Items[Items.IndexOf(item)].Count > 1)
                Items[Items.IndexOf(item)].Count--;
            else
                Items.Remove(item);
        }
        public void PrintItems()
        {
            Console.WriteLine(string.Join("\n", Items));
        }

    }
}
