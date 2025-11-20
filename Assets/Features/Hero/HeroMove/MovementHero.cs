using Features.Input.Scripts;
using Features.Hero.HeroAnimator;
using UnityEngine;
using Zenject;

namespace Features.Hero.HeroMove
{
    [RequireComponent(typeof(CharacterController))]
    public class MovementHero : MonoBehaviour
    {
        public float speed = 5f;

        private Camera mainCamera;
        private CharacterController _controller;
        private Vector3 _currentMove;

        private InputMovementPlayer _movementInput;
        private HeroAnimatorController _heroAnimatorController;

        [Inject]
        public void Construct(InputMovementPlayer movementInput)
        {
            _movementInput = movementInput;

            _movementInput.OnMove += OnMove;
            _movementInput.OnDragMouse += OnMouseMove;
        }

        private void OnEnable()
        {
            _heroAnimatorController =GetComponent<HeroAnimatorController>();
            _controller = GetComponent<CharacterController>();
            if (mainCamera == null)
                mainCamera = Camera.main;
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
            if (_currentMove != Vector3.zero)
                _controller.Move(_currentMove * (speed * Time.deltaTime));
        }

        private void RotateHero(Vector2 mousePos)
        {
            Plane groundPlane = new Plane(Vector3.up, transform.position);
            Ray ray = mainCamera.ScreenPointToRay(mousePos);

            if (groundPlane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                Vector3 direction = hitPoint - transform.position;
                direction.y = 0;

                if (direction.sqrMagnitude > 0.001f)
                    transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
}
