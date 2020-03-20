using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using System;
using Microsoft.Win32;

namespace Seven9.DarkRPC
{
    [InitializeOnLoad]
    public class DiscordMain : ScriptableObject
    {
        static bool canBeEnabled = false;
        static bool canShowScene = false;
        static bool canShowProject = false;

        static DiscordMain()
        {
            canBeEnabled = EditorPrefs.GetBool("DarkRPC_EnableOnStartup", false);
            canShowProject = EditorPrefs.GetBool("DarkRPC_CanShowProject", true);
            canShowScene = EditorPrefs.GetBool("DarkRPC_CanShowScene", true);
            if (canBeEnabled)
            {
                try
                {
                    Discord.EventHandlers handlers = new Discord.EventHandlers();
                    Discord.Initialize("594837567556550666", handlers);

                    EditorApplication.playmodeStateChanged = HandleOnPlayModeChanged;
                    EditorSceneManager.activeSceneChangedInEditMode += EditorSceneManager_activeSceneChangedInEditMode;
                    Discord.RichPresence presence = new Discord.RichPresence();
                    if(canShowProject)
                    {
                        presence.details = "Project: " + GetProjectName();
                    }
                    else
                    {
                        presence.details = "Version: " + Application.unityVersion;
                    }
                    if(canShowScene)
                    {
                        presence.state = "Scene: " + EditorSceneManager.GetActiveScene().name;
                    }
                    else
                    {
                        presence.state = "DarkRPC adds an Discord RPC into Unity Editor...";
                    }
                    presence.startTimestamp = GetTimestamp(DateTime.Now);
                    presence.endTimestamp = 0;
                    presence.largeImageKey = "newicon";
                    Discord.UpdatePresence(presence);
                    Debug.Log("Discord RPC successfully started!");
                }
                catch
                {
                    Debug.LogError("Cannot initialize Discord RPC!");
                }
            }
        }

        [MenuItem("DarkRPC/Enable Discord Client")]
        static void EnableDiscord()
        {
            Debug.Log("Initializating Discord RPC...");
            try
            {
                Discord.EventHandlers handlers = new Discord.EventHandlers();
                Discord.Initialize("594837567556550666", handlers);

                EditorApplication.playmodeStateChanged = HandleOnPlayModeChanged;
                EditorSceneManager.activeSceneChangedInEditMode += EditorSceneManager_activeSceneChangedInEditMode;
                Discord.RichPresence presence = new Discord.RichPresence();
                if (canShowProject)
                {
                    presence.details = "Project: " + GetProjectName();
                }
                else
                {
                    presence.details = "Version: " + Application.unityVersion;
                }
                if (canShowScene)
                {
                    presence.state = "Scene: " + EditorSceneManager.GetActiveScene().name;
                }
                else
                {
                    presence.state = "DarkRPC adds an Discord RPC into Unity Editor...";
                }
                presence.startTimestamp = GetTimestamp(DateTime.Now);
                presence.endTimestamp = 0;
                presence.largeImageKey = "newicon";
                Discord.UpdatePresence(presence);
                Debug.Log("Discord RPC successfully started!");
            }
            catch
            {
                Debug.LogError("Cannot initialize Discord RPC!");
            }
        }

        [MenuItem("DarkRPC/Disable Discord Client")]
        static void DisableDiscord()
        {
            Debug.Log("Disabling Discord RPC...");
            Discord.Shutdown();
        }

        [MenuItem("DarkRPC/About")]
        static void AboutDarkRPC()
        {
            EditorUtility.DisplayDialog("About DarkRPC", "DarkRPC version 1.0\nMade by: Seven9 (Sevenisko)\nDiscord server: https://discord.gg/4HJCw88", "OK");
        }

        static void HandleOnPlayModeChanged()
        {
            if (EditorApplication.isPaused)
            {
                Discord.RichPresence presence = new Discord.RichPresence();
                if (canShowProject)
                {
                    presence.details = "Project: " + GetProjectName() + " (Paused)";
                }
                else
                {
                    presence.details = "Version: " + Application.unityVersion;
                }
                if (canShowScene)
                {
                    presence.state = "Scene: " + EditorSceneManager.GetActiveScene().name;
                }
                else
                {
                    presence.state = "DarkRPC adds an Discord RPC into Unity Editor...";
                }
                presence.largeImageKey = "newicon";
                Discord.UpdatePresence(presence);
            }
            else if (EditorApplication.isPlaying)
            {
                Discord.RichPresence presence = new Discord.RichPresence();
                if (canShowProject)
                {
                    presence.details = "Project: " + GetProjectName() + " (Playing)";
                }
                else
                {
                    presence.details = "Version: " + Application.unityVersion;
                }
                if (canShowScene)
                {
                    presence.state = "Scene: " + EditorSceneManager.GetActiveScene().name;
                }
                else
                {
                    presence.state = "DarkRPC adds an Discord RPC into Unity Editor...";
                }
                presence.largeImageKey = "unityicon";
                Discord.UpdatePresence(presence);
            }
            else if (EditorApplication.isCompiling)
            {
                Discord.RichPresence presence = new Discord.RichPresence();
                if (canShowProject)
                {
                    presence.details = "Project: " + GetProjectName() + " (Compiling)";
                }
                else
                {
                    presence.details = "Version: " + Application.unityVersion;
                }
                if (canShowScene)
                {
                    presence.state = "Scene: " + EditorSceneManager.GetActiveScene().name;
                }
                else
                {
                    presence.state = "DarkRPC adds an Discord RPC into Unity Editor...";
                }
                presence.largeImageKey = "newicon";
                Discord.UpdatePresence(presence);
            }
        }

        static long GetTimestamp(DateTime value)
        {
            long val = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            Debug.Log("Used timestamp: " + val);
            return val;
        }

        static string GetProjectName()
        {
            string[] s = Application.dataPath.Split('/');
            string projectName = s[s.Length - 2];
            return projectName;
        }

        private static void EditorSceneManager_activeSceneChangedInEditMode(Scene arg0, Scene arg1)
        {
            Discord.RichPresence presence = new Discord.RichPresence();
            if (canShowProject)
            {
                presence.details = "Project: " + GetProjectName();
            }
            else
            {
                presence.details = "Version: " + Application.unityVersion;
            }
            if (canShowScene)
            {
                presence.state = "Scene: " + EditorSceneManager.GetActiveScene().name;
            }
            else
            {
                presence.state = "DarkRPC adds an Discord RPC into Unity Editor...";
            }
            presence.largeImageKey = "newicon";
            Discord.UpdatePresence(presence);
        }

        [PreferenceItem("DarkRPC")]
        public static void DarkRPCSettings()
        {
            canBeEnabled = EditorPrefs.GetBool("DarkRPC_EnableOnStartup", false);
            canShowProject = EditorPrefs.GetBool("DarkRPC_CanShowProject", true);
            canShowScene = EditorPrefs.GetBool("DarkRPC_CanShowScene", true);

            canBeEnabled = EditorGUILayout.Toggle("Enable Discord RPC on Startup", canBeEnabled);
            canShowProject = EditorGUILayout.Toggle("Allow to show Project name", canShowProject);
            canShowScene = EditorGUILayout.Toggle("Allow to show current scene", canShowScene);

            if (GUI.changed)
            {
                EditorPrefs.SetBool("DarkRPC_EnableOnStartup", canBeEnabled);
                EditorPrefs.SetBool("DarkRPC_CanShowProject", canShowProject);
                EditorPrefs.SetBool("DarkRPC_CanShowScene", canShowScene);
            }
        }
    }
}