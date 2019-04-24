using Assets.Scripts.GameLogic.DataModels;
using UnityEngine;

namespace Assets.Scripts.GUI
{
    public class AllowFairy : MonoBehaviour
    {
        public bool IsAllow;
        public bool IsUsed;

        private Vector3 initialPosition;
        private Vector3 ScreenPoint;
        private Vector3 offset;
        private bool isDrag;

        private void Start()
        {
            EventAggregator.DisableFairy += MakeUnused;
        }
        private void OnMouseDown()
        {
            if (!IsAllow || IsUsed)
            {
                return;
            }
            gameObject.tag = "Allow Fairy";

            //переместили чуть вперед, чтобы не скрывался под другими
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
            if (!isDrag)
            {
                initialPosition = transform.position;
                isDrag = true;
            }

            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenPoint.z + 10));
        }

        private void OnMouseUp()
        {
            if (!IsAllow || IsUsed)
            {
                return;
            }
            transform.position = initialPosition;
            isDrag = false;
        }
        private void OnMouseDrag()
        {
            if (!IsAllow || IsUsed)
            {
                return;
            }
            var curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenPoint.z + 10);
            var curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.tag != "Active Fairy" || gameObject.tag != "Allow Fairy")
            {
                return;
            }

            if (GameDataManager.Instance.FairyActive(gameObject.name))
            {
                Debug.Log("text");
                return;
            }

            if (!collider.gameObject.GetComponent<ActiveFairy>().IsEmpty)
            {
                EventAggregator.PublishFairyDeactivation(collider.gameObject.name);
            }
            EventAggregator.PublishFairyActivation(gameObject.name);

            ChangeFairy(collider.gameObject, gameObject);
            MakeUsed();
        }

        private void ChangeFairy(GameObject oldFairy, GameObject newFairy)
        {
            oldFairy.name = newFairy.name;
            oldFairy.GetComponent<SpriteRenderer>().sprite = newFairy.GetComponent<SpriteRenderer>().sprite;
            oldFairy.GetComponent<ActiveFairy>().IsEmpty = false;
        }

        private void MakeUnused(string name)
        {
            if (name == gameObject.name)
            {
                IsUsed = false;
            }
        }

        private void MakeUsed()
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/EmptyPrefab");
            transform.position = initialPosition;
            IsUsed = true;
            isDrag = false;
        }

        
    }
}