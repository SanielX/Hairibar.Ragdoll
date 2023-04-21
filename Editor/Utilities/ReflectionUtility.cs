using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Hairibar.Ragdoll.Editor
{
    internal static class ReflectionUtility
    {
        public static PropertyInfo GetProperty(UnityEngine.Object serializedObject, string name)
        {
            return serializedObject.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        }
        
        public static FieldInfo GetField(UnityEngine.Object serializedObject, string name)
        {
            return serializedObject.GetType().GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public static MethodInfo GetMethod(Object target, string methodName)
        {
            return target.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        }
    }
}