using UnityEngine;

namespace Features.AbstractMinion.Script
{
    public class CanvasMinionSystem : MonoBehaviour
    {
        private GameObject _minionPrefab;
        private IHealth _health;
        private Camera _cameraMain;
        public void Init(GameObject minionPrefab, IHealth health, Camera cameraMain)
        {
            _minionPrefab = minionPrefab;
            _health = health;
            _cameraMain = cameraMain;
            transform.localRotation = _cameraMain.transform.rotation;
            if (_health != null) _health.HealthOver += OnDeath;
            UpdateCanvasPos();
        }
        private void OnDeath()
        {
            if (_health != null) _health.HealthOver -= OnDeath;
            gameObject.SetActive(false);
        }
        public void UpdateCanvasPos()
        {
            if (!_minionPrefab) return;
            transform.position = _minionPrefab.transform.position;
        }
    }
}