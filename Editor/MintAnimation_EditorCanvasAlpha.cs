using UnityEditor;
using UnityEngine;

namespace MintAnimation.Editor {

    [CustomEditor(typeof(MintAnimation.MintAnimation_CanvasAlpha), true), CanEditMultipleObjects]
    public class MintAnimation_EditorCanvasAlpha : MintAnimation_EditorBase
    {
        private SerializedProperty StartAlpha;
        private SerializedProperty EndAlpha;

        protected override void Init()
        {
            base.Init();
            StartAlpha =  MintAnimData.FindPropertyRelative("StartValue");
            EndAlpha =    MintAnimData.FindPropertyRelative("EndValue");
        }


        protected override void DrawTitle()
        {
            GUIStyle gUIStyle = new GUIStyle();
            gUIStyle.fontSize = 18;
            gUIStyle.normal.textColor = new Color32(56, 56, 56, 255);
            gUIStyle.normal.background = MintAnimation_EditorBase.GetTexture2D(new Color32(138, 255, 0, 255));
            gUIStyle.alignment = TextAnchor.MiddleCenter;
            gUIStyle.margin = new RectOffset(0, 0, 8, 0);
            GUILayout.Box(" Mint Canvas Alpha ", gUIStyle);
            gUIStyle = null;
        }

        public override void Draw()
        {
            base.Draw();

            EditorGUILayout.PropertyField(AutoStartValue);
            GUILayout.Space(10);

            if (!AutoStartValue.boolValue)
            {
                EditorGUILayout.Slider(StartAlpha, 0 , 1 , new GUIContent("StartAlpha"));
            }
            EditorGUILayout.Slider(EndAlpha, 0, 1, new GUIContent("EndAlpha"));
        }
    }
}
