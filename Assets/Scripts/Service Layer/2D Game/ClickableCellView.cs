using CoreLayer.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ServiceLayer.Game2D
{
    public class ClickableCellView:CellView
    {
        [SerializeField]
        private Color clickedColor, normalColor;

        private Image backGroundImage;

        public Action<ICell> OnClickedAction;
        private void Awake()
        {
            this.backGroundImage = this.GetComponent<Image>();
        }

        public void OnClick()
        {
            this.backGroundImage.color = this.clickedColor;
            this.OnClickedAction?.Invoke(this.CellController);
        }

        public void ResetColor()
        {
            this.backGroundImage.color = this.normalColor;
        }
        
        
    }
}