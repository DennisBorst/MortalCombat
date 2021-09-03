using ToolBox;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MortalCombat
{
    public class CharacterSelectionComponent : MonoBehaviour
    {
        [SerializeField] private int playerId = 0;
        [SerializeField] Sprite[] sprites = null;
        [SerializeField] Image spriteRenderer;

        [SerializeField] Image leftArrow;
        [SerializeField] Image rightArrow;
        [SerializeField] Image panel;

        private KeyCode keyCodeLeft;
        private KeyCode keyCodeRight;
        private KeyCode keyCodeReady;
        private KeyCode keyCodeUnReady;

        private Color color;

        private int currentIndex;
        private bool ready;

        private void Awake()
        {
            switch (playerId)
            {
                case 0:
                    keyCodeLeft = KeyCode.Joystick1Button4;
                    keyCodeRight = KeyCode.Joystick1Button5;
                    keyCodeReady = KeyCode.Joystick1Button0;
                    keyCodeUnReady = KeyCode.Joystick1Button1;
                    break;
                case 1:
                    keyCodeLeft = KeyCode.Joystick2Button4;
                    keyCodeRight = KeyCode.Joystick2Button5;
                    keyCodeReady = KeyCode.Joystick2Button0;
                    keyCodeUnReady = KeyCode.Joystick2Button1;
                    break;
                case 2:
                    keyCodeLeft = KeyCode.Joystick3Button4;
                    keyCodeRight = KeyCode.Joystick3Button5;
                    keyCodeReady = KeyCode.Joystick3Button0;
                    keyCodeUnReady = KeyCode.Joystick3Button1;
                    break;
                case 3:
                    keyCodeLeft = KeyCode.Joystick4Button4;
                    keyCodeRight = KeyCode.Joystick4Button5;
                    keyCodeReady = KeyCode.Joystick4Button0;
                    keyCodeUnReady = KeyCode.Joystick4Button1;
                    break;
            }

            color = panel.color;
        }


        private void Update()
        {
            if (!ready)
            {
                if (Input.GetKeyDown(keyCodeLeft))
                    Previous();
                if (Input.GetKeyDown(keyCodeRight))
                    Next();
                if (Input.GetKeyDown(keyCodeReady))
                {
                    ready = true;
                    leftArrow.gameObject.SetActive(false);
                    rightArrow.gameObject.SetActive(false);
                    panel.color = Color.green;
                    PlayerConfiguration.Instance.SetSelectedIndex(playerId, currentIndex);
                    GlobalEvents.SendMessage(new PlayerReady(playerId));
                }
                if (Input.GetKeyDown(keyCodeUnReady))
                {
                    GoToPreviousScene();
                }
            }
            else
            {
                if (Input.GetKeyDown(keyCodeUnReady))
                {
                    ready = false;
                    leftArrow.gameObject.SetActive(true);
                    rightArrow.gameObject.SetActive(true);
                    panel.color = color;
                    GlobalEvents.SendMessage(new PlayerUnready(playerId));
                }
            }
        }

        private void Next()
        {
            currentIndex++;
            if (currentIndex == sprites.Length)
                currentIndex = 0;
            SwapCharacterSprite(sprites[currentIndex]);
        }

        private void Previous()
        {
            currentIndex--;
            if (currentIndex == -1)
                currentIndex = sprites.Length - 1;
            SwapCharacterSprite(sprites[currentIndex]);
        }

        private void SwapCharacterSprite(Sprite characterObject)
        {
            spriteRenderer.sprite = characterObject;
        }

        public void GoToPreviousScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}