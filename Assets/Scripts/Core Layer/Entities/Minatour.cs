using CoreLayer.Enums;
using CoreLayer.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TopLayer.MainControllers;
using UnityEngine;
namespace CoreLayer.Entities
{
    public class Minatour :Player, IMinatour
    {
        public Minatour(IBoard board,IPlayerView view,IPathCalculator pathCalculator) : base(board,view, PlayerType.Minatour)
        {
            this.pathCalculator = pathCalculator;
        }

        private IPathCalculator pathCalculator;
        public override bool MoveToCell(ICell cell)
        {
           return base.MoveToCell(cell);
        }

        public bool TakeTurn()
        {
           var moved= this.MoveOneCell();

            if (moved)
            {
                GameController.Instance.StartCoroutine(this.waitThenMoveRoutine());
                return true;
            }

            return false;
        }

        private IEnumerator waitThenMoveRoutine()
        {
            yield return new WaitForSeconds(0.25f);
            this.MoveOneCell();
        }
        public bool MoveOneCell()
        {
            var thesusLocation = this.Board.GetCell(c => c.CurrentPlayer == PlayerType.Thesus);
            int leftMovingPathLength = int.MaxValue, rightMovingPathLength = int.MaxValue, upMovingPathLength = int.MaxValue, downMovingPathLength = int.MaxValue;
            if (thesusLocation == null || this.CurrentLocation == null)
            {
                return false;
            }

            var currentLocationPathLength = this.pathCalculator.GetVirtualPathLength(this.CurrentLocation, thesusLocation);

            if (this.CanMoveLeft())
            {
                var leftCell = this.CurrentLocation.GeTLeftNeighbour();
                leftMovingPathLength = this.pathCalculator.GetVirtualPathLength(leftCell, thesusLocation);
                if (leftMovingPathLength == 0)
                {
                    return this.MoveToCell(leftCell);
                }

            }

            if (this.CanMoveRight())
            {
                var rightCell = this.CurrentLocation.GeTRightNeighbour();
                rightMovingPathLength = this.pathCalculator.GetVirtualPathLength(rightCell, thesusLocation);
                if (rightMovingPathLength == 0)
                {
                    return this.MoveToCell(rightCell);
                }
            }

            if (this.CanMoveUp())
            {
                var upCell = this.CurrentLocation.GetTopNeighbour();
                upMovingPathLength = this.pathCalculator.GetVirtualPathLength(upCell, thesusLocation);
                if (upMovingPathLength == 0)
                {
                    return this.MoveToCell(upCell);
                }
            }

            if (this.CanMoveDown())
            {
                var downCell = this.CurrentLocation.GetBottomNeighbour();
                downMovingPathLength = this.pathCalculator.GetVirtualPathLength(downCell, thesusLocation);
                if (downMovingPathLength == 0)
                {
                    return this.MoveToCell(downCell);
                }
            }

            if (leftMovingPathLength < currentLocationPathLength)
            {
                return this.Move(MovingCommand.Left);
            }

            if (rightMovingPathLength < currentLocationPathLength)
            {
                return this.Move(MovingCommand.Right);
            }

             if (upMovingPathLength < currentLocationPathLength)
            {
                return this.Move(MovingCommand.Up);
            }

            if (downMovingPathLength < currentLocationPathLength)
            {
                return this.Move(MovingCommand.Down);
            }

            return false;
        }

    }
}