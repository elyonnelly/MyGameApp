using System;
using System.Collections;
using System.Collections.Generic;
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

        private void Awake()
        {
            player = GameDataManager.Instance.PlayerData;
            player.Name = "Player";
            enemy = GameDataManager.Instance.EnemyData;
            enemy.Name = "Enemy";
            FillSpellInfo(player, PlayerSpells);
            FillSpellInfo(enemy, EnemySpells);

            EventAggregator.DisableFairy += OnDisableFairy;
            EventAggregator.ActivateFairy += OnActivateFairy;
            EventAggregator.RemoveSpell += OnRemoveSpell;
            EventAggregator.AddSpell += OnAddSpell;
            EventAggregator.FairyAttack += OnFairyAttackEvent;
            EventAggregator.EnemyAttack += OnFairyAttackEvent;
        }

        private void OnDisable()
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


        private void VictoryCheck(Player hero)
        {
            var count = 0;
            foreach (var fairy in hero.ActiveFairies)
            {
                if (fairy.IsDead)
                {
                    count++;
                }
            }

            if (count != 5)
            {
                count = 0;
                if (hero.Name == "Player")
                {
                    foreach (var spell in PlayerSpells)
                    {
                        if (spell.Mana <= 0)
                        {
                            count++;
                        }
                    }
                }
                else
                {
                    foreach (var spell in EnemySpells)
                    {
                        if (spell.Mana <= 0)
                        {
                            count++;
                        }
                    }
                }
                if (count != 5)
                {
                    return;
                }
            }

            var winner = hero.Name == "Player" ? "Enemy" : "Player";
            switch (winner)
            {
                case "Player":
                    RecoverFairy();
                    GiveAwards();
                    SceneManager.LoadScene("Winner Scene");
                    break;
                case "Enemy":
                    RecoverFairy();
                    SceneManager.LoadScene("Losing Scene");
                    break;
            }

            EventAggregator.OnVictoryInBattle(winner);
        }

        private void RecoverFairy()
        {
            foreach (var fairy in player.ActiveFairies)
            {
                fairy.HealthPoint = 11 + fairy.HitPoints * 8 + fairy.Level * fairy.HitPoints * 2;
            }
        }

        private void GiveAwards()
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
            }

            if (randomizer.Next(2) == 0 && player.AllowFairies.Count < 70)
            {
                var fairies = new Fairy[DataOfModels.Fairies.Count];
                DataOfModels.Fairies.Values.CopyTo(fairies, 0);
                player.AllowFairies.Add((Fairy)fairies[player.AllowFairies.Count].Clone());
            }
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
                //foreach(var spell in fairy.GetComponentsInChildren<BattleOffensiveSpellController>())
                for (var i = 0; i < fairy.transform.childCount; i++)
                {
                    if (i % 2 == 0)
                    {
                        fairy.transform.GetChild(i).gameObject.GetComponent<BattleOffensiveSpellController>().Inactive = true;
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
                        fairy.transform.GetChild(i).gameObject.GetComponent<BattleOffensiveSpellController>().Inactive = false;
                    }
                }
            }
        }

        private void EnemyMove()
        {
            var newNumber = randomizer.Next(10);

            if (newNumber < 4)
            {
                ChoiceVictim(1);
            }
            else if (newNumber >= 4 && newNumber < 7)
            {
                ChoiceVictim(0);
            }
            else if (newNumber >= 7 && newNumber < 8)
            {
                ChoiceVictim(-1);
            }
            else
            {
                var fNum = randomizer.Next(5);
                EventAggregator.OnEnemyAttack(fNum, randomizer.Next(5), enemy.ActiveFairies[fNum].Spells[0] != "EmptySlot" ? enemy.ActiveFairies[fNum].Spells[0] : enemy.ActiveFairies[fNum].Spells[2], "Player");

            }


            NewMove("Player");
        }

        private bool ChoiceVictim(int effectiveness)
        {
            var i = 0;
            var j = 0;
            var attacks = new List<Attack>();

            foreach (var enemyFairy in enemy.ActiveFairies)
            {
                j = 0;
                foreach (var playerFairy in player.ActiveFairies)
                {
                    if (DataOfModels.TableOfEffectiveness[(int)enemyFairy.Element, (int)playerFairy.Element] == effectiveness)
                    {
                        if (enemyFairy.Spells[0] != "Empty Slot" && EnemySpells[i, 0].Mana > 0)
                        {
                            attacks.Add(new Attack(i, j, EnemySpells[i, 0]));
                        }

                        if (enemyFairy.Spells[2] != "Empty Slot" && EnemySpells[i, 2].Mana > 0)
                        {
                            attacks.Add(new Attack(i, j, EnemySpells[i, 2]));
                        }
                    }

                    j++;
                }

                i++;
            }

            var choiceAttack = new Attack();
            var newNumber = randomizer.Next(10);
            if (newNumber < 4)
            {
                var damage = 0.0;
                foreach (var attack in attacks)
                {
                    if (DataOfModels.OffensiveSpells[attack.Spell.Name].Damage * enemy.ActiveFairies[attack.Forward].LevelCoefficient > damage)
                    {
                        damage = DataOfModels.OffensiveSpells[attack.Spell.Name].Damage * enemy.ActiveFairies[attack.Forward].LevelCoefficient;
                        choiceAttack = attack;
                    }
                }
            }
            else if (newNumber >= 4 && newNumber < 7)
            {
                var maxHP = 0.0;
                foreach (var attack in attacks)
                {
                    if (player.ActiveFairies[attack.Victim].HealthPoint > maxHP)
                    {
                        maxHP = player.ActiveFairies[attack.Victim].HealthPoint;
                        choiceAttack = attack;
                    }
                }
            }
            else if (newNumber >= 7 && newNumber < 9)
            {
                var minHP = 30.0;
                foreach (var attack in attacks)
                {
                    if (player.ActiveFairies[attack.Victim].HealthPoint < minHP)
                    {
                        minHP = player.ActiveFairies[attack.Victim].HealthPoint;
                        choiceAttack = attack;
                    }
                }
            }
            else
            {
                choiceAttack = attacks[randomizer.Next(attacks.Count)];
            }

            if (choiceAttack.WasChoice)
            {
                EventAggregator.OnEnemyAttack(choiceAttack.Forward, choiceAttack.Victim, choiceAttack.Spell.Name, "Player");
                return true;
            }

            return false;
        }

        private void CastDefensiveSpells(Fairy fairy, Spell spell1, Spell spell2)
        {
            if (spell1.Mana > 0)
            {
                var defensiveSpellI = DataOfModels.DefensiveSpells[spell1.Name];
                Effects.SetOfEffects[Math.Min((int)defensiveSpellI.Effect, 13)](fairy, fairy, defensiveSpellI);
            }

            if (spell2.Mana > 0)
            {
                var defensiveSpellII = DataOfModels.DefensiveSpells[spell2.Name];
                Effects.SetOfEffects[Math.Min((int)defensiveSpellII.Effect, 13)](fairy, fairy, defensiveSpellII);
            }
        }

        private void ReplayAttack(int forwardFairyNumber, int victimFairyNumber, string spellName, Player forward, Player victim)
        {
            var forwardFairy = forward.ActiveFairies[forwardFairyNumber];
            var victimFairy = victim.ActiveFairies[victimFairyNumber];

            if (victimFairy.IsDead)
            {
                return;
            }

            if (forward.Name == "Player")
            {
                CastDefensiveSpells(forwardFairy, PlayerSpells[forwardFairyNumber, 1], PlayerSpells[forwardFairyNumber, 3]);
            }
            else
            {
                CastDefensiveSpells(forwardFairy, EnemySpells[forwardFairyNumber, 1], EnemySpells[forwardFairyNumber, 3]);
            }

            var spell = DataOfModels.OffensiveSpells[spellName];

            var effectiveness = DataOfModels.TableOfEffectiveness[(int)forwardFairy.Element, (int)victimFairy.Element];
            ShowEffect(effectiveness);

            forwardFairy.Attack(victimFairy, spell);

            if (victim.Name == "Player")
            {
                CastDefensiveSpells(victimFairy, PlayerSpells[forwardFairyNumber, 1], PlayerSpells[forwardFairyNumber, 3]);
            }
            else
            {
                CastDefensiveSpells(victimFairy, EnemySpells[forwardFairyNumber, 1], EnemySpells[forwardFairyNumber, 3]);
            }

            ReduceMana(spellName, forwardFairyNumber, forward.Name);
            ReduceMana(victimFairy.Spells[1], victimFairyNumber, victim.Name);
            ReduceMana(victimFairy.Spells[3], victimFairyNumber, victim.Name);
            StartCoroutine(GotoNewMove(victim.Name));
        }


        private void OnFairyAttackEvent(int forwardFairyNumber, int victimFairyNumber, string spellName, string victim)
        {
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
                    if (PlayerSpells[fairy, i] != null && PlayerSpells[fairy, i].Name == spell)
                    {
                        PlayerSpells[fairy, i].Mana = Math.Max(PlayerSpells[fairy, i].Mana - 1, 0);
                    }
                }
                else
                {
                    if (EnemySpells[fairy, i] != null && EnemySpells[fairy, i].Name == spell)
                    {
                        EnemySpells[fairy, i].Mana = Math.Max(EnemySpells[fairy, i].Mana - 1, 0);
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