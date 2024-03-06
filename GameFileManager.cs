using System;
using System;
using System.IO;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Xml;
using TwoPlayerBoardGames;
using static System.Console;

//public abstract class GameFileManager
//{
//    protected Game game;

//    public GameFileManager(Game game)
//    {
//        this.game = game;
//    }
//    public void SaveGame(GameState gameState, string filePath)
//    {
//        var options = new JsonSerializerOptions { WriteIndented = true };
//        string jsonString = JsonSerializer.Serialize(gameState, options);
//        File.WriteAllText(filePath, jsonString);
//    }
//    public GameState LoadGame(string filePath)
//    {
//        string jsonString = File.ReadAllText(filePath);
//        GameState gameState = JsonSerializer.Deserialize<GameState>(jsonString);
//        return gameState;
//    }
//}

