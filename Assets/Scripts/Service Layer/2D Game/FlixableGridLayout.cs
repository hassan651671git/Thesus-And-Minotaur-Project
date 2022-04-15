using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlixableGridLayout : LayoutGroup
{
    public int Rows = 1;
    public int Columns = 1;
    public Vector2 CellSize;
    public Vector2 Spacing;
    public override void CalculateLayoutInputVertical()
    {

    }

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();
        float parentWidth = this.rectTransform.rect.width;
        float parentHeight = this.rectTransform.rect.height;
        this.CellSize.x = parentWidth / this.Columns - (Spacing.x * (this.Columns - 1) /(float) this.Columns) - (this.padding.right + this.padding.left) /(float)this.Columns;
        this.CellSize.y = parentHeight / this.Rows - (Spacing.y * (this.Rows - 1 )/ (float)this.Rows) - (this.padding.top + this.padding.bottom) / (float)this.Rows;
        int rowNumper = 0, columnNumper = 0;
        
        for(int childIndex = 0; childIndex < this.rectTransform.childCount; childIndex++)
        {
            rowNumper = childIndex / this.Columns;
            columnNumper = childIndex % Columns;
            var cell = this.rectChildren[childIndex];
            var xPos = CellSize.x * columnNumper + this.Spacing.x * columnNumper + this.padding.left;
            var yPos = CellSize.y * rowNumper + this.Spacing.y * rowNumper + this.padding.top;
            this.SetChildAlongAxis(cell, 0, xPos, CellSize.x);
            this.SetChildAlongAxis(cell, 1, yPos, CellSize.y);

        }
    }
    public override void SetLayoutHorizontal()
    {
    }

    public override void SetLayoutVertical()
    {
    }

    public void Clear()
    {
        for(int index = 0; index < this.rectTransform.childCount; index++)
        {
            Destroy(this.transform.GetChild(index).gameObject);
        }
    }
}
