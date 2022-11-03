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
            EditorGUI.indentLevel++;
            CrosshairConfiguration configuration = (CrosshairConfiguration)config.objectReferenceValue;
            configuration.distance = EditorGUILayout.FloatField("Distance", configuration.distance);
            configuration.width = EditorGUILayout.FloatField("Width", configuration.width);
            configuration.length = EditorGUILayout.FloatField("Length", configuration.length);
            configuration.dot = EditorGUILayout.Toggle("Dot", configuration.dot);
            configuration.tshaped = EditorGUILayout.Toggle("Tshaped", configuration.tshaped);
            configuration.color = EditorGUILayout.ColorField("Color", configuration.color);
            EditorGUI.indentLevel--;
        }

        (target as CrosshairConfigurator).ApplyCrosshair();
        serializedObject.ApplyModifiedProperties();
    }
}
