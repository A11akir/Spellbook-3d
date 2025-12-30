using DG.Tweening;
using Features.Spells.Fireball;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.Hero
{
    public class SilenceHeroSystem : MonoBehaviour
    {
        [Inject] private SpellPanelsView _spellPanelsView;
        [SerializeField] private RectTransform _silenceImage;
        [SerializeField] private GameObject _imageBook;

        public void ActivateSilence()
        {
            _spellPanelsView.SilenceViewActivate();
        }

        public void DeactivateSilence()
        {
            _spellPanelsView.SilenceViewInactivate();
        }

        public void SielenceSpellFeedback()
        {
            _silenceImage.gameObject.SetActive(true);
            _imageBook.SetActive(false);

            _silenceImage.DOShakeAnchorPos(0.5f, 15, 10)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    DOVirtual.DelayedCall(2f, () =>
                    {
                        _silenceImage.gameObject.SetActive(false);
                        _imageBook.SetActive(true);
                    });
                });
        }
    }
}