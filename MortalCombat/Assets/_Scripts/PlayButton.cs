using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MortalCombat
{
    public class PlayButton : MonoBehaviour
    {
        [SerializeField] public Button button;

        void Start()
        {
            button.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("Game", LoadSceneMode.Single);
            });
        }
    }
}