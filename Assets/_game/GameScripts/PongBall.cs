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

        private int lastScore;

        Rigidbody2D rigi;
        // Use this for initialization
        void Start()
        {
            rigi = GetComponent<Rigidbody2D>();
            lastScore = 0;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ReSpawn()
        {
            gameObject.transform.position = Vector3.zero;
            gameObject.SetActive(true);
            rigi.velocity = new Vector3(1, Random.Range(0.1f, lastScore == 0 ? -1 : 1), 0);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Wall"))
                rigi.velocity *= new Vector3(-1, 1, 0);
            if (collision.CompareTag("Ceiling"))
                rigi.velocity *= new Vector3(1, -1, 0);
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
