#if UNITY_EDITOR
// Create an AssetBundle for Windows.
using UnityEngine;
using UnityEditor;

public class BuildAssetBundlesExample : MonoBehaviour
{
    [MenuItem("Asset Builder/Build Asset Bundles")]
    static void BuildABs()
    {
        BuildPipeline.BuildAssetBundles("Assets/exported", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }
}

#endif