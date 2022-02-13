using ToolBox;
using ToolBox.Services;
using UnityEngine;

namespace MortalCombat
{
    public class CharacterManagerService : MonoBehaviour, IService
    {
        private CharacterConfiguration[] _Characters;
        private CharacterConfiguration[] Characters => _Characters ??= Resources.LoadAll<CharacterConfiguration>("Characters/"); 

        public CharacterConfiguration[] GetAllCharacters()
        {
            return Characters;
        }

        public CharacterConfiguration GetCharacter(Character character)
        {
            if (_Characters.TryResolve(character, out CharacterConfiguration config)) {
                return config;

            }

            Debug.LogError($"Couldn't find character with character id {character}.", this);

            return null;
        }
    }
}
