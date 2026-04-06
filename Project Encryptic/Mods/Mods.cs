using DG.Tweening.Plugins.Core;
using Photon.Pun;
using Photon.Pun.Demo.Procedural;
using Photon.Voice.Unity;
using SCPE;
using Steamworks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using UnityEngine.UIElements;
using UnityEngine.XR;
using Zorro.Core;
using static _02Scripts.DieRoller;
using static Mono.Security.X509.PKCS12.DeriveBytes;
using static UnityEngine.Rendering.DebugUI.Table;

namespace Project_Encryptic.Mods
{
    internal class Mods
    {
        public static string[] itemList = new string[]
        {
    "0_Items/Airplane Food",
    "0_Items/AloeVera",
    "0_Items/AncientIdol",
    "0_Items/Anti-Rope Spool",
    "0_Items/Antidote",
    "0_Items/Apple Berry Green",
    "0_Items/Apple Berry Red",
    "0_Items/Apple Berry Yellow",
    "0_Items/Backpack",
    "0_Items/Balloon",
    "0_Items/BalloonBunch",
    "0_Items/Bandages",
    "0_Items/BaseConstructable Variant",
    "0_Items/BaseConstructablePreview",
    "0_Items/BaseObject",
    "0_Items/PortableStovetop_Preview",
    "0_Items/ScoutCannon_Preview",
    "0_Items/TestConstructable Variant",
    "0_Items/Basketball",
    "0_Items/Beehive",
    "0_Items/Beetle",
    "0_Items/Berrynana Blue",
    "0_Items/Berrynana Brown",
    "0_Items/Berrynana Peel Blue Variant",
    "0_Items/Berrynana Peel Brown Variant",
    "0_Items/Berrynana Peel Pink Variant",
    "0_Items/Berrynana Peel Yellow",
    "0_Items/Berrynana Pink",
    "0_Items/Berrynana Yellow",
    "0_Items/BingBong",
    "0_Items/BingBong_Prop Variant",
    "0_Items/Binoculars",
    "0_Items/Binoculars_Prop",
    "0_Items/BookOfBones",
    "0_Items/BounceShroom",
    "0_Items/BounceShroomSpawn",
    "0_Items/Bugfix",
    "0_Items/Bugle",
    "0_Items/Bugle_Magic",
    "0_Items/Bugle_Prop Variant",
    "0_Items/Bugle_Scoutmaster Variant",
    "0_Items/C_Bishop B",
    "0_Items/C_Bishop W",
    "0_Items/C_Bishop_f",
    "0_Items/C_Bishop_f Variant",
    "0_Items/C_Bishop_m",
    "0_Items/C_Bishop_m Variant",
    "0_Items/C_King",
    "0_Items/C_King B",
    "0_Items/C_King Variant",
    "0_Items/C_King W",
    "0_Items/C_Knight",
    "0_Items/C_Knight B",
    "0_Items/C_Knight Variant",
    "0_Items/C_Knight W",
    "0_Items/C_Pawn B",
    "0_Items/C_Pawn W",
    "0_Items/C_Pawn_f",
    "0_Items/C_Pawn_f Variant",
    "0_Items/C_Pawn_m",
    "0_Items/C_Pawn_m Variant",
    "0_Items/C_Queen",
    "0_Items/C_Queen B",
    "0_Items/C_Queen Variant",
    "0_Items/C_Queen W",
    "0_Items/C_Rook B",
    "0_Items/C_Rook W",
    "0_Items/C_Rook_f",
    "0_Items/C_Rook_f Variant",
    "0_Items/C_Rook_m",
    "0_Items/C_Rook_m Variant",
    "0_Items/CactusBall",
    "0_Items/ChainShooter",
    "0_Items/Cheat Compass",
    "0_Items/ClimbingChalk",
    "0_Items/ClimbingSpike",
    "0_Items/ClimbingSpikeHammered",
    "0_Items/ClimbingSpikeHammered_Shitty",
    "0_Items/ClimbingSpikePreview",
    "0_Items/CloudFungus",
    "0_Items/CloudFungusPlaced",
    "0_Items/Clusterberry Black",
    "0_Items/Clusterberry Red",
    "0_Items/Clusterberry Yellow",
    "0_Items/Clusterberry_UNUSED",
    "0_Items/Compass",
    "0_Items/Cure-All",
    "0_Items/Cursed Skull",
    "0_Items/Dynamite",
    "0_Items/Egg",
    "0_Items/EggHalf1",
    "0_Items/EggHalf2",
    "0_Items/EggTurkey",
    "0_Items/Energy Drink",
    "0_Items/FireWood",
    "0_Items/FirstAidKit",
    "0_Items/Flag_Plantable_Checkpoint",
    "0_Items/FlagPlantablePreview",
    "0_Items/Flare",
    "0_Items/FortifiedMilk",
    "0_Items/FortifiedMilk_TEMP",
    "0_Items/Frisbee",
    "0_Items/Glizzy",
    "0_Items/Granola Bar",
    "0_Items/Guidebook",
    "0_Items/GuidebookPage",
    "0_Items/GuidebookPage_0_Intro",
    "0_Items/GuidebookPage_1_Mushrooms",
    "0_Items/GuidebookPage_2_Campfire",
    "0_Items/GuidebookPage_3_Revival",
    "0_Items/GuidebookPage_4_BodyHeat Variant",
    "0_Items/GuidebookPage_5_Sleepy Variant",
    "0_Items/GuidebookPage_6_Awake Variant",
    "0_Items/GuidebookPage_7_Crashout Variant",
    "0_Items/GuidebookPage_8_FirstTeams",
    "0_Items/GuidebookPageScroll Variant",
    "0_Items/HealingDart Variant",
    "0_Items/HealingPuffShroom",
    "0_Items/HealingPuffShroomSpawn",
    "0_Items/Heat Pack",
    "0_Items/Item_Coconut",
    "0_Items/Item_Coconut_half",
    "0_Items/Item_Honeycomb",
    "0_Items/Kingberry Green",
    "0_Items/Kingberry Purple",
    "0_Items/Kingberry Yellow",
    "0_Items/Lantern",
    "0_Items/Lantern_Faerie",
    "0_Items/Lollipop",
    "0_Items/Lollipop_Prop",
    "0_Items/LuggageAncient",
    "0_Items/LuggageBig",
    "0_Items/LuggageCursed",
    "0_Items/LuggageEpic",
    "0_Items/LuggageSmall",
    "0_Items/MagicBean",
    "0_Items/Mandrake",
    "0_Items/Mandrake_Hidden",
    "0_Items/Marshmallow",
    "0_Items/MedicinalRoot",
    "0_Items/Megaphone",
    "0_Items/Mushroom Chubby",
    "0_Items/Mushroom Cluster",
    "0_Items/Mushroom Cluster Poison",
    "0_Items/Mushroom Glow",
    "0_Items/Mushroom Lace",
    "0_Items/Mushroom Lace Poison",
    "0_Items/Mushroom Normie",
    "0_Items/Mushroom Normie Poison",
    "0_Items/Napberry",
    "0_Items/NestEgg",
    "0_Items/PandorasBox",
    "0_Items/Parasol",
    "0_Items/Parasol_Roots Variant",
    "0_Items/Passport",
    "0_Items/Pepper Berry",
    "0_Items/PickAxeHammered_Shitty",
    "0_Items/Pirate Compass",
    "0_Items/PortableStovetopItem",
    "0_Items/Prickleberry_Gold",
    "0_Items/Prickleberry_Red",
    "0_Items/RescueHook",
    "0_Items/RescueHook_Infinite",
    "0_Items/RopeShooter",
    "0_Items/RopeShooterAnti",
    "0_Items/RopeSpool",
    "0_Items/Scorpion",
    "0_Items/ScoutCannonItem",
    "0_Items/ScoutCookies",
    "0_Items/ScoutEffigy",
    "0_Items/ShelfShroom",
    "0_Items/ShelfShroomPrePlaced",
    "0_Items/ShelfShroomSpawn",
    "0_Items/Shell Big",
    "0_Items/Shell Small",
    "0_Items/Shroomberry_Blue",
    "0_Items/Shroomberry_Green",
    "0_Items/Shroomberry_Purple",
    "0_Items/Shroomberry_Red",
    "0_Items/Shroomberry_Yellow",
    "0_Items/Skull",
    "0_Items/Snowball",
    "0_Items/Sports Drink",
    "0_Items/Stone",
    "0_Items/Strange Gem",
    "0_Items/Sunscreen",
    "0_Items/Torch",
    "0_Items/TrailMix",
    "0_Items/AK",
    "0_Items/Apple Berry Weird",
    "0_Items/Berrynana UNUSED",
    "0_Items/Cure-Some",
    "0_Items/Darkberry",
    "0_Items/Energy Elixir",
    "0_Items/foodTest",
    "0_Items/Lollipop_Evil",
    "0_Items/Matchbook",
    "0_Items/Painkillers",
    "0_Items/Portable Speaker",
    "0_Items/Propeller",
    "0_Items/Skyberry",
    "0_Items/Wand of Wind",
    "0_Items/Wonderberry",
    "0_Items/Warp Compass",
    "0_Items/Winterberry Orange",
    "0_Items/Winterberry Yellow"
        };

