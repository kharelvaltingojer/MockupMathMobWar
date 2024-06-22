using System;
using System.Collections;
using Game.Scripts.Projects;
using UnityEngine;

namespace Game.Scripts.Characters
{
    public class EnemyDetector : MonoBehaviour
    {
        public GameObject character;
        public GameObject shooter;
        public float spellCooldown = 1f;
        public float spellTimer = 0f;

        private void OnEnable()
        {
            spellCooldown = UnityEngine.Random.Range(0.9f, 1.2f);
        }

        private void Update()
        {
            spellTimer += Time.deltaTime;
            gameObject.transform.position = character.transform.position;
        }

        private void OnTriggerStay(Collider other)
        {
            try
            {
                if (other.CompareTag("BallShoot"))
                {
                    return;
                }
                if (other.CompareTag("CharacterFOV"))
                {
                    return;
                }
                bool isCharacterLayer = other.gameObject.layer == LayerMask.NameToLayer("Character");
                if (!isCharacterLayer)
                {
                    return;
                }

                StartCoroutine(ProcessTrigger(other));
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        IEnumerator ProcessTrigger(Collider other)
        {
            if (spellTimer < spellCooldown)
            {
                yield break;
            }
            spellTimer = 0f;
            string myTag = character.tag;
            bool amIPlayerOrMiniPlayer = myTag == "Player" || myTag == "MiniPlayer";
            bool isOtherPlayerOrMiniPlayer = other.CompareTag("Player") || other.CompareTag("MiniPlayer");
            bool amIEnemy = myTag == "Enemy";
            bool isOtherEnemy = other.CompareTag("Enemy");

            bool isPlayerToEnemy = amIPlayerOrMiniPlayer && isOtherEnemy;
            bool isEnemyToPlayer = amIEnemy && isOtherPlayerOrMiniPlayer;

            if (isPlayerToEnemy || isEnemyToPlayer)
            {
                shooter.GetComponent<ShooterController>().ShootTo(other.gameObject);
            }

            if (isPlayerToEnemy)
            {
                Debug.Log("Player detected enemy");
            }
            else if (isEnemyToPlayer)
            {
                Debug.Log("Enemy detected player");
            }

            yield return null;
        }
    }
}
