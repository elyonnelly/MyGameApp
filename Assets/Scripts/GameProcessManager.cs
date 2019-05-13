using System.Collections;
using System.Timers;
using Assets.Scripts.GameLogic;
using Assets.Scripts.GameLogic.DataModels;
using Assets.Scripts.GUI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

namespace Assets.Scripts
{
    public class GameProcessManager : MonoBehaviour
    {
        public static Spell[,] PlayerSpells = new Spell[5, 4];
        public static Spell[,] EnemySpells = new Spell[5, 4];

        private static readonly Random randomizer = new Random();

        private Player player;
        private Player enemy;
        private Timer timer;
        private string currentHero;
        private double allCurrentDamage;
        private void Awake()
        {
            player = GameDataManager.Instance.PlayerData;
            player.Name = "Player";
            enemy = GameDataManager.Instance.EnemyData;
            enemy.Name = "Enemy";
            timer = new Timer(180000); // 3 минуты
            timer.Elapsed += NewMoveFromTimer;
            timer.Enabled = true;
            FillSpellInfo(player, PlayerSpells);
            FillSpellInfo(enemy, EnemySpells);

            currentHero = "player";

            EventAggregator.DisableFairy += OnDisableFairy;
            EventAggregator.ActivateFairy += OnActivateFairy;
            EventAggregator.RemoveSpell += OnRemoveSpell;
            EventAggregator.AddSpell += OnAddSpell;
            EventAggregator.FairyAttack += OnFairyAttackEvent;
            EventAggregator.EnemyAttack += OnFairyAttackEvent;
        }

        void OnDisable()
        {
            EventAggregator.DisableFairy -= OnDisableFairy;
            EventAggregator.ActivateFairy -= OnActivateFairy;
            EventAggregator.RemoveSpell -= OnRemoveSpell;
            EventAggregator.AddSpell -= OnAddSpell;
            EventAggregator.FairyAttack -= OnFairyAttackEvent;
            EventAggregator.EnemyAttack -= OnFairyAttackEvent;
        }