        private static float originalMovementForce = -1f;
        private static float originalJumpImpulse = -1f;
        public static bool LowGravityEnabled = false;
        public static float GravityMultiplier = 0.2f;
        public static bool ReverseGravityEnabled = false;
        private static string lastShape = "PP";
        private static int activeItemIndex = -1;
        private static float spawnDelay = 0.5f;
        private static float lastSpawnTime = 0f;
        private static LineRenderer gunLine;
        private static GameObject gunBall;
        private static Vector3 gunHitPos;
        public static bool gunActive;

        public static void ItemSpawnerGUI()
        {
            GUILayout.Label("Mass Item Spawner", new GUIStyle(GUI.skin.label)
            {
                fontSize = 18,
                fontStyle = FontStyle.Bold
            });
            GUILayout.Space(5);
            GUILayout.BeginVertical();
            for (int i = 0; i < itemList.Length; i++)
            {
                GUIStyle style = new GUIStyle(GUI.skin.button);
                if (i == activeItemIndex)
                {
                    style.normal.textColor = Color.green;
                    style.fontStyle = FontStyle.Bold;
                }
                if (GUILayout.Button(itemList[i], style, GUILayout.Height(30)))
                {
                    activeItemIndex = (activeItemIndex == i) ? -1 : i;
                    lastSpawnTime = 0f;
                }
                if (activeItemIndex == i)
                {
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("Spam", GUILayout.Height(25))) { }
                    if (GUILayout.Button("PP", GUILayout.Height(25))) { lastShape = "PP"; CreatePP(itemList[i]); activeItemIndex = -1; }
                    if (GUILayout.Button("Square", GUILayout.Height(25))) { lastShape = "Square"; CreateSquare(itemList[i]); activeItemIndex = -1; }
                    if (GUILayout.Button("Triangle", GUILayout.Height(25))) { lastShape = "Triangle"; CreateTriangle(itemList[i]); activeItemIndex = -1; }
                    if (GUILayout.Button("Line", GUILayout.Height(25))) { lastShape = "Line"; CreateLine(itemList[i]); activeItemIndex = -1; }
                    if (GUILayout.Button("Circle", GUILayout.Height(25))) { lastShape = "Circle"; CreateCircle(itemList[i]); activeItemIndex = -1; }
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("Orbit", GUILayout.Height(25))) { lastShape = "Orbit"; CreateOrbit(itemList[i]); activeItemIndex = -1; }
                    if (GUILayout.Button("Rain", GUILayout.Height(25))) { lastShape = "Rain"; CreateRain(itemList[i]); activeItemIndex = -1; }
                    if (GUILayout.Button("Tower", GUILayout.Height(25))) { lastShape = "Tower"; CreateTower(itemList[i]); activeItemIndex = -1; }
                    if (GUILayout.Button("Explode", GUILayout.Height(25))) { lastShape = "Explode"; CreateExplode(itemList[i]); activeItemIndex = -1; }
                    if (GUILayout.Button("ZigZag", GUILayout.Height(25))) { lastShape = "ZigZag"; CreateZigZag(itemList[i]); activeItemIndex = -1; }
                    if (GUILayout.Button("BlackHole", GUILayout.Height(25))) { lastShape = "BlackHole"; CreateBlackHole(itemList[i]); activeItemIndex = -1; }
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("Laser", GUILayout.Height(25))) { lastShape = "Laser"; CreateLaser(itemList[i]); activeItemIndex = -1; }
                    if (GUILayout.Button("Wall", GUILayout.Height(25))) { lastShape = "Wall"; CreateWall(itemList[i]); activeItemIndex = -1; }
                    if (GUILayout.Button("Spiral", GUILayout.Height(25))) { lastShape = "Spiral"; CreateSpiral(itemList[i]); activeItemIndex = -1; }
                    if (GUILayout.Button("Meteor", GUILayout.Height(25))) { lastShape = "Meteor"; CreateMeteor(itemList[i]); activeItemIndex = -1; }
                    if (GUILayout.Button("Dome", GUILayout.Height(25))) { lastShape = "Dome"; CreateDome(itemList[i]); activeItemIndex = -1; }
                    if (GUILayout.Button("Dance", GUILayout.Height(25))) { lastShape = "Dance"; CreateDance(itemList[i]); activeItemIndex = -1; }
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("Gun", GUILayout.Height(25))) { gunActive = !gunActive; if (gunActive) CreateGunVisuals(); else DestroyGunVisuals(); }
                    GUILayout.EndHorizontal();
                }
            }
            GUILayout.EndVertical();
            if (activeItemIndex != -1 && Time.time - lastSpawnTime >= spawnDelay)
            {
                lastSpawnTime = Time.time;
                string prefabName = itemList[activeItemIndex];
                GameObject prefab = Resources.Load<GameObject>(prefabName);
                if (prefab != null)
                {
                    PhotonNetwork.Instantiate(prefabName, Camera.main.transform.position, Camera.main.transform.rotation, 0, null);
                }
                else
                {
                    activeItemIndex = -1;
                }
            }
        }

        private static void CreateGunVisuals()
        {
            if (gunLine != null) return;
            GameObject go = new GameObject("GunLine");
            gunLine = go.AddComponent<LineRenderer>();
            gunLine.material = new Material(Shader.Find("Sprites/Default"));
            gunLine.material.color = Color.red;
            gunLine.startWidth = gunLine.endWidth = 0.03f;
            gunLine.positionCount = 2;
            gunBall = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            gunBall.name = "GunBall";
            gunBall.transform.localScale = Vector3.one * 0.2f;
            gunBall.GetComponent<Renderer>().material.color = Color.red;
            UnityEngine.Object.Destroy(gunBall.GetComponent<Collider>());
        }

        private static void DestroyGunVisuals()
        {
            if (gunLine) { UnityEngine.Object.Destroy(gunLine.gameObject); gunLine = null; }
            if (gunBall) { UnityEngine.Object.Destroy(gunBall); gunBall = null; }
        }

        public static void UpdateGun()
        {
            if (!gunActive) return;
            if (PlayerManager.GetLocalPlayer() == null) { gunActive = false; DestroyGunVisuals(); return; }
            if (Camera.main == null) return;
            if (gunLine == null || gunBall == null) CreateGunVisuals();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 300f))
                gunHitPos = hit.point;
            else
                gunHitPos = ray.GetPoint(50f);
            gunLine.SetPosition(0, Camera.main.transform.position);
            gunLine.SetPosition(1, gunHitPos);
            if (gunBall) gunBall.transform.position = gunHitPos;
            if (Input.GetMouseButtonDown(0) && activeItemIndex >= 0) FirePattern(itemList[activeItemIndex]);
        }

        private static Vector3 spawnCenter = Vector3.zero;
        private static Quaternion spawnRotation = Quaternion.identity;

        private static void CreateDome(string itemName)
        {
            for (int r = 1; r <= 6; r++)
            {
                for (int p = 0; p < r * 6; p++)
                {
                    Vector3 pos = Camera.main.transform.position + Vector3.up * 2f + new Vector3(Mathf.Cos(p * Mathf.PI * 2f / r * 6) * 3f * r / 6, Mathf.Sin((float)r / 6 * Mathf.PI * 0.5f) * 3f, Mathf.Sin(p * Mathf.PI * 2f / r * 6) * 3f * r / 6);
                    PhotonNetwork.Instantiate(itemName, Vector3.zero, Quaternion.identity).GetComponent<PhotonView>().RPC("SetKinematicRPC", 0, new object[] { true, pos, Quaternion.identity });
                }
            }
        }

        private static void CreateMeteor(string itemName)
        {
            Vector3 start = Camera.main.transform.position + Vector3.up * 25f + UnityEngine.Random.insideUnitSphere * 8f;
            Vector3 end = Camera.main.transform.position + Camera.main.transform.forward * 10f;
            GameObject go = PhotonNetwork.Instantiate(itemName, start, Quaternion.identity);
            Rigidbody rb = go.GetComponent<Rigidbody>();
            if (rb) rb.linearVelocity = (end - start).normalized * 35f;
        }

        private static void CreateSpiral(string itemName)
        {
            Vector3 basePos = Camera.main.transform.position + Vector3.up * 0.5f + Camera.main.transform.forward * 3f;
            const int steps = 25;
            const float rise = 0.2f, radius = 2f, angleStep = 25f;
            for (int s = 0; s < steps; s++)
            {
                float a = s * angleStep * Mathf.Deg2Rad;
                Vector3 p = basePos + new Vector3(Mathf.Cos(a), 0, Mathf.Sin(a)) * radius + Vector3.up * s * rise;
                GameObject go = PhotonNetwork.Instantiate(itemName, Vector3.zero, Quaternion.identity);
                go.GetComponent<PhotonView>().RPC("SetKinematicRPC", 0, new object[] { true, p, Quaternion.identity });
            }
        }

        private static void CreateWall(string itemName)
        {
            for (int r = 0; r < 5; r++)
            {
                for (int c = 0; c < 5; c++)
                {
                    Vector3 p = Camera.main.transform.position + Vector3.up * 1.5f + Camera.main.transform.forward * 5f + Vector3.up * r * 0.8f + Vector3.right * (c - 5 / 2f) * 0.8f;
                    PhotonNetwork.Instantiate(itemName, Vector3.zero, Quaternion.identity).GetComponent<PhotonView>().RPC("SetKinematicRPC", 0, new object[] { true, p, Quaternion.identity });
                }
            }
        }

        private static void CreateLaser(string itemName)
        {
            for (int i = 0; i < 30; i++)
            {
                Rigidbody rb = PhotonNetwork.Instantiate(itemName, Camera.main.transform.position + Camera.main.transform.forward + Camera.main.transform.forward * (i * 0.4f), Quaternion.identity).GetComponent<Rigidbody>();
                if (rb) rb.linearVelocity = Camera.main.transform.forward * 40f;
            }
        }

        private static void CreateDance(string itemName)
        {
            Vector3 basePos = Camera.main.transform.position + Vector3.up * 1f + Camera.main.transform.forward * 4f;
            for (int i = 0; i < 8; i++)
            {
                GameObject go = PhotonNetwork.Instantiate(itemName, Vector3.zero, Quaternion.identity);
                go.GetComponent<PhotonView>().RPC("SetKinematicRPC", 0, new object[] { true, basePos + Vector3.right * (i - 8 / 2f) * 0.8f, Quaternion.identity });
                go.AddComponent<Dancer>().offset = i * 0.7f;
            }
        }

        public class Dancer : MonoBehaviour
        {
            public float offset;
            void Update()
            {
                Vector3 p = transform.position;
                p.y = 1f + Mathf.Sin((Time.time + offset) * 3f) * 0.5f;
                transform.position = p;
            }
        }

        private static void CreateBlackHole(string itemName)
        {
            Vector3 center = Camera.main.transform.position + Vector3.up * 2f + Camera.main.transform.forward * 6f;
            for (int i = 0; i < 30; i++)
            {
                Vector3 pos = center + new Vector3(Mathf.Cos(i * Mathf.PI * 2f / 30), 0, Mathf.Sin(i * Mathf.PI * 2f / 30)) * 5f;
                GameObject go = PhotonNetwork.Instantiate(itemName, pos, Quaternion.identity);
                go.AddComponent<BlackHoleVictim>().Init(center, 3f);
            }
        }

        private static void CreateZigZag(string itemName)
        {
            Vector3 start = Camera.main.transform.position + Vector3.up * 2f + Camera.main.transform.forward * 3f;
            for (int s = 0; s < 12; s++)
            {
                Vector3 pos = start + Vector3.forward * s * 0.8f + Vector3.up * (s % 2 == 0 ? 0f : 1f) + Vector3.right * (s % 3 == 0 ? -1f : (s % 3 == 1 ? 1f : 0f));
                GameObject go = PhotonNetwork.Instantiate(itemName, Vector3.zero, Quaternion.identity);
                go.GetComponent<PhotonView>().RPC("SetKinematicRPC", 0, new object[] { true, pos, Quaternion.identity });
            }
        }

        public class BlackHoleVictim : MonoBehaviour
        {
            private Vector3 target;
            private float life;
            public void Init(Vector3 ctr, float t) { target = ctr; life = t; }
            void Update()
            {
                life -= Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, target, 3f * Time.deltaTime);
                if (life <= 0) PhotonNetwork.Destroy(gameObject);
            }
        }

        public class Orbiter : MonoBehaviour
        {
            private Vector3 orbitCenter;
            private float orbitRadius;
            private float orbitSpeed;
            private float currentAngle;
            public void Set(Vector3 center, float radius, float startAngle, float speed)
            {
                orbitCenter = center;
                orbitRadius = radius;
                orbitSpeed = speed;
                currentAngle = startAngle;
            }
            void Update()
            {
                currentAngle += orbitSpeed * Time.deltaTime * Mathf.Deg2Rad;
                Vector3 newPos = orbitCenter + new Vector3(Mathf.Cos(currentAngle), 0, Mathf.Sin(currentAngle)) * orbitRadius;
                transform.position = newPos;
            }
        }

        private static void CreateExplode(string itemName)
        {
            for (int k = 0; k < 20; k++)
            {
                GameObject go = PhotonNetwork.Instantiate(itemName, Camera.main.transform.position + Camera.main.transform.forward * 2f, Quaternion.identity);
                if (go.GetComponent<Rigidbody>() != null) go.GetComponent<Rigidbody>().linearVelocity = UnityEngine.Random.onUnitSphere * UnityEngine.Random.Range(15f, 25f);
            }
        }

        private static void CreateTower(string itemName)
        {
            for (int y = 0; y < 20; y++)
            {
                Vector3 pos = Camera.main.transform.position + Vector3.up * 1f + Camera.main.transform.forward * 3f + Vector3.up * y * 0.5f;
                PhotonNetwork.Instantiate(itemName, Vector3.zero, Quaternion.identity).GetComponent<PhotonView>().RPC("SetKinematicRPC", 0, new object[] { true, pos, Quaternion.identity });
            }
        }

        private static void CreateRain(string itemName)
        {
            const int drops = 25;
            for (int i = 0; i < drops; i++)
            {
                Vector3 rnd = UnityEngine.Random.insideUnitSphere * 8f;
                rnd.y = 0;
                Vector3 spawn = Camera.main.transform.position + Vector3.up * 12f + rnd;
                PhotonNetwork.Instantiate(itemName, spawn, Quaternion.identity).GetComponent<PhotonView>().RPC("SetKinematicRPC", 0, new object[] { false, spawn, Quaternion.identity });
            }
        }

        private static void CreateOrbit(string itemName)
        {
            for (int i = 0; i < 6; i++)
            {
                PhotonNetwork.Instantiate(itemName, Vector3.zero, Quaternion.identity).GetComponent<PhotonView>().RPC("SetKinematicRPC", 0, new object[] { true, Camera.main.transform.position + Vector3.up * 1.5f + new Vector3(Mathf.Cos(i * Mathf.PI * 2f / 6), 0, Mathf.Sin(i * Mathf.PI * 2f / 6)) * 60f, Quaternion.identity });
                PhotonNetwork.Instantiate(itemName, Vector3.zero, Quaternion.identity).AddComponent<Orbiter>().Set(Camera.main.transform.position + Vector3.up * 1.5f, 2f, i * Mathf.PI * 2f / 6, 60f);
            }
        }

        private static void CreatePP(string itemName)
        {
            for (int i = 0; i < 5; i++)
            {
                Quaternion worldRot = Quaternion.LookRotation(Camera.main.transform.forward, Camera.main.transform.up) * Quaternion.Euler(-10f, 0f, 0f) * Quaternion.Euler(0f, 360f / 5 * i, 0f);
                PhotonNetwork.Instantiate(itemName, Vector3.zero, Quaternion.identity).GetComponent<PhotonView>().RPC("SetKinematicRPC", RpcTarget.All, true, Camera.main.transform.position + Vector3.up * 1.5f + Camera.main.transform.forward * 5f + i * 360f / 5 * Mathf.Deg2Rad * new Vector3(Mathf.Cos(i * 360f / 5 * Mathf.Deg2Rad), 0f, Mathf.Sin(i * 360f / 5 * Mathf.Deg2Rad)) * 1.5f, worldRot);
            }
        }

        private static void CreateSquare(string itemName)
        {
            Vector3[] offsets =
            {
        new Vector3(-1.2f, 0, -1.2f),
        new Vector3( 1.2f, 0, -1.2f),
        new Vector3( 1.2f, 0,  1.2f),
        new Vector3(-1.2f, 0,  1.2f)
    };
            SpawnShape(itemName, Camera.main.transform.position + Camera.main.transform.forward * 5f + Vector3.up * 1.5f, offsets);
        }

        private static void CreateTriangle(string itemName)
        {
            Vector3[] offsets =
            {
                new Vector3(0, 0,  1.3f), new Vector3( 1.3f * 0.866f, 0, -1.3f * 0.5f), new Vector3(-1.3f * 0.866f, 0, -1.3f * 0.5f)
            };
            SpawnShape(itemName, Camera.main.transform.position + Camera.main.transform.forward * 5f + Vector3.up * 1.5f, offsets);
        }

        private static void CreateLine(string itemName)
        {
            for (int i = -2; i <= 2; i++)
            {
                SpawnSingle(itemName, Camera.main.transform.position + Camera.main.transform.forward * 5f + Vector3.up * 1.5f + Vector3.right * (i * 1f));
            }
        }

        private static void CreateCircle(string itemName)
        {
            for (int i = 0; i < 8; i++)
            {
                SpawnSingle(itemName, Camera.main.transform.position + Camera.main.transform.forward * 5f + Vector3.up * 1.5f + new Vector3(Mathf.Cos(i * Mathf.PI * 2f / 8), 0, Mathf.Sin(i * Mathf.PI * 2f / 8)) * 1.5f);
            }
        }

        private static void SpawnShape(string itemName, Vector3 center, Vector3[] offsets)
        {
            foreach (Vector3 o in offsets) SpawnSingle(itemName, center + o);
        }

        private static void SpawnSingle(string itemName, Vector3 worldPos)
        {
            PhotonNetwork.Instantiate(itemName, Vector3.zero, Quaternion.identity).GetComponent<PhotonView>().RPC("SetKinematicRPC", 0, new object[] { true, worldPos, Quaternion.identity });
        }

        private static void FirePattern(string prefabName)
        {
            spawnCenter = gunHitPos;
            spawnRotation = Quaternion.identity;
            switch (lastShape)
            {
                case "PP": CreatePP(prefabName); break;
                case "Square": CreateSquare(prefabName); break;
                case "Triangle": CreateTriangle(prefabName); break;
                case "Line": CreateLine(prefabName); break;
                case "Circle": CreateCircle(prefabName); break;
                case "Orbit": CreateOrbit(prefabName); break;
                case "Rain": CreateRain(prefabName); break;
                case "Tower": CreateTower(prefabName); break;
                case "Explode": CreateExplode(prefabName); break;
                case "ZigZag": CreateZigZag(prefabName); break;
                case "BlackHole": CreateBlackHole(prefabName); break;
                case "Laser": CreateLaser(prefabName); break;
                case "Wall": CreateWall(prefabName); break;
                case "Spiral": CreateSpiral(prefabName); break;
                case "Meteor": CreateMeteor(prefabName); break;
                case "Dome": CreateDome(prefabName); break;
                case "Dance": CreateDance(prefabName); break;
                default: CreatePP(prefabName); break;
            }
        }



        public static void GiveAllBadges()
        {
            foreach (ACHIEVEMENTTYPE a in Enum.GetValues(typeof(ACHIEVEMENTTYPE)))
            {
                try
                {
                    Singleton<AchievementManager>.Instance.TryThrowStatLinkedAchievement(a);
                    typeof(AchievementManager).GetMethod("ThrowAchievement", BindingFlags.Instance | BindingFlags.NonPublic)?.Invoke(Singleton<AchievementManager>.Instance, new object[] { a });
                }
                catch { }
            }

            SteamUserStats.StoreStats();
        }

        public static void InfiniteStamina()
        {
            if (PlayerManager.GetLocalPlayer() != null)
            {
                Character.InfiniteStamina();
                Character.GainFullStamina();
            }
        }

        public static void GodMode()
        {
            if (PlayerManager.GetLocalPlayer() != null)
            {
                Character.LockStatuses();
            }
        }

        public static void SetSpeedMultiplier(float multiplier)
        {
            var movement = PlayerManager.GetLocalPlayer().refs?.movement;
            if (originalMovementForce < 0f) originalMovementForce = movement.movementForce;
            movement.movementForce = originalMovementForce * multiplier;
        }

        public static void SetJumpMultiplier(float multiplier)
        {
            var movement = PlayerManager.GetLocalPlayer().refs?.movement;
            if (originalJumpImpulse < 0f) originalJumpImpulse = movement.jumpImpulse;
            movement.jumpImpulse = originalJumpImpulse * multiplier;
        }

        public static void ApplyGravity()
        {
            float multiplier = LowGravityEnabled ? GravityMultiplier : 1f;

            foreach (var part in PlayerManager.GetLocalPlayer().refs.ragdoll.partList)
            {
                if (part?.Rig == null) continue;

                Vector3 gravity = Physics.gravity * multiplier;
                part.Rig.AddForce(gravity, ForceMode.Acceleration);
            }
        }

        public static void ApplyReverseGravity()
        {
            foreach (var part in PlayerManager.GetLocalPlayer().refs.ragdoll.partList)
            {
                if (part?.Rig == null) continue;

                Vector3 reverseGravity = -Physics.gravity;
                part.Rig.AddForce(reverseGravity, ForceMode.Acceleration);
            }
        }

        public static void KillPlayer(Character target)
        {
            target.photonView.RPC("RPCA_Die", RpcTarget.All, target.Center + Vector3.up * 0.2f + Vector3.forward * 0.1f);
        }

        public static void RevivePlayer(Character target)
        {
            target.photonView.RPC("RPCA_ReviveAtPosition", RpcTarget.All, target.Ghost != null ? target.Ghost.transform.position : target.Head + Vector3.up, false, -1);
        }

        public static void TeleportPlayerToMe(Character target)
        {
            PlayerManager.GetLocalPlayer().photonView.RPC("WarpPlayerRPC", RpcTarget.All, PlayerManager.GetLocalPlayer().Center + Vector3.forward * 1.5f, true);
        }

        public static void CrashPlayer(Character target)
        {
            if (target.photonView != null && target.photonView.Controller != null)
            {
                PhotonNetwork.DestroyPlayerObjects(target.photonView.Controller);
                PhotonNetwork.OpRemoveCompleteCacheOfPlayer(1);
                PhotonNetwork.OpRemoveCompleteCache();
            }
        }

        public static void CrashAllPlayers()
        {
            foreach (var player in PlayerManager.GetAllPlayers())
            {
                if (player.photonView != null && player.photonView.Controller != null)
                {
                    PhotonNetwork.DestroyPlayerObjects(player.photonView.Controller);
                    PhotonNetwork.OpRemoveCompleteCacheOfPlayer(1);
                    PhotonNetwork.OpRemoveCompleteCache();
                }
            }
        }

        public static void FlingPlayer(Character target)
        {
            target.photonView.RPC("RPCA_Fall", RpcTarget.All, 3f);
            Vector3 direction = Camera.main != null ? Camera.main.transform.forward : Vector3.forward;
            target.photonView.RPC("RPCA_AddForceToBodyPart", RpcTarget.All, 0, (Vector3.up * 1000000f) + (direction * 4000f));
        }

        public static void TumblePlayer(Character target)
        {
            float randomX = UnityEngine.Random.Range(-5000f, 5000f);
            float randomY = UnityEngine.Random.Range(2000f, 6000f);
            float randomZ = UnityEngine.Random.Range(-5000f, 5000f);
            target.photonView.RPC("RPCA_Fall", RpcTarget.All, 3f);
            target.photonView.RPC("RPCA_AddForceToBodyPart", RpcTarget.All, 0, new Vector3(randomX, randomY, randomZ));
        }

        public static void FreezePlayer(Character target)
        {
            target.photonView.RPC("RPCA_Stick", RpcTarget.All, (int)BodypartType.Hip, target.transform.position, target.transform.position, (int)CharacterAfflictions.STATUSTYPE.Cold, 0.1f);
        }

        public static void UnfreezePlayer(Character target)
        {
            target.photonView.RPC("RPCA_Unstick", RpcTarget.All);
        }

        public static void ExplodePlayer(Character target)
        {
            GameObject dynamite = PhotonNetwork.Instantiate("0_Items/Dynamite", target.Head, Quaternion.identity);
            if (dynamite != null)
            {
                PhotonView pv = dynamite.GetComponent<PhotonView>();
                if (pv != null)
                {
                    pv.RPC("RPC_Explode", RpcTarget.All);
                    PhotonNetwork.Destroy(dynamite);
                }
            }
        }

        public static void PassOutPlayer(Character target)
        {
            target.photonView.RPC("RPCA_PassOut", RpcTarget.All);
        }

        public static void UnPassOutPlayer(Character target)
        {
            target.photonView.RPC("RPCA_UnPassOut", RpcTarget.All);
        }

        public static void ReviveSelf()
        {
            RevivePlayer(PlayerManager.GetLocalPlayer());
        }

        public static void DumpItemsToFile()
        {
            GameObject[] items = Resources.LoadAll<GameObject>("0_Items");
            using (StreamWriter writer = new StreamWriter(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "ItemList.txt"), false))
            {
                foreach (GameObject item in items)
                {
                    writer.WriteLine("0_Items/" + item.name);
                }
            }
        }

        public static void DumpRPCsToFile()
        {
            try
            {
                new StringBuilder().AppendLine("--- RPC Dump Generated by Peak Mod ---");
                new StringBuilder().AppendLine($"--- Dumped on: {DateTime.Now} ---");
                new StringBuilder().AppendLine();
                foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    try
                    {
                        foreach (Type type in assembly.GetTypes())
                        {
                            foreach (MethodInfo method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance))
                            {
                                if (Attribute.IsDefined(method, typeof(PunRPC)))
                                {
                                    new StringBuilder().AppendLine($"{type.FullName}.{method.Name}");
                                }
                            }
                        }
                    }
                    catch (ReflectionTypeLoadException) { continue; }
                }

                File.WriteAllText(Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads"), "Game_RPC_Dump.txt"), new StringBuilder().ToString());
            }
            catch (Exception ex)
            {
            }
        }
    }
}