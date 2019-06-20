using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MintAnimation.Editor
{
    [CanEditMultipleObjects]
    public class MintAnimation_EditorBase : UnityEditor.Editor
    {
        protected SerializedProperty AnimationInfo;

        private SerializedProperty Duration;

        private SerializedProperty IsAuto;

        private SerializedProperty IsBack;
        private SerializedProperty IsLoop;
        private SerializedProperty LoopCount;

        private SerializedProperty IsCustomEase;
        private SerializedProperty EaseType;
        private SerializedProperty TimeCurve;

        private SerializedProperty DriveType;
        private SerializedProperty UpdaterTypeEnum;
        private SerializedProperty CustomDrive; 

        protected SerializedProperty AutoStartValue;
        protected SerializedProperty CompleteAction;

        private bool foldoutType = true;

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
            Init();
        }

        protected virtual void Init()
        {
            AnimationInfo = this.serializedObject.FindProperty("MintAnimationOptions");
            Duration = AnimationInfo.FindPropertyRelative("Duration");
            IsAuto = this.serializedObject.FindProperty("IsAutoPlay");
            IsBack = AnimationInfo.FindPropertyRelative("IsBack");
            IsLoop = AnimationInfo.FindPropertyRelative("IsLoop");
            LoopCount = AnimationInfo.FindPropertyRelative("LoopCount");
            IsCustomEase = AnimationInfo.FindPropertyRelative("IsCustomEase");
            EaseType = AnimationInfo.FindPropertyRelative("EaseType");
            TimeCurve = AnimationInfo.FindPropertyRelative("TimeCurve");
            DriveType = AnimationInfo.FindPropertyRelative("DriveType");
            UpdaterTypeEnum = AnimationInfo.FindPropertyRelative("UpdaterTypeEnum");
            CustomDrive = AnimationInfo.FindPropertyRelative("CustomDrive");
            AutoStartValue = AnimationInfo.FindPropertyRelative("AutoStartValue");
            CompleteAction = this.serializedObject.FindProperty("CompleteAction");
        }


        public override void OnInspectorGUI()
        {
            // 更新显示
            this.serializedObject.Update();
            DrawTitle();
            this.foldoutType = EditorGUILayout.Foldout(foldoutType, "Mint Animation Info");
            if (this.foldoutType) {
                Draw();
            }
            // 应用属性修改
            this.serializedObject.ApplyModifiedProperties();
        }

        public virtual void Draw()
        {
            GUILayout.Space(15);
            EditorGUILayout.PropertyField(Duration);
            if (this.Duration.floatValue < 0) this.Duration.floatValue = 0;
            GUILayout.Box(GUIContent.none, GUILayout.ExpandWidth(true), GUILayout.Height(0.5f));

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

            EditorGUILayout.PropertyField(DriveType);
            if (DriveType.enumValueIndex == 0)
            {
                //custom
                EditorGUILayout.PropertyField(CustomDrive);
            }
            else
            {
                EditorGUILayout.PropertyField(UpdaterTypeEnum);
            }

            GUILayout.Space(10);
            GUILayout.Box(GUIContent.none, GUILayout.ExpandWidth(true), GUILayout.Height(0.5f));
            
            EditorGUILayout.PropertyField(CompleteAction);
            
            GUILayout.Space(10);
            GUILayout.Box(GUIContent.none, GUILayout.ExpandWidth(true), GUILayout.Height(0.5f));
        }

        protected virtual void DrawTitle()
        {
        }
    }
}
