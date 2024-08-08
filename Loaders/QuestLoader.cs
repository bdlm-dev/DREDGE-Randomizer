using Randomizer.Randomizers;
using Winch.Core.API.Events.Addressables;
using Winch.Core;
using Yarn.Unity;
using UnityEngine.Localization.Tables;
using Randomizer.Helpers;

namespace Randomizer.Loaders;

class QuestLoader
{
    public static void PostQuestLoad(object sender, AddressablesLoadedEventArgs<QuestData> args)
    {
        try
        {
            var allQuests = (sender as DataLoader).allQuests;
            var allQuestSteps = (sender as DataLoader).allQuestSteps;

            QuestRandomizer.RandomizeQuestSteps(allQuestSteps);
            QuestRandomizer.RandomizeQuestGrids();
        } 
        catch (Exception e)
        {
            WinchCore.Log.Error("Error randomizing quests: " + e);
        }

        foreach (KeyValuePair<string, QuestData> quest in (sender as DataLoader).allQuests)
        {
            foreach (QuestStepData step in quest.Value.steps)
            {
                foreach (QuestStepCondition condition in step.showConditions)
                {

                }
            }
        }


       /*
       (sender as DataLoader).allQuests.ForEach(x =>
       {
           WinchCore.Log.Debug(x.Key);
           foreach (PropertyDescriptor d in TypeDescriptor.GetProperties(x.Value))
           {
               WinchCore.Log.Debug(String.Format("{0}:{1}", d.Name, d.GetValue(x.Value)));
           }
           WinchCore.Log.Debug(x.Value);
           x.Value.steps.ForEach(y =>
           {
               WinchCore.Log.Debug("step: " + y.name);
               foreach (QuestStepCondition cond in y.completeConditions)
               {
                   WinchCore.Log.Debug("condition: " + cond.ToString());
               }
               foreach (PropertyDescriptor d in TypeDescriptor.GetProperties(y))
               {
                   WinchCore.Log.Debug(String.Format("step: {0}:{1}", d.Name, d.GetValue(y)));
               }
           });
       });
       */
    }
}