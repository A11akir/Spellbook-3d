using UnityEngine;

namespace Features.Scripts.Input
{
    public class InputGamePlay
    {
        public Controls _inputActions;

        public InputGamePlay()
        {
            _inputActions = new Controls();
            _inputActions.Enable();
        }
    }
}