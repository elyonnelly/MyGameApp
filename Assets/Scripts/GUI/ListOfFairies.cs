using System;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.GameLogic.DataModels;
using Assets.Scripts.GUI;

namespace Assets.Scripts
{
    public class ListOfFairies : MonoBehaviour
    {
        public GameObject Fairy;
        public Dictionary<string, GameObject> TableOfFairies;

        // Start is called before the first frame update
        private void Start()
        {
            EventAggregator.FreedomFairy += UpdateTable;

            TableOfFairies = new Dictionary<string, GameObject>();
            var player = GameDataManager.Instance.PlayerData;
            var NamesOfFairies = new string[Fairies.ListOfFairies.Count];

            Fairies.ListOfFairies.Keys.CopyTo(NamesOfFairies, 0);
            var count = 0;

            for (var y = transform.position.y; y > transform.position.y - 6; y -= 1)
            {
                for (var x = transform.position.x; x < transform.position.x + 6; x += 1)
                {
                    var newFairy = Instantiate(Fairy, new Vector3(x, y, -1), Quaternion.identity);
                    newFairy.transform.SetParent(transform);
                    newFairy.SetActive(false);

                    newFairy.name = count < NamesOfFairies.Length ? NamesOfFairies[count] : $"EmptySlot + {count}";
                    try
                    {
                        TableOfFairies.Add(newFairy.name, newFairy);
                    }
                    catch(Exception ex)
                    {
                        Debug.Log(ex.Message);

                    }
                    count++;
                }
            }

            var i = 0;
            foreach (var fairy in player.AllowFairies)
            {
                TableOfFairies[fairy.Name].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Sprites/Fairies Icon/{fairy.Name}/{fairy.Name}");
                TableOfFairies[fairy.Name].AddComponent<DragAllowFairy>();
                    //GetComponent<DragAllowFairy>().AllowForDrag = true;
            }

            foreach (var fairy in player.ActiveFairies)
            {
                TableOfFairies[fairy.Name].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Sprites/EmptyPrefab");
                Destroy(TableOfFairies[fairy.Name].GetComponent<DragAllowFairy>());
            }
        }

        // Update is called once per frame
        private void Update()
        {
        }

        private void UpdateTable(string fairyName)
        {
            TableOfFairies[fairyName].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Sprites/Fairies Icon/{fairyName}/{fairyName}");
            TableOfFairies[fairyName].AddComponent<DragAllowFairy>();
            //TableOfFairies[fairyName].GetComponent<DragAllowFairy>().IsUsed = false;
        }
    }

}