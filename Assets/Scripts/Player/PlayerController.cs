using Assets.Scripts.Events;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private CapsuleCollider collider;
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float laneChangeSpeed = 5f;
        [SerializeField] private Vector3 spawnPosition = new Vector3(-4f, 0, 0);
        [SerializeField] private float laneWidth = 2.0f;
        [SerializeField] private float groundCheckDistance = 1.1f;
        [SerializeField] private LayerMask groundLayer = 3;

        private int currentLane = 1;
        private int targetLane = 1;
        private bool isActive;
        private bool isChangingLanes = false;
        private EventService eventService;
        private Vector3[] lanePositions;
        public bool isGrounded = false;

        public void SetService(EventService eventService)
        {
            this.eventService = eventService;
        }

        private void Awake()
        {
            isActive = false;
            lanePositions = new Vector3[3];
            lanePositions[1] = spawnPosition;
            lanePositions[0] =
                new Vector3(transform.position.x, transform.position.y, transform.position.z + laneWidth);
            lanePositions[2] =
                new Vector3(transform.position.x, transform.position.y, transform.position.z - laneWidth);
        }


        private void Update()
        {
            if (!isActive) return;
            IsGrounded();
            HandlePlayerInput();
            HandleLaneTransition();
        }

        private void HandlePlayerInput()
        {
            //move left
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) ChangeLane(-1);
            //move right
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) ChangeLane(1);
            //jump
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) Jump();
        }

        private void ChangeLane(int direction)
        {
            int newLane = currentLane + direction;
            if (newLane < 0 || newLane > 2) return;
            targetLane = newLane;
            // Center lane (1) is 0, left (0) is positive, right (2) is negative
            float z = (1 - currentLane) * laneWidth;
            isChangingLanes = true;
        }

        private void HandleLaneTransition()
        {
            if (!isChangingLanes) return;

            Vector3 targetPosition = new Vector3(
                lanePositions[targetLane].x,
                transform.position.y, // Keep current Y position
                lanePositions[targetLane].z
            );

            // Smoothly move towards target lane
            Vector3 newPosition =
                Vector3.MoveTowards(transform.position, targetPosition, laneChangeSpeed * Time.deltaTime);
            transform.position = newPosition;

            // Check if we've reached the target lane
            if (Mathf.Abs(transform.position.z - targetPosition.z) < 0.01f)
            {
                currentLane = targetLane;
                isChangingLanes = false;
                // Snap to exact position to avoid floating point errors
                transform.position =
                    new Vector3(transform.position.x, transform.position.y, lanePositions[currentLane].z);
            }
        }

        private void Jump()
        {
            if (isGrounded)
            {
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0f, rigidbody.velocity.z);
                rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        private void IsGrounded()
        {
            isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundLayer);
        }

        public void ResetPlayer()
        {
            isActive = false;
            currentLane = 1;
            targetLane = 1;
            isChangingLanes = false;
            transform.position = spawnPosition;
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }


        public void OnGameStart()
        {
            ResetPlayer();
            isActive = true;
        }

        public void OnGamePause()
        {
            isActive = false;
        }

        public void OnGameResume()
        {
            isActive = true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision == null) Debug.Log("null");
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                eventService?.OnGameEnd.Invoke();
                isActive = false;
            }
            else if (collision.gameObject.CompareTag("Coins"))
            {
                eventService.OnCoinCollected.Invoke();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, groundCheckDistance);
        }
    }
}
