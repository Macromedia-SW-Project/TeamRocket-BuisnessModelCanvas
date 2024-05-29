using UnityEngine;
using UnityEngine.AI;

namespace Course.PrototypeScripting
{
    public class MovementToAnimator : MonoBehaviour
    {
        public Rigidbody character;
        public string forwardSpeedParameterName = "ForwardSpeed";
        public string strafeSpeedParameterName = "StrafeSpeed";
        public string jumpParameterName = "Jump";
        public float internSpeed;
        public Animator animator;

        private float distToGround = 0.0f;
        private Collider characterCollider = null;

        private void Awake()
        {
            if(animator == null)
                animator = character.GetComponent<Animator>();

            characterCollider = character.GetComponent<Collider>();
            if (characterCollider != null)
            {
                distToGround = characterCollider.bounds.extents.y;
            }
        }

        private bool IsGrounded()
        {
            return Physics.Raycast(character.transform.position, -Vector3.up, distToGround + 0.01f);
        }

        // Update is called once per frame
        void Update()
        {
            var velocity = character.velocity;

            var forwardSpeed = Vector3.Dot(transform.forward, velocity);
            var strafeSpeed = Vector3.Dot(transform.right, velocity);

            internSpeed = character.velocity.magnitude;
            if (GetComponent<NavMeshAgent>())
                internSpeed = GetComponent<NavMeshAgent>().velocity.magnitude;
            if (animator)
            {
                animator.SetFloat(forwardSpeedParameterName, IsGrounded() ? forwardSpeed : 0.0f);
                animator.SetFloat(strafeSpeedParameterName, IsGrounded() ?  strafeSpeed : 0.0f);
                animator.SetBool(jumpParameterName, !IsGrounded());
            }
        }
    }
}
