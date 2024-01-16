using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GH_Boot_Sequence_Animation_Skipper
{
    [BepInPlugin(GUID, NAME, VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "arcane.gh.boot.sequence.animation.skipper.plugin";

        public const string NAME = "GH Boot Sequence Animation Skipper";

        public const string VERSION = "1.0.0";

        public static Plugin instance = null;

        public ManualLogSource logger = null;

        public Harmony harmony = null;

        public void Awake()
        {
            instance = this;

            this.logger = this.Logger;

            this.harmony = new Harmony(GUID);

            this.harmony.PatchAll();

            this.logger.LogMessage("Plugin loaded successfully.");
        }
    }
}
