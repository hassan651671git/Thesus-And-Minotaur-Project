using CoreLayer.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CoreLayer.Interfaces
{
    public interface ILevelRepository 
    {

        bool ISLevelExist(int levelNumber);
        Level GetLevel(int levelNumber);

        void SaveLevel(Level level);
    }
}