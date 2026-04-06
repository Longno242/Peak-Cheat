using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using Photon.Pun;
using Project_Encryptic.Mods;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ModernModMenu
{
    [BepInPlugin("com.yourname.modernmodmenu", "Modern Mod Menu", "1.0.0")]
    public class ModernModMenu : BaseUnityPlugin
    {
        private static ModernModMenu Instance;
        internal static new ManualLogSource Logger;

        private bool showMenu = false;
        private Rect windowRect = new Rect(100, 100, 450, 600);
        private Vector2 scrollPosition;
        private bool infiniteStamina = false;
        private bool godmode = false;
        private float speedMultiplier = 1f;
        private float jumpMultiplier = 1f;
        public static bool NoFallDamage = false;
        public static bool KeepItems = false;
        public static bool NoPassOut = false;
        public static bool NoDeath = false;
        private bool lowGravity = false;
        private float gravityMultiplier = 0.2f;
        private bool reverseGravity = false;
        private Dictionary<string, Character> playerDict = new Dictionary<string, Character>();
        private string selectedPlayerName = "";
        private Character selectedCharacter;
        private Vector2 playerListScroll;
        private Vector2 playerActionScroll;
        private string playerSearch = "";
        private string search = "";
        private readonly Dictionary<string, Character> players = new Dictionary<string, Character>();
        private string selectedName;
        private int spawnIndex = -1;
        private float lastSpawn;
        private Vector2 itemSpawnerScroll;

        private enum Category
        {
            Main,
            LocalPlayer,
            PlayerTargeting,
            Target,
            ItemSpawner,
            GlobalChaos,
            Visuals,
            Server,
            Settings
        }

        private Category currentCategory = Category.Main;
        private ConfigEntry<KeyboardShortcut> menuToggleKey;
        private Texture2D toggleOnTex;
        private Texture2D toggleOffTex;

        private void Awake()
        {
            Instance = this;
            Logger = base.Logger;

            menuToggleKey = Config.Bind("Settings", "Menu Toggle Key", new KeyboardShortcut(KeyCode.Delete), "Key to toggle menu");

            toggleOnTex = new Texture2D(1, 1);
            toggleOnTex.SetPixel(0, 0, Color.green);
            toggleOnTex.Apply();
            toggleOffTex = new Texture2D(1, 1);
            toggleOffTex.SetPixel(0, 0, Color.red);
            toggleOffTex.Apply();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                showMenu = !showMenu;
                if (showMenu)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    Time.timeScale = 0f;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    Time.timeScale = 1f;
                }
            }

            if (infiniteStamina)
            {
                Mods.InfiniteStamina();
            }

            if (godmode)
            {
                Mods.GodMode();
            }

            if (reverseGravity)
            {
                Mods.ApplyReverseGravity();
            }

            Mods.SetSpeedMultiplier(speedMultiplier);
            Mods.SetJumpMultiplier(jumpMultiplier);

            Mods.LowGravityEnabled = lowGravity;
            Mods.GravityMultiplier = gravityMultiplier;
            Mods.ReverseGravityEnabled = reverseGravity;

            Mods.ApplyGravity();

            if (Mods.gunActive)
            {
                Mods.UpdateGun();
            }
        }

        private void OnGUI()
        {
            if (!showMenu) return;

            GUI.backgroundColor = new Color(0.08f, 0.08f, 0.12f, 0.98f);

            windowRect.width = Mathf.Clamp(windowRect.width, 400, 900);
            windowRect.height = Mathf.Clamp(windowRect.height, 400, 800);
            windowRect = GUI.Window(123456, windowRect, DrawWindow, "⚡ MODERN MOD MENU");
        }

        private void DrawWindow(int id)
        {
            GUI.DragWindow(new Rect(0, 0, windowRect.width, 25));

            GUILayout.BeginVertical();

            DrawCategoryTabs();

            GUILayout.Space(5);

            scrollPosition = GUILayout.BeginScrollView(
                scrollPosition,
                GUILayout.ExpandWidth(true),
                GUILayout.ExpandHeight(true)
            );

            GUILayout.BeginVertical("box");
            GUILayout.Space(5);

            switch (currentCategory)
            {
                case Category.Main:
                    GUILayout.Label("✨ WELCOME");
                    GUILayout.Label("Modern Mod Menu v1.0");
                    break;

                case Category.LocalPlayer:
                    DrawToggle("Infinite Stamina", ref infiniteStamina);
                    DrawToggle("Godmode", ref godmode);
                    DrawToggle("No Fall Damage", ref NoFallDamage);
                    DrawToggle("Keep Items", ref KeepItems);
                    DrawToggle("No Pass Out", ref NoPassOut);
                    DrawToggle("No Death", ref NoDeath);
                    DrawToggle("Low Gravity", ref lowGravity);
                    DrawToggle("Reverse Gravity", ref reverseGravity);
                    DrawSlider("Speed Boost", ref speedMultiplier, 1f, 100f);
                    DrawSlider("Jump Boost", ref jumpMultiplier, 1f, 100f);
                    DrawSlider("Gravity Strength", ref gravityMultiplier, 0.0f, 1f);
                    break;

                case Category.PlayerTargeting:
                    DrawPlayerTargeting();
                    break;

                case Category.Target:
                    DrawTarget();
                    break;

                case Category.ItemSpawner:
                    DrawItemSpawner();
                    break;

                case Category.GlobalChaos:
                    DrawButton("Crash All Players", () =>
                    {
                        Mods.CrashAllPlayers();
                    });
                    break;

                case Category.Visuals:
                    GUILayout.Label("Visual features coming soon...");
                    break;

                case Category.Server:
                    DrawButton("Give All Badges", () =>
                    {
                        Mods.GiveAllBadges();
                    });

                    DrawButton("Dump Items", () =>
                    {
                        Mods.DumpItemsToFile();
                    });

                    DrawButton("Dump RPC", () =>
                    {
                        Mods.DumpRPCsToFile();
                    });
                    break;

                case Category.Settings:
                    GUILayout.Label("Settings coming soon...");
                    break;
            }

            GUILayout.Space(10);
            GUILayout.EndVertical();
            GUILayout.EndScrollView();

            GUILayout.EndVertical();
        }

        private void DrawCategoryTabs()
        {
            GUILayout.BeginVertical();

            int buttonsPerRow = 4;
            int count = 0;

            GUILayout.BeginHorizontal();

            foreach (Category cat in System.Enum.GetValues(typeof(Category)))
            {
                GUIStyle style = new GUIStyle(GUI.skin.button);
                style.normal.textColor = (currentCategory == cat) ? Color.cyan : Color.white;

                if (GUILayout.Button(cat.ToString(), style, GUILayout.Height(25)))
                {
                    currentCategory = cat;
                    scrollPosition = Vector2.zero;
                }

                count++;

                if (count % buttonsPerRow == 0)
                {
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                }
            }

            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }

        private void DrawToggle(string label, ref bool value)
        {
            GUILayout.BeginHorizontal("box");

            GUILayout.Label(label, GUILayout.ExpandWidth(true));

            if (GUILayout.Button(value ? toggleOnTex : toggleOffTex, GUILayout.Width(50), GUILayout.Height(22)))
            {
                value = !value;
            }

            GUILayout.EndHorizontal();
        }

        private void DrawButton(string label, System.Action action)
        {
            GUILayout.BeginHorizontal("box");

            if (GUILayout.Button(label, GUILayout.Height(30)))
            {
                action?.Invoke();
            }

            GUILayout.EndHorizontal();
        }

        private void DrawSlider(string label, ref float value, float min, float max)
        {
            GUILayout.BeginVertical("box");

            GUILayout.Label($"{label}: {value:F1}x");

            value = GUILayout.HorizontalSlider(value, min, max);

            GUILayout.EndVertical();
        }

        private void RefreshPlayers()
        {
            playerDict.Clear();
            players.Clear();

            if (PlayerManager.GetAllPlayers() == null) return;

            foreach (var player in PlayerManager.GetAllPlayers())
            {
                if (player == null) continue;

                string name = player.characterName;

                if (player == PlayerManager.GetLocalPlayer())
                    name += " (YOU)";

                if (!playerDict.ContainsKey(name))
                {
                    playerDict.Add(name, player);
                    players.Add(name, player);
                }
            }
        }

        private void DrawPlayerTargeting()
        {
            if (playerDict.Count == 0)
                RefreshPlayers();

            GUILayout.BeginHorizontal();

            GUILayout.BeginVertical(GUILayout.Width(180));

            if (GUILayout.Button("Refresh Players"))
                RefreshPlayers();

            playerSearch = GUILayout.TextField(playerSearch);

            playerListScroll = GUILayout.BeginScrollView(playerListScroll);

            foreach (var entry in playerDict)
            {
                if (!string.IsNullOrEmpty(playerSearch) && !entry.Key.ToLower().Contains(playerSearch.ToLower()))
                    continue;

                GUIStyle playerButtonStyle = new GUIStyle(GUI.skin.button);
                if (entry.Value == PlayerManager.GetLocalPlayer())
                {
                    playerButtonStyle.normal.textColor = Color.yellow;
                }

                if (GUILayout.Button(entry.Key, playerButtonStyle))
                {
                    selectedPlayerName = entry.Key;
                    selectedCharacter = entry.Value;
                }
            }

            GUILayout.EndScrollView();
            GUILayout.EndVertical();

            GUILayout.BeginVertical("box");

            if (selectedCharacter == null)
            {
                GUILayout.Label("Select a player...");
            }
            else
            {
                string displayName = selectedPlayerName;
                if (selectedCharacter == PlayerManager.GetLocalPlayer())
                    displayName += " (YOU)";

                GUILayout.Label($"Target: {displayName}");

                playerActionScroll = GUILayout.BeginScrollView(playerActionScroll);

                DrawPlayerActions();

                GUILayout.EndScrollView();
            }

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();
        }

        GUIStyle Header() => new GUIStyle(GUI.skin.label) { fontSize = 16, fontStyle = FontStyle.Bold };
        GUIStyle Button() => new GUIStyle(GUI.skin.button) { fontSize = 14 };
        GUIStyle Small() => new GUIStyle(GUI.skin.label) { fontSize = 11 };

        void DrawTarget()
        {
            GUILayout.BeginHorizontal();

            GUILayout.BeginVertical(GUILayout.Width(260));

            if (GUILayout.Button("Refresh Players"))
                RefreshPlayers();

            search = GUILayout.TextField(search);
            GUILayout.Space(4);

            foreach (var kv in players.Where(p => string.IsNullOrEmpty(search) || p.Key.ToLower().Contains(search.ToLower())))
            {
                GUIStyle s = new GUIStyle(GUI.skin.button);
                if (kv.Key == selectedName)
                    s.fontStyle = FontStyle.Bold;

                if (GUILayout.Button(kv.Key, s))
                {
                    selectedName = kv.Key;
                    selectedCharacter = kv.Value;
                }
            }

            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            if (selectedCharacter == null)
            {
                GUILayout.Label("No target selected.");
            }
            else
            {
                GUILayout.Label($"Target: {selectedName}", Header());

                if (GUILayout.Button("Crash", Button()))
                    Mods.CrashPlayer(selectedCharacter);

                if (GUILayout.Button("Launch", Button()))
                    selectedCharacter.transform.position += Vector3.up * 15f;

                GUILayout.Space(6);
                GUILayout.Label("Spawn Item:");

                Vector2 itemScroll = Vector2.zero;
                itemScroll = GUILayout.BeginScrollView(itemScroll, GUILayout.Height(200));

                for (int i = 0; i < Mods.itemList.Length; i++)
                {
                    if (GUILayout.Button(Mods.itemList[i], Small()))
                        spawnIndex = spawnIndex == i ? -1 : i;
                }

                GUILayout.EndScrollView();

                if (spawnIndex != -1 && Time.time - lastSpawn > 0.5f)
                {
                    lastSpawn = Time.time;
                    PhotonNetwork.Instantiate(Mods.itemList[spawnIndex], selectedCharacter.transform.position, Quaternion.identity);
                }
            }

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }

        private void DrawItemSpawner()
        {
            itemSpawnerScroll = GUILayout.BeginScrollView(itemSpawnerScroll, GUILayout.ExpandHeight(true));

            Mods.ItemSpawnerGUI();

            GUILayout.EndScrollView();
        }

        private void DrawPlayerActions()
        {
            GUILayout.Label("Core");

            if (GUILayout.Button("Kill"))
                Mods.KillPlayer(selectedCharacter);

            if (GUILayout.Button("Revive"))
                Mods.RevivePlayer(selectedCharacter);

            if (GUILayout.Button("Teleport To Me"))
                Mods.TeleportPlayerToMe(selectedCharacter);

            if (GUILayout.Button("Crash"))
                Mods.CrashPlayer(selectedCharacter);


            GUILayout.Space(10);
            GUILayout.Label("Movement");

            if (GUILayout.Button("Fling"))
                Mods.FlingPlayer(selectedCharacter);

            if (GUILayout.Button("Tumble"))
                Mods.TumblePlayer(selectedCharacter);

            if (GUILayout.Button("Freeze"))
                Mods.FreezePlayer(selectedCharacter);

            if (GUILayout.Button("Unfreeze"))
                Mods.UnfreezePlayer(selectedCharacter);


            GUILayout.Space(10);
            GUILayout.Label("Fun");

            if (GUILayout.Button("Explode"))
                Mods.ExplodePlayer(selectedCharacter);

            if (GUILayout.Button("Launch Up"))
                selectedCharacter.transform.position += Vector3.up * 30f;


            GUILayout.Space(10);
            GUILayout.Label("Troll");

            if (GUILayout.Button("Force Pass Out"))
                Mods.PassOutPlayer(selectedCharacter);
        }
    }
}