using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class PlayerController : MonoBehaviour
    {
        public float MoveSpeed = 5f;
        public float yLimits;
        public KeyCode up;
        public KeyCode down;

        // Update is called once per frame
        void Update()
        {

            if (Input.GetKey(up))
            {
                transform.Translate(Vector3.up * MoveSpeed * Time.deltaTime);
            }


            if (Input.GetKey(down))
            {
                transform.Translate(Vector3.down * MoveSpeed * Time.deltaTime);
            }

        }
    }
}