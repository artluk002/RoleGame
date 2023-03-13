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
        public void RemoveItem(string itemName)
        {
            if (Items.ContainsKey(itemName))
            {
                if (Items[itemName].Count > 1)
                {
                    Items[itemName].Count--;
                    Items[itemName].Forse = 100;
                }
                else
                    Items.Remove(itemName);
            }

        }
        public String GetItems() => string.Join(", ", Items.Keys);
        public bool IsItIn(string name) => Items.ContainsKey(name);
        public void PrintItems()
        {
            foreach (var item in Items)
                Console.WriteLine($"{item.Key} - {item.Value.Count}");
        }

    }
}
