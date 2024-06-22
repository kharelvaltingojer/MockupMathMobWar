using System.Collections;
using Game.Scripts.Characters;
using Game.Scripts.enums;
using Game.Scripts.utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Map
{
    public class SpawnerManager : MonoBehaviour
    {
        [SerializeField] private bool isBoss = false;
        [SerializeField] private MathPanelInfo mathPanelInfo;
        private GameObject _mathPanel;
        
        [SerializeField] private int availablePower = 0;
        [SerializeField] private GameObject spawnCommon;
        [SerializeField] private GameObject spawnNormal;
        [SerializeField] private GameObject spawnRare;
        [SerializeField] private GameObject spawnEpic;
        [SerializeField] private GameObject spawnMiniBoss;
        [SerializeField] private GameObject spawnBoss;
        [SerializeField] private GameObject spawnPrize;

        private void OnEnable()
        {
            RenameMe();
            FixMyPosition();
            StartCoroutine(CheckIfReadyToSpawn());
        }
        private void OnValidate()
        {
            RenameMe();
            FixMyPosition();
        }

        private void RenameMe()
        {
            if (mathPanelInfo != null)
            {
                _mathPanel = mathPanelInfo.gameObject;
            }
            
            if (_mathPanel != null)
            {
                gameObject.name = $"{GetType().Name}-{mathPanelInfo.gameObject.name}";
            }
            else
            {
                gameObject.name = $"{GetType().Name}-not-assigned-to-math-panel";
            }
        }
        private void FixMyPosition()
        {
            if (_mathPanel != null)
            {
                transform.position = new Vector3(_mathPanel.transform.position.x, transform.position.y, _mathPanel.transform.position.z + 50);
                transform.rotation = _mathPanel.transform.rotation;
            }
        }
        
        private IEnumerator CheckIfReadyToSpawn()
        {
            while (!mathPanelInfo.IsReady())
            {
                yield return new WaitForSeconds(1);
            }
            Spawn();
        }

        private void Spawn()
        {
            availablePower = mathPanelInfo.GetMostSpawnablePower();
            int localAvailablePower = availablePower;

            if (isBoss)
            {
                localAvailablePower /= 2;
                BossSpawn(localAvailablePower);
                CommonSpawn(localAvailablePower);
            }
            else
            {
                CommonSpawn(localAvailablePower);
            }
        }
        
        private void CommonSpawn(int localAvailablePower)
        {
            if (availablePower < 10 && !isBoss)
            {
                Instantiate(spawnPrize, new Vector3(transform.position.x, 0, transform.position.z), transform.rotation);
                return;
            }
            else
            {
                while (localAvailablePower > 0)
                {
                    CharacterPowerEnum characterToSpawn =
                        CharacterPowerUtils.RandomFromCommonToPowerful(localAvailablePower);
                    int totalPower = CharacterPowerUtils.GetTotalPower(characterToSpawn);
                    localAvailablePower -= totalPower;
                    
                    GameObject spawn = null;
                    switch (characterToSpawn)
                    {
                        case CharacterPowerEnum.common:
                            spawn = spawnCommon;
                            break;
                        case CharacterPowerEnum.normal:
                            spawn = spawnNormal;
                            break;
                        case CharacterPowerEnum.rare:
                            spawn = spawnRare;
                            break;
                        case CharacterPowerEnum.epic:
                            spawn = spawnEpic;
                            break;
                        case CharacterPowerEnum.miniBoss:
                            spawn = spawnMiniBoss;
                            break;
                        case CharacterPowerEnum.boss:
                            spawn = spawnBoss;
                            break;
                    }
                    
                    if (spawn != null)
                    {
                        var randomXVariant = Random.Range(-4f, 4f); 
                        var randomZVariant = Random.Range(-5f, 5f);
                        var randomPosition = new Vector3(transform.position.x + randomXVariant, transform.position.y, transform.position.z + randomZVariant);
                        Instantiate(spawn, randomPosition, transform.rotation);
                    }
                }
            }
        }
        
        private void BossSpawn(int withPower)
        {
            GameObject boss = Instantiate(spawnBoss, transform.position, transform.rotation);
            HealthManager hm = boss.GetComponent<HealthManager>();
            hm.SetCurrentHealth(withPower);
            hm.SetMaxHealth(withPower);
        }
    }
}
