using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ADAPT.UI
{
    //[CustomPropertyDrawer(typeof(IResource), true)]
    public class ADAPT_UI_Resources_Any : Editor
    {
        public override void OnInspectorGUI()
        {
            //EditorGUILayout.PropertyField(serializedObject.FindProperty("resourceName"));
        }
    }
}
