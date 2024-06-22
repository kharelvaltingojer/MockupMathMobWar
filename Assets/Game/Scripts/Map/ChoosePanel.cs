using UnityEngine;

namespace Game.Scripts.Map
{
    public class ChoosePanel : MonoBehaviour
    {
        private GameObject _panel1;
        private GameObject _panel2;

        private void Awake()
        {
            GetChildPanel();
            DisableAllPanel();
            RandomEnablePanel();
        }
    
        void GetChildPanel()
        {
            _panel1 = transform.GetChild(0).gameObject;
            _panel2 = transform.GetChild(1).gameObject;
        }
    
        void DisableAllPanel()
        {
            _panel1.SetActive(false);
            _panel2.SetActive(false);
        }
    
        void RandomEnablePanel()
        {
            System.Random random = new System.Random();
            int randomValue = random.Next(0, 2);
        
            if (randomValue == 0)
            {
                _panel1.SetActive(true);
            }
            else
            {
                _panel2.SetActive(true);
            }
        }
    }
}
