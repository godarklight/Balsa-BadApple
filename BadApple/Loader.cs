using System;
using BalsaCore;
using UnityEngine;

namespace BadApple
{
    [BalsaAddon]
    public class Loader
    {
        private static GameObject go;
        [BalsaAddonInit(invokeTime = AddonInvokeTime.MainMenu)]
        public static void BalsaInit()
        {
            if (go == null)
            {
                go = new GameObject();
                BadAppleMod ba = go.AddComponent<BadAppleMod>();
                MonoBehaviour.DontDestroyOnLoad(go);
            }
        }

        //Game exit
        [BalsaAddonFinalize]
        public static void BalsaFinalize()
        {
        }
    }
}
