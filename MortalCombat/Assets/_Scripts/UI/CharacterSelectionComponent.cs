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

        [SerializeField] Image currentface;
        [SerializeField] Image previousFace;
        [SerializeField] Image nextFace;

        [SerializeField] Animator mainAnimator;
        [SerializeField] Animator faceAnimator;
        [SerializeField] Animator arrowLeft;
        [SerializeField] Animator arrowRight;

        [SerializeField] OutroAnimationEventListener listener;
        
        [SerializeField] TMPro.TMP_Text text;

        [Dependency] PlayerStatsService playerStats;
        [Dependency] InputService input;

        private int currentIndex;
        private bool ready;
        private bool sentReady;

        protected override void Awake()
        {
            base.Awake();
        }

        public void Start()
        {
            listener.OnAnimationCompleted += SendPlayerReady;

            currentIndex = PlayerConfiguration.Instance.GetSelectedIndex(playerId);
            SetSprites();
            text.text = "PLAYER " + (playerId + 1);
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
                    mainAnimator.SetTrigger("ready");

                    ready = true;
                    PlayerConfiguration.Instance.SetSelectedIndex(playerId, currentIndex);
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
                    mainAnimator.SetTrigger("unready");
                    ready = false;

                    if (sentReady)
                        GlobalEvents.SendMessage(new PlayerUnready(playerId));
                    sentReady = false;
                }
            }
        }

        public void SendPlayerReady()
        {
            sentReady = true;
            GlobalEvents.SendMessage(new PlayerReady(playerId));
        }

        private void Next()
        {
            currentIndex = WrapSpriteIndex(currentIndex - 1);
            SetSprites();
            faceAnimator.SetTrigger("next");
            arrowRight.SetTrigger("Start");
        }

        private void Previous()
        {
            currentIndex = WrapSpriteIndex(currentIndex + 1);
            SetSprites();
            faceAnimator.SetTrigger("previous");
            arrowLeft.SetTrigger("Start");
        }

        private void SetSprites()
        {
            previousFace.sprite = GetCharacterSprite(currentIndex + 1); 
            currentface.sprite = GetCharacterSprite(currentIndex); 
            nextFace.sprite = GetCharacterSprite(currentIndex - 1);
        }

        private Sprite GetCharacterSprite(int i)
        {
            return sprites[WrapSpriteIndex(i)];
        }

        private int WrapSpriteIndex(int index)
        {
            if (index >= 0)
                return index % sprites.Length;
            else return (sprites.Length - 1) - ((-index) % sprites.Length);
        }

        public void GoToPreviousScene()
        {
            playerStats.ResetKills();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}