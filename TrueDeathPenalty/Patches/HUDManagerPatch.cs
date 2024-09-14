using System;
using HarmonyLib;

namespace TrueDeathPenalty.Patches;

[HarmonyPatch(typeof(HUDManager))]
public class HUDManagerPatch
{
    private static int prevGroupCredits;

    [HarmonyPatch("ApplyPenalty")]
    [HarmonyPrefix]
    public static void SavePreviousGroupCredits(Terminal ___terminalScript)
    {
        prevGroupCredits = ___terminalScript.groupCredits;
    }

    [HarmonyPatch("ApplyPenalty")]
    [HarmonyPostfix]
    // ReSharper disable InconsistentNaming
    public static void AdjustPenaltyTextPatch(int playersDead, int bodiesInsured, EndOfGameStatUIElements ___statsUIElements, Terminal ___terminalScript)
    // ReSharper restore InconsistentNaming
    {
        int penalty =  (int)Math.Round((1 - (float)___terminalScript.groupCredits / Math.Max(prevGroupCredits, 1) ) * 100f);

        ___statsUIElements.penaltyAddition.text = $"{playersDead} casualties: -{penalty}%\n({bodiesInsured} bodies recovered)";
    }
}