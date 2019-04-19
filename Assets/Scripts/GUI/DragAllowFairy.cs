using Assets.Scripts.GameLogic.DataModels;
using Assets.Scripts.GUI;
using UnityEngine;

namespace Assets.Scripts
{
    public class DragAllowFairy : MonoBehaviour
    {
        private Vector3 initialPosition;
        private Vector3 ScreenPoint;
        private Vector3 offset;
        private bool isDrag;
        private bool isUsed;

        private void OnMouseDown()
        {
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
            transform.position = initialPosition;
            isDrag = false;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.tag != "Active Fairy" || gameObject.tag != "Allow Fairy" )
            {
                return;
            }

            if (GameDataManager.Instance.PlayerData.ActiveFairies.Exists(x => x.Name == gameObject.name))
            {
                return;
            }

            if (!collider.gameObject.GetComponent<ChangeActiveFairy>().IsEmpty)
            {
                GameDataManager.Instance.PlayerData.ActiveFairies.Remove(Fairies.ListOfFairies[collider.gameObject.name]);
                EventAggregator.PublishFreedomFairy(collider.gameObject.name);
            }

            GameDataManager.Instance.PlayerData.ActiveFairies.Add(Fairies.ListOfFairies[gameObject.name]);

            collider.gameObject.name = gameObject.name;
            collider.gameObject.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            collider.gameObject.GetComponent<ChangeActiveFairy>().IsEmpty = false;

            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/EmptyPrefab");
            Destroy(gameObject.GetComponent<DragAllowFairy>());
            transform.position = initialPosition;
            isUsed = true;
            isDrag = false;
        }

        private void OnMouseDrag()
        {
            if (isUsed)
            {
                return;
            }

            var curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenPoint.z + 10);
            var curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
    }
}