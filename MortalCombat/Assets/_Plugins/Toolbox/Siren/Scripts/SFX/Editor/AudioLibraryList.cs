#if UNITY_EDITOR
using Siren.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using ToolBox.Editor;
using ToolBox;

namespace Siren.Editor
{
    /// <summary>
    /// Used to render and control a selectable list of all audio libraries in the project
    /// /// </summary>
    public class AudioLibraryList
    {
        public event System.Action<AudioAssetLibrary> OnSelected;
        public event System.Action OnRequestRepaint;

		private readonly EditorWindow _CurrentWindow;
        private readonly SelectableList _SelectionList;
        private Vector2 _ScrollPosition = Vector2.zero;

        private AudioAssetLibrary[] _Libraries;

        public AudioLibraryList(EditorWindow windowInstance)
        {
            _SelectionList = new SelectableList(DrawElement);
            _SelectionList.OnSelect += OnSelect;
			_CurrentWindow = windowInstance;
		}

        public void DoList()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label($"{nameof(AudioLibraryList)}");

            if (GUILayout.Button("Create New Library"))
            {
				EditorWindow.CreateInstance<CreateAudioLibraryAssetPopup>()
					.SetFolder(Config.PATH_AUDIO_LIBRARIES)
					.CenterOnRect(_CurrentWindow.position)
                    .OnCreated += OnNewElementcreated;
            }

            if (GUILayout.Button("Delete"))
            {
                RemoveSelectedElement();
            }

            GUILayout.EndHorizontal();
            _ScrollPosition = EditorGUILayout.BeginScrollView(_ScrollPosition);
            _SelectionList.DoList(_Libraries.Length, _ScrollPosition);
            EditorGUILayout.EndScrollView();
        }

		internal void OnFocusAndEnable()
		{
			FetchResources();
		}

        internal bool HasAnyReferenceTo(AudioAsset asset)
        {
            return _Libraries.Any(x => x.ReferencesAudioAsset(asset));
        }

		private void OnSelect(int index)
        {
			GUI.FocusControl(null);

			if (index == -1)
			{
				OnSelected?.Invoke(null);
				return;
			}

            OnSelected?.Invoke(_Libraries[index]);
        }

        private void DrawElement(int index)
        {
            GUILayout.Label(_Libraries[index].name);
        }

        private void FetchResources()
        {
            _Libraries = AudioFileSystem.LoadAllLibraries();
        }

        private void OnNewElementcreated(AudioAssetLibrary element)
        {
			FetchResources();
			OnRequestRepaint?.Invoke();
        }

        private void RemoveSelectedElement()
        {
            int index = _SelectionList.SelectedElementIndex;

            if (index == -1) // nothing selected delete
                return;

            EditorWindow.CreateInstance<ConfirmActionPopup>()
                .SetQuestion($"Are you sure you want to delete {_Libraries[index].name}?")
				.CenterOnRect(_CurrentWindow.position)
                .OnConfirm += () => RemoveAsset(AssetUtil.GetPath(_Libraries[index]));
        }

		private void RemoveAsset(string name)
		{
			AssetDatabase.DeleteAsset(name);
			FetchResources();
			_SelectionList.ResetSelection();
			OnRequestRepaint?.Invoke();
		}
    }
}
#endif