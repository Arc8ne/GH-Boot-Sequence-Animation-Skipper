using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;
using TMPro;
using UI.Dialogs;
using UnityEngine.SceneManagement;

namespace GH_Boot_Sequence_Animation_Skipper
{
    [HarmonyPatch]
    public class Patcher
    {
        [HarmonyPatch(typeof(BootUp), nameof(BootUp.AnimScreen))]
        [HarmonyPostfix]
        public static IEnumerator BootUpAnimScreenPostfix(IEnumerator enumerator, string errorMsg, bool isFirstInstall, int termSafePID, bool isGameOver, BootUp __instance)
        {
            __instance.partesIni[2] = "Main Processor : " + ((__instance.pc.GetHardware() != null) ? __instance.pc.GetHardware().cpus[0].name : "3200c-170M") + "\n";
            
            System.Random systRandom = new System.Random();
            
            // __instance.StartCoroutine(__instance.MostrarDesktopIcons(false, false));
            
            __instance.mostrarSecuencia = true;
            
            if (__instance.mostrarSecuencia)
            {
                // yield return new WaitForSeconds(1.5f);
                
                __instance.ActivarImagenes(true);
                
                __instance.backToMenuAvail = true;
                
                __instance.textoScreen.text = __instance.partesIni[0];
                
                // yield return new WaitForSeconds(0.7f);
                
                TMP_Text tmp_Text = __instance.textoScreen;
                
                tmp_Text.text += __instance.partesIni[1];
                
                // yield return new WaitForSeconds(0.35f);
                
                TMP_Text tmp_Text2 = __instance.textoScreen;
                
                tmp_Text2.text += __instance.partesIni[2];
                
                // yield return new WaitForSeconds(0.5f);
                
                TMP_Text tmp_Text3 = __instance.textoScreen;
                
                tmp_Text3.text += __instance.partesIni[3];
                
                __instance.animMemTest = __instance.MemoryTesting();
                
                // yield return __instance.StartCoroutine(__instance.animMemTest);
                
                TMP_Text tmp_Text4 = __instance.textoScreen;
                
                tmp_Text4.text += __instance.partesIni[4];
                
                // yield return new WaitForSeconds(1f);
                
                __instance.ActivarImagenes(false);
                
                __instance.backToMenuAvail = false;
                
                __instance.textoScreen.text = "";
                
                __instance.textoScreen.fontSize = 18f;
                
                string[] lineasTextoIniOs = __instance.textIniOs.text.Split(new char[] { '\n' }, StringSplitOptions.None);
                
                int num3;
                
                for (int i = 0; i < lineasTextoIniOs.Length; i = num3 + 1)
                {
                    TMP_Text tmp_Text5 = __instance.textoScreen;
                    
                    tmp_Text5.text = tmp_Text5.text + lineasTextoIniOs[i].Trim() + "\n";
                    
                    Canvas.ForceUpdateCanvases();
                    
                    __instance.scrollBar.value = 0f;
                    
                    // float num = 0.005f;
                    
                    // float num2 = 0.05f;
                    
                    // yield return new WaitForSeconds((float)(systRandom.NextDouble() * (double)(num2 - num) + (double)num));
                    
                    num3 = i;
                }
                
                lineasTextoIniOs = null;
            }
            
            // yield return new WaitForSeconds(0.7f);
            
            if (isFirstInstall)
            {
                __instance.textoScreen.text = "";
                
                __instance.textoScreen.rectTransform.anchoredPosition = Vector2.zero;
                
                // yield return new WaitForSeconds(0.7f);
                
                __instance.panelInstall.SetAsLastSibling();
                
                __instance.mouseCursor.transform.SetAsLastSibling();
                
                __instance.mouseCursor.SetWaitCursor();
                
                __instance.panelInstall.GetComponent<InstallOS>().ShowUserConfig();
            }
            else if (isGameOver)
            {
                __instance.desktopFinder.GetComponentInChildren<DesktopMessageUser>().Iniciar(true, "");
                
                __instance.desktopFinder.transform.SetAsLastSibling();
            }
            else if (errorMsg.Length > 0)
            {
                if (__instance.objSafeTerminal != null)
                {
                    UnityEngine.Object.Destroy(__instance.objSafeTerminal);
                    
                    yield return null;
                }
                
                __instance.textoScreen.text = "";
                
                __instance.textoScreen.ForceMeshUpdate(false, false);
                
                if (errorMsg.Equals("safe-mode"))
                {
                    errorMsg = "Starting safe mode at user request...OK\nType 'reboot' to exit from safe mode.\nType 'exit' to exit to main menu.";
                }
                else
                {
                    errorMsg += "Boot interrupted.\nStarting safe mode...OK\nType 'reboot' after restore missing libraries.\nType 'exit' to exit to main menu.";
                }
                
                RectTransform component = __instance.GetComponent<RectTransform>();
                
                uDialog uDialog = uDialog.NewDialog(__instance.objTerminal, component);
                
                Rect rect = component.rect;
                
                uDialog.RectTransform.sizeDelta = new Vector2(rect.width, rect.height);
                
                Terminal componentInChildren = uDialog.GetComponentInChildren<Terminal>();
                
                componentInChildren.PrintLinea(errorMsg, false, false);
                
                componentInChildren.SetPromptEnabled(true);
                
                componentInChildren.SetPID(termSafePID, -10);
                
                __instance.mouseCursor.SetWaitCursor();
                
                __instance.objSafeTerminal = uDialog.gameObject;
            }
            else
            {
                __instance.textoScreen.text = "Autologin enabled\nPlease wait...";
                
                __instance.textoScreen.rectTransform.anchoredPosition = Vector2.zero;
                
                // yield return __instance.StartCoroutine(__instance.Teletipo());
                
                // yield return new WaitForSeconds(0.85f);
                
                __instance.ResumeBoot();
            }
            
            yield break;
        }

        [HarmonyPatch(typeof(LoadingAnim), nameof(LoadingAnim.Animation))]
        [HarmonyPostfix]
        public static IEnumerator LoadingAnimAnimationPostfix(IEnumerator enumerator)
        {
            yield break;
        }

        [HarmonyPatch(typeof(Intro), nameof(Intro.AnimCursor))]
        [HarmonyPostfix]
        public static IEnumerator IntroAnimCursor(IEnumerator enumerator, Intro __instance)
        {
            __instance.cursor.gameObject.SetActive(false);

            SceneManager.LoadScene("Game");

            yield break;
        }
    }
}
