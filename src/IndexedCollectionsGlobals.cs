#region

using System;
using UnityEditor;

#endregion

namespace collections.src
{
#if UNITY_EDITOR

    [InitializeOnLoad]
    public static class IndexedCollectionsGlobals
    {
        static IndexedCollectionsGlobals()
        {
            FrameStart.Delegates.onEnable += Initialize;
        }

        static void Initialize()
        {
            _UI_DEBUG.WakeUp();
        }
        

#region MENU_ENABLE_

        private const string G_ = "Indexed Collections";
        private const string MENU_BASE_ = "Tools/" + G_ + "/";
        private const string MENU_UI_DEBUG_ = MENU_BASE_ + "Show All Fields";

        [NonSerialized] public static readonly PREF<bool> _UI_DEBUG = PREFS.REG(G_, "Show Debugging Fields", false);

        [MenuItem(MENU_UI_DEBUG_, true)]
        private static bool MENU_UI_DEBUG_VALIDATE()
        {
            if (!_UI_DEBUG.IsAwake)
            {
                return false;
            }

            Menu.SetChecked(MENU_UI_DEBUG_, _UI_DEBUG.Value);
            return true;
        }

        [MenuItem(MENU_UI_DEBUG_, false, MENU_P.TOOLS.MESH_BURY.ENABLE)]
        private static void MENU_UI_DEBUG()
        {
            if (!_UI_DEBUG.IsAwake)
            {
                return;
            }

            _UI_DEBUG.Value = !_UI_DEBUG.Value;
        }

#endregion
    }

#endif
}
