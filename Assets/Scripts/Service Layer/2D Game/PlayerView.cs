using CoreLayer.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ServiceLayer.Game2D
{
    public class PlayerView : MonoBehaviour,IPlayerView
    {
        public GameObject MonoObject => this.gameObject;

        public void SetLocation(ICell cell)
        {
            this.ParentRectTransform = cell.CellView.MonoObject.GetComponent<RectTransform>();
        }

        private RectTransform RectTransform;
        private RectTransform ParentRectTransform;
        private void Awake()
        {
            this.RectTransform = this.GetComponent<RectTransform>();
        }

        private void FixedUpdate()
        {
            if (this.ParentRectTransform == null)
            {
                return;
            }

            if (Time.fixedTime < 0.2f)
            {
                this.RectTransform.anchoredPosition = this.ParentRectTransform.anchoredPosition;
                this.RectTransform.sizeDelta = this.ParentRectTransform.sizeDelta;
            }
            this.RectTransform.anchoredPosition = Vector2.Lerp(this.RectTransform.anchoredPosition, this.ParentRectTransform.anchoredPosition, Time.fixedDeltaTime * 5);
            this.RectTransform.sizeDelta = this.ParentRectTransform.sizeDelta;
        }
    }
}