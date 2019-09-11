using UnityEngine;
using UnityEditor;

namespace MintAnimation.Editor
{
    [CustomEditor(typeof(MintAnimation.MintAnimation_Scale), true), CanEditMultipleObjects]
    public class MintAnimation_EditorScale : MintAnimation_EditorBase
    {
        private SerializedProperty StartScale;
        private SerializedProperty EndScale;

        private SerializedProperty IsSizeDelta;

        protected override void Init()
        {
            base.Init();
            StartScale =  MintAnimData.FindPropertyRelative("StartValue");
            EndScale =  MintAnimData.FindPropertyRelative("EndValue");
            IsSizeDelta = serializedObject.FindProperty("IsSizeDelta");
        }

        protected override void DrawTitle()
        {
            GUIStyle gUIStyle = new GUIStyle();
            gUIStyle.fontSize = 18;
            gUIStyle.normal.textColor = new Color32(56, 56, 56, 255);
            gUIStyle.normal.background = MintAnimation_EditorBase.GetTexture2D(new Color32(0, 180, 255, 255));
            gUIStyle.alignment = TextAnchor.MiddleCenter;
            gUIStyle.margin = new RectOffset(0, 0, 8, 0);
            GUILayout.Box(" Mint Scale ", gUIStyle);
            gUIStyle = null;
        }

        public override void Draw()
        {
            base.Draw();

            EditorGUILayout.PropertyField(AutoStartValue);
            EditorGUILayout.PropertyField(this.IsSizeDelta);
            if (!AutoStartValue.boolValue)
            {
                EditorGUILayout.PropertyField(StartScale, new GUIContent("StartScale"));
            }
            EditorGUILayout.PropertyField(EndScale, new GUIContent("EndScale"));

            GUILayout.Box(GUIContent.none, GUILayout.ExpandWidth(true), GUILayout.Height(0.5f));

        }
    }
}

