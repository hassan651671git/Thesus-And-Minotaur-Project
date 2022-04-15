using CoreLayer.Enums;
using CoreLayer.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameResolver 
{
    private IBoard board;
    private Queue<Node> buffer = new Queue<Node>();
    public List<MovingCommand> solution { get; private set; } = new List<MovingCommand>();
    public List<ICell> thesusPath { get; private set; } = new List<ICell>();
    public List<ICell> minatorPath { get; private set; } = new List<ICell>();
    public GameResolver(IBoard board)
    {
        this.board = board;
    }

    public void Resolve()
    {
        Node rootNode = new Node(this.board, null, MovingCommand.Wait);
        Node resultNode=null;
        List<Node> traversedNodes = new List<Node>();
        this.buffer.Enqueue(rootNode);
        int searchingInChildCount = 0;
        int maxChildSearch = 10000;

        while (this.buffer.Count > 0)
        {
            searchingInChildCount++;

            if (searchingInChildCount > maxChildSearch)
            {
                break;
            }

            var node = this.buffer.Dequeue();
            if (node.GameState == GameState.PlayerWon)
            {
                resultNode = node;
                break;
            }

            if (node.GameState == GameState.PlayerLose)
            {
                continue;
            }

            if (traversedNodes.FirstOrDefault(n => n.IsEqual(node)) != null)
            {
                continue;
            }

            traversedNodes.Add(node);

            node.GenerateChildren();
            foreach (var child in node.Children)
            {
                this.buffer.Enqueue(child);
            }
        }

        if (resultNode != null)
        {
            while (resultNode.Parent != null)
            {
                solution.Add(resultNode.MovingCommand);
                thesusPath.Add(resultNode.Thesus.CurrentLocation);
                minatorPath.Add(resultNode.Minatour.CurrentLocation);
                resultNode = resultNode.Parent;
            }
            this.solution.Reverse();
            this.thesusPath.Reverse();
            this.minatorPath.Reverse();
        }
    }
}
