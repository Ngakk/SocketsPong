﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class GameManager : MonoBehaviour
    {
        public FloatVariable p1Score, p2Score;

        // Use this for initialization
        void Start()
        {
            p1Score.value = 0;
            p2Score.value = 0;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LogInt(int var)
        {
            Debug.Log(var);
        }

        public void OnScore(int player)
        {
            if (player == 0)
                p1Score.value++;
            else
                p2Score.value++;
        }
    }
}