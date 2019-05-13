using UnityEngine;
using UnityEditor;

namespace MintAnimation
{
    [CanEditMultipleObjects, CustomEditor(typeof(MintAnimation_Base<Vector3>), true)]
    public class MintAnimation_EditorPosition : MintAnimation_EditorBase
    {
        private SerializedProperty IsLocalPosition;
        private SerializedProperty StartPosition;
        private SerializedProperty EndPosition;

        protected override void Init()
        {
            base.Init();
            IsLocalPosition = serializedObject.FindProperty("IsLocal");
            StartPosition = AnimationInfo.FindPropertyRelative("StartV3");
            EndPosition = AnimationInfo.FindPropertyRelative("EndV3");
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
            GUILayout.Space(10);

            if (!AutoStartValue.boolValue)
            {
                EditorGUILayout.PropertyField(StartPosition , new GUIContent("StartPostion"));
            }
            EditorGUILayout.PropertyField(EndPosition, new GUIContent("EndPostion"));

            GUILayout.Box(GUIContent.none, GUILayout.ExpandWidth(true), GUILayout.Height(0.5f));

            
            
        }
    }
}
