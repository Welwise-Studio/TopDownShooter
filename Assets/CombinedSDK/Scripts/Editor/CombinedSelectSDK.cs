using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class CombinedSelectSDK
{
    const string define_YG = "YANDEX_GAMES";
    const string define_YGM = "YANDEX_GAMES_MEDIATION";
    const string define_CG = "CRAZY_GAMES";
    const string define_GD = "GAME_DISTRIBUTION";
    const string define_UA = "UNITY_ADS";
    const string define_GP = "GAME_PUSH";

    public static void SwitchSelectedSDK()
    {
        CombinedSDKSettings settings = Resources.Load<CombinedSDKSettings>("CombinedSDKSettings");
        if (settings == null)
        {
            Debug.Log(
                "Файл CombinedSDKSettings не обнаружен, пожалуйста загрузите CombinedSDK корректно");
            return;
        }
        switch (settings.selectSDK)
        {
            case CombinedSDKSettings.CombinedSDKSettingsSelectSDK.None:
                ForeachAllSDK("None");
                break;
            case CombinedSDKSettings.CombinedSDKSettingsSelectSDK.YandexGames:
                AddDefine(define_YG);
                ForeachAllSDK(define_YG);
                break;
            case CombinedSDKSettings.CombinedSDKSettingsSelectSDK.YandexGamesMediation:
                AddDefine(define_YGM);
                ForeachAllSDK(define_YGM);
                break;
            case CombinedSDKSettings.CombinedSDKSettingsSelectSDK.CrazyGames:
                AddDefine(define_CG);
                ForeachAllSDK(define_CG);
                break;
            case CombinedSDKSettings.CombinedSDKSettingsSelectSDK.GameDistribution:
                AddDefine(define_GD);
                ForeachAllSDK(define_GD);
                break;
            case CombinedSDKSettings.CombinedSDKSettingsSelectSDK.UnityADS:
                AddDefine(define_UA);
                ForeachAllSDK(define_UA);
                break;
            case CombinedSDKSettings.CombinedSDKSettingsSelectSDK.GamePush:
                AddDefine(define_GP);
                ForeachAllSDK(define_GP);
                break;
        }
    }

    private static void ForeachAllSDK(string dontRemove)
    {
        Debug.Log("<color=yellow>[CombinedSDK] Вы используете " + dontRemove + " SDK</color>");

        if (define_YG != dontRemove && CheckDefine(define_YG))
            RemoveDefine(define_YG);
        if (define_YGM != dontRemove && CheckDefine(define_YGM))
            RemoveDefine(define_YGM);
        if (define_CG != dontRemove && CheckDefine(define_CG))
            RemoveDefine(define_CG);
        if (define_GD != dontRemove && CheckDefine(define_GD))
            RemoveDefine(define_GD);
        if (define_UA != dontRemove && CheckDefine(define_UA))
            RemoveDefine(define_UA);
        if (define_GP != dontRemove && CheckDefine(define_GP))
            RemoveDefine(define_GP);
    }

    private static bool CheckDefine(string define)
    {
        BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
        string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);

        if (defines.Contains(define))
        {
            return true;
        }
        else return false;
    }

    private static void AddDefine(string define)
    {
        BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
        string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);

        if (defines.Contains(define))
            return;

        PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, (defines + ";" + define));
    }

    private static void RemoveDefine(string define)
    {
        BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
        string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);

        if (defines.Contains(define))
        {
            string[] defineArray = defines.Split(';');

            List<string> updatedDefines = new List<string>();
            foreach (string d in defineArray)
            {
                if (d != define)
                {
                    updatedDefines.Add(d);
                }
            }

            string newDefines = string.Join(";", updatedDefines);
            PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, newDefines);
        }
    }
}