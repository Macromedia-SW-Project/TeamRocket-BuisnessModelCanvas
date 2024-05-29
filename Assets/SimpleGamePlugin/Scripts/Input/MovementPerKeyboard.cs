using UnityEngine;

namespace Course.PrototypeScripting
{
    public class MovementPerKeyboard : MonoBehaviour
    {
    
    
        public Rigidbody character;
        public enum DirectionRelative { CameraDirection, CharacterDirection  }
        public DirectionRelative directionRelative = DirectionRelative.CameraDirection;
        public float moveAcceleration = 10f;
        public float maxSpeed = 2f;
        public float rotationSpeed = 0.25f;
        public KeyCode mainInteractionKey;
        bool canJump = true;

  

        Transform relativeMoveTransform;

        UnityEngine.Camera mainCam;

        Vector3 moveDirection;
        private void Awake()
        {
            mainCam = UnityEngine.Camera.main;
            RuntimeGlobal.keyboardMovement = this;
        }

        // Update is called once per frame
        void Update()
        {
            if (RuntimeGlobal.gameState != RuntimeGlobal.GameState.NormalGame)
                return;

            KeyboardInput();
            JumpTimer();

        }

        void JumpTimer()
        {
            if (timeUntilJumpAgain > 0 && !canJump)
            {
                timerJumpAgain -= Time.deltaTime;
                if (timerJumpAgain < 0)
                {
                    CanJumpAgain();
                }
            }
        }

        void KeyboardInput()
        {
            if (directionRelative == DirectionRelative.CameraDirection)
                relativeMoveTransform = mainCam.transform;
            else
                relativeMoveTransform = character.transform;
            if (UnityEngine.Input.GetKey(KeyCode.W))
            {
                MoveForward();
            }

            if (UnityEngine.Input.GetKey(KeyCode.A))
            {
                if(directionRelative == DirectionRelative.CharacterDirection)
                    RotateLeft();
                else
                    MoveLeft();
            }

            if (UnityEngine.Input.GetKey(KeyCode.S))
            {
                MoveBack();
            }

            if (UnityEngine.Input.GetKey(KeyCode.D))
            {
                if (directionRelative == DirectionRelative.CharacterDirection)
                    RotateRight();
                else
                    MoveRight();
            }

            Move(moveDirection);
            moveDirection = Vector3.zero;

            if (UnityEngine.Input.GetKeyDown(jumpKey) && canJump)
            {
                Jump();
            }

            if (UnityEngine.Input.GetKeyUp(mainInteractionKey) && RuntimeGlobal.selectedObject != null)
                StartInteraction();
        }

        public void MoveLeft()
        {
            MoveSide(-1f);
        }

        public void MoveRight()
        {
            MoveSide(1f);
        }

        void MoveSide(float direction)
        {
            Vector3 planeDirection = new Vector3(relativeMoveTransform.transform.right.x, 0, relativeMoveTransform.transform.right.z);
            moveDirection += planeDirection * direction;
        }

        public void RotateLeft()
        {
            character.transform.Rotate(new Vector3(0, 1, 0), -rotationSpeed  * Time.deltaTime * 1000f);
        }

        public void RotateRight()
        {
            character.transform.Rotate(new Vector3(0, 1, 0), rotationSpeed * Time.deltaTime * 1000f);
        }

        public void MoveForward()
        {
            MoveForwards(1f); // character.transform.forward);
        }

        public void MoveBack()
        {
            MoveForwards(-1f); // character.transform.forward);
        }

        void MoveForwards(float direction)
        {
            Vector3 planeDirection = new Vector3(relativeMoveTransform.transform.forward.x, 0, relativeMoveTransform.transform.forward.z);
            moveDirection += planeDirection * direction;

        }

        private void Move(Vector3 direction)
        {
            if (direction == Vector3.zero)
                return;

            float y = character.velocity.y;
            Vector3 newDirection = character.velocity + direction.normalized * (moveAcceleration / 100f) * Time.deltaTime * 1000f;
            newDirection.y = 0;
            newDirection = newDirection.normalized * Mathf.Clamp(newDirection.magnitude, -maxSpeed, maxSpeed);
        
            character.velocity = new Vector3(newDirection.x, y, newDirection.z) ;

            if(directionRelative == DirectionRelative.CameraDirection)
            {
                character.transform.forward = FlattenVerticalVectorPart(Vector3.Lerp(character.transform.forward, newDirection, rotationSpeed * Time.deltaTime));
            }
          

            if (character.GetComponent<UnityEngine.AI.NavMeshAgent>())
                character.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        }

        void StartInteraction()
        {
            RuntimeGlobal.InteractWithSelectedObject();
        }
        public KeyCode jumpKey;
        public float jumpForce = 10f;
        public float timeUntilJumpAgain = 1f;
        float timerJumpAgain = 0f;

        void Jump()
        {
            canJump = false;
            if (timeUntilJumpAgain > 0)
                timerJumpAgain = timeUntilJumpAgain;
            character.velocity = new Vector3(character.velocity.x, character.velocity.y + jumpForce, character.velocity.z);
        }

        Vector3 FlattenVerticalVectorPart(Vector3 vec)
        {
            return new Vector3(vec.x, 0, vec.z);
        }

        public void SetMaxSpeed(float newMaxSpeed)
        {
            maxSpeed = newMaxSpeed;
        }
        public void SetJumpForce(float newJumpForce)
        {
            jumpForce = newJumpForce;
        }


        public void CanJumpAgain()
        {
            canJump = true;
        }

    }
}
