using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RoleGame.Plot
{
    public class JsonOperations
    {
        private string path;
        public JsonOperations()
        {
            path = "";
        }
        public JsonOperations(string path)
        {
            this.path = path;
        }
        public void SaveCharacters(Character Characters)
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            if (!File.Exists("Characters.json"))
                File.Create("Characters.json").Close();
            var CharactersList = ReadCharacters();
            if (CharactersList != null)
            {
                foreach (Character character in CharactersList)
                {
                    if (character.Id == Characters.Id)
                    {
                        CharactersList.Remove(character);
                        break;
                    }
                }
                File.Delete("Characters.json");
                File.Create("Characters.json").Close();
                foreach (Character character in CharactersList)
                    File.AppendAllText("Characters.json", JsonConvert.SerializeObject(character, Formatting.Indented, settings));

            }
            File.AppendAllText("Characters.json", JsonConvert.SerializeObject(Characters, Formatting.Indented, settings));
        }
        public List<Character> ReadCharacters()
        {
            if (!File.Exists("Characters.json"))
            {
                File.Create("Characters.json").Close();
                return null;
            }
            List<Character> Characters = new List<Character>();
            JsonTextReader reader = new JsonTextReader(new StreamReader("Characters.json"));
            reader.SupportMultipleContent = true;
            while (true)
            {
                if (!reader.Read())
                    break;
                JsonSerializer serializer = new JsonSerializer();
                serializer.TypeNameHandling = TypeNameHandling.Auto;
                Character temp_character = serializer.Deserialize<Character>(reader);
                Characters.Add(temp_character);
            }
            reader.Close();
            return Characters;
        }
    }
}
