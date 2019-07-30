using System.Collections;
using System.Collections.Generic;
using MintAnimation.Core;
using UnityEditor;
using UnityEngine;

namespace MintAnimation.Editor
{
    [CustomEditor(typeof(MintAnimation.MintAnimation_Rotation), true) , CanEditMultipleObjects]
    public class MintAnimation_EditorRotation : MintAnimation_EditorBase
    {
        private SerializedProperty IsLocalRotation;
        private SerializedProperty StartRotation;
        private SerializedProperty EndRotation;

        protected override void Init()
        {
            base.Init();
            IsLocalRotation = serializedObject.FindProperty("IsLocal");
            StartRotation = MintAnimData.FindPropertyRelative("StartValue");
            EndRotation = MintAnimData.FindPropertyRelative("EndValue");
        }

        protected override void DrawTitle()
        {
            GUIStyle gUIStyle = new GUIStyle();
            gUIStyle.fontSize = 18;
            gUIStyle.normal.textColor = new Color32(56, 56, 56, 255);
            gUIStyle.normal.background = MintAnimation_EditorBase.GetTexture2D(new Color32(255, 95, 0, 255));
            gUIStyle.alignment = TextAnchor.MiddleCenter;
            gUIStyle.margin = new RectOffset(0, 0, 8, 0);
            GUILayout.Box(" Mint Rotation ", gUIStyle);
            gUIStyle = null;
        }

        public override void Draw()
        {
            base.Draw();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(AutoStartValue);
            IsLocalRotation.boolValue = GUILayout.Toolbar(IsLocalRotation.boolValue ? 0 : 1,
                new string[2] { "Local", "World" },
                new GUILayoutOption[] {
                GUILayout.Width(120),
                GUILayout.Height(20),
            })
            == 0 ? true : false;
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(10);

            if (!AutoStartValue.boolValue)
            {
                EditorGUILayout.PropertyField(StartRotation, new GUIContent("StartRotation"));
            }
            EditorGUILayout.PropertyField(EndRotation, new GUIContent("EndRotation"));

            GUILayout.Box(GUIContent.none, GUILayout.ExpandWidth(true), GUILayout.Height(0.5f));

        }
    }
}

