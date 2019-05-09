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

                FillInfoAboutSpell(fairyObject, 0, fairy.Spells[0].Name, "Offensive Spells");
                FillInfoAboutSpell(fairyObject, 1, fairy.Spells[1].Name, "Defensive Spells");
                FillInfoAboutSpell(fairyObject, 2, fairy.Spells[2].Name, "Offensive Spells");
                FillInfoAboutSpell(fairyObject, 3, fairy.Spells[3].Name, "Defensive Spells");

                i++;
            }

        }

        void FillInfoAboutSpell( Transform fairy,int  index, string name, string type)
        {
            var spellObject = fairy.transform.GetChild(index);
            spellObject.name = name;
            spellObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Sprites/Spells Icon/{type}/{name}");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}