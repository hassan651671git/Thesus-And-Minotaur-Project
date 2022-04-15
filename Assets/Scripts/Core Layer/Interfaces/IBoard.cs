using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CoreLayer.Interfaces
{
    public interface IBoard:IClonable
    {
        int Rows { get; }
        int Columns { get; }

        void AddCell(ICell cell);
        ICell GetCell(int rowPosition, int ColumnPosition);

        ICell GetCell(Func<ICell, bool> filter);

    }
}