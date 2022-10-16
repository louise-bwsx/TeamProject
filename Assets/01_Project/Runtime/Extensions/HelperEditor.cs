using UnityEditor;
using System.Reflection;
using System;

//不能放在Editor資料夾 因為Assembly找不到
//創了Editor的Assembly會One or more cyclic dependencies detected between assemblies
public static class HelperEditor
{
    private static MethodInfo _clearConsoleMethod;
    private static MethodInfo ClearConsoleMethod
    {
        get
        {
#if UNITY_EDITOR
            if (_clearConsoleMethod == null)
            {
                Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
                Type logEntries = assembly.GetType("UnityEditor.LogEntries");
                _clearConsoleMethod = logEntries.GetMethod("Clear");
            }
#endif
            return _clearConsoleMethod;
        }
    }

    public static void ClearConsoleLog()
    {
        ClearConsoleMethod.Invoke(new object(), null);
    }
}