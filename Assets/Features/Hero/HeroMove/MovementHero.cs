using System.Collections;
using System.Collections.Generic;
using Features.Input.Scripts;
using Features.Hero.HeroAnimator;
using Features.Hero.HeroStats.HeroHP;
using Features.Spells.Fireball;
using UnityEngine;
using Zenject;

namespace Features.Hero.HeroMove
{
    [RequireComponent(typeof(CharacterController))]
    public class MovementHero : MonoBehaviour
    {
        public int _moveSpeed;
        private Camera _mainCamera;
        private CharacterController _controller;
        private Vector3 _currentMove;

        private InputMovementPlayer _movementInput;
        private HeroAnimatorController _heroAnimatorController;
        private HeroStatsData _heroStats;
        private DashData _dashData;
        private float _dashDistance;
        private float _dashDuration;

        private bool _isDashing;
        private Vector3 _dashDirection;
        
        [Inject]
        public void Construct(InputMovementPlayer movementInput, DashData dashData, HeroStatsData heroStats)
        {
            _movementInput = movementInput;
            _dashData = dashData;
            _heroStats = heroStats;
            

            
            _movementInput.OnMove += OnMove;
            _movementInput.OnDragMouse += OnMouseMove;
            _movementInput.OnDash += TryDash;
        }

        private void OnEnable()
        {
            _heroAnimatorController = GetComponent<HeroAnimatorController>();
            _controller = GetComponent<CharacterController>();
            if (_mainCamera == null)
                _mainCamera = Camera.main;

            _dashDistance = _dashData.Distance;
            _dashDuration = _dashData.Duration;
        }

        private void OnDestroy()
        {
            if (_movementInput == null) return;

            _movementInput.OnMove -= OnMove;
            _movementInput.OnDragMouse -= OnMouseMove;
            _movementInput.OnDash -= TryDash;
        }
        
        private void OnMove(Vector2 dir)
        {
            _currentMove = new Vector3(dir.x, 0, dir.y);
            
            if (_heroAnimatorController != null)
            {
                if (_currentMove != Vector3.zero)
                    _heroAnimatorController.PlayMove();
                else
                    _heroAnimatorController.StopMove();
            }
        }

        private void OnMouseMove(Vector2 mousePos) =>
            RotateHero(mousePos);

        private void Update() =>
            MoveHero();

        private void MoveHero()
        {
            if (_isDashing)
                return;
            
            if (_currentMove != Vector3.zero)
                _controller.Move(_currentMove * (_moveSpeed * Time.deltaTime));
        }

        private void RotateHero(Vector2 mousePos)
        {
            Plane groundPlane = new Plane(Vector3.up, transform.position);
            Ray ray = _mainCamera.ScreenPointToRay(mousePos);

            if (groundPlane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                Vector3 direction = hitPoint - transform.position;
                direction.y = 0;

                if (direction.sqrMagnitude > 0.001f)
                    transform.rotation = Quaternion.LookRotation(direction);
            }
        }
        
        private void TryDash()
        {
            if (_isDashing)
                return;

            _dashDirection = transform.forward;
            StartCoroutine(DashRoutine());
        }
        
        private IEnumerator DashRoutine()
        {
            _isDashing = true;

            float elapsed = 0f;
            float dashSpeed = _dashDistance / _dashDuration;
            

            while (elapsed < _dashDuration)
            {
                _controller.Move(_dashDirection * (dashSpeed * Time.deltaTime));
                elapsed += Time.deltaTime;
                yield return null;
            }

            _isDashing = false;
        }
    }
}
