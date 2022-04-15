using CoreLayer.Entities;
using CoreLayer.Enums;
using CoreLayer.Interfaces;
using InfraStructure.Database;
using InfraStructure.Helpers;
using ServiceLayer.Controllers;
using ServiceLayer.Game2D;
using System;
using System.Collections;
using System.Collections.Generic;
using TopLayer.UiControllers;
using Unity;
using UnityEngine;
using UnityEngine.UI;

namespace TopLayer.MainControllers {
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private BoardView boardView;
        [SerializeField]
        private PlayerView thesusView;
        [SerializeField]
        private PlayerView minatourView;
        [SerializeField]
        private InputManager inputManager;

        [SerializeField]
        private Text resultText;

        private int levelNumber=1;

        private IThesus Thesus;
        private IMinatour Minatour;
        private Level CurrentLevel;
        private bool isGameFinished = false;
        private bool isPlayerWon = false;
        private ILevelRepository levelRepository;
        public static GameController Instance { get; private set; }

        private IHistory boardHistory;
       private void Awake()
        {
            Instance = this;
            this.levelRepository = new JsonLevelRepository("Levels Data/");
            this.ConfigureCommand();
            this.loadLevel(this.levelNumber);
        }

        private void loadLevel(int levelNumber)
        {

            this.CurrentLevel = levelRepository.GetLevel(levelNumber);
            var board = new Board(CurrentLevel.Board.Columns, CurrentLevel.Board.Rows);
            this.boardView.Board = this.CurrentLevel.Board;
            this.Thesus = new Thesus(this.CurrentLevel.Board, this.thesusView);
            this.Thesus.GameOver = this.GameOver;
            this.Minatour = new Minatour(this.CurrentLevel.Board, this.minatourView,new PathCalculator());
            this.Minatour.GameOver = this.GameOver;
            this.isGameFinished = false;
            this.isPlayerWon = false;
            this.inputManager.Resume();
            this.boardHistory = new History(this.CurrentLevel.Board);
            this.resultText.gameObject.SetActive(false);
            this.inputManager.solutionText.text = "";
            this.inputManager.commentText.text = this.CurrentLevel.Comment;
            this.inputManager.LevelNumper.text = this.levelNumber.ToString();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void GameOver(bool isPlayerWon)
        {
            this.isGameFinished = true;
            this.isPlayerWon = isPlayerWon;
        }

        private void ConfigureCommand()
        {
            foreach (UserCommand userCommand in Enum.GetValues(typeof(UserCommand)))
            {
                this.inputManager.SetCommandAction(userCommand, this.commandHandler);
            }
        }

        private void commandHandler(UserCommand userCommand)
        {
            switch (userCommand)
            {
                case UserCommand.MovePlayerDown:
                    StartCoroutine(this.handlePlayCommandRoutine(MovingCommand.Down));
                    break;
                case UserCommand.MovePlayerLeft:
                    StartCoroutine(this.handlePlayCommandRoutine(MovingCommand.Left));
                    break;
                case UserCommand.MovePlayerRight:
                    StartCoroutine(this.handlePlayCommandRoutine(MovingCommand.Right));
                    break;
                case UserCommand.MovePlayerUp:
                    StartCoroutine(this.handlePlayCommandRoutine(MovingCommand.Up));
                    break;
                case UserCommand.Wait:
                    StartCoroutine(this.handlePlayCommandRoutine(MovingCommand.Wait));
                    break;
                case UserCommand.UnDoLastAction:
                    this.unDo();
                    break;
                case UserCommand.NextPlevel:
                    this.levelNumber = this.levelRepository.ISLevelExist(levelNumber + 1) ? levelNumber + 1 : levelNumber;
                    this.loadLevel(this.levelNumber);
                    break;
                case UserCommand.PreviousLevel:
                    this.levelNumber = this.levelRepository.ISLevelExist(levelNumber -1) ? levelNumber - 1 : levelNumber;
                    this.loadLevel(this.levelNumber);
                    break;
                case UserCommand.Restart:
                    this.loadLevel(this.levelNumber);
                    break;
                case UserCommand.Solve:
                    this.solve();
                    break;
            }

        }

        private void solve()
        {
            if (this.CurrentLevel == null) 
            {
                return;
            }

            this.inputManager.solutionText.text = this.CurrentLevel.Solution;
        }
        private void unDo()
        {
            this.boardHistory?.Undo();
            this.Thesus.CurrentLocation = this.CurrentLevel.Board.GetCell(c => c.CurrentPlayer == PlayerType.Thesus);
            this.Minatour.CurrentLocation = this.CurrentLevel.Board.GetCell(c => c.CurrentPlayer == PlayerType.Minatour);
        }
        private IEnumerator handlePlayCommandRoutine(MovingCommand movingCommand) 
        {
            this.inputManager.Pause();
            this.boardHistory.SaveSnapshot();
            var thesusMoved = this.Thesus.Move(movingCommand);
            if (!thesusMoved && movingCommand != MovingCommand.Wait)
            {
                this.inputManager.Resume();
                yield break;
            }

            yield return new WaitForSeconds(0.2f);
            if (this.isGameFinished)
            {
                this.gameOverInternal();
                yield break;
            }

            this.Minatour.TakeTurn();
            yield return new WaitForSeconds(0.4f);

            if (this.isGameFinished)
            {
                this.gameOverInternal();
                yield break;
            }

            this.inputManager.Resume();
        }

        private void gameOverInternal()
        {
            this.resultText.gameObject.SetActive(true);
            this.resultText.text = this.isPlayerWon ? "You Won!" : "You Lose!";
        }

    }
}