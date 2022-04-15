using CoreLayer.Enums;
using CoreLayer.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CoreLayer.Entities
{
    public class Thesus : Player, IThesus
    {

        public Thesus(IBoard board,IPlayerView view):base(board,view,PlayerType.Thesus)
        {
        }

        public override bool MoveToCell(ICell cell)
        {
            if (cell.IsFinal)
            {
                this.GameOver?.Invoke(true);
            }

            return base.MoveToCell(cell);
        }

        bool IThesus.Move(MovingCommand movingCommand)
        {
            return base.Move(movingCommand);
        }
    }
}