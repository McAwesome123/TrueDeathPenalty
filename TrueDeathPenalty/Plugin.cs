using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace TrueDeathPenalty;



[BepInPlugin(pluginGuid, pluginName, pluginVersion)]
public class Plugin : BaseUnityPlugin
{
    private const string pluginGuid = "Lexderi.TrueDeathPenalty";
    private const string pluginName = "True Death Penalty";
    private const string pluginVersion = "1.0.0";
    
    private readonly Harmony harmony = new(pluginGuid);
        
    public static ManualLogSource LogSource => Instance.Logger;
        
    public static Plugin Instance { get; private set; }
        
    private void Awake()
    {
        // Plugin startup logic
        Logger.LogInfo($"Plugin {pluginGuid} is loaded!");

        // Create Singleton
        if (Instance == null) Instance = this;
        else Logger.LogWarning("There should not be two instances of mod " + pluginGuid);
            
        harmony.PatchAll();
    }
}