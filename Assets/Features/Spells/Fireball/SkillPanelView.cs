using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Spells.Fireball
{
    public class SkillPanelView : MonoBehaviour
    {
        [SerializeField] private Image _iconSkillFillCooldown;
        [SerializeField] private Image _frameAutoCastImage;
        [SerializeField] private TextMeshProUGUI _textSkill;

        private SpellStateBase _state;

        private void Awake()
        {
            HideCooldownView();
        }

        public void UseSpell(SpellStateBase state)
        {
            _state = state;
            ShowCooldownView();
            UpdateView();
        }

        private void Update()
        {
            if (!_state) return;

            if (_state.Cooldown <= 0f)
            {
                HideCooldownView();
                _state = null;
                return;
            }

            UpdateView();
        }

        private void UpdateView()
        {
            SetAutocastImage();
            float normalized = _state.Cooldown / _state.MaxCooldown;
            _iconSkillFillCooldown.fillAmount = normalized;
            _textSkill.text = Mathf.Ceil(_state.Cooldown).ToString();
        }

        private void ShowCooldownView()
        {
            _iconSkillFillCooldown.enabled = true;
            _textSkill.enabled = true;
        }

        private void HideCooldownView()
        {
            _iconSkillFillCooldown.enabled = false;
            _textSkill.enabled = false;
        }

        private void SetAutocastImage()
        {
            _frameAutoCastImage.enabled = true;
        }
    }
}