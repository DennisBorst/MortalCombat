using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace MortalCombat
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_ScoreText;

        private int m_ScorePlayerOne;
        private int m_ScorePlayerTwo;

        private static ScoreManager instance;

        private void Awake()
        {
            instance = this;

            m_ScorePlayerOne = PlayerPrefs.GetInt("ScorePlayerOne", 0);
            m_ScorePlayerTwo = PlayerPrefs.GetInt("ScorePlayerTwo", 0);

            SetText();
        }

        public static ScoreManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ScoreManager();
                }

                return instance;
            }
        }

        public void SetScore(int playerID)
        {
            if(playerID == 0)
            {
                m_ScorePlayerOne++;
                PlayerPrefs.SetInt("ScorePlayerOne", m_ScorePlayerOne);
            }
            if(playerID == 1)
            {
                m_ScorePlayerTwo++;
                PlayerPrefs.SetInt("ScorePlayerTwo", m_ScorePlayerTwo);
            }

            SetText();
        }

        private void SetText()
        {
            if(m_ScoreText != null) { m_ScoreText.text = m_ScorePlayerOne + " - " + m_ScorePlayerTwo; }
        }
    }
}
