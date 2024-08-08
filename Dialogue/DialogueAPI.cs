using System;
using System.Globalization;
using FluffyUnderware.DevTools.Extensions;
using HarmonyLib;
using Randomizer.Randomizers;
using UnityEngine;
using UnityEngine.Networking.Types;
using Winch.Core;
using Yarn;

namespace Dialogue;

public class DialogueAPI : MonoBehaviour
{
    public void Awake()
    {
        WinchCore.Log.Debug($"{nameof(DialogueAPI)} has loaded!");
        try
        {
            DialogueLoader.LoadDialogues();
        }
        catch (Exception ex)
        {
            WinchCore.Log.Error(ex);
        }

        GameManager.Instance.OnGameStarted += GameStarted;

        WinchCore.Log.Debug("Success!");
    }

    public void GameStarted()
    {
        Inject();
    }

    public void Inject()
    {
        try
        {
            DialogueLoader.Inject();
            WinchCore.Log.Debug("Injected");

            DredgeDialogueRunner runner = GameManager.Instance.DialogueRunner;
            Program program = Traverse.Create(runner.Dialogue).Field("program").GetValue<Program>();

            // DumpYarnDialogue();

            QuestRandomizer.RandomizeQuests();

                // DialogueLoader.AddInstruction("TravellingMerchant_ChatOptions", 1, Instruction.Types.OpCode.AddOption, "line:01f8b99", "L84shortcutoption_TravellingMerchant_ChatOptions_6", 0, false);
                // DialogueLoader.ReplaceInstruction("TravellingMerchant_ChatOptions", 38, Instruction.Types.OpCode.AddOption, "line:06bbdc5", "L82shortcutoption_TravellingMerchant_ChatOptions_4", 0, false);
            }
        catch (Exception e)
        {
            WinchCore.Log.Error(e);
        }
    }

    public static void Initialize()
    {
        var gameObject = new GameObject(nameof(DialogueAPI));
        gameObject.AddComponent<DialogueAPI>();
        GameObject.DontDestroyOnLoad(gameObject);
    }

    public static void DumpYarnDialogue(string fileName = "yarn_dump.txt")
    {
        DredgeDialogueRunner runner = GameManager.Instance.DialogueRunner;
        Program program = Traverse.Create(runner.Dialogue).Field("program").GetValue<Program>();

        var sb = new System.Text.StringBuilder();

        foreach (var entry in program.Nodes)
        {
            sb.AppendLine("Node " + entry.Key + ":");

            int instructionCount = 0;
            foreach (var instruction in entry.Value.Instructions)
            {
                string instructionText;

                instructionText = "    " + instruction.ToString();

                string preface;

                if (instructionCount % 5 == 0 || instructionCount == entry.Value.Instructions.Count - 1)
                {
                    preface = string.Format(CultureInfo.InvariantCulture, "{0,6}   ", instructionCount);
                }
                else
                {
                    preface = string.Format(CultureInfo.InvariantCulture, "{0,6}   ", " ");
                }

                sb.AppendLine(preface + instructionText);

                instructionCount++;
            }

            sb.AppendLine();
        }

        using (StreamWriter outputFile = new(Path.Combine(fileName)))
            outputFile.WriteLine(sb.ToString());

        /*
         * turns out there was a prebuilt script to dump it to string so not using this anymore, but maybe it'll end up useful
         * 
        using (StreamWriter outputFile = new(Path.Combine(fileName)))
        {
            List<string> output = new();
            foreach (var node in program.Nodes)
            {
                var value = node.Value;
                outputFile.WriteLine(node.Value.Name + ":");
                foreach (var instruction in value.Instructions.Select((x, i) => new { Value = x, Index = i }))
                {
                    outputFile.Write("\t");
                    if (instruction.Index % 5 == 0 || instruction.Index == value.Instructions.Count - 1)
                    {
                        outputFile.Write(instruction.Index.ToString().PadRight(4));
                    }
                    else
                    {
                        outputFile.Write("".PadRight(4));
                    }
                    outputFile.Write(instruction.Value.Opcode.ToString().PadRight(16));
                    foreach (var operand in instruction.Value.Operands)
                    {
                        outputFile.Write(operand + " ");
                    }
                    outputFile.WriteLine();
                }
                outputFile.WriteLine();
            }
        }
        */
    }
}