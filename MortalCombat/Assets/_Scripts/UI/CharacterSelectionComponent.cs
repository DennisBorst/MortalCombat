using ToolBox;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ToolBox.Injection;
using ToolBox.Input;

namespace MortalCombat
{
    public class CharacterSelectionComponent : DependencyBehavior
    {
        [SerializeField] private int playerId = 0;
        [SerializeField] Sprite[] sprites = null;
        [SerializeField] Image spriteRenderer;
        [SerializeField] Image leftArrow;
        [SerializeField] Image rightArrow;
        [SerializeField] Image panel;

        [SerializeField] Animator leftArrowAnimator;
        [SerializeField] Animator rightArrowAnimator;
        [SerializeField] Animator panelAnimator;

        [Dependency] PlayerStatsService playerStats;
        [Dependency] InputService input;

        private Color color;

        private int currentIndex;
        private bool ready;

        protected override void Awake()
        {
            base.Awake();

            color = panel.color;
        }

        public void Start()
        {
            currentIndex = PlayerConfiguration.Instance.GetSelectedIndex(playerId);
            SwapCharacterSprite(sprites[currentIndex]);
        }


        private void Update()
        {
            if (!ready)
            {
                if (input.Down(playerId, "Left") || input.Down(playerId, "LeftUI"))
                    Previous();
                if (input.Down(playerId, "Right") || input.Down(playerId, "RightUI"))
                    Next();

                if (input.Down(playerId, "Confirm"))
                {
                    ready = true;
                    panelAnimator.SetTrigger("ready");

                    leftArrow.gameObject.SetActive(false);
                    rightArrow.gameObject.SetActive(false);
                    panel.color = Color.green;
                    PlayerConfiguration.Instance.SetSelectedIndex(playerId, currentIndex);
                    GlobalEvents.SendMessage(new PlayerReady(playerId));
                }

                if (input.Down(playerId, "Back"))
                {
                    GoToPreviousScene();
                }
            }
            else
            {
                if (input.Down(playerId, "Back"))
                {
                    panelAnimator.SetTrigger("unready");
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

            rightArrowAnimator.SetTrigger("trigger");
            currentIndex++;
            if (currentIndex == sprites.Length)
                currentIndex = 0;
            SwapCharacterSprite(sprites[currentIndex]);
        }

        private void Previous()
        {
            leftArrowAnimator.SetTrigger("trigger");
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
            playerStats.ResetKills();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}