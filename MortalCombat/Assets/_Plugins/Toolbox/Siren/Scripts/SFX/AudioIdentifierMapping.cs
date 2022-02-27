using UnityEngine;
using System;
using ToolBox;

namespace Siren
{
    /// <summary>
    /// DataClass: Maps an indentifier to an audio asset
    /// </summary>
    [Serializable]
    public class AudioIdentifierMapping : IIdentifiable<string>
    {
        [SerializeField] private string _Identifier = "id";
		[SerializeField] private AudioAsset _AudioAsset = null;
        
        public string Identifier => _Identifier;
        public AudioAsset AudioAsset => _AudioAsset;
        string IIdentifiable<string>.Id => _Identifier;
    }
}
