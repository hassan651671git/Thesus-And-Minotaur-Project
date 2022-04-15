using CoreLayer.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ServiceLayer.Game2D
{
    public class BoardView : MonoBehaviour, IBoardView
    {

        [SerializeField]
        private FlixableGridLayout gridLayout;
        [SerializeField]
        private CellView cellPrefab;
        private IBoard board;
       private int rows;
        private int columns;
        public int Rows
        {
            get
            {
                return this.rows;
            }
            set
            {
                this.rows = value;
                this.gridLayout.Rows = this.rows;
            }
        }
        public int Columns
        {
            get
            {
                return this.columns;
            }
            set
            {
                this.columns = value;
                this.gridLayout.Columns = this.columns;
            }
        }
        public IBoard Board
        {
            get
            {
                return this.board;
            }
            set
            {
                this.board = value;
                this.initialize(value);
            }
        }

        private void initialize(IBoard board)
        {
            this.gridLayout.Clear();
            this.Columns = board.Columns;
            this.Rows = board.Rows;
            for (int row = 0; row < this.Rows; row++)
            {
                for (int column = 0; column < this.Columns; column++)
                {
                    var cellView = GameObject.Instantiate<CellView>(this.cellPrefab, this.gridLayout.transform);
                    var cellController = this.board.GetCell(row, column);
                    cellView.SetController(cellController);
                    cellController.CellView = cellView;
                }
            }
        }

    }
}