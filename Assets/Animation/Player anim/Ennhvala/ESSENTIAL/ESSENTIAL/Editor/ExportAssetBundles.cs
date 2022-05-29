using UnityEngine;
using UnityEditor;

public class BuildAssetBundlesExample : MonoBehaviour
{
    [MenuItem("Bundle/Build AssetBundles")]
    static void BuildABs()
    {
		//puts the bundles in the project folder
        BuildPipeline.BuildAssetBundles(Application.dataPath, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows); 
    }
}