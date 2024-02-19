using HarmonyLib;

namespace TrueDeathPenalty.Patches;

[HarmonyPatch(typeof(HUDManager))]
public class HUDManagerPatch
{
    
    [HarmonyPatch(nameof(HUDManager.ApplyPenalty))]
    [HarmonyPostfix]
    // ReSharper disable InconsistentNaming
    public static void AdjustPenaltyTextPatch(int ___playersDead, int ___bodiesInsured, Terminal ___objectOfType, int groupCredits, EndOfGameStatUIElements ___statsUIElements)
    // ReSharper restore InconsistentNaming
    {
        Plugin.LogSource.LogInfo($"players dead: {___playersDead}, bodies insured: {___bodiesInsured}, object of type: {___objectOfType}, group credits: {groupCredits}, status ui elements: {___statsUIElements}");
        
        int penalty = (int)((1 - (float)___objectOfType.groupCredits / groupCredits) * 100f);
        
        ___statsUIElements.penaltyAddition.text = $"{(object)___playersDead} casualties: -{penalty}%\n({___bodiesInsured} bodies recovered)";
    }
}