using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreLayer.Interfaces
{
    public interface IPlayerView
    {
        void SetLocation(ICell cell);
        GameObject MonoObject { get; }
    }
}