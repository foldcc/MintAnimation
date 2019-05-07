using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MintAnimation
{
    [CustomEditor(typeof(MintAnimation_Color))]
    public class MintAnimation_ColorEditor : Editor
    {
        private SerializedProperty AnimationInfo;

        private SerializedProperty Duration;

        private SerializedProperty IsAuto;

        private SerializedProperty IsBack;
        private SerializedProperty IsLoop;
        private SerializedProperty LoopCount;

        private SerializedProperty IsCustomEase;
        private SerializedProperty EaseType;
        private SerializedProperty TimeCurve;

        private SerializedProperty StartCor;
        private SerializedProperty EndCor;

        public static Texture2D GetTexture2D(Color32 color32)
        {
            Texture2D texturesss = new Texture2D(4, 4);
            Color32[] colors = texturesss.GetPixels32();
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = color32;
            }
            texturesss.SetPixels32(colors);
            texturesss.Apply();
            return texturesss;
        }

        private void OnEnable()
        {
            AnimationInfo = this.serializedObject.FindProperty("AnimationInfo");
            Duration = AnimationInfo.FindPropertyRelative("Duration");
            IsAuto = this.serializedObject.FindProperty("IsAutoPlay");
            IsBack = AnimationInfo.FindPropertyRelative("IsBack");
            IsLoop = AnimationInfo.FindPropertyRelative("IsLoop");
            LoopCount = AnimationInfo.FindPropertyRelative("LoopCount");
            IsCustomEase = AnimationInfo.FindPropertyRelative("IsCustomEase");
            EaseType = AnimationInfo.FindPropertyRelative("EaseType");
            TimeCurve = AnimationInfo.FindPropertyRelative("TimeCurve");
            StartCor = AnimationInfo.FindPropertyRelative("StartCor");
            EndCor = AnimationInfo.FindPropertyRelative("EndCor");
        }

        public override void OnInspectorGUI()
        {

            // 更新显示
            this.serializedObject.Update();

            GUIStyle gUIStyle = new GUIStyle();

            gUIStyle.fontSize = 18;
            gUIStyle.normal.textColor = new Color32(56, 56, 56, 255);
            gUIStyle.normal.background = MintAnimation_ColorEditor.GetTexture2D(new Color32(0, 255, 198, 255));
            gUIStyle.alignment = TextAnchor.MiddleCenter;
            gUIStyle.margin = new RectOffset(0, 0, 8, 0);
            GUILayout.Box(" Mint Color ", gUIStyle);
            gUIStyle = null;

            GUILayout.Space(15);
            
            EditorGUILayout.PropertyField(Duration);
            if (this.Duration.floatValue < 0) this.Duration.floatValue = 0;

            GUILayout.Box(GUIContent.none , GUILayout.ExpandWidth(true), GUILayout.Height(0.5f));

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(IsAuto);
            EditorGUILayout.PropertyField(IsBack);
            EditorGUILayout.EndHorizontal();

            GUILayout.Box(GUIContent.none, GUILayout.ExpandWidth(true), GUILayout.Height(0.5f));
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(IsLoop);
            if (this.IsLoop.boolValue)
            {
                EditorGUILayout.PropertyField(LoopCount);
                if (this.LoopCount.intValue == 0 || this.LoopCount.intValue < -1) this.LoopCount.intValue = 1;
            }
            EditorGUILayout.EndHorizontal();

            GUILayout.Box(GUIContent.none, GUILayout.ExpandWidth(true), GUILayout.Height(0.5f));

            EditorGUILayout.PropertyField(IsCustomEase);
            if (this.IsCustomEase.boolValue)
            {
                
                EditorGUILayout.PropertyField(TimeCurve);
                EditorGUILayout.HelpBox("注：自定义曲线 只会读取 x为[0-1]之间的数值", MessageType.Info);
            }
            else
            {
                EditorGUILayout.PropertyField(EaseType);
            }

            GUILayout.Space(10);
            GUILayout.Box(GUIContent.none, GUILayout.ExpandWidth(true), GUILayout.Height(0.5f));

            //EditorGUILayout.PropertyField(AutoStartValue);
            EditorGUILayout.PropertyField(StartCor);
            EditorGUILayout.PropertyField(EndCor);

            // 应用属性修改
            this.serializedObject.ApplyModifiedProperties();
        }
    }
}
