using BepInEx;
using HarmonyLib;
using Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Project_Encryptic.Helpers
{
    internal class HP
    {
        public static bool IsPatched { get; private set; }
        private static Harmony instance;

        public static Harmony Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Harmony(Info.PluginInfo.PLUGIN_GUID);
                }
                return instance;
            }
        }

        internal static void ApplyHarmonyPatches()
        {
            if (!IsPatched)
            {
                Instance.PatchAll(Assembly.GetExecutingAssembly());
                IsPatched = true;
            }
        }

        internal static void RemoveHarmonyPatches()
        {
            if (Instance != null && IsPatched)
            {
                Instance.UnpatchSelf();
                IsPatched = false;
            }
        }
    }
}
