using System;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Hero.HeroStats.HeroHP
{
    public class HpBarView : MonoBehaviour
    {
        public Slider SliderHP;
        public void SetValue(float current, float max) =>
        SliderHP.value = (current/max)*max;
        
        private Camera _camera;

        private void OnEnable()
        {
            _camera = Camera.main;
        }

        private void LateUpdate()
        {
            transform.LookAt(new Vector3(transform.position.x, _camera.transform.position.y, _camera.transform.position.z));
            transform.Rotate(0, 180, 0);
        }
    }
}