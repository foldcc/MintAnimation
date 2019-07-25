using UnityEngine;
using UnityEditor;

namespace MintAnimation.Editor
{
    [CustomEditor(typeof(MintAnimation.MintAnimation_Color), true), CanEditMultipleObjects]
    public class MintAnimation_EditorColor : MintAnimation_EditorBase
    {
        protected override void Init()
        {
            base.Init();
            this.AutoStartValue.boolValue = false;
        }

        public override void Draw()
        {
            base.Draw();
            EditorGUILayout.PropertyField(this.MintAnimData.FindPropertyRelative("StartValue"));
            EditorGUILayout.PropertyField(this.MintAnimData.FindPropertyRelative("EndValue"));
        }

        protected override void DrawTitle()
        {
            GUIStyle gUIStyle = new GUIStyle();
            gUIStyle.fontSize = 18;
            gUIStyle.normal.textColor = new Color32(56, 56, 56, 255);
            gUIStyle.normal.background = MintAnimation_EditorBase.GetTexture2D(new Color32(0, 255, 198, 255));
            gUIStyle.alignment = TextAnchor.MiddleCenter;
            gUIStyle.margin = new RectOffset(0, 0, 8, 0);
            GUILayout.Box(" Mint Color ", gUIStyle);
            gUIStyle = null;
        }
    }
}

    