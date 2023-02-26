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
        public Dictionary<string, Artifact> Items;

        public Inventory()
        {
            Items = new Dictionary<string, Artifact>();
        }

        public void AddItem(Artifact item)
        {
            if (Items.ContainsKey(item.GetName()))
                Items[item.GetName()].Count++;
            else
                Items.Add(item.GetName(), item);
        }
        public void RemoveItem(Artifact item)
        {
            if (Items.ContainsKey(item.GetName()))
            {
                if (Items[item.GetName()].Count > 1)
                    Items[item.GetName()].Count--;
                else
                    Items.Remove(item.GetName());
            }

        }
        public void PrintItems()
        {
            Console.WriteLine(string.Join("\n", Items.Keys));
        }

    }
}
