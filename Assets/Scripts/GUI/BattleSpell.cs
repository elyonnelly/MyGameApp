using UnityEngine;

namespace Assets.Scripts.GUI
{
    public class BattleSpell : MonoBehaviour
    {
        // Start is called before the first frame update

        private bool isDrag;
        private Vector3 initialPosition;
        private Vector3 offset;
        void OnMouseDown()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z );
            if (!isDrag)
            {
                initialPosition = transform.position;
                isDrag = true;
            }

            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));

        }

        private void OnMouseClick()
        {
            Debug.Log("test");
        }
        private void OnMouseDrag()
        {
            if (!isDrag)
            {
                return;
            }
            var curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2);
            var curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
        private void OnMouseUp()
        {
            transform.position = initialPosition;
            isDrag = false;
        }


        private void OnTriggerEnter2D(Collider2D collider)
        {
            //Debug.Log(collider.gameObject.tag);
            if (tag == "Player Spell" && collider.tag == "Enemy Fairy")
            {
                EventAggregator.PublishFairyAttack(transform.GetComponentInParent<FairyComponent>().Number,
                                                   collider.GetComponent<FairyComponent>().Number, name, 
                                                   collider.transform.parent.tag);
                Debug.Log(initialPosition);
                transform.position = initialPosition;
                Debug.Log(transform.position);
                isDrag = false;
            }
        }
    }
}
