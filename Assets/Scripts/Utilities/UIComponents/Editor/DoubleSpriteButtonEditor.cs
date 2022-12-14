using TMPro;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

namespace Chebureck.Utilities
{
    [CustomEditor(typeof(DoubleSpriteButton))]
    public class DoubleSpriteButtonEditor : ButtonEditor
    {
        protected SerializedProperty _states;
        protected SerializedProperty _setNativeSize;
        protected SerializedProperty _controlColor;

        protected override void OnEnable()
        {
            base.OnEnable();

            _states = serializedObject.FindProperty("states");
            _setNativeSize = serializedObject.FindProperty("setNativeSize");
            _controlColor = serializedObject.FindProperty("controlColor");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (EditorGUILayout.PropertyField(_states, new GUIContent("States")))
            {
                _states.arraySize = Mathf.Clamp(EditorGUILayout.IntField("Size", _states.arraySize), 0, 99);

                for (int i = 0; i < _states.arraySize; i++)
                {
                    SerializedProperty property = _states.GetArrayElementAtIndex(i);

                    if (EditorGUILayout.PropertyField(property, new GUIContent($"Element {i}")))
                    {
                        property.FindPropertyRelative("spritePressed").objectReferenceValue =
                            (Sprite)EditorGUILayout.ObjectField(new GUIContent("Pressed"), property.FindPropertyRelative("spritePressed").objectReferenceValue, typeof(Sprite), true);

                        property.FindPropertyRelative("spriteNormal").objectReferenceValue =
                            (Sprite)EditorGUILayout.ObjectField(new GUIContent("Normal"), property.FindPropertyRelative("spriteNormal").objectReferenceValue, typeof(Sprite), true);

                        property.FindPropertyRelative("spriteDisabled").objectReferenceValue =
                           (Sprite)EditorGUILayout.ObjectField(new GUIContent("Disabled"), property.FindPropertyRelative("spriteDisabled").objectReferenceValue, typeof(Sprite), true);

                        property.FindPropertyRelative("image").objectReferenceValue =
                            (UnityEngine.UI.Image)EditorGUILayout.ObjectField(new GUIContent("Image"), property.FindPropertyRelative("image").objectReferenceValue, typeof(UnityEngine.UI.Image), true);

                        property.FindPropertyRelative("text").objectReferenceValue =
                            (TextMeshProUGUI)EditorGUILayout.ObjectField(new GUIContent("Text"), property.FindPropertyRelative("text").objectReferenceValue, typeof(TextMeshProUGUI), true);

                        property.FindPropertyRelative("disabledColor").colorValue =
                            EditorGUILayout.ColorField(new GUIContent("Disabled Color"), property.FindPropertyRelative("disabledColor").colorValue);

                        property.FindPropertyRelative("normalColor").colorValue =
                            EditorGUILayout.ColorField(new GUIContent("Normal Color"), property.FindPropertyRelative("normalColor").colorValue);

                        property.FindPropertyRelative("pressedColor").colorValue =
                            EditorGUILayout.ColorField(new GUIContent("Pressed Color"), property.FindPropertyRelative("pressedColor").colorValue);
                    }
                }
            }

            _setNativeSize.boolValue = EditorGUILayout.Toggle(new GUIContent("Set Native Size"), _setNativeSize.boolValue);

            _controlColor.boolValue = EditorGUILayout.Toggle(new GUIContent("Control Color"), _controlColor.boolValue);

            serializedObject.ApplyModifiedProperties();
        }
    }
}