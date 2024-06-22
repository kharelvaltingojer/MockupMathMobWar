using Game.Scripts.Characters;
using Game.Scripts.Enums;
using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

namespace Game.Scripts.Projects
{
    public class ProjectTrigger : MonoBehaviour
    {
        public readonly static int DELAYED_DESTROY = 5;

        public ProjectElement element = ProjectElement.Stone;
        public int baseDamage = 100;
        public int level = 1;
        private readonly int _fireTickCount = 3;
        public Source source = Source.Unknown;
        
        public enum Source
        {
            Unknown,
            Player,
            Enemy
        }

        private void OnTriggerEnter(Collider other)
        {
            try {
                if (other.CompareTag("BallShoot"))
                {
                    return;
                }
                if (other.CompareTag("CharacterFOV"))
                {
                    return;
                }
                bool isCharacterLayer = other.gameObject.layer == LayerMask.NameToLayer("Character");
                if (isCharacterLayer)
                {
                    bool isPlayerToPlayer = source == Source.Player && other.CompareTag("Player");
                    bool isPlayerToMiniPlayer = source == Source.Player && other.CompareTag("MiniPlayer");
                    bool isEnemyToEnemy = source == Source.Enemy && other.CompareTag("Enemy");

                    if (isPlayerToPlayer || isPlayerToMiniPlayer || isEnemyToEnemy)
                    {
                        return;
                    }
                    
                    GetComponent<Collider>().enabled = false;

                    GameObject character = other.gameObject;
                    HealthManager healthManager = character.GetComponent<HealthManager>();

                    int damage = CalculateDamage(baseDamage, level, element);

                    healthManager.TakeDamage(damage);

                    if (element == ProjectElement.Fire)
                    {
                        StartCoroutine(ApplyPeriodicDamage(healthManager, (float)damage * 0.1f, _fireTickCount));
                    }
                    else if (element == ProjectElement.Ice)
                    {
                        PlayerController playerController = character.GetComponent<PlayerController>();
                        if (playerController != null)
                        {
                            StartCoroutine(ApplyIceEffect(playerController, 4, 0.5f));
                        }
                    }
                }

                StartCoroutine(DeactivateAndDestroy());
            } catch (System.Exception e) {
                Debug.LogError($"Error in ProjectTrigger {gameObject.name} x {other.name}: " + e.Message);
            }
        }

        private int CalculateDamage(int bDamage, int level, ProjectElement elem)
        {
            int damage = bDamage * level;
            ProjectElementDamage projectElementDamage = new ProjectElementDamage(elem);

            damage += Mathf.FloorToInt(projectElementDamage.Damage());

            return damage;
        }

        private IEnumerator ApplyPeriodicDamage(HealthManager healthManager, float tickDamage, int tickCount)
        {
            for (int i = 0; i < tickCount; i++)
            {
                yield return new WaitForSeconds(1);
                Debug.Log("Fire tick: Brun! Burn! Burn to ashes, Burn to the ground! And fall to the hell!");
                healthManager.TakeDamage((int)tickDamage);
            }
        }
        
        private IEnumerator ApplyIceEffect(PlayerController playerController, float duration, float speedReductionFactor)
        {
            float originalSpeed = playerController.playerSpeed;
            playerController.playerSpeed *= speedReductionFactor;
            Debug.Log("Ice effect: Chill down bitch!");
            yield return new WaitForSeconds(duration);
            Debug.Log("Ice effect out");
            playerController.playerSpeed = originalSpeed;
        }
        
        private IEnumerator DeactivateAndDestroy()
        {
            // gameObject.SetActive(false);
            yield return new WaitForSeconds(DELAYED_DESTROY);
            Destroy(gameObject);
        }

    }
}
