using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleGame.Characters
{
    public class Team
    {
        public List<Character> Characters = new List<Character>();
        public string Name { get; set; }
        public Team(string name)
        {
            Characters = new List<Character>();
            Name = name;
        }
        public Team() { }
        public void AddCharacter(Character character)
        {
            Characters.Add(character);
        }
        public void RemoveCharacter(Character character)
        {
            Characters.Remove(character);
        }

    }
}
