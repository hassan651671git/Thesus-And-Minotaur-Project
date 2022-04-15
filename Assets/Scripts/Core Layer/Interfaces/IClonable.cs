using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CoreLayer.Interfaces
{
    public interface IClonable
    {
        IClonable Clone();
        void Restore(IClonable clonable);

    }
}