using CoreLayer.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
namespace CoreLayer.Entities
{
    [DataContract()]
    public class Board : IBoard
    {
        [DataMember()]
        public int Rows { get; private set; }

        [DataMember()]
        public int Columns { get; private set; }
        [DataMember()]
        private ICell[,] BoardBuffer;

        public Board()
        {

        }

        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            // I left this for time limit , we need to inject it
            this.BoardBuffer = new Cell[rows, columns];
        }

        private Board(Board board)
        {
            this.applay(board);
        }

        private void applay(IBoard board)
        {
            this.Rows = board.Rows;
            this.Columns = board.Columns;
            this.BoardBuffer = new Cell[this.Rows, this.Columns];
            for (int row = 0; row < board.Rows; row++)
            {
                for (int column = 0; column < board.Columns; column++)
                {
                    var cell = board.GetCell(row, column).Clone() as ICell;
                    cell.Board = this;
                    this.AddCell(cell);
                }
            }
        }

        public void AddCell(ICell cell)
        {
            try
            {
                this.BoardBuffer[cell.RowPosition, cell.ColumnPosition] = cell;
            }
            catch (Exception ex)
            {
                MonoBehaviour.print(ex.Message);
            }
        }

        public ICell GetCell(int rowPosition, int ColumnPosition)
        {
            try
            {
                return this.BoardBuffer[rowPosition, ColumnPosition];
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public ICell GetCell(Func<ICell, bool> filter)
        {
            for (var row = 0; row < this.Rows; row++)
            {
                for(var column = 0; column < this.Columns; column++)
                {
                    var cell = this.GetCell(row, column);

                    if (cell == null)
                    {
                        return cell;
                    }

                    if (filter(cell))
                    {
                        return cell;
                    }
                }
            }

            return null;
        }

        public IClonable Clone()
        {
            return new Board(this);
        }

        public void Restore(IClonable board)
        {
            this.applay(board as IBoard);
        }

        
    }
}