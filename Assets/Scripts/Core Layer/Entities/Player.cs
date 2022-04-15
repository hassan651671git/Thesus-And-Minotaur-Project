using CoreLayer.Enums;
using CoreLayer.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CoreLayer.Entities
{
    public abstract class Player : IPlayer
    {
        protected Player(IBoard board,IPlayerView playerView, PlayerType playerType)
        {
            this.Board = board;
            this.View = playerView;
            this.PlayerType = playerType;
            this.initializeCurrentLocation();
        }

        public IBoard Board { get; private set; }

        public ICell CurrentLocation
        {
            get
            {
                return this.currentLocation;

            }
            set
            {
                this.currentLocation = value;
                this.View?.SetLocation(value);
            }
        }

        public PlayerType PlayerType { get; private set; }
        public IPlayerView View { get ; set; }
        public Action<bool> GameOver { get; set; }

        private ICell currentLocation;
        public bool CanMoveDown()
        {
            if (this.CurrentLocation.IsBottomBlocked)
            {
                return false;
            }

            var bottomNeighbour = this.CurrentLocation.GetBottomNeighbour();

            if(bottomNeighbour == null)
            {
                return false;
            }

            return !bottomNeighbour.IsTopBlocked;
        }

        public bool CanMoveLeft()
        {
            if (this.CurrentLocation.IsLeftBlocked)
            {
                return false;
            }

            var leftNeighbour = this.CurrentLocation.GeTLeftNeighbour();

            if (leftNeighbour == null)
            {
                return false;
            }

            return !leftNeighbour.IsRightBlocked;
        }

        public bool CanMoveRight()
        {
            if (this.CurrentLocation.IsRightBlocked)
            {
                return false;
            }

            var rightNeighbour = this.CurrentLocation.GeTRightNeighbour();

            if (rightNeighbour == null)
            {
                return false;
            }

            return !rightNeighbour.IsLeftBlocked;
        }

        public bool CanMoveUp()
        {
            if (this.CurrentLocation.IsTopBlocked)
            {
                return false;
            }

            var topNeighbour = this.CurrentLocation.GetTopNeighbour();

            if (topNeighbour == null)
            {
                return false;
            }

            return !topNeighbour.IsBottomBlocked;
        }

        private bool MoveLeft()
        {
            if (this.CanMoveLeft())
            {
                var leftCell = this.CurrentLocation.GeTLeftNeighbour();
                return this.MoveToCell(leftCell);
            }

            return false;
        }

        private bool MoveRight()
        {
            if (this.CanMoveRight())
            {
                var rightCell = this.CurrentLocation.GeTRightNeighbour();
                return this.MoveToCell(rightCell);
            }

            return false;
        }

        private bool MoveUp()
        {
            if (this.CanMoveUp())
            {
                var upCell = this.CurrentLocation.GetTopNeighbour();
                return this.MoveToCell(upCell);
            }

            return false;
        }

        private bool MoveDown()
        {
            if (this.CanMoveDown())
            {
                var bottomCell = this.CurrentLocation.GetBottomNeighbour();
                return this.MoveToCell(bottomCell);
            }

            return false;
        }
        protected bool Move(MovingCommand movingCommand)
        {
            switch (movingCommand)
            {
                case MovingCommand.Right:
                    return this.MoveRight();
                case MovingCommand.Left:
                    return this.MoveLeft();
                case MovingCommand.Up:
                    return this.MoveUp();
                case MovingCommand.Down:
                    return this.MoveDown();
                default:
                    return false;
            }
        }

        private void initializeCurrentLocation()
        {
            var cell = this.Board.GetCell(c => c.CurrentPlayer == this.PlayerType);

            if (cell != null)
            {
                this.CurrentLocation = cell;
                return;
            }

            if (this.PlayerType == PlayerType.Thesus)
            {
                this.CurrentLocation = this.Board.GetCell(0, 0);
            }

            if (this.PlayerType == PlayerType.Minatour)
            {
                this.CurrentLocation = this.Board.GetCell(this.Board.Rows-1, this.Board.Columns-1);
            }

            this.CurrentLocation.CurrentPlayer = this.PlayerType;
        }
        public virtual bool MoveToCell(ICell cell)
        {
            if (cell.CurrentPlayer != PlayerType.UnKnown)
            {
                this.GameOver?.Invoke(false);
            }

            this.View?.SetLocation(cell);
            this.CurrentLocation.CurrentPlayer = PlayerType.UnKnown;
            this.CurrentLocation = cell;
            this.CurrentLocation.CurrentPlayer =this.PlayerType;
            return true;
        }
    }
}