using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GUI
{
    public class ListOfFairies : MonoBehaviour
    {
        public GameObject Fairy;
        public static Dictionary<string, GameObject> tableOfFairies;

        private void Awake()
        {
            EventAggregator.DisableFairy += UpdateTable;

            tableOfFairies = new Dictionary<string, GameObject>();
            FillTableOfFairies();

            var player = GameDataManager.Instance.PlayerData;

            foreach (var fairy in player.AllowFairies)
            {
                if (tableOfFairies.ContainsKey(fairy.Name))
                {
                    tableOfFairies[fairy.Name].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Sprites/Fairies Icon/{fairy.Name}");
                    tableOfFairies[fairy.Name].GetComponent<AllowFairy>().IsAllow = true;
                }


            }

            foreach (var fairy in player.ActiveFairies)
            {
                tableOfFairies[fairy.Name].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Sprites/EmptyPrefab");
                tableOfFairies[fairy.Name].GetComponent<AllowFairy>().IsUsed = true;
            }


        }

        private void FillTableOfFairies()
        {
            var fairies = GameDataManager.Instance.PlayerData.AllowFairies;
            var count = 0;

            for (var y = transform.position.y; y > transform.position.y - 6; y -= 1)
            {
                for (var x = transform.position.x; x < transform.position.x + 6; x += 1)
                {
                    tableOfFairies.Add(fairies[count].Name, CreateNewFairyObject(fairies[count].Name, new Vector3(x, y, -1)));
                    count++;
                }
            }

        }

        private GameObject CreateNewFairyObject(string name, Vector3 position)
        {
            var newFairy = Instantiate(Fairy, position, Quaternion.identity);
            newFairy.transform.SetParent(transform);
            newFairy.SetActive(false);
            newFairy.name = name;
            return newFairy;
        }

        private void UpdateTable(int position, string fairyName)
        {
            tableOfFairies[fairyName].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Sprites/Fairies Icon/{fairyName}");
        }

        /*void Update()
        {
            Debug.Log(tableOfFairies["Tadana"] == null);
        }*/

    }

}