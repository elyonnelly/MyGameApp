using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.GameLogic.DataModels;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    public class EnemyFairyComponent : MonoBehaviour
    {
        // Start is called before the first frame update

        public Text FairyInfo;
        public int Number;
        private Fairy fairy;
        public GameObject Sparkle;
        void Start()
        {
            fairy = GameDataManager.Instance.EnemyData.ActiveFairies[Number];
            FairyInfo.text = fairy.GetState();

            if (SceneManager.GetActiveScene().name == "Battlefield Scene")
            {
                EventAggregator.EnemyAttack += Attack;
            }
        }

        void OnDisable()
        {
            EventAggregator.EnemyAttack -= Attack;
        }

        // Update is called once per frame
        void Update()
        {
            FairyInfo.text = fairy.GetState();
        }

        void Attack(int forwardFairy, int victimFairy, string spell, string victim)
        {
            if (forwardFairy != Number)
            {
                return;
            }

            //иначе нужно нарисовать искорки

            var sparkles = new List<GameObject>();
            var position = new Vector3();
            position = transform.GetChild(0).name == spell ? transform.GetChild(0).transform.position : transform.GetChild(2).transform.position;
            var victimPosition = GameObject.FindGameObjectWithTag("Player").transform.GetChild(victimFairy).transform.position;
            //Debug.Log(victimPosition);
            var directionVector = (victimPosition - transform.position) /5;

            StartCoroutine(ShowMagic(position, directionVector, victimPosition));


        }

        private IEnumerator ShowMagic(Vector3 position, Vector3 directionVector, Vector3 victimPosition)
        {
            var sparkles = new List<GameObject>();
            for (var i = 0; i < 5; i++)
            {
                position.z += -1;
                sparkles.Add(Instantiate(Sparkle, position, Quaternion.identity));
                position += directionVector;
                yield return new WaitForSeconds(0.1f);

            }
            victimPosition.z -= 1;
            sparkles.Add(Instantiate(Sparkle, victimPosition, Quaternion.identity));
            yield return new WaitForSeconds(0.1f);

            foreach (var sparkle in sparkles)
            {
                Destroy(sparkle);
            }
        }

    }
}
