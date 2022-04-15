using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CoreLayer.Interfaces
{
    public interface IHistory
    {
        void SaveSnapshot();
        void Undo();
    }
}