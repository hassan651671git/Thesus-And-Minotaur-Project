using CoreLayer.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CoreLayer.Interfaces
{
    public interface IPlayer
    {
        IBoard Board { get; }
        ICell CurrentLocation { get; set; }
        PlayerType PlayerType { get; }
        IPlayerView View { get; set; }
        Action<bool> GameOver { get; set; }
        bool MoveToCell(ICell cell);
        bool CanMoveRight();
        bool CanMoveLeft();
        bool CanMoveUp();
        bool CanMoveDown();
    }
}