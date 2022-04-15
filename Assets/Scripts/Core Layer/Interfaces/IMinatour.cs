using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CoreLayer.Interfaces
{
    public interface IMinatour:IPlayer
    {
        bool TakeTurn();
        bool MoveOneCell();
    }
}