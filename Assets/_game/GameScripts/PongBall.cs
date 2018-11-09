using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Mangos
{
    public class PongBall : NetworkBehaviour
    {
        public float respawnTime;
        public float Velocity = 5f;
        public float increments = 0.5f;
        
        private Vector3 verticalCol;
        private Vector3 horizontalCol;

        Rigidbody2D rigi;

        private void Awake()
        {
            StaticManager.ball = this;
        }

        // Use this for initialization
        void Start()
        {
            verticalCol = new Vector3(-1, 1, 0);
            horizontalCol = new Vector3(1, -1, 0);
            rigi = GetComponent<Rigidbody2D>();
        }

        public void StartGame()
        {
            if(isServer)
                StartVelocity();
        }

        public void StartVelocity()
        {
            rigi.velocity = new Vector3(1, 0, 0) * Velocity;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Respawn()
        {
            if (!isServer)
                return;
            gameObject.transform.position = Vector3.zero;
            gameObject.SetActive(true);
            StartVelocity();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!isServer)
                return;
            if (collision.CompareTag("Wall"))
            {
                rigi.velocity = Vector3.Scale(rigi.velocity, verticalCol);
                Rigidbody2D colRigi = collision.GetComponentInParent<Rigidbody2D>();
                if(colRigi)
                {
                    rigi.velocity += colRigi.velocity.normalized * increments;
                    Debug.Log("Collided with pallete, vel=" + colRigi.velocity);
                }
                else
                {
                    Debug.Log("Collided with something that didn't had rigidbvody2d");
                }
            }

            if (collision.CompareTag("Ceiling"))
            {
                rigi.velocity = Vector3.Scale(rigi.velocity, horizontalCol);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!isServer)
                return;
            if (collision.CompareTag("Boundaries"))
            {
                gameObject.SetActive(false);
                rigi.velocity = Vector3.zero;
                Invoke("Respawn", respawnTime);
            }       

        }
    }
}
