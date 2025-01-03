using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Reflection;
using QRG.QuantumForge.Runtime;


namespace QRG.QuantumForge.Editor
{
    [CustomPropertyDrawer(typeof(BasisValueDropdownAttribute))]
    public class BasisValueDropdownAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            base.OnGUI(position, property, label);
            try
            {
                var errorStyle = new GUIStyle(EditorStyles.textField);
                errorStyle.normal.textColor = Color.red;

                //BasisValueAttribute attr = (BasisValueAttribute)attribute;
                EditorGUI.LabelField(position, property.name, $"The List is empty", errorStyle);

                // Look for basis sibling
                var baseMaster = property.serializedObject.targetObject;
                var listObj = ReflectionSystem.GetValue(baseMaster, "basis.values");
                if (listObj != null)
                {
                    IList ilist = (IList)listObj;
                    DrawListDropdown(position, property, ilist);
                    return;
                }

                // No basis sibling found, look for QuantumProperty

                // parse propertyPath
                int idx = property.propertyPath.LastIndexOf('.');

                var parentPath = idx == -1 ? "" : property.propertyPath.Substring(0, idx);
                Debug.Log($"ParentPath: {parentPath}");

                // find a Basis or QuantumProperty in the parent
                var parentProperty = parentPath == "" ? property : property.serializedObject.FindProperty(parentPath);
                Debug.Log($"ParentProperty: {parentProperty.name} Type: {parentProperty.propertyType} Type2: {parentProperty.type}");

                if (parentProperty == null)
                {
                    EditorGUI.LabelField(position, property.name, $"The object containing Basis: \"{parentPath}\" could not be found", errorStyle);
                    return;
                }

                do
                {

                    if (parentProperty.type.Contains("QuantumProperty"))
                    {
                        //Debug.Log($"QuantumProperty: {parentProperty.name} Type: {parentProperty.propertyType} Type2: {parentProperty.type}");
                        // FindPropertyRelative does not work with QuantumProperty, not entirely sure why.
                        // Creating a new SerializedObject and finding the property works.
                        // https://discussions.unity.com/t/solved-property-drawer-findpropertyrelative-does-not-work-within-serialized-scriptable-objects/746350/6
                        // See comment from Bunny83
                        var qp = new SerializedObject(parentProperty.objectReferenceValue);
                        var basis = qp.FindProperty("basis");
                        //Debug.Log($"Basis: {basis.name} Type: {basis.propertyType} Type2: {basis.type}");
                        var basisObj = new SerializedObject(basis.objectReferenceValue);

                        DrawBasisDropdown(position, property, basisObj);
                        return;
                    }
                    
                } while (parentProperty.Next(true));

            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }

        }

        private static void DrawBasisDropdown(Rect position, SerializedProperty property, SerializedObject basisObj)
        {
            object baseMaster = basisObj.targetObject;
            object listObj = ReflectionSystem.GetValue(baseMaster, "values");
            IList ilist = (IList)listObj;

            DrawListDropdown(position, property, ilist);
        }

        private static void DrawListDropdown(Rect position, SerializedProperty property, IList ilist)
        {
            //Debug.Log($"List Count: {ilist.Count}");
            for (int i = 0; i < ilist.Count; ++i)
            {
                var value = ilist[i];
                BasisValue bv = (BasisValue)value;
                //Debug.Log($"List Value {i} name: {bv.Name}");
            }

            string[] names = ilist.Cast<BasisValue>().Select(x => x.Name).ToArray();

            object target = property.GetValue();
            int selectedID = 0;
            for (int i = 0; i < ilist.Count; ++i)
            {
                var value = ilist[i];
                BasisValue bv = (BasisValue)value;
                BasisValue targetBV = (BasisValue)target;
                if (bv.Name == targetBV.Name)
                {
                    selectedID = i;
                    break;
                }
            }

            int newSelectedID = EditorGUI.Popup(position, property.name, selectedID, names);
            if (newSelectedID != selectedID)
            {
                selectedID = newSelectedID;
                property.SetValue(ilist[selectedID]);
                EditorUtility.SetDirty(property.serializedObject.targetObject);
            }

            BasisValue tbv = (BasisValue)property.GetValue();
            Debug.Log($"Selected ID: {selectedID} BasisValue: {tbv.Name}");
        }
    }
}
