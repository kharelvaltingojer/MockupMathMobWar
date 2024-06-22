using System;
using Game.Scripts.Characters;
using UnityEngine;

namespace Game.Scripts.Map
{
    // require script MathPanelRandomOperation at te same GameObject
    public class MathPanelTrigger : MonoBehaviour
    {
        public GameObject mathPanels;
        MathPanelRandomOperation _mathPanelRandomOperation;
        
        
        
        private void OnTriggerEnter(Collider other)
        {
            _mathPanelRandomOperation = gameObject.GetComponent<MathPanelRandomOperation>();
            if (other.CompareTag("Player"))
            {
                MathPanelRandomOperation.Operation operation = _mathPanelRandomOperation.GetOperation();
                int operand = _mathPanelRandomOperation.GetOperand();
                
                PlayerMiniSpawner playerMiniSpawner = other.GetComponent<PlayerMiniSpawner>();
                PlayerData playerData = other.GetComponent<PlayerController>().playerData;
                int currentSpawns = playerData.spawns;

                int result = MathPathHelper.GetResult(operation, operand, currentSpawns);
                
                if (result > 0)
                {
                    playerMiniSpawner.SpawnPlayerMini(result);
                }
                else if (result < 0)
                {
                    playerMiniSpawner.KillPlayersMini(Mathf.Abs(result));
                }
                var before = playerData.spawns;
                playerData.spawns += result;
                playerData.spawns = Math.Max(playerData.spawns, 0);
                
                mathPanels.SetActive(false);
            }
        }
    }
}
