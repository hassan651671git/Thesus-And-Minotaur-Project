using CoreLayer.Enums;
using CoreLayer.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
namespace CoreLayer.Entities
{
    [DataContract()]
    public class Cell : ICell
    {
        [DataMember()]
        public int RowPosition { get; private set; }

        [DataMember()]
        public int ColumnPosition { get; private set; }

        [DataMember()]
        private bool isRightBlocked, isLeftBlocked, isTopBlocked, isBottomBlocked;

        [DataMember()]
        public bool IsRightBlocked
        {
            get
            {
                return this.isRightBlocked;
            }
            set
            {
                this.isRightBlocked = value;
                this.CellView?.Refresh();
            }
        }

        [DataMember()]
        public bool IsLeftBlocked
        {
            get
            {
                return this.isLeftBlocked;
            }
            set
            {
                this.isLeftBlocked = value;
                this.CellView?.Refresh();
            }
        }

        [DataMember()]
        public bool IsTopBlocked
        {
            get
            {
                return this.isTopBlocked;
            }
            set
            {
                this.isTopBlocked = value;
                this.CellView?.Refresh();
            }
        }

        [DataMember()]
        public bool IsBottomBlocked
        {
            get
            {
                return this.isBottomBlocked;
            }
            set
            {
                this.isBottomBlocked = value;
                this.CellView?.Refresh();
            }
        }

        [DataMember()]
        public bool IsFinal { get; set; }

        [DataMember()]
        public PlayerType CurrentPlayer { get ; set; }

        [JsonIgnore]
        public ICellView CellView { get; set; }

        [DataMember()]
        public IBoard Board { get; set; }

        public Cell()
        {

        }

        public Cell(IBoard board,int rowPosition, int columnPosition, bool isRightBlocked, bool isLeftBlocked, bool isTopBlocked, bool isBottomBlocked, bool isFinal)
        {
            RowPosition = rowPosition;
            ColumnPosition = columnPosition;
            IsRightBlocked = isRightBlocked;
            IsLeftBlocked = isLeftBlocked;
            IsTopBlocked = isTopBlocked;
            IsBottomBlocked = isBottomBlocked;
            IsFinal = isFinal;
            this.Board = board;
        }

        private Cell(Cell celll)
        {
            this.RowPosition = celll.RowPosition;
            this.ColumnPosition = celll.ColumnPosition;
            this.IsRightBlocked = celll.IsRightBlocked;
            this.IsLeftBlocked = celll.IsLeftBlocked;
            this.IsTopBlocked = celll.IsTopBlocked;
            this.IsBottomBlocked = celll.IsBottomBlocked;
            this.CurrentPlayer = celll.CurrentPlayer;
            this.IsFinal = celll.IsFinal;
            this.CellView = celll.CellView;
        }
        public ICell GetBottomNeighbour()
        {
            return this.Board.GetCell(this.RowPosition + 1, this.ColumnPosition);
        }

        public ICell GeTLeftNeighbour()
        {
            return this.Board.GetCell(this.RowPosition, this.ColumnPosition-1);
        }

        public ICell GetTopNeighbour()
        {
            return this.Board.GetCell(this.RowPosition - 1, this.ColumnPosition);
        }

        public ICell GeTRightNeighbour()
        {
            return this.Board.GetCell(this.RowPosition , this.ColumnPosition+1);
        }

        public IClonable Clone()
        {
            return new Cell(this);
        }

        public void Restore(IClonable clonable)
        {
            throw new System.NotImplementedException();
        }

        public bool IsEqual(ICell cell)
        {
            return this.RowPosition == cell.RowPosition && this.ColumnPosition == cell.ColumnPosition;
        }
    }
}