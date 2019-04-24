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
            Debug.ClearDeveloperConsole();
            if (collider.gameObject.tag != "Active Fairy" || gameObject.tag != "Allow Fairy")
            {
                return;
            }
            var oldFairy = collider.gameObject.GetComponent<ActiveFairy>();
            var newFairy = gameObject;
            
            if (!oldFairy.IsEmpty)
            {
                EventAggregator.PublishFairyDeactivation(oldFairy.Number, oldFairy.gameObject.name);
            }
            EventAggregator.PublishFairyActivation(oldFairy.Number, newFairy.name);
            ChangeFairy(oldFairy.gameObject, newFairy);
            MakeUsed();

            foreach (var fairy in GameDataManager.Instance.PlayerData.ActiveFairies)
            {
                Debug.Log(fairy.Name);
            }
        }

        private void ChangeFairy(GameObject oldFairy, GameObject newFairy)
        {
            oldFairy.name = newFairy.name;
            oldFairy.GetComponent<SpriteRenderer>().sprite = newFairy.GetComponent<SpriteRenderer>().sprite;
            oldFairy.GetComponent<ActiveFairy>().IsEmpty = false;
        }

        //эту обработку возможно стоит перенести в ну например ListOfFairies
        private void MakeUnused(int position, string name)
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