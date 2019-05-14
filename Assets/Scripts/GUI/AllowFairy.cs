using Assets.Scripts.GameLogic.DataModels;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    public class AllowFairy : MonoBehaviour
    {
        public Text Info;
        public Text Description;
        public SpriteRenderer Photo;
        public bool IsAllow;
        public bool IsUsed;

        private Vector3 initialPosition;
        private Vector3 offset;
        private bool isDrag;
        private Fairy fairy;

        private void Start()
        {
            EventAggregator.DisableFairy += MakeUnused;
            Info = GameObject.FindGameObjectWithTag("FairyInfo").GetComponent<Text>();
            Description = GameObject.FindGameObjectWithTag("FairyDescription").GetComponent<Text>();
            Photo = GameObject.FindGameObjectWithTag("FairyPhoto").GetComponent<SpriteRenderer>();


            foreach (var fairy in GameDataManager.Instance.PlayerData.AllowFairies)
            {
                if (fairy.Name == name)
                {
                    this.fairy = fairy;
                }
            }

        }
        private void OnDestroy()
        {
            EventAggregator.DisableFairy -= MakeUnused;
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

            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            ViewInfo();
        }

        private void ViewInfo()
        {
            var fairy = (Fairy)DataOfModels.Fairies[name].Clone();
            Photo.sprite = Resources.Load<Sprite>($"Sprites/Fairies Icon/Description Icon/{fairy.Name}");
            Info.text = $"Level:  {fairy.Level} \n Mana: {fairy.Magic}\n Hit Points: {fairy.HitPoints}";
            Description.text = $"{fairy.Name}\n\n{fairy.Description}\n ";
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
            var curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            var curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag != "Active Fairy" || tag != "Allow Fairy")
            {
                return;
            }
            var oldFairy = collider.GetComponent<ActiveFairy>();
            var newFairy = gameObject;
            
            if (!oldFairy.IsEmpty)
            {
                EventAggregator.OnDisableFairy(oldFairy.Number, oldFairy.gameObject.name);
            }
            EventAggregator.OnActivateFairy(oldFairy.Number, newFairy.name);
            ChangeFairy(oldFairy.gameObject, newFairy);
            MakeUsed();
        }

        private void ChangeFairy(GameObject oldFairy, GameObject newFairy)
        {
            oldFairy.name = newFairy.name;
            for (int i = 0; i < 4; i++)
            {
                oldFairy.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Spells Icon/Defensive Spells/Empty Slot");
            }
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
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/EmptyPrefab");
            transform.position = initialPosition;
            IsUsed = true;
            isDrag = false;
        }

    }
}