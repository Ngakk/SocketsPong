using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class PongBall : MonoBehaviour
    {
        public GameEvent OnPlayer1Score;
        public GameEvent OnPlayer2Score;
        public GameEvent OnBallHit;

        public float respawnTime;
        public float Velocity = 5f;
        public float increments = 0.5f;

        private int lastScore;
        private Vector3 verticalCol;
        private Vector3 horizontalCol;

        Rigidbody2D rigi;
        // Use this for initialization
        void Start()
        {
            verticalCol = new Vector3(-1, 1, 0);
            horizontalCol = new Vector3(1, -1, 0);
            rigi = GetComponent<Rigidbody2D>();
            lastScore = 0;
        }

        public void StartGame()
        {
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
            gameObject.transform.position = Vector3.zero;
            gameObject.SetActive(true);
            StartVelocity();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.CompareTag("Wall"))
            {
                rigi.velocity = Vector3.Scale(rigi.velocity, verticalCol);
                Rigidbody2D colRigi = collision.GetComponent<Rigidbody2D>();
                if(colRigi)
                {
                    rigi.velocity = colRigi.velocity.normalized * increments;
                }
                OnBallHit.Raise();
            }

            if (collision.CompareTag("Ceiling"))
            {
                rigi.velocity = Vector3.Scale(rigi.velocity, horizontalCol);
                OnBallHit.Raise();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Boundaries"))
            {
                if (transform.position.x > 0)
                {
                    OnPlayer1Score.Raise();
                    lastScore = 0;
                }
                else
                {
                    OnPlayer2Score.Raise();
                    lastScore = 1;
                }
                gameObject.SetActive(false);
                rigi.velocity = Vector3.zero;
                Invoke("Respawn", respawnTime);
            }       

        }
    }
}
