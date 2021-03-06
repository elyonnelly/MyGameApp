﻿using System;
using System.Collections;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    public class InputController : MonoBehaviour
    {
        public GameObject AllowFairies;
        public GameObject AllowSpells;
        public int PageSpell;
        public int PageFairy;
        public string CurrentEssence;

        public Text FairyInfo;
        public Text FairyDescription;
        public Text SpellInfo;
        public SpriteRenderer FairyPhoto;
        public Text Message;

        public void ExitApp()
        {
            Application.Quit();
            try
            {
                using (var writer = new StreamWriter(Application.persistentDataPath + "/player.json"))
                {
                    writer.Write(JsonConvert.SerializeObject(GameDataManager.Instance.PlayerData));
                }
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }

        public void LoadScene(string sceneName)
        {
            if (SceneManager.GetActiveScene().name == "Choise Fairy Scene" || SceneManager.GetActiveScene().name == "Winner Scene")
            {
                foreach (var fairy in GameDataManager.Instance.PlayerData.ActiveFairies)
                {
                    if (fairy.Name == null)
                    {
                        ShowMessage();
                        return;
                    }
                }

                try
                {
                    using (var writer = new StreamWriter(Application.persistentDataPath +"player.json"))
                    {
                        writer.Write(JsonConvert.SerializeObject(GameDataManager.Instance.PlayerData));
                    }
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                }
            }

            SceneManager.LoadScene(sceneName);
        }
        

        public void DisplayAllowFairies()
        {
            CurrentEssence = "fairy";
            SpellInfo.text = "";

            for (var i = 0; i < AllowSpells.transform.childCount; i++)
            {
                AllowSpells.transform.GetChild(i).gameObject.SetActive(false);
            }

            ShowFairies();
        }

        public void DisplayAllowSpells()
        {
            CurrentEssence = "spell";
            FairyInfo.text = "";
            FairyDescription.text = "";
            FairyPhoto.sprite = null;

            for (var i = 0; i < AllowFairies.transform.childCount; i++)
            {
                AllowFairies.transform.GetChild(i).gameObject.SetActive(false);
            }

            ShowSpells();
        }

        public void ScrollRight()
        {
            if (CurrentEssence == "spell")
            {
                ScrollRightSpells();
            }
            else
            {
                ScrollRightFairies();
            }
        }

        public void ScrollLeft()
        {
            if (CurrentEssence == "spell")
            {
                ScrollLeftSpells();
            }
            else
            {
                ScrollLeftFairies();
            }
        }

        private void ScrollRightSpells()
        {
            PageSpell = PageSpell > 1 ? PageSpell : PageSpell + 1;
            ShowSpells();
        }

        private void ScrollLeftSpells()
        {
            PageSpell = PageSpell < 1 ? PageSpell : PageSpell - 1;
            ShowSpells();
        }
        private void ScrollRightFairies()
        {
            PageFairy = PageFairy > 1 ? PageFairy : PageFairy + 1;
            ShowFairies();
        }

        private void ScrollLeftFairies()
        {
            PageFairy = PageFairy < 1 ? PageFairy : PageFairy - 1;
            ShowFairies();
        }

        private void ShowMessage()
        {
            Message.text = "Please fill all slots";
            StartCoroutine(FadeText(Message));
        }

        private IEnumerator FadeText(Text text)
        {
            var color = new Color(text.color.r, text.color.g, text.color.b, 10);
            text.color = color;
            for (var f = 1f; f >= 0; f -= 0.02f)
            {
                text.color = color;
                color.a = f;
                yield return new WaitForEndOfFrame();
            }
        }


        private void ShowFairies()
        {
            for (var i = 0; i < AllowFairies.transform.childCount; i++)
            {
                AllowFairies.transform.GetChild(i).gameObject.SetActive(false);
            }

            var fairies = new string[AllowFairies.GetComponent<AllowFairiesController>().TableOfFairies.Count];

            AllowFairies.GetComponent<AllowFairiesController>().TableOfFairies.Keys.CopyTo(fairies, 0);
            for (var i = PageFairy * 36; i < Math.Min((PageFairy + 1) * 36, fairies.Length); i++)
            {
                AllowFairies.GetComponent<AllowFairiesController>().TableOfFairies[fairies[i]].SetActive(true);
            }
        }

        private void ShowSpells()
        {
            for (var i = 0; i < AllowSpells.transform.childCount; i++)
            {
                AllowSpells.transform.GetChild(i).gameObject.SetActive(false);
            }

            var spells = new string[AllowSpells.GetComponent<AllowSpellsController>().TableOfSpells.Count];

            AllowSpells.GetComponent<AllowSpellsController>().TableOfSpells.Keys.CopyTo(spells, 0);
            for (var i = PageSpell * 36; i < Math.Min((PageSpell + 1) * 36, spells.Length); i++)
            {
                AllowSpells.GetComponent<AllowSpellsController>().TableOfSpells[spells[i]].SetActive(true);
            }
        }
    }
}