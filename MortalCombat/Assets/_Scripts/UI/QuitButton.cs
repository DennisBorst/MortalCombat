using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MortalCombat
{
    public class QuitButton : MonoBehaviour
    {
        [SerializeField] public Button button;

        void Start()
        {
            button.onClick.AddListener(() =>
            {
                Application.Quit();
            });
        }
    }

}