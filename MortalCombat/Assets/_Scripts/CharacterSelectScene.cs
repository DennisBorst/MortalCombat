using System;
using System.Collections;
using System.Collections.Generic;
using ToolBox;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MortalCombat
{
    public class CharacterSelectScene : MonoBehaviour
    {
        int totalPlayersReady;

        private void Start()
        {
            GlobalEvents.AddListener<PlayerReady>(OnPlayerReady);
            GlobalEvents.AddListener<PlayerUnready>(OnPlayerUnready);
        }

        private void OnDestroy()
        {
            GlobalEvents.RemoveListener<PlayerReady>(OnPlayerReady);
            GlobalEvents.RemoveListener<PlayerUnready>(OnPlayerUnready);
        }

        private void OnPlayerReady(PlayerReady obj)
        {
            totalPlayersReady++;

            if (totalPlayersReady == 2)
                SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }

        private void OnPlayerUnready(PlayerUnready obj)
        {
            totalPlayersReady--;
        }
    }
}
