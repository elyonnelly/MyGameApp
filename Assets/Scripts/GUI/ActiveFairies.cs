using UnityEngine;

namespace Assets.Scripts.GUI
{
    public class ActiveFairies : MonoBehaviour
    {
        void Start()
        {
            var i = 0;

            foreach (var fairy in GameDataManager.Instance.PlayerData.ActiveFairies)
            {
                var fairyObject = transform.GetChild(i);

                fairyObject.name = fairy.Name;
                fairyObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Sprites/Fairies Icon/{fairy.Name}");

                var j = 0;
                foreach (var spell in fairy.Spells)
                {
                    var spellObject = fairyObject.transform.GetChild(j);
                    spellObject.name = spell.Name;
                    spellObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Sprites/Spells Icon/{spell.Name}");
                    j++;
                }

                i++;
            }

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}