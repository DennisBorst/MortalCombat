using ToolBox;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ToolBox.Injection;
using ToolBox.Input;
using Siren;
using UnityEngine.Serialization;
using System.Linq;

namespace MortalCombat
{
    public class CharacterSelectionComponent : DependencyBehavior
    {
        [SerializeField, FormerlySerializedAs("AudioReady")] private string _AudioReady = "Menu/Confirm";
        [SerializeField, FormerlySerializedAs("AudioCancel")] private string _AudioCancel = "Menu/Cancel";

        [SerializeField, FormerlySerializedAs("playerId")] private int _PlayerId = 0;

        [SerializeField, FormerlySerializedAs("currentFace")] private Image _Currentface;
        [SerializeField, FormerlySerializedAs("previousFace")] private Image _PreviousFace;
        [SerializeField, FormerlySerializedAs("nextFace")] private Image _NextFace;

        [SerializeField, FormerlySerializedAs("mainAnimator")] private Animator _MainAnimator;
        [SerializeField, FormerlySerializedAs("faceAnimator")] private Animator _FaceAnimator;
        [SerializeField, FormerlySerializedAs("arrowLeft")] private Animator _ArrowLeft;
        [SerializeField, FormerlySerializedAs("arrowRight")] private Animator _ArrowRight;

        [SerializeField, FormerlySerializedAs("listener")] OutroAnimationEventListener _Listener;
        
        [SerializeField, FormerlySerializedAs("text")] TMPro.TMP_Text _Text;

        private CharacterConfiguration[] _Characters;

        [Dependency] PlayerStatsService _PlayerStats;
        [Dependency] InputService _Input;
        [Dependency] CharacterManagerService _CharacterManager;

        private int currentIndex;
        private bool ready;
        private bool sentReady;

        protected override void Awake()
        {
            base.Awake();
            _Characters = _CharacterManager.GetAllCharacters();
        }

        public void Start()
        {
            _Listener.OnAnimationCompleted += SendPlayerReady;

            currentIndex = PlayerConfiguration.Instance.GetSelectedIndex(_PlayerId);
            SetSprites();
            _Text.text = "PLAYER " + (_PlayerId + 1);
        }

        private void Update()
        {
            if (!ready)
            {
                if (_Input.Down(_PlayerId, "Left") || _Input.Down(_PlayerId, "LeftUI"))
                    Previous();
                if (_Input.Down(_PlayerId, "Right") || _Input.Down(_PlayerId, "RightUI"))
                    Next();

                if (_Input.Down(_PlayerId, "Confirm"))
                {
                    _MainAnimator.SetTrigger("ready");
                    Audio.Play(_AudioReady);
                    Audio.Play(_Characters[currentIndex].CharacterAnnoucerAudioId);

                    ready = true;
                    PlayerConfiguration.Instance.SetSelectedIndex(_PlayerId, currentIndex);
                }

                if (_Input.Down(_PlayerId, "Back"))
                {
                    Audio.Play(_AudioCancel);
                    GoToPreviousScene();
                }
            }
            else
            {
                if (_Input.Down(_PlayerId, "Back"))
                {
                    _MainAnimator.SetTrigger("unready");
                    ready = false;

                    if (sentReady)
                        GlobalEvents.SendMessage(new PlayerUnready(_PlayerId));
                    sentReady = false;

                    Audio.Play(_AudioCancel);
                }
            }
        }

        public void SendPlayerReady()
        {
            sentReady = true;
            GlobalEvents.SendMessage(new PlayerReady(_PlayerId));
        }

        private void Next()
        {
            currentIndex = WrapSpriteIndex(currentIndex - 1);
            SetSprites();
            Audio.Play("Menu/CharacterShift");
            _FaceAnimator.SetTrigger("next");
            _ArrowRight.SetTrigger("Start");
        }

        private void Previous()
        {
            currentIndex = WrapSpriteIndex(currentIndex + 1);
            SetSprites();
            Audio.Play("Menu/CharacterShift");
            _FaceAnimator.SetTrigger("previous");
            _ArrowLeft.SetTrigger("Start");
        }

        private void SetSprites()
        {
            _PreviousFace.sprite = GetCharacterSprite(currentIndex + 1); 
            _Currentface.sprite = GetCharacterSprite(currentIndex); 
            _NextFace.sprite = GetCharacterSprite(currentIndex - 1);
        }

        private Sprite GetCharacterSprite(int i)
        {
            return _Characters[WrapSpriteIndex(i)].CharacterSelectSprite;
        }

        private int WrapSpriteIndex(int index)
        {
            if (index >= 0)
                return index % _Characters.Length;
            else return _Characters.Length - ((-index) % _Characters.Length);
        }

        public void GoToPreviousScene()
        {
            _PlayerStats.ResetKills();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}