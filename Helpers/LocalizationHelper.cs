using System.Collections;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using Winch.Core;

namespace Randomizer.Helpers;

internal class LocalizationHelper
{
    private static LocalizationComponent? _component;
    private static IEnumerator GetAllTables(Locale locale)
    {
        var asyncOperationHandle = LocalizationSettings.StringDatabase.GetAllTables(locale);
        yield return asyncOperationHandle;

        var tables = asyncOperationHandle.Result.ToList();
        foreach (var table in tables)
        {
            WinchCore.Log.Debug("table: " + table.TableCollectionName);
        }
    }

    public static void GetAllTables()
    {
        if (_component == null)
            _component = Loader.gameObject.AddComponent<LocalizationComponent>();
    }

    private class LocalizationComponent : MonoBehaviour
    {
        void Start()
        {
            StartCoroutine(GetAllTables(LocalizationSettings.SelectedLocale));
        }
    }

    /* 
     * Tables:
     *  - Characters
     *  - Items
     *  - Quests
     *  - Strings
     *  - Yarn
     * 
     * 
     */
}
