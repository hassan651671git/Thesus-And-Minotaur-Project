using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreLayer.Interfaces
{
    public interface ICellView
    {
        GameObject MonoObject { get; }
        public ICell CellController{get;}
        void SetController(ICell cell);
        void Refresh();
    }

}