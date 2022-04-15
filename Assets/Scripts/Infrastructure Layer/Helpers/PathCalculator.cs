using CoreLayer.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace InfraStructure.Helpers
{
    public class PathCalculator : IPathCalculator
    {
        public int GetVirtualPathLength(ICell startingLocation, ICell endLocation)
        {
            if (startingLocation == null || endLocation == null)
            {
                return int.MaxValue;
            }

            var xLength = Mathf.Abs(startingLocation.RowPosition - endLocation.RowPosition);
            var yLength = Mathf.Abs(startingLocation.ColumnPosition - endLocation.ColumnPosition);
            return xLength + yLength;
        }
    }

}