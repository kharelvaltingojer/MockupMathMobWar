using System;
using System.Collections.Generic;
using Game.Scripts.Map;
using UnityEngine;

namespace Game.Scripts.Characters
{
    public class LifeManager : MonoBehaviour
    {
        private static LifeManager Instance;
        public TMPro.TextMeshProUGUI text;
        public UnityEngine.UI.Slider slider;

        private static int _maxLife = 10;
        private static int _currentLife = 10;
    
        private IDictionary<string, MathPanelInfo> _mathPanelInfo = new Dictionary<string, MathPanelInfo>();
    
    
        private void Awake()
        {
            // check if there is already an instance of LifeManager
            if (Instance == null)
            {
                // if not, set the instance to this
                Instance = this;
            }
            else if (Instance != this)
            {
                // if there is already an instance of LifeManager, destroy this
                Destroy(gameObject);
            }
        }
    
        private void Start()
        {
            UpdateLifeUI();
        }

        private static void UpdateLifeUI()
        {
            Instance.text.text = $"{_currentLife}/{_maxLife}";
            Instance.slider.maxValue = _maxLife;
            Instance.slider.value = _currentLife;
        }
    
        public static void SetMaxLife(int value)
        {
            _maxLife = value;
            Instance.slider.maxValue = _maxLife;
            UpdateLifeUI();
        }
    
        public static void SetCurrentLife(int value)
        {
            _currentLife = value;
            Instance.slider.value = _currentLife;
            UpdateLifeUI();
        }
    
        public static void AddMaxLife(int value)
        {
            _maxLife += value;
            Instance.slider.maxValue = _maxLife;
            UpdateLifeUI();
        }
    
        public static void AddCurrentLife(int value)
        {
            _currentLife += value;
            UpdateLifeUI();
        }
    
        public static void SetMathPanelInfo(string key, MathPanelInfo value, int mostExpectedMinions)
        {
            if (Instance._mathPanelInfo.ContainsKey(key))
            {
                Instance._mathPanelInfo[key] = value;
            }
            else
            {
                Instance._mathPanelInfo.Add(key, value);
            }
        
            _maxLife = Math.Max(_maxLife, mostExpectedMinions);
            UpdateLifeUI();
        }
    
    
    }
}
