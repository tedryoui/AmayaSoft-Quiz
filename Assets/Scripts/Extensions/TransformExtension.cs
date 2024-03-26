using UnityEditor;
using UnityEngine;

namespace Quiz.Extensions
{
    public static class TransformExtension
    {
        public static void ClearChildren(this Transform transform)
        {
            var childCount = transform.childCount;
            
            for (int i = childCount - 1; i >= 0; i--)
            {
#if UNITY_EDITOR
                if (!EditorApplication.isPlaying)
                    Object.DestroyImmediate(transform.GetChild(i).gameObject);
                else
                    Object.Destroy(transform.GetChild(i).gameObject);
#else
                Object.Destroy(transform.GetChild(i).gameObject);
#endif    
            }
        }
    }
}