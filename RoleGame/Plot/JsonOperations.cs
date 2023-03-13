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
                /*ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,*/
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
                /*serializer.PreserveReferencesHandling = PreserveReferencesHandling.Objects;*/
                Character temp_character = serializer.Deserialize<Character>(reader);
                Characters.Add(temp_character);
            }
            reader.Close();
            return Characters;
        }
        public void SaveGame(Game game)
        {
            if (!File.Exists("Game.json"))
                File.Create("Game.json").Close();
            var GameList = ReadGame();
            if(GameList != null)
            { 
                foreach (var Game in GameList)
                {
                    if(Game.Name == game.Name)
                    {
                        GameList.Remove(Game);
                        break;
                    }
                }
                File.Delete("Game.json");
                File.Create("Game.json").Close();
                foreach(Game _game in GameList)
                    File.AppendAllText("Game.json", JsonConvert.SerializeObject(_game, Formatting.Indented));
            }
            File.AppendAllText("Game.json", JsonConvert.SerializeObject(game, Formatting.Indented));
        }
        public List<Game> ReadGame()
        {
            if(!File.Exists("Game.json"))
            {
                File.Create("Game.json").Close();
                return null;
            }
            List<Game> Games = new List<Game>();
            JsonTextReader reader = new JsonTextReader(new StreamReader("Game.json"));
            reader.SupportMultipleContent= true;
            while (true)
            {
                if (!reader.Read())
                    break;
                JsonSerializer serializer = new JsonSerializer();
                serializer.TypeNameHandling = TypeNameHandling.Auto;
                Game tmep_game = serializer.Deserialize<Game>(reader);
                Games.Add(tmep_game);
            }
            reader.Close();
            return Games;
        }
    }
}
