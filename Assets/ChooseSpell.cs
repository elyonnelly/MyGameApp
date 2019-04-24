using UnityEngine;

namespace Assets
{
    public class ChooseSpell : MonoBehaviour
    {
        private Vector3 initialPosition;
        private Vector3 screenPoint;
        private Vector3 offset;
        private bool isDrag;
        private bool isUsed;
        void OnMouseDown()
        {
            gameObject.tag = "Allow Fairy";

            //переместили чуть вперед, чтобы не скрывался под другими
            //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
            if (!isDrag)
            {
                initialPosition = transform.position;
                isDrag = true;
            }

            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z + 10));

        }

        private void OnMouseUp()
        {
            transform.position = initialPosition;
            isDrag = false;
        }

        private void OnMouseDrag()
        {
            if (isUsed)
            {
                return;
            }

            var curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z + 10);
            var curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.tag != "Active Spell")
            {
                return;
            }


        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
