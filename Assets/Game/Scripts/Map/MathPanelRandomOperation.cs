using UnityEngine;

namespace Game.Scripts.Map
{
    public class MathPanelRandomOperation : MonoBehaviour
    {
        public enum Operation
        {
            Addition = 0,
            Subtraction = 1,
            Multiplication = 2,
            Division = 3
        }

        readonly int _min = 1;
        readonly int _maxSumSub = 50;
        readonly int _maxMulDiv = 5;
        
        private Operation _operation;
        private int _operand;
        TMPro.TextMeshProUGUI _text;
        [SerializeField] private bool isSumOnly;


        private void Awake()
        {
            FindTMProInChildrenTree();
            Setup();
        }
        
        public void Setup()
        {
            SetOperation();
            SetOperand();
            SetText();
        }
        
        void SetOperation()
        {
            if (isSumOnly)
            {
                _operation = Operation.Addition;
            }
            else
            {
                _operation = (Operation)Random.Range(0, 4);
            }
        }
        
        void SetOperand()
        {
            if (_operation == Operation.Addition || _operation == Operation.Subtraction)
            {
                _operand = Random.Range(_min, _maxSumSub);
            }
            else
            {
                _operand = Random.Range(_min, _maxMulDiv);
            }
        }
        
        void FindTMProInChildrenTree()
        {
            _text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        }
        
        string GetCharacterFromOperation()
        {
            string result = "";
            switch (_operation)
            {
                case Operation.Addition:
                    result = "+";
                    break;
                case Operation.Subtraction:
                    result = "-";
                    break;
                case Operation.Multiplication:
                    result = "*";
                    break;
                case Operation.Division:
                    result = "÷";
                    break;
            }
            return result;
        }
        
        void SetText()
        {
            string operand = _operand.ToString();
            string operation = GetCharacterFromOperation();
            _text.text = $"{operation}\n{operand}";
        }
        
        
        
        public Operation GetOperation()
        {
            return _operation;
        }
        
        public int GetOperand()
        {
            return _operand;
        }
        
    }
}