using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreLayer.Interfaces
{
    public interface IBoardView
    {
         int Rows { get; set; }
         int Columns { get; set; }
        IBoard Board { get; set; }
    }
}