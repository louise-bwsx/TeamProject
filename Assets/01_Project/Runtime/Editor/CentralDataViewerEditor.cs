using UnityEditor;
[CustomEditor(typeof(CentralDataViewer))]
public class CentralDataViewerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        CentralDataViewer mTarget = (CentralDataViewer)target;
        EditorGUILayout.LabelField("BGM", mTarget.BGMVol.ToString());
        EditorGUILayout.LabelField("SFX", mTarget.SFXVol.ToString());
    }
}
