using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CoreLayer.Interfaces
{
    public interface IPathCalculator
    {
        int GetVirtualPathLength(ICell startingLocation, ICell endLocation);
    }
}