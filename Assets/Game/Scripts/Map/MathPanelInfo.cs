using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Scripts.Characters;
using Game.Scripts.Utils;
using UnityEngine;

namespace Game.Scripts.Map
{
    public class MathPanelInfo : MonoBehaviour
    {
        [SerializeField] private GameObject[] _mathPanels;
        private IList<GameObject> _activePanels = new List<GameObject>();
        private GameObject _panel1Go;
        private GameObject _panel2Go;
        private MathPanelRandomOperation _panel1Operation;
        private MathPanelRandomOperation _panel2Operation;
        private IList<MathPanelRandomOperation> _buffs = new List<MathPanelRandomOperation>();
        private MathPanelRandomOperation _bestBuff;
        private MathPanelRandomOperation _worstBuff;
        private bool _isReady = false;
        private int _mostExpectedMinions = 0;
        private int _lastStepMinions = 0;
        private int _mostSpawnablePower = 0;

        void Start()
        {
            SetupActivePanels();
        }

        private void Update()
        {
            if (_activePanels.Count != 2)
            {
                SetupActivePanels();
            }
        }

        private async void SetupActivePanels()
        {
            foreach (var panel in _mathPanels)
            {
                if (panel.activeSelf)
                {
                    _activePanels.Add(panel);
                }
            }

            if (_activePanels.Count == 2)
            {
                _panel1Go = _activePanels[0];
                _panel2Go = _activePanels[1];

                _panel1Operation = _panel1Go.GetComponent<MathPanelRandomOperation>();
                _panel2Operation = _panel2Go.GetComponent<MathPanelRandomOperation>();
            
                await GetTheLastStepMinions();

                ClassifyBuffs();
                CalculateMostExpectedMinions();
                LifeManager.SetMathPanelInfo(gameObject.name, this, _mostExpectedMinions);
                _isReady = true;
            }
        }
    
        public bool IsReady()
        {
            return _isReady;
        }
    
        public MathPanelRandomOperation GetBestBuffs()
        {
            return _bestBuff;
        }

        private void ClassifyBuffs()
        {
            if (_panel1Operation == null || _panel2Operation == null)
            {
                return;
            }
        
            _buffs.Add(_panel1Operation);
            _buffs.Add(_panel2Operation);
        
            int simulation1 = MathPathHelper.GetResult(_panel1Operation.GetOperation(), _panel1Operation.GetOperand(), _lastStepMinions);
            int simulation2 = MathPathHelper.GetResult(_panel2Operation.GetOperation(), _panel2Operation.GetOperand(), _lastStepMinions);
        
            while (simulation1 == simulation2 /*|| _panel1Operation.GetOperation() == _panel2Operation.GetOperation()*/)
            {
                _panel1Operation.Setup();
            
                simulation1 = MathPathHelper.GetResult(_panel1Operation.GetOperation(), _panel1Operation.GetOperand(), _lastStepMinions);
            }
        
            _bestBuff = _panel2Operation;
            _worstBuff = _panel1Operation;
        
            if (simulation1 > simulation2)
            {
                _bestBuff = _panel1Operation;
                _worstBuff = _panel2Operation;
            }
        }
    
    
        private async Task GetTheLastStepMinions()
        {
            int currentStep = int.Parse(gameObject.name.Split('-')[1]);
            int lastStep = currentStep - 1;
            int lastStepMinions = 0;
        
            if (lastStep == 0)
            {
                _lastStepMinions = lastStepMinions;
            }
            else
            {
                GameObject lastMathPanel = GameObject.Find($"{gameObject.name.Split('-')[0]}-{lastStep}");
                MathPanelInfo lastMathPanelInfo = lastMathPanel.GetComponent<MathPanelInfo>();
                bool isReady = false;
                while (!isReady)
                {
                    await Task.Delay(100);
                    isReady = lastMathPanelInfo.IsReady();
                }
            
                lastStepMinions = lastMathPanelInfo.GetMostExpectedMinions();
                int lastStepSpawnablePower = lastMathPanelInfo.GetMostSpawnablePower();
                _lastStepMinions = lastStepMinions - lastStepSpawnablePower;
            }
        }

        private void CalculateMostExpectedMinions()
        {
            var buff = GetBestBuffs();
            var operation = buff.GetOperation();
            var operand = buff.GetOperand();
        
            var minions = MathPathHelper.GetResult(operation, operand, _lastStepMinions) + _lastStepMinions;
            _mostExpectedMinions = minions;
            if (minions > 100)
            {
                _mostSpawnablePower = minions - 30;
            }
            else if (minions > 10)
            {
                _mostSpawnablePower = minions - (minions / 4);
            }
            else
            {
                _mostSpawnablePower = minions + Constants.MinionsGiftNumber;
            }
        }
    
        public int GetMostExpectedMinions()
        {
            return _mostExpectedMinions;
        }
    
        public int GetMostSpawnablePower()
        {
            return _mostSpawnablePower;
        }
    
        public IDictionary<MathPanelRandomOperation.Operation, int> GetOperations()
        {
            var operations = new Dictionary<MathPanelRandomOperation.Operation, int>();
            operations.Add(_panel1Operation.GetOperation(), _panel1Operation.GetOperand());
            operations.Add(_panel2Operation.GetOperation(), _panel2Operation.GetOperand());
            return operations;
        }

    }
}


