using System;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets {
    public class StarterAssetsInputs : MonoBehaviour {
        [Header("Character Input Values")]
        public Vector2 move;
        public bool jump;

        public event Action<Vector2> Move = delegate { };
        public event Action<bool> Jump = delegate { };
        public event Action Attack = delegate { };
        public event Action Interact = delegate { };
        public event Action Exit = delegate { };


#if ENABLE_INPUT_SYSTEM
        public void OnMove(InputValue value) {
			MoveInput(value.Get<Vector2>());
		}

        public void OnJump(InputValue value) {
			JumpInput(value.isPressed);
		}
		
        public void OnInteract(InputValue value) {
            Interact();
        }

        public void OnExit(InputValue value){
            Exit();
        } 
#endif

        public void MoveInput(Vector2 newMoveDirection) {
            move = newMoveDirection;
            Move(move);
        }

        public void JumpInput(bool newJumpState) {
            jump = newJumpState;
            Jump(jump);
        }

    }

}
