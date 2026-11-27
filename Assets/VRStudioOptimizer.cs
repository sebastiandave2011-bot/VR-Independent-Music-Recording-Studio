#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;

/// <summary>
/// Editor utility to batch-optimize ALL scene MeshRenderers and project 3D Models for Meta Quest VR.
/// </summary>
public class VRStudioOptimizer : EditorWindow
{
    [MenuItem("Tools/Optimizar Estudio VR/1. Optimizar TODA la Escena")]
    public static void OptimizeEntireScene()
    {
        // Finds every MeshRenderer in the active scene, including inactive ones
        MeshRenderer[] allRenderers = Object.FindObjectsByType<MeshRenderer>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        int modifiedCount = 0;

        foreach (MeshRenderer mr in allRenderers)
        {
            // Skip player hands or controllers inside XR Origin
            if (mr.transform.root.name.Contains("XR Origin")) continue;

            Undo.RecordObject(mr, "Optimize Scene MeshRenderer for VR");

            // Disable realtime shadow casting on scene objects
            mr.shadowCastingMode = ShadowCastingMode.Off;

            // Reduce lightmap texel footprint
            SerializedObject so = new SerializedObject(mr);
            SerializedProperty scaleProp = so.FindProperty("m_ScaleInLightmap");
            if (scaleProp != null)
            {
                scaleProp.floatValue = 0.25f;
                so.ApplyModifiedProperties();
            }

            EditorUtility.SetDirty(mr);
            modifiedCount++;
        }

        Debug.Log($"[VR Optimizer] ¡Éxito! Se optimizaron {modifiedCount} MeshRenderers en TODA la escena (Cast Shadows = Off, Scale In Lightmap = 0.25).");
    }

    [MenuItem("Tools/Optimizar Estudio VR/2. Comprimir Todos los Modelos 3D (Assets)")]
    public static void CompressAllProjectModels()
    {
        string[] modelGuids = AssetDatabase.FindAssets("t:Model", new[] { "Assets" });
        int compressedCount = 0;

        try
        {
            AssetDatabase.StartAssetEditing();
            for (int i = 0; i < modelGuids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(modelGuids[i]);

                // Skip XR packages or internal template folders
                if (path.Contains("XR") || path.Contains("VRTemplate")) continue;

                ModelImporter importer = AssetImporter.GetAtPath(path) as ModelImporter;
                if (importer != null)
                {
                    bool changed = false;

                    if (importer.meshCompression != ModelImporterMeshCompression.High)
                    {
                        importer.meshCompression = ModelImporterMeshCompression.High;
                        changed = true;
                    }

                    if (importer.isReadable)
                    {
                        importer.isReadable = false;
                        changed = true;
                    }

                    if (changed)
                    {
                        importer.SaveAndReimport();
                        compressedCount++;
                    }
                }
            }
        }
        finally
        {
            AssetDatabase.StopAssetEditing();
            AssetDatabase.Refresh();
        }

        Debug.Log($"[VR Optimizer] ¡Éxito! Se comprimieron {compressedCount} modelos 3D a Mesh Compression High y se desactivó Read/Write.");
    }
}
#endif