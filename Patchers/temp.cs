/*
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Localization.Tables;
using Winch.Core;
using Yarn.Unity;

namespace Randomizer.Patchers;

[HarmonyPatch(typeof(DredgeDialogueRunner))]
[HarmonyPatch(nameof(DredgeDialogueRunner.StartDialogue))]
class temp
{
    public static void Postfix(string nodeName, DredgeDialogueRunner __instance)
    {
        WinchCore.Log.Debug("Started dialogue: " + nodeName);
        WinchCore.Log.Debug(__instance.inMemoryVariableStorage);
        Type storageType = typeof(InMemoryVariableStorage);
        FieldInfo variablesField = storageType.GetField("variables", BindingFlags.NonPublic | BindingFlags.Instance);
        WinchCore.Log.Debug("tried:" + variablesField);
        if (variablesField != null)
        {
            var variables = (Dictionary<string, object>)variablesField.GetValue(__instance.inMemoryVariableStorage);
            WinchCore.Log.Debug(variables);

            // Print all variables
            foreach (var kvp in variables)
            {
                WinchCore.Log.Debug($"Variable Name: {kvp.Key}, Value: {kvp.Value}");
            }
        }
        else
        {
            WinchCore.Log.Debug("Could not find the 'variables' field.");
        }

        DredgeDialogueRunner runner = GameManager.Instance.DialogueRunner;
        /*
        DredgeLocalizedLineProvider lineProvider = runner.lineProvider as DredgeLocalizedLineProvider;
        StringTable table = lineProvider.stringTable;
        WinchCore.Log.Debug("----------------");
        WinchCore.Log.Debug(lineProvider.YarnProject);
        WinchCore.Log.Debug(lineProvider.YarnProject.searchAssembliesForActions);
        WinchCore.Log.Debug(table.ToString());
        WinchCore.Log.Debug("FOUND: " + table.GetEntry("PackageDelivery_1"));
        /
    }
}


*/