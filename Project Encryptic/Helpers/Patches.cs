using DG.Tweening.Plugins.Core;
using HarmonyLib;
using ModernModMenu;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Project_Encryptic.Helpers
{
    internal class Patches
    {

        public static bool AlwaysAnchorGlobal;
        public static bool InfiniteRopeGlobal;
        public static bool ZeroGravityLocal;
        public static bool InfiniteRopeLocal;
        public static bool AntiConsume;
        public static bool LowGravityEnabled;
        public static bool TimeScaleEnabled;
        public static float TimeScaleValue = 1.0f;
        public static bool CustomPoofEnabled;
        public static Color CustomPoofColor = Color.white;
        public static bool ItemSpawnEnabled;
        public static float CustomPoofScale = 1.0f;
        public static bool NoDstry;
        public static bool NoR;
        private static Dictionary<string, float> originalJumpValues = new Dictionary<string, float>();
        public static bool NoFallDamageEnabled { get; set; } = false;
        public static float JumpMultiplier { get; set; } = 1.0f;
        public static float SpeedMultiplier { get; set; } = 1.0f;
        public static bool AlwaysSprintEnabled { get; set; } = false;



        [HarmonyPatch(typeof(Character), "StartPassedOutOnTheBeach")]
        public static class PreventBeachPassout
        {
            [HarmonyPostfix]
            static void WakeUp(Character __instance)
            {
                if (__instance.IsLocal)
                {
                    __instance.data.passedOut = false;
                    __instance.data.fullyPassedOut = false;
                    __instance.data.passOutValue = 0f;
                    __instance.data.passedOutOnTheBeach = 0f;
                    __instance.data.lastPassedOut = float.MinValue;
                    __instance.photonView.RPC("RPCA_UnFall", RpcTarget.All);
                }
            }
        }

        [HarmonyPatch(typeof(Character), "FixedUpdate")]
        [HarmonyPostfix]
        public static void GravityPatch(Character __instance)
        {
            if (LowGravityEnabled || NoR)
            {
                float gravityMultiplier = LowGravityEnabled ? 0.2f : (NoR ? -1f : 1f);
                Vector3 gravityForce = Vector3.up * (Physics.gravity.y * (1f - gravityMultiplier));
                __instance.photonView.RPC("RPCA_ApplyGravityForce", RpcTarget.All, gravityForce);
            }
        }

        [HarmonyPatch(typeof(Character), "Awake")]
        [HarmonyPostfix]
        public static void InvisibilityPatch(Character __instance)
        {
            if (NoDstry && __instance.IsLocal)
            {
                __instance.photonView.RPC("RPCA_SetVisibility", RpcTarget.All, false);
            }
        }

        [HarmonyPatch(typeof(Character), "Update")]
        [HarmonyPostfix]
        public static void TimeScalePatch(Character __instance)
        {
            if (TimeScaleEnabled && PhotonNetwork.IsMasterClient && __instance.IsLocal)
            {
                float targetTimeScale = TimeScaleValue;
                __instance.photonView.RPC("RPCA_SetTimeScale", RpcTarget.All, targetTimeScale);
            }
        }

        [HarmonyPatch(typeof(Character), "WarpPlayer")]
        [HarmonyPostfix]
        public static void CustomPoofPatch(Character __instance, Vector3 position, bool poof)
        {
            if (poof && CustomPoofEnabled)
            {
                __instance.photonView.RPC("RPCA_CustomPoofVFX", RpcTarget.All, CustomPoofColor, CustomPoofScale);
            }
        }

        [HarmonyPatch(typeof(CharacterItems), "EquipSlotRpc")]
        [HarmonyPostfix]
        public static void ItemSpawnPatch(CharacterItems __instance, byte slot, int viewID)
        {
            if (ItemSpawnEnabled)
            {
                PhotonView itemView = PhotonView.Find(viewID);
            }
        }

        [HarmonyPatch(typeof(Item), nameof(Item.CarryWeight), MethodType.Getter)]
        [HarmonyPrefix]
        public static bool CarryWeight_Getter(ref int __result)
        {
            __result = 0;
            return false;
        }

        [HarmonyPatch(typeof(Item))]
        [HarmonyPatch("Update", MethodType.Normal)]
        [HarmonyPrefix]
        public static void Update_Prefix(Item __instance)
        {
            object groundState = Enum.Parse(__instance.GetType().Assembly.GetType("ItemState") ?? throw new Exception("ItemState enum not found"), "Ground");
            if (__instance.itemState.Equals(groundState))
            {
                typeof(Item).GetField("destroyTick", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(__instance, 0f);
            }
        }

        [HarmonyPatch(typeof(Item))]
        [HarmonyPatch("ContinueUsePrimary", MethodType.Normal)]
        [HarmonyPrefix]
        public static bool ContinueUsePrimary_Prefix(Item __instance)
        {
            if (__instance.isUsingPrimary)
            {
                MethodInfo finishMethod = typeof(Item).GetMethod("FinishCastPrimary", BindingFlags.Instance | BindingFlags.NonPublic);
                finishMethod.Invoke(__instance, null);
            }
            return false;
        }

        [HarmonyPatch(typeof(Item))]
        [HarmonyPatch("CanUsePrimary", MethodType.Normal)]
        [HarmonyPrefix]
        public static bool CanUsePrimary_Prefix(ref bool __result)
        {
            __result = true;
            return false;
        }

        [HarmonyPatch(typeof(Item))]
        [HarmonyPatch("CanUseSecondary", MethodType.Normal)]
        [HarmonyPrefix]
        public static bool CanUseSecondary_Prefix(ref bool __result)
        {
            __result = true;
            return false;
        }

        [HarmonyPatch(typeof(Item))]
        [HarmonyPatch("RemoveFromActiveList", MethodType.Normal)]
        [HarmonyPrefix]
        public static bool RemoveFromActiveList_Prefix()
        {
            return false;
        }

        [HarmonyPatch(typeof(Item))]
        [HarmonyPatch("ConsumeDelayed", MethodType.Normal)]
        [HarmonyPrefix]
        public static bool ConsumeDelayed_Prefix(Item __instance)
        {
            __instance.Consume(__instance.holderCharacter != null ? __instance.holderCharacter.photonView.ViewID : -1);
            return false;
        }

        [HarmonyPatch(typeof(Item), nameof(Item.IsInteractible))]
        [HarmonyPrefix]
        public static bool IsInteractible_Prefix(ref bool __result)
        {
            __result = true;
            return false;
        }

        [HarmonyPatch(typeof(Item), nameof(Item.Interact))]
        [HarmonyPrefix]
        public static void Interact_Prefix(Item __instance, Character interactor)
        {
            __instance.Consume(interactor.photonView.ViewID);
        }

        [HarmonyPatch(typeof(Character), "UseStamina")]
        [HarmonyPrefix]
        public static bool AlwaysSprintPatch(Character __instance, float usage)
        {
            if (AlwaysSprintEnabled && __instance.data.isSprinting)
            {
                return false;
            }
            return true;
        }

        [HarmonyPatch]
        public static class SprintPatches
        {
            [HarmonyPatch(typeof(Character), "CheckSprint")]
            [HarmonyPostfix]
            public static void ForceSprintCheck(Character __instance, ref bool __result)
            {
                if (AlwaysSprintEnabled && __instance.IsLocal)
                {
                    if (__instance.data.fullyConscious && __instance.input.movementInput.y > 0.01f)
                    {
                        __result = true;
                    }
                }
            }

            [HarmonyPatch(typeof(Character), "SetMovementState")]
            [HarmonyPostfix]
            public static void ForceSprintState(Character __instance)
            {
                if (AlwaysSprintEnabled && __instance.IsLocal && __instance.data != null)
                {
                    if (__instance.input.movementInput.y > 0.01f &&
                        __instance.data.fullyConscious &&
                        !__instance.data.isClimbing &&
                        !__instance.data.isRopeClimbing &&
                        !__instance.data.isVineClimbing)
                    {
                        __instance.data.isSprinting = true;
                    }
                }
            }

            [HarmonyPatch(typeof(CharacterMovement), "CheckFallDamage")]
            class NoFallDamagePatch
            {
                public static bool Prefix()
                {
                    return !ModernModMenu.ModernModMenu.NoFallDamage;
                }
            }

            [HarmonyPatch(typeof(Item), "Consume")]
            public class ConsumePatch
            {
                private static bool Prefix(Item __instance)
                {
                    return !AntiConsume || !__instance.holderCharacter.photonView.IsMine;
                }
            }

            [HarmonyPatch(typeof(CharacterItems), "DropAllItems")]
            class KeepItemsPatch
            {
                public static bool Prefix()
                {
                    return !ModernModMenu.ModernModMenu.KeepItems;
                }
            }

            [HarmonyPatch]
            public static class RopePatches
            {
                [HarmonyPatch(typeof(Rope), "AttachToAnchor_Rpc")]
                [HarmonyPostfix]
                public static void ForceAnchor(Rope __instance)
                {
                    if (AlwaysAnchorGlobal)
                    {
                        __instance.attachmenState = Rope.ATTACHMENT.anchored;
                        __instance.isClimbable = true;
                    }
                }

                private static IEnumerator ResetJumpImpulse(CharacterMovement movementInstance, float originalJump, string instanceId)
                {
                    yield return new WaitForSeconds(0.5f);
                    if (movementInstance != null)
                    {
                        movementInstance.jumpImpulse = originalJump;
                    }
                    originalJumpValues.Remove(instanceId);
                }

                [HarmonyPatch(typeof(CharacterMovement), "JumpRpc")]
                [HarmonyPrefix]
                public static void JumpHackPatch(CharacterMovement __instance)
                {
                    if (JumpMultiplier > 1.0f)
                    {
                        string instanceId = __instance.GetInstanceID().ToString();
                        if (!originalJumpValues.ContainsKey(instanceId))
                        {
                            originalJumpValues[instanceId] = __instance.jumpImpulse;
                        }

                        __instance.jumpImpulse *= JumpMultiplier;
                        __instance.StartCoroutine(ResetJumpImpulse(__instance, originalJumpValues[instanceId], instanceId));
                    }
                }

                [HarmonyPatch(typeof(CharacterMovement), "GetMovementForce")]
                [HarmonyPostfix]
                public static void SpeedHackPatch(ref float __result)
                {
                    if (SpeedMultiplier > 1.0f)
                    {
                        __result *= SpeedMultiplier;
                    }
                }

                [HarmonyPatch(typeof(Rope), "MaxSegments", MethodType.Getter)]
                [HarmonyPostfix]
                public static void IncreaseMaxSegments(ref int __result)
                {
                    if (InfiniteRopeGlobal)
                    {
                        __result = 999;
                    }
                }

                [HarmonyPatch(typeof(Rope), "FixedUpdate")]
                [HarmonyPrefix]
                public static void ApplyZeroGravity(Rope __instance)
                {
                    if (ZeroGravityLocal && __instance.view != null && __instance.view.IsMine)
                    {
                        __instance.antigrav = true;
                    }
                }

                [HarmonyPatch(typeof(Rope), "Segments", MethodType.Setter)]
                [HarmonyPrefix]
                public static void AllowLongerRopes(Rope __instance, ref float value)
                {
                    if (InfiniteRopeLocal && value > 0f)
                    {
                        value = Mathf.Clamp(value, 0f, 999f);
                    }
                }

                [HarmonyPatch(typeof(Rope), "Awake")]
                [HarmonyPostfix]
                public static void MakeClimbable(Rope __instance)
                {
                    if (AlwaysAnchorGlobal)
                    {
                        __instance.isClimbable = true;
                    }
                }
            }
        }
    }
}
