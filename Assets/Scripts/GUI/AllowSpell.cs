using Assets.Scripts.GameLogic.DataModels;
using UnityEngine;

namespace Assets.Scripts.GUI
{
    public class AllowSpell : MonoBehaviour
    {
        private Vector3 initialPosition;
        private Vector3 screenPoint;
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

            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z + 10));

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

            var fairy = collider.gameObject.transform.GetComponentInParent<ActiveFairy>();
            var spell = collider.gameObject.GetComponent<ActiveSpell>();

            if (DataOfModels.Spells[gameObject.name].MajorElement != DataOfModels.Fairies[fairy.name].Element &&
                DataOfModels.Spells[gameObject.name].MinorElement != DataOfModels.Fairies[fairy.name].Element)
            {
                return;
            }

            if (!collider.gameObject.GetComponent<ActiveSpell>().IsEmpty)
            {
                EventAggregator.PublishRemovalSpell(fairy.Number, spell.Number, spell.gameObject.name);
            }
            EventAggregator.PublishAddingSpell(fairy.Number, spell.Number, gameObject.name);
            
            ChangeActiveSpell(collider.gameObject, gameObject);
            MakeUsed();

            foreach (var spelly in GameDataManager.Instance.PlayerData.ActiveFairies[fairy.Number].Spells)
            {
                Debug.Log(spelly.Name);
            }
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
        void Update()
        {
        
        }
    }
}
