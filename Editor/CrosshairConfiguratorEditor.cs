using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using SmartCrosshair;

[CustomEditor(typeof(CrosshairConfigurator))]
[CanEditMultipleObjects]
public class CrosshairConfiguratorEditor : Editor
{
    SerializedProperty _useGlobalSettings;
    SerializedProperty config;

    void OnEnable()
    {
        _useGlobalSettings = serializedObject.FindProperty("_useGlobalSettings");
        config = serializedObject.FindProperty("config");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(_useGlobalSettings);
        if (!_useGlobalSettings.boolValue)
        {
            EditorGUILayout.PropertyField(config);
        }

        (target as CrosshairConfigurator).ApplyCrosshair();
        serializedObject.ApplyModifiedProperties();
    }
}
