using CoreLayer.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CoreLayer.Interfaces
{
    public interface IThesus:IPlayer
    {
        bool Move(MovingCommand movingCommand);

    }
}