using Assets.Scripts.GameLogic.DataModels;
using UnityEngine;

namespace Assets.Scripts.GUI
{
    public class AllowSpell : MonoBehaviour
    {
        private Vector3 initialPosition;
        private Vector3 offset;
        private bool isDrag;
        private bool isUsed;

        void Start()
        {
            EventAggregator.RemoveSpell += MakeUnused;
        }

        void OnMouseDown()
        {
            if (isUsed)
            {
                return;
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2);
            if (!isDrag)
            {
                initialPosition = transform.position;
                isDrag = true;
            }

            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));

        }

        private void OnMouseUp()
        {
            if (isUsed)
            {
                return;
            }
            transform.position = initialPosition;
            isDrag = false;
        }

        private void OnMouseDrag()
        {
            if (isUsed)
            {
                return;
            }
            var curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y,  10);
            var curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag != "Active Spell")
            {
                return;
            }
            
            var fairy = collider.GetComponentInParent<ActiveFairy>();
            var spell = collider.GetComponent<ActiveSpell>();

            if (fairy.IsEmpty)
            {
                return;
            }
            if (DataOfModels.Spells[name].MajorElement != DataOfModels.Fairies[fairy.name].Element &&
                DataOfModels.Spells[name].MinorElement != DataOfModels.Fairies[fairy.name].Element)
            {
                return;
            }

            if (!collider.GetComponent<ActiveSpell>().IsEmpty)
            {
                EventAggregator.OnRemoveSpell(fairy.Number, spell.Number, spell.gameObject.name);
            }
            EventAggregator.OnAddSpell(fairy.Number, spell.Number, gameObject.name);
            
            ChangeActiveSpell(collider.gameObject, gameObject);
            MakeUsed();
        }

        void ChangeActiveSpell(GameObject oldSpell, GameObject newSpell)
        {
            oldSpell.GetComponent<SpriteRenderer>().sprite = newSpell.GetComponent<SpriteRenderer>().sprite;
            oldSpell.name = newSpell.name;
            oldSpell.GetComponent<ActiveSpell>().IsEmpty = false;
        }

        void MakeUsed()
        {
            transform.position = initialPosition;
            isUsed = true;
            isDrag = false;
        }

        void MakeUnused(int fairyPosition, int spellPosition, string name)
        {
            if (gameObject.name == name)
            {
                isUsed = false;
            }
        }
    }
}
