using UnityEngine;

namespace Game.Scripts.Characters
{
    public class HealthManager : MonoBehaviour
    {
        public TMPro.TextMeshProUGUI text;
        public UnityEngine.UI.Slider slider;
        public int maxHealth = 10;
        public int currentHealth = 10;
        public bool redirectDamageToMinions = false;
        public string minionTag = "MiniPlayer";
        public int baseHealth = 10;
        public bool isPlayer = false;
    
        private void Start()
        {
            UpdateHealthUI();
        }
    
        private void UpdateHealthUI()
        {
            // text.text = $"{currentHealth}/{maxHealth}";
            text.text = $"{maxHealth}";
            slider.maxValue = maxHealth;
            slider.value = currentHealth;
        }
    
        public void RefreshHealthWithMinions()
        {
            if (!redirectDamageToMinions)
            {
                if (GetMinionsCount() > 0)
                {
                    currentHealth = baseHealth + GetMinionsCount();
                }
            }
        
            UpdateHealthUI();
        }
    
        private int GetMinionsCount()
        {
            return GameObject.FindGameObjectsWithTag(minionTag).Length;
        }
    
        public void SetMaxHealth(int value)
        {
            maxHealth = value;
            slider.maxValue = maxHealth;
            UpdateHealthUI();
        }
    
        public void SetCurrentHealth(int value)
        {
            currentHealth = value;
            slider.value = currentHealth;
            UpdateHealthUI();
        }
    
        public void AddMaxHealth(int value)
        {
            maxHealth += value;
            slider.maxValue = maxHealth;
            UpdateHealthUI();
        }
    
        public void AddCurrentHealth(int value)
        {
            currentHealth += value;
            slider.value = currentHealth;
            UpdateHealthUI();
        }
    
        public void TakeDamage(int damage)
        {
            if (redirectDamageToMinions)
            {
                GameObject firstMinion = GameObject.FindGameObjectWithTag(minionTag);
                if (firstMinion != null)
                {
                    firstMinion.GetComponent<HealthManager>().TakeDamage(damage);
                }
                else
                {
                    DamageSelf(damage);
                }
            }
            else
            {
                DamageSelf(damage);
            }
        
            UpdateHealthUI();
        }

        private void DamageSelf(int damage)
        {
            currentHealth *= 100;
            currentHealth -= damage;
            currentHealth /= 100;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                if (isPlayer)
                {
                    GameController.IsDead = true;
                }
                else
                {
                    if (gameObject != null)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