        private void FillSpellInfo(Player hero, Spell[,] spells)
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    spells[i, j] = new Spell();
                    if (j % 2 == 0)
                    {
                        if (hero.ActiveFairies != null && DataOfModels.OffensiveSpells.ContainsKey(hero.ActiveFairies[i].Spells[j]))
                        {
                            spells[i, j] = (Spell)DataOfModels.OffensiveSpells[hero.ActiveFairies[i].Spells[j]].Clone();
                        }
                    }
                    else
                    {
                        if (hero.ActiveFairies != null && DataOfModels.DefensiveSpells.ContainsKey(hero.ActiveFairies[i].Spells[j]))
                        {
                            spells[i, j] = (Spell)DataOfModels.DefensiveSpells[hero.ActiveFairies[i].Spells[j]].Clone();
                        }
                    }
                }
            }
        }

        private void Update()
        {
            VictoryCheck(player);
            VictoryCheck(enemy);
        }
        void VictoryCheck(Player hero)
        {
            var count = 0;
            foreach (var fairy in hero.ActiveFairies)
            {
                if (fairy.IsDead)
                {
                    count++;
                }
            }
            if (count == 5)
            {
                var winner = hero.Name == "Player" ? "Enemy" : "Player";
                if (winner == "Player")
                {
                    RecoverFairy();
                    GiveAwards();
                    SceneManager.LoadScene("Winner Scene");
                }
                if (winner == "Enemy")
                {
                    RecoverFairy();
                    SceneManager.LoadScene("Losing Scene");
                }

                EventAggregator.OnVictoryInBattle(winner);
            }
        }


        void RecoverFairy()
        {
            foreach (var fairy in player.ActiveFairies)
            {
                fairy.HealthPoint = fairy.HitPoints * 8 + 11;
            }
        }
        void GiveAwards()
        {
            foreach (var fairy in player.ActiveFairies)
            {
                foreach (var enemyFairy in enemy.ActiveFairies)
                {
                    if (enemyFairy.Level >= fairy.Level)
                    {
                        fairy.ExperiencePoints += 5;
                    }
                    else
                    {
                        fairy.ExperiencePoints += 3;
                    }
                }

                Debug.Log(fairy.ExperiencePoints);

            }
        }

        private void NewMoveFromTimer(object source, ElapsedEventArgs e)
        {
            ShowEffect("Time is over!");
            NewMove("Enemy");
        }

        private void NewMove(string hero)
        {
            if (hero == "Player")
            {
                UnlockPlayerSpells();
            }

            if (hero == "Enemy")
            {
                BlockPlayerSpells();
                EnemyMove();
            }
        }

        private void BlockPlayerSpells()
        {
            foreach (var fairy in GameObject.FindGameObjectsWithTag("Player Fairy"))
            {
                //foreach(var spell in fairy.GetComponentsInChildren<BattleSpell>())
                for (var i = 0; i < fairy.transform.childCount; i++)
                {
                    if (i % 2 == 0)
                    {
                        fairy.transform.GetChild(i).gameObject.GetComponent<BattleSpell>().Inactive = true;
                    }
                }
            }
        }

        private void UnlockPlayerSpells()
        {
            foreach (var fairy in GameObject.FindGameObjectsWithTag("Player Fairy"))
            {
                for (var i = 0; i < fairy.transform.childCount; i++)
                {
                    if (i % 2 == 0)
                    {
                        fairy.transform.GetChild(i).gameObject.GetComponent<BattleSpell>().Inactive = false;
                    }
                }
            }
        }

        private void EnemyMove()
        {
            ChoiceVictim();
            NewMove("Player");
        }

        private void ChoiceVictim()
        {
            var enemyFairy = randomizer.Next(5);
            var playerFairy = randomizer.Next(5);
            while (player.ActiveFairies[playerFairy].IsDead)
            {
                playerFairy = randomizer.Next(5);
            }

            EventAggregator.OnEnemyAttack(enemyFairy, playerFairy, enemy.ActiveFairies[enemyFairy].Spells[0], "Player");
        }

        private void ChoiceVictim(int effectiveness)
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    var enemyFairy = enemy.ActiveFairies[i];
                    var playerFairy = player.ActiveFairies[j];

                    if (DataOfModels.TableOfEffectiveness[(int)enemyFairy.Element, (int)playerFairy.Element] != effectiveness || playerFairy.IsDead)
                    {
                        continue;
                    }

                    if (randomizer.Next(2) == 0 && enemyFairy.Spells[0] != "Empty Slot")
                    {
                        EventAggregator.OnEnemyAttack(i, j, enemyFairy.Spells[0], "Player");
                        return;
                    }

                    if (enemyFairy.Spells[2] != "Empty Slot")
                    {
                        EventAggregator.OnEnemyAttack(i, j, enemyFairy.Spells[2], "Player");
                        return;
                    }
                }
            }
        }

        private void ReplayAttack(int forwardFairyNumber, int victimFairyNumber, string spellName, Player forward, Player victim)
        {
            var forwardFairy = forward.ActiveFairies[forwardFairyNumber];
            var victimFairy = victim.ActiveFairies[victimFairyNumber];

            /*if (victimFairy.IsDead)
            {
                return;
            }*/
            var spell = DataOfModels.OffensiveSpells[spellName];

            var effectiveness = DataOfModels.TableOfEffectiveness[(int)forwardFairy.Element, (int)victimFairy.Element];
            ShowEffect(effectiveness);

            forwardFairy.Attack(victimFairy, spell);

            ReduceMana(spellName, forwardFairyNumber, forward.Name);
            StartCoroutine(GotoNewMove(victim.Name));
        }

        private void OnFairyAttackEvent(int forwardFairyNumber, int victimFairyNumber, string spellName, string victim)
        {
            Debug.Log(forwardFairyNumber + " " + victimFairyNumber + " " + spellName + " " + victim);
            if (victim == "Player")
            {
                ReplayAttack(forwardFairyNumber, victimFairyNumber, spellName, enemy, player);
            }

            if (victim == "Enemy")
            {
                ReplayAttack(forwardFairyNumber, victimFairyNumber, spellName, player, enemy);
            }
        }

        private void ShowEffect(int effectiveness)
        {
            if (effectiveness == 1)
            {
                ShowEffect("very effective");
            }

            if (effectiveness == -1)
            {
                ShowEffect("inefficient");
            }
        }

        private IEnumerator GotoNewMove(string hero)
        {
            yield return new WaitForSeconds(0.5f);
            NewMove(hero);
        }

        private void ShowEffect(string effect)
        {
            var text = GameObject.FindGameObjectWithTag("Effect").GetComponent<Text>();
            switch (effect)
            {
                case "very effective":
                    text.color = Color.green;
                    break;
                case "inefficient":
                    text.color = Color.red;
                    break;
                default:
                    text.color = Color.white;
                    break;
            }

            text.text = effect;

            StartCoroutine(FadeText(text));
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

        private void ReduceMana(string spell, int fairy, string forward)
        {
            for (var i = 0; i < 4; i++)
            {
                if (forward == "Player")
                {
                    if (PlayerSpells[fairy, i].Name == spell)
                    {
                        PlayerSpells[fairy, i].Mana--;
                    }
                }
                else
                {
                    if (EnemySpells[fairy, i].Name == spell)
                    {
                        EnemySpells[fairy, i].Mana--;
                    }
                }
            }
        }

        private void OnRemoveSpell(int fairyPosition, int spellPosition, string name)
        {
            player.ActiveFairies[fairyPosition].Spells[spellPosition] = "Empty Slot";
        }

        private void OnAddSpell(int fairyPosition, int spellPosition, string name)
        {
            player.ActiveFairies[fairyPosition].Spells[spellPosition] = name;
        }

        private void OnDisableFairy(int position, string name)
        {
            player.ActiveFairies[position] = new Fairy();
        }

        private void OnActivateFairy(int position, string name)
        {
            player.ActiveFairies[position] = (Fairy)DataOfModels.Fairies[name].Clone();
        }
    }
}