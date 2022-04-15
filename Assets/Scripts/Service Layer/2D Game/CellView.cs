using CoreLayer.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        private Color normalColor;
        private Image backGroundImage;

        private void Awake()
        {
            this.backGroundImage = this.GetComponent<Image>();
            this.normalColor = this.backGroundImage.color;
        }
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
            if (this.backGroundImage == null)
            {
                return;
            }

            this.backGroundImage.color = this.CellController.IsFinal ? Color.green: this.normalColor;
        }

        public void SetController(ICell cell)
        {
            this.CellController = cell;
            this.Refresh();

        }

        
    }
}