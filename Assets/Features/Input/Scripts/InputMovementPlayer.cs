using System;
using Features.Scripts.Input;
using UnityEngine;
using Zenject;

namespace Features.Input.Scripts
{
    public class InputMovementPlayer
    {
        private readonly InputGamePlay _inputGamePlay;

        public event Action<Vector2> OnMove;
        public event Action<Vector2> OnDragMouse;

        [Inject]
        public InputMovementPlayer(InputGamePlay inputGamePlay)
        {
            _inputGamePlay = inputGamePlay;
            
            _inputGamePlay._inputActions.PlayerControl.Movement.performed += ctx =>
            {
                Vector2 value = ctx.ReadValue<Vector2>();
                OnMove?.Invoke(value);
            };
            _inputGamePlay._inputActions.PlayerControl.Movement.canceled += ctx =>
            {
                OnMove?.Invoke(Vector2.zero);
            };
            
            _inputGamePlay._inputActions.PlayerControl.Rotate.performed += ctx =>
            {
                Vector2 mousePos = ctx.ReadValue<Vector2>();
                OnDragMouse?.Invoke(mousePos);
            };
        }
    }
}