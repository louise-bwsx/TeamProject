using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(CentralDataViewer))]
public class CentralDataViewerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        CentralDataViewer mTarget = (CentralDataViewer)target;
        EditorGUILayout.LabelField("Audio", mTarget.AudioVol.ToString());

    }
}
