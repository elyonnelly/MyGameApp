using UnityEngine;

namespace Assets.Scripts
{
    public class ActiveFairies : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var i = 0;
            foreach (var fairy in GameDataManager.Instance.PlayerData.ActiveFairies)
            {
                transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Sprites/Fairies Icon/{fairy.Name}/{fairy.Name}");
                transform.GetChild(i).name = fairy.Name;
                i++;
            }

        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
