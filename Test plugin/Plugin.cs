using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace Test_plugin;
[BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
[BepInProcess("DDSS.exe")]
public class Plugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;

    private void __internalAwake ()
    {
        // Plugin startup logic
        Logger = base.Logger;
        var harmony = new Harmony("FreeItems");
        harmony.PatchAll();
        Logger.LogInfo($"Plugin {PluginInfo.Name} is loaded!");
    }
}

[HarmonyPatch(typeof(Currency.ItemManager))]
public class FreeItemsPatch
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(Currency.ItemManager.SubtractCurrency))]
    public static bool SubtractPatch (ref bool __result, int amount)
    {
        __result = true;
        return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch("IsItemOwned")]
    public static bool ItemOwnedPatch(ref bool __result)
    {
        __result = true;
        return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch("IsColorItemOwned")]
    public static bool ColorItemOwnedPatch (ref bool __result)
    {
        __result = true;
        return false;
    }
}

public static class PluginInfo
{
    public const string GUID = "com.boxfriend.freeitems";
    public const string Name = "Free Items";
    public const string Version = "1.0.0";
}
