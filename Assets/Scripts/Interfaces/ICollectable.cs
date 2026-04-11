using UnityEngine;

namespace Interfaces
{
    public interface ICollectable
    {
        public void Collect(Transform parent);
        public void Drop();
    }
}