using CoreLayer.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CoreLayer.Interfaces
{
    public interface ICell:IClonable
    {

        int RowPosition { get; }
        int ColumnPosition { get; }
        bool IsRightBlocked { get; set; }

        bool IsLeftBlocked { get; set; }
        bool IsTopBlocked { get; set; }
        bool IsBottomBlocked { get; set; }
        bool IsFinal { get; set; }
        PlayerType CurrentPlayer { get; set; }
        ICell GeTRightNeighbour();
        ICell GeTLeftNeighbour();
        ICell GetTopNeighbour();
        ICell GetBottomNeighbour();

        ICellView CellView { get; set; }
        IBoard Board { get; set; }
        bool IsEqual(ICell cell);
    }
}