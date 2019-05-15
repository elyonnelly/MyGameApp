using UnityEngine;

namespace Assets.Scripts.GUI
{
    public class ActiveFairyController : MonoBehaviour, IDragHandler
    {
        public bool IsEmpty;
        public int Number;
        public bool IsDrag;

        private Vector3 initialPosition;
        private Vector3 offset;

        public void OnMouseDown()
        {
            if (IsEmpty)
            {
                return;
            }

            if (!IsDrag)
            {
                initialPosition = transform.position;
                IsDrag = true;
            }

            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2);

            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        }

        public void OnMouseUp()
        {
            if (IsEmpty)
            {
                return;
            }

            if (!IsEmpty)
            {
                EventAggregator.OnDisableFairy(Number, name);
            }

            MakeEmpty();
        }

        public void OnMouseDrag()
        {
            if (IsEmpty)
            {
                return;
            }

            var curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            var curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }

        private void Start()
        {
            foreach (var spell in transform.GetComponentsInChildren<ActiveSpellController>())
            {
                spell.IsEmpty = true;
            }
        }

        private void MakeEmpty()
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/EmptyPrefab");
            name = $"ActiveFairy{Number}";
            transform.position = initialPosition;
            IsEmpty = true;
            IsDrag = false;
            for (var i = 0; i < 4; i++)
            {
                transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Spells Icon/Defensive Spells/Empty Slot");
            }
        }
    }
}