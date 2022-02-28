#if UNITY_EDITOR
using Siren.Utilities.Editor;
using UnityEditor;
using UnityEngine;

using System;
using ToolBox;
using ToolBox.Editor;

using EditorScriptUtil = Siren.Utilities.Editor.EditorScriptUtil;

namespace Siren.Editor
{
    /// <summary>
    /// Used to render and control a window to edit an audio library asset
    /// </summary>
    public class AudioLibraryEditor
    {
        private readonly SelectableList _SelectableAudioAssetList;
        public event Action<AudioAsset> OnSelected;
        public event Action<AudioAssetContext> OnPreview;
        public event Action OnRequestRepaint;

        public event Action<AudioAsset> OnElementDeleted;


        private EditorWindow _currentWindow;
		private AudioAssetLibrary _RawTarget;
        private SerializedObject _SerializedTarget;
        private SerializedProperty _MappingList;
        private SerializedProperty _DefaultMixerChannel;

        private readonly GUIContent label = new GUIContent("Default mixer group", "The default mixer group that this audio library will play through.");


        private Vector2 _ScrollVector;

        public AudioLibraryEditor(EditorWindow windowInstance)
        {
            _SelectableAudioAssetList = new SelectableList(DrawElement);
            _SelectableAudioAssetList.OnSelect += OnAudioAssetSelected;
			_currentWindow = windowInstance;
		}

		public void SetTarget(AudioAssetLibrary library)
		{
			_SelectableAudioAssetList.ResetSelection();

			if (library == null)
			{
				_RawTarget = null;
				_SerializedTarget = null;
				_MappingList = null;
				return;
			}

			_RawTarget = library;
			_SerializedTarget = new SerializedObject(library);
			_MappingList = _SerializedTarget.FindProperty("_AudioAssetIdentifierMappings");
			_DefaultMixerChannel = _SerializedTarget.FindProperty("_AudioMixerGroup");
        }

		public void DoLibraryEditor()
        {
            if (_RawTarget == null)
            {
                return;
            }
            GUILayout.BeginHorizontal();
            GUILayout.Label($"{nameof(AudioLibraryEditor)}");

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.ObjectField(_DefaultMixerChannel, label);
            if (EditorGUI.EndChangeCheck())
                _SerializedTarget.ApplyModifiedProperties();
            GUILayout.EndHorizontal();

            Rect rect = GUILayoutUtility.GetRect(0, 1, GUILayout.ExpandWidth(true));
            EditorScriptUtil.DrawLine(rect.GetPosition(0, 0), rect.GetPosition(1, 0), Color.black);

            _ScrollVector = GUILayout.BeginScrollView(_ScrollVector);

            DrawAssetMappingList();

            GUILayout.EndScrollView();
        }

        private void DrawAssetMappingList()	
        {
	        GUILayout.BeginVertical(GUI.skin.box);
	        GUILayout.BeginHorizontal();

	        GUILayout.Label("Audio Asset Mappings");

			if (GUILayout.Button("Nuke"))
	        {
		        ScriptableObject.CreateInstance<ConfirmActionPopup>()
			        .SetQuestion("You're about to nuke all elements from this library, are you sure?")
					.CenterOnRect(_currentWindow.position)
					.OnConfirm += DeleteAllElements;
	        }

	        if (GUILayout.Button("Remove"))
	        {
		        RemoveSelectedElement();
	        }

	        if (GUILayout.Button("Add"))
	        {
		        EditorWindow.CreateInstance<CreateAudioAssetPopup>()
			        .SetFolder(Config.PATH_AUDIO_ASSETS)
					.CenterOnRect(_currentWindow.position)
			        .OnCreated += AddElement;
	        }
	        GUILayout.EndHorizontal();
	        _SelectableAudioAssetList.DoList(_MappingList.arraySize, _ScrollVector);
	        GUILayout.EndVertical();
        }


        private bool PreviewKeyPressed(int index)
        {
	        return GUI.enabled
		        && Event.current.isKey
				   && Event.current.type == EventType.KeyDown
				   && Event.current.keyCode == KeyCode.P
	               && _SelectableAudioAssetList.SelectedElementIndex == index;
        }

