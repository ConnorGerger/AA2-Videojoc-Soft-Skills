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
        public bool attack;
        public bool charge;

        public event Action<Vector2> Move = delegate { };
        public event Action<bool> Jump = delegate { };
        public event Action<bool> Attack = delegate { };
        public event Action<bool> Charge = delegate { };
        public event Action Interact = delegate { };
        public event Action Exit = delegate { };


#if ENABLE_INPUT_SYSTEM
        public void OnMove(InputValue value) {
            move = value.Get<Vector2>();
            Move(move);
		}

        public void OnJump(InputValue value) {
            jump = value.isPressed;
            Jump(jump);
        }

        public void OnAttack(InputValue value) {
            attack = value.isPressed;
            if (attack)
                Attack(attack);
        }
        public void OnCharge(InputValue value) {
            charge = value.isPressed;
            Charge(charge);
        }

        public void OnInteract(InputValue value) {
            Interact();
        }

        public void OnExit(InputValue value){
            Exit();
        } 
#endif

    }

}
