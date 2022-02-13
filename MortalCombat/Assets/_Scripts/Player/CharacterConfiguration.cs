using ToolBox;
using UnityEngine;

#pragma warning disable IDE0044 // Add readonly modifier
namespace MortalCombat
{
    public class CharacterConfiguration : ScriptableObject, IIdentifiable<Character>
    {
        [SerializeField] private Character _Character = Character.Blue;
        [SerializeField] private GameObject _Skin = null;
        [SerializeField] private Sprite _PlayerIconSprite = null;
        [SerializeField] private Sprite _CharacterSelectSprite = null;
        [SerializeField] private string _CharacterAnnoucerAudioId = "";

        public Character Character => _Character;
        public GameObject Skin => _Skin;
        public Sprite CharacterSelectSprite => _CharacterSelectSprite;
        public Sprite PlayerIconSprite => _PlayerIconSprite;
        public string CharacterAnnoucerAudioId => _CharacterAnnoucerAudioId;

        Character IIdentifiable<Character>.Id => Character;
    }
}
#pragma warning restore IDE0044 // Add readonly modifier