        private void DrawElement(int index)
        {
            SerializedProperty currentMapping = _MappingList.GetArrayElementAtIndex(index);
            SerializedProperty audioAssetProperty = currentMapping.FindPropertyRelative("_AudioAsset");
            SerializedProperty audioIdentifierProperty = currentMapping.FindPropertyRelative("_Identifier");
			AudioAsset referencedAudioAsset = (AudioAsset)audioAssetProperty.objectReferenceValue;

			EditorGUILayout.BeginVertical();
			EditorGUILayout.LabelField(referencedAudioAsset.name);
            audioIdentifierProperty.stringValue = EditorGUILayout.TextField(audioIdentifierProperty.stringValue);

			GUILayout.BeginHorizontal();
			GUILayout.Label("Audio Asset: ");
			audioAssetProperty.objectReferenceValue = EditorGUILayout.ObjectField(audioAssetProperty.objectReferenceValue, typeof(AudioAsset), false);
			GUILayout.Space(2);
			bool audioAssetContainsClips = referencedAudioAsset.ContainsAudioClips;
			DrawPreviewButton(audioAssetContainsClips, index, new AudioAssetContext(_RawTarget, referencedAudioAsset));

            EditorGUILayout.EndHorizontal();
			EditorGUILayout.EndVertical();

		}

		private void DrawPreviewButton(bool enabled, int index, AudioAssetContext assetContext)
        {
			if (!enabled)
				GUI.enabled = false;

	        if (GUILayout.Button("Preview") || PreviewKeyPressed(index))
	        {
		        OnPreview?.Invoke(assetContext);
	        }

			if (!enabled)
				GUI.enabled = true;
		}

		private void AddElement(AudioAsset asset)
        {
            int index = _MappingList.arraySize;
            _MappingList.InsertArrayElementAtIndex(index);
            SerializedProperty currentMapping = _MappingList.GetArrayElementAtIndex(index);
			// SetObject
            SerializedProperty audioAssetProperty = currentMapping.FindPropertyRelative("_AudioAsset");
            audioAssetProperty.objectReferenceValue = asset;

			SerializedProperty audioIdentifierProperty = currentMapping.FindPropertyRelative("_Identifier");
			audioIdentifierProperty.stringValue = _RawTarget.name + "/" + asset.name;

			_SerializedTarget.ApplyModifiedProperties();
            OnRequestRepaint?.Invoke();
        }

        private void RemoveSelectedElement()
        {
            int index = _SelectableAudioAssetList.SelectedElementIndex;

            if (index == -1)
            {
                return;
            }

            var property = _MappingList.GetArrayElementAtIndex(index);
            var relative = property.FindPropertyRelative("_AudioAsset");
            var asset = (AudioAsset)relative.objectReferenceValue;
            _MappingList.DeleteArrayElementAtIndex(index);
_SerializedTarget.ApplyModifiedProperties();
            OnElementDeleted?.Invoke(asset);
        }

        private void DeleteAllElements()
        {
            AudioAsset[] assetsToDelete = _MappingList.ToArray<AudioAsset>();

            _MappingList.ClearArray();
            _SerializedTarget.ApplyModifiedProperties();

            assetsToDelete.Each(OnElementDeleted);
        }

        private void OnAudioAssetSelected(int index)
        {
			GUI.FocusControl(null);

			if (index == -1)
            {
                OnSelected?.Invoke(null);
                return;
            }

            SerializedProperty audioAssetProperty =
                EditorScriptUtil.FromPropertyRelativeFromIndex(_MappingList, index, "_AudioAsset");

            if (!EditorScriptUtil.TryFetchPropertyGuid(audioAssetProperty, out string guid))
            {
                OnSelected?.Invoke(null);
                return;
            }

            string path = AssetDatabase.GUIDToAssetPath(guid);
            AudioAsset audioAsset = AssetDatabase.LoadAssetAtPath<AudioAsset>(path);

            OnSelected?.Invoke(audioAsset);
		}
    }
}
#endif