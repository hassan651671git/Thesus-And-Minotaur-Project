using CoreLayer.Entities;
using CoreLayer.Interfaces;
using InfraStructure.Database;
using InfraStructure.Helpers;
using ServiceLayer.Game2D;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TopLayer.LevelCreator
{
    public class MainController : MonoBehaviour
    {
        [SerializeField]
        private BoardView boardView;
        [SerializeField]
        private PlayerView thesusView;
        [SerializeField]
        private PlayerView minotaurView;
        [SerializeField]
        private InputManager inputManager;
        private int rows, columns;
        private IBoard Board;
        private IThesus Thesus;
        private IMinatour Minatour;
        private ICell sellectedCell;

        private string savingLocation="Assets/Resources/Levels Data/";
        // Start is called before the first frame update
        void Start()
        {
            this.configureInputManager();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void upBlockValueChanged(bool isOn)
        {
            if (this.sellectedCell == null)
            {
                return;
            }
            this.sellectedCell.IsTopBlocked = isOn;
            var neighbour = this.sellectedCell.GetTopNeighbour();
            if (neighbour != null)
            {
                neighbour.IsBottomBlocked = isOn;
            }

        }

        private void leftBlockValueChanged(bool isOn)
        {
            if (this.sellectedCell == null)
            {
                return;
            }
            this.sellectedCell.IsLeftBlocked = isOn;
            var neighbour = this.sellectedCell.GeTLeftNeighbour();
            if (neighbour != null)
            {
                neighbour.IsRightBlocked = isOn;
            }

        }

        private void bottmBlockValueChanged(bool isOn)
        {
            if (this.sellectedCell == null)
            {
                return;
            }
            this.sellectedCell.IsBottomBlocked = isOn;
            var neighbour = this.sellectedCell.GetBottomNeighbour();
            if (neighbour != null)
            {
                neighbour.IsTopBlocked = isOn;
            }

        }

        private void rightBlockValueChanged(bool isOn)
        {
            if (this.sellectedCell == null)
            {
                return;
            }
            this.sellectedCell.IsRightBlocked = isOn;
            var neighbour = this.sellectedCell.GeTRightNeighbour();
            if (neighbour != null)
            {
                neighbour.IsLeftBlocked = isOn;
            }

        }

        private void isFinalValueChanged(bool value)
        {
            if (this.sellectedCell == null)
            {
                return;
            }

            var previousFinal = this.Board.GetCell(c => c.IsFinal);

            if (previousFinal != null && value)
            {
                previousFinal.IsFinal = false;
            }
            this.sellectedCell.IsFinal = value;
        }

        private void  isMinotaurValueChanged(bool isOn)
        {
            if (this.sellectedCell == null)
            {
                return;
            }

            if (isOn)
            {
                this.Minatour.MoveToCell(this.sellectedCell);
            }
        }

        private void isThesusValueChanged(bool isOn)
        {
            if (this.sellectedCell == null)
            {
                return;
            }

            if (isOn)
            {
                this.Thesus.MoveToCell(this.sellectedCell);
            }
        }

        private void generateBtnAction()
        {
            try
            {
                this.inputManager.SetSelectedCell(null);
                this.rows = int.Parse(this.inputManager.RowsInputField.text);
                this.columns = int.Parse(this.inputManager.ColumnsInputField.text);
                this.Board = new Board(this.rows, this.columns);
                this.setDeafultBoardBlocks();
                this.boardView.Board = this.Board;
                this.setCellsClickAction();
                this.Thesus = new Thesus(this.Board, this.thesusView);
                this.Minatour = new Minatour(this.Board, this.minotaurView, new PathCalculator());
            }catch(StackOverflowException ex)
            {
                print(ex.Message);
            }
        }

        private void setDeafultBoardBlocks()
        {
            for(int row = 0; row < this.Board.Rows; row++)
            {
                for(int column = 0; column < this.Board.Columns; column++)
                {
                    var cell = new Cell(this.Board, row, column, false, false, false, false, false);
                    if (row == 0)
                    {
                        cell.IsTopBlocked = true;
                    }
                    if (column == 0)
                    {
                        cell.IsLeftBlocked = true;
                    }
                    if (row == this.Board.Rows - 1)
                    {
                        cell.IsBottomBlocked = true;
                    }
                    if (column == this.Board.Columns - 1)
                    {
                        cell.IsRightBlocked = true;
                    }
                    this.Board.AddCell(cell);
                }
            }

        }

        private void setCellsClickAction()
        {
            for (int row = 0; row < this.Board.Rows; row++)
            {
                for (int column = 0; column < this.Board.Columns; column++)
                {
                    var cellView = this.Board.GetCell(row, column).CellView as ClickableCellView;
                    cellView.OnClickedAction += this.cellClickedHandler;
                }
            }
        }
        private void cellClickedHandler(ICell cell)
        {
            var view = this.sellectedCell?.CellView as ClickableCellView;
            view?.ResetColor();
            this.sellectedCell = cell;
            this.inputManager.SetSelectedCell(this.sellectedCell);
        }
        private void saveBtnAction()
        {
            Level level = new Level();
            level.Board = this.Board;
            level.Comment =this.inputManager.CommentInputField.text;
            level.LevelNumber =int.Parse( this.inputManager.LevelInputfield.text);
            level.Solution = this.inputManager.solvingCommandsText.text;
            ILevelRepository levelRepository = new JsonLevelRepository(this.savingLocation);
            levelRepository.SaveLevel(level);
        }

        private void solveBtnAction()
        {
            GameResolver gameResolver = new GameResolver(this.Board);
            gameResolver.Resolve();
            this.inputManager.solvingCommandsText.text = String.Join(" > ",gameResolver.solution);
        }


        private void configureInputManager()
        {
            inputManager.BottomBlockValueChanged += this.bottmBlockValueChanged;
            inputManager.UpBlockValueChanged += this.upBlockValueChanged;
            inputManager.LeftBlockValueChanged += this.leftBlockValueChanged;
            inputManager.RightBlockValueChanged += this.rightBlockValueChanged;
            inputManager.IsFinalValueChanged += this.isFinalValueChanged;
            inputManager.IsThesusLocationValueChanged += this.isThesusValueChanged;
            inputManager.IsMinotaurValueChanged += this.isMinotaurValueChanged;
            inputManager.GenerateBtnAction += this.generateBtnAction;
            inputManager.SaveBtnAction += this.saveBtnAction;
            inputManager.SolveBtnAction += this.solveBtnAction;

        }
    }
}