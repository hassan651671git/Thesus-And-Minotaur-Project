using CoreLayer.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ServiceLayer.Controllers
{
    public class History : IHistory
    {
        private Stack<IClonable> snapShots = new Stack<IClonable>();
        private IClonable originalObject;

        public History(IClonable originalObject)
        {
            this.originalObject = originalObject;
        }

        public void SaveSnapshot()
        {
            this.snapShots.Push(this.originalObject.Clone());
        }

        public void Undo()
        {
            if (this.snapShots.Count > 0)
            {
                var snapShot = this.snapShots.Pop();
                this.originalObject.Restore(snapShot);
            }
        }
    }
}