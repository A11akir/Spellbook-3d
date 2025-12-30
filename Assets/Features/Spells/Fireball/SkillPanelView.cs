using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Spells.Fireball
{
    public class SkillPanelView : MonoBehaviour
    {
        [SerializeField] private Image _iconSkillFillCooldown;
        [SerializeField] private Image _frameAutoCastImage;
        [SerializeField] private GameObject _frameSilenceImage;
        [SerializeField] private TextMeshProUGUI _textSkill;
        
        private bool _isAutocastEnabled = true;

        private SpellStateBase _state;

        private void Awake()
        {
            HideCooldownView();
        }

        public void UseSpell(SpellStateBase state)
        {
            _state = state;
            ShowCooldownView();
            SetAutocastImage();
            UpdateView();
        }

        public void TickSkillPanel()
        {
            if (!IsStateValid())
                return;

            UpdateView();
        }

        private bool IsStateValid()
        {
            if (!_state)
                return false;

            if (_state.Cooldown > 0f)
                return true;

            HideCooldownView();
            _state = null;
            return false;
        }

        private void UpdateView()
        {
            float normalized = _state.Cooldown / _state.MaxCooldown;
            _iconSkillFillCooldown.fillAmount = normalized;
            _textSkill.text = Mathf.Ceil(_state.Cooldown).ToString();
        }

        private void ShowCooldownView()
        {
            _iconSkillFillCooldown.enabled = true;
            _textSkill.enabled = true;
        }

        public void HideCooldownView()
        {
            _iconSkillFillCooldown.enabled = false;
            _textSkill.enabled = false;
        }

        private void SetAutocastImage()
        {
            if (_isAutocastEnabled) 
                _frameAutoCastImage.enabled = true;
        }

        public void HideAutocastImage()
        {
            _frameAutoCastImage.enabled = false;
        }

        public void ActivateSilenceView()
        {
            _frameSilenceImage.SetActive(true);
        }
        public void InactivateSilenceView()
        {
            _frameSilenceImage.SetActive(false);
        }

        public void DisableAutocastImage()
        {
           _isAutocastEnabled = false; 
        }

        public void EnableAutocastImage()
        {
           _isAutocastEnabled = true; 
        }
    }
}