using CoreLayer.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLayer.Game2D
{
    public class CellView : MonoBehaviour,ICellView
    {


        [SerializeField]
        private GameObject LeftBlock;

        [SerializeField]
        private GameObject RightBlock;

        [SerializeField]
        private GameObject TopBlock;

        [SerializeField]
        private GameObject BottomBlock;

        public ICell CellController { get; private set; }
        public GameObject MonoObject => this.gameObject;

        public void Refresh()
        {
            if (this.CellController == null)
            {
                return;
            }

            this.LeftBlock.SetActive(this.CellController.IsLeftBlocked);
            this.RightBlock.SetActive(this.CellController.IsRightBlocked);
            this.TopBlock.SetActive(this.CellController.IsTopBlocked);
            this.BottomBlock.SetActive(this.CellController.IsBottomBlocked);
        }

        public void SetController(ICell cell)
        {
            this.CellController = cell;
            this.Refresh();

        }

        
    }
}