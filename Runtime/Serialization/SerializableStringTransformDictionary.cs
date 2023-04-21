using UnityEngine;

#pragma warning disable CA2229 // Implement serialization constructors
namespace Hairibar.EngineExtensions.Serialization
{
    [System.Serializable]
    internal class SerializableStringTransformDictionary : SerializableDictionary<string, Transform> { }
}