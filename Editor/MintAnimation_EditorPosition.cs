using UnityEngine;
using UnityEditor;

namespace MintAnimation.Editor
{
    [CustomEditor(typeof(MintAnimation.MintAnimation_Position), true), CanEditMultipleObjects]
    public class MintAnimation_EditorPosition : MintAnimation_EditorBase
    {
        private SerializedProperty IsLocalPosition;
        private SerializedProperty IsRectPosition;
        private SerializedProperty StartPosition;
        private SerializedProperty EndPosition;
        
        private SerializedProperty IsBezier;
        private SerializedProperty BezierP1;
        private SerializedProperty BezierP2;

        protected override void Init()
        {
            base.Init();
            IsLocalPosition = serializedObject.FindProperty("IsLocal");
            this.IsRectPosition = serializedObject.FindProperty("IsRectPosition");
            StartPosition = MintAnimData.FindPropertyRelative("StartValue");
            EndPosition = MintAnimData.FindPropertyRelative("EndValue");
            
            IsBezier = serializedObject.FindProperty("IsBezier");
            BezierP1 = serializedObject.FindProperty("BezierP1");
            BezierP2 = serializedObject.FindProperty("BezierP2");
        }

        protected override void DrawTitle()
        {
            GUIStyle gUIStyle = new GUIStyle();
            gUIStyle.fontSize = 18;
            gUIStyle.normal.textColor = new Color32(56, 56, 56, 255);
            gUIStyle.normal.background = MintAnimation_EditorBase.GetTexture2D(new Color32(255, 236, 0, 255));
            gUIStyle.alignment = TextAnchor.MiddleCenter;
            gUIStyle.margin = new RectOffset(0, 0, 8, 0);
            GUILayout.Box(" Mint Position ", gUIStyle);
            gUIStyle = null;
        }

        public override void Draw()
        {
            base.Draw();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(AutoStartValue);
           
            IsLocalPosition.boolValue = GUILayout.Toolbar(IsLocalPosition.boolValue ? 0 : 1,
                new string[2] { "Local", "World" },
                new GUILayoutOption[] {
                GUILayout.Width(120),
                GUILayout.Height(20),
            })
            == 0 ? true : false;
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.PropertyField(this.IsRectPosition);
            GUILayout.Space(10);
            
            if (!AutoStartValue.boolValue)
            {
                EditorGUILayout.PropertyField(StartPosition , new GUIContent("StartPostion"));
            }
            EditorGUILayout.PropertyField(EndPosition, new GUIContent("EndPostion"));
           
            GUILayout.Box(GUIContent.none, GUILayout.ExpandWidth(true), GUILayout.Height(0.5f));
            
            GUILayout.Space(10);
            EditorGUILayout.PropertyField(this.IsBezier);
            if (this.IsBezier.boolValue)
            {
                EditorGUILayout.PropertyField(this.BezierP1);
                EditorGUILayout.PropertyField(this.BezierP2);
                EditorGUILayout.HelpBox("注：P1,P2 为StartValue的相对坐标", MessageType.Info);
            }
            
            GUILayout.Box(GUIContent.none, GUILayout.ExpandWidth(true), GUILayout.Height(0.5f));
        }
    }
}
