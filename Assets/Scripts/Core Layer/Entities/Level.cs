using CoreLayer.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CoreLayer.Entities
{
    [Serializable]
    public class Level
    {
        [SerializeField]
        public int LevelNumber { get; set; }

        [SerializeField]
        public IBoard Board { get; set; }

        [SerializeField]
        public string Comment { get; set; }

        [SerializeField]
        public string Solution { get; set; }

        public Level()
        {

        }


    }

}