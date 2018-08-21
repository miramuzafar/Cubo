using UnityEditor;
using UnityEditor.UI;
 
[CustomEditor(typeof(AeButton), true)]
public class AeButtonEditor : ButtonEditor
{
    SerializedProperty _onDownProperty;
 
    protected override void OnEnable()
    {
        base.OnEnable();
        _onDownProperty = serializedObject.FindProperty("_onDown");
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();

        serializedObject.Update();
        EditorGUILayout.PropertyField(_onDownProperty);
        serializedObject.ApplyModifiedProperties();
    }
}