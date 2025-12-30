using System;

namespace Features.ScaleOrderDebuff.Script
{
    public class ScaleSpellSystem
    {
        private readonly int _maxValue = 50;
        private int _balance;
        private bool _isOverloaded;

        public event Action<int> BalanceChanged;
        public event Action<bool> OverloadTriggered;

        public void ApplySpell(bool isChaos, int cost)
        {
            int delta = isChaos ? cost : -cost;
            _balance = Math.Clamp(_balance + delta, -_maxValue, _maxValue);

            BalanceChanged?.Invoke(_balance);

            
            CheckOverload();
        }

        private void CheckOverload()
        {
            if (_isOverloaded) return;

            if (_balance == _maxValue)
            {
                _isOverloaded = true;
                OverloadTriggered?.Invoke(false);
            }
            else if (_balance == -_maxValue)
            {
                _isOverloaded = true;
                OverloadTriggered?.Invoke(true);
            }
        }
        

        public void ResetOverload()
        {
            _isOverloaded = false;
            _balance = 0;
            BalanceChanged?.Invoke(_balance);
        }
    }
}