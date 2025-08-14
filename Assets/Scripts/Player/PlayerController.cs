using Assets.Scripts.Events;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private CapsuleCollider collider;
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private float jumpForce;
        [SerializeField] private Vector3 spawnPosition;

        private Vector3[] pos;

        [SerializeField] private float laneWidth = 2.0f; 
        private int currentLane = 1;
        private bool isActive;
        private EventService eventService;

        public void SetService(EventService eventService)
        {
            this.eventService = eventService;
        }
        private void Awake()
        {
            isActive = false;
            pos = new Vector3[3];
            pos[1] = spawnPosition;
            pos[0] = new Vector3(transform.position.x, transform.position.y, transform.position.z +laneWidth);
            pos[2] = new Vector3(transform.position.x, transform.position.y, transform.position.z-laneWidth);
        }


        private void Update()
        {
           if(!isActive)return;
           HandlePlayerInput();
        }

        private void HandlePlayerInput()
        {
            throw new System.NotImplementedException();
        }

        public void ResetPlayer()
        {
            isActive = false;
            currentLane = 1;
            transform.position = spawnPosition;
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
                eventService.OnGameEnd.Invoke();
              //  animator.SetBool("running", false);
                isActive = false;
            }else if (collision.gameObject.CompareTag("Coins"))
            {
                eventService.OnCoinCollected.Invoke();
            }
        }
    }
}
