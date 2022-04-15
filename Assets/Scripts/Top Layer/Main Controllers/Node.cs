using CoreLayer.Entities;
using CoreLayer.Enums;
using CoreLayer.Interfaces;
using InfraStructure.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{


    public IBoard Board { get; private set; }
    public List<Node> Children { get; private set; }
    public Node Parent { get; private set; }

    public IThesus Thesus { get; private set; }
    public IMinatour Minatour { get; private set; }
    public GameState GameState { get; set; }

    public MovingCommand MovingCommand { get; private set; }


    public Node(IBoard board, Node parent, MovingCommand movingCommand)
    {
        Board = board;
        Parent = parent;
        MovingCommand = movingCommand;
        this.Children = new List<Node>();
        this.Thesus = new Thesus(this.Board, null);
        this.Minatour = new Minatour(this.Board, null, new PathCalculator());
    }

    public void GenerateChildren()
    {

        this.AddMovingChild(MovingCommand.Right);
        this.AddMovingChild(MovingCommand.Left);
        this.AddMovingChild(MovingCommand.Up);
        this.AddMovingChild(MovingCommand.Down);
        this.AddMovingChild(MovingCommand.Wait);

    }

    private void AddMovingChild(MovingCommand movingCommand)
    {
        var board = this.Board.Clone() as IBoard;
        Node childNode = new Node(board, this, movingCommand);

        if (movingCommand == MovingCommand.Wait)
        {
            if (childNode.moveMinoatur())
            {
                this.Children.Add(childNode);
                childNode.GameState = childNode.calculateGameState();
                if (GameState == GameState.Playing)
                return;
            }
        }

        if (childNode.Thesus.Move(movingCommand))
        {
            childNode.GameState = childNode.calculateGameState();
            if (childNode.GameState == GameState.Playing)
            {
                childNode.moveMinoatur();
            }

            this.Children.Add(childNode);
            childNode.GameState = childNode.calculateGameState();
        }

    }

    private bool moveMinoatur()
    {
        var moved = this.Minatour.MoveOneCell();
        this.GameState = this.calculateGameState();
        if (GameState == GameState.Playing)
        {
            this.Minatour.MoveOneCell();
        }
        return moved;
    }

    private GameState calculateGameState()
    {
        var thesusLocation = this.Board.GetCell(c => c.CurrentPlayer == PlayerType.Thesus);
        var minatourLocation = this.Board.GetCell(c => c.CurrentPlayer == PlayerType.Minatour);

        if (minatourLocation == null || thesusLocation == null)
        {
            return GameState.PlayerLose;
        }

        if (thesusLocation.IsFinal)
        {
            return GameState.PlayerWon;
        }

        return GameState.Playing;
    }

    public bool IsEqual(Node node)
    {
        var isEqual = this.Thesus.CurrentLocation.IsEqual(node.Thesus.CurrentLocation) &&
            this.Minatour.CurrentLocation.IsEqual(node.Minatour.CurrentLocation);
        return isEqual;
    }

}
