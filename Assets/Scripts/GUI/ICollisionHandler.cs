using UnityEngine;

namespace Assets.Scripts.GUI
{
    public interface ICollisionHandler
    {
        void OnTriggerEnter2D(Collider2D collider);
        void ChangeObject(GameObject oldFairy, GameObject newFairy);
        void MakeUsed();
    }
}