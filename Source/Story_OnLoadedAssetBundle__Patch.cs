using HarmonyLib;
using UnityEngine;
using static SISClothingDurability_Config;

[HarmonyPatch(typeof(Story), "OnLoadedAssetBundle")]
public class Story_OnLoadedAssetBundle__Patch
{
    private static void Postfix(Story __instance, AssetBundle assetBundle)
    {
        if (assetBundle.name == "clothingbreaks_sounds")
        {
            foreach (Resource<AudioClip> resource in __instance.Sounds)
            {
                Log("OnLoadedAssetBundle - Adding sound " + resource.GetPath());
                SoundsClothingBroken.Add(resource);
            }
        }
    }
}
