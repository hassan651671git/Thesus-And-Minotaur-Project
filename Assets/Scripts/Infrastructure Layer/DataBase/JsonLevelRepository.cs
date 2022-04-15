using CoreLayer.Entities;
using CoreLayer.Enums;
using CoreLayer.Interfaces;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace InfraStructure.Database
{
    public class JsonLevelRepository : ILevelRepository
    {
        private string savingLocation = "";

        public JsonLevelRepository()
        {

        }
        public JsonLevelRepository(string savingLocation)
        {
            this.savingLocation = savingLocation;
        }
        public Level GetLevel(int levelNumber)
        {
            var fileName = "Level_" + levelNumber;
            var filePath = this.savingLocation + fileName;
            var file = Resources.Load<TextAsset>(filePath);
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
            settings.Converters.Add(new BoardConverter());
            settings.Converters.Add(new CellConverter());
            var level = JsonConvert.DeserializeObject<Level>(file.text, settings);

            for(int row = 0; row < level.Board.Rows; row++)
            {
                for(int column = 0; column < level.Board.Columns; column++)
                {
                    level.Board.GetCell(row, column).Board = level.Board;
                }
            }
            return level;
            //var level = new Level();
            //level.LevelNumber = 1;
            //level.Board = new Board(3, 4);
            //// first row
            //level.Board.AddCell(new Cell(level.Board, 0, 0, false, true, true, false, false));
            //level.Board.AddCell(new Cell(level.Board, 0, 1, false, false, true, false, false) { CurrentPlayer=PlayerType.Thesus});
            //level.Board.AddCell(new Cell(level.Board, 0, 2, true, false, true, false, false));
            //level.Board.AddCell(new Cell(level.Board, 0, 3, false, true, false, true, false));
            ////second row
            //level.Board.AddCell(new Cell(level.Board, 1, 0, false, true, false, false, false));
            //level.Board.AddCell(new Cell(level.Board, 1, 1, true, false, true, true, false));
            //level.Board.AddCell(new Cell(level.Board, 1, 2, false, true, false, false, false));
            //level.Board.AddCell(new Cell(level.Board, 1, 3, false, false, true, true, true));
            ////third row
            //level.Board.AddCell(new Cell(level.Board, 2, 0, false, true, false, true, false));
            //level.Board.AddCell(new Cell(level.Board, 2, 1, false, false, true, true, false) { CurrentPlayer = PlayerType.Minatour });
            //level.Board.AddCell(new Cell(level.Board, 2, 2, true, false, false, true, false));
            //level.Board.AddCell(new Cell(level.Board, 2, 3, false, true, true, false, false));
            //return level;
        }

        public bool ISLevelExist(int levelNumber)
        {
            var fileName = "Level_" + levelNumber;
            var filePath = this.savingLocation + fileName;
            var file = Resources.Load<TextAsset>(filePath);
            return file != null;
        }

        public void SaveLevel(Level level)
        {
            if (!Directory.Exists(this.savingLocation))
            {
                Directory.CreateDirectory(this.savingLocation);
            }

            var fileName = "Level_" + level.LevelNumber + ".json";
            var filePath = this.savingLocation + fileName;
            var settings = new JsonSerializerSettings { ReferenceLoopHandling=ReferenceLoopHandling.Ignore };
            var levelInJson = JsonConvert.SerializeObject(level, settings);
            File.WriteAllText(filePath, levelInJson);
        }
    }
}