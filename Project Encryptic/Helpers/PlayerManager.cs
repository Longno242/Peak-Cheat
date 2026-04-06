using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zorro.Core.CLI;

public static class PlayerManager
{
    private static Character _localPlayer;
    private static readonly List<Character> _otherPlayers = new List<Character>();
    private static readonly List<Character> _allPlayers = new List<Character>();
    private static readonly Dictionary<int, Character> _playerLookup = new Dictionary<int, Character>();
    private static float _lastUpdateTime;
    private const float CACHE_UPDATE_INTERVAL = 0.5f;
    public static event Action OnPlayerCacheUpdated;
    public static Character LocalPlayer => GetLocalPlayer();
    public static IReadOnlyList<Character> AllPlayers => GetAllPlayers();
    public static IReadOnlyList<Character> OtherPlayers => GetOtherPlayers();
    public static int PlayerCount => GetPlayerCount();
    public static bool HasLocalPlayer => _localPlayer != null;
    private static bool ShouldUpdateCache() => Time.time - _lastUpdateTime >= CACHE_UPDATE_INTERVAL;

    private static void TryUpdateCache(bool forceRefresh = false)
    {
        if (forceRefresh || ShouldUpdateCache())
        {
            RefreshCache();
        }
    }

    public static void RefreshCache()
    {
        ClearCache();

        var allCharacters = Character.AllCharacters;
        if (allCharacters == null)
        {
            OnPlayerCacheUpdated?.Invoke();
            return;
        }

        foreach (var character in allCharacters)
        {
            _allPlayers.Add(character);

            if (character.IsLocal)
            {
                _localPlayer = character;
                continue;
            }

            if (character.photonView != null)
            {
                _playerLookup[character.photonView.ViewID] = character;
                _otherPlayers.Add(character);
            }

            else if (character.isBot)
            {
                _otherPlayers.Add(character);
            }
        }

        OnPlayerCacheUpdated?.Invoke();
    }

    private static void ClearCache()
    {
        _localPlayer = null;
        _otherPlayers.Clear();
        _allPlayers.Clear();
        _playerLookup.Clear();
        _lastUpdateTime = Time.time;
    }

    public static Character GetLocalPlayer(bool forceRefresh = false)
    {
        TryUpdateCache(forceRefresh);
        return _localPlayer;
    }

    public static IReadOnlyList<Character> GetOtherPlayers(bool forceRefresh = false)
    {
        TryUpdateCache(forceRefresh);
        return _otherPlayers;
    }

    public static IReadOnlyList<Character> GetAllPlayers(bool forceRefresh = false)
    {
        TryUpdateCache(forceRefresh);
        return _allPlayers;
    }

    public static int GetPlayerCount(bool forceRefresh = false)
    {
        TryUpdateCache(forceRefresh);
        return _allPlayers.Count;
    }

    public static Character GetPlayerByViewID(int viewID)
    {
        TryUpdateCache();
        return _playerLookup.TryGetValue(viewID, out var player) ? player : null;
    }

    public static Character GetPlayerByActorNumber(int actorNumber)
    {
        TryUpdateCache();
        return _allPlayers.FirstOrDefault(p => p?.photonView?.OwnerActorNr == actorNumber);
    }

    public static Character GetPlayerByName(string name, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
    {
        TryUpdateCache();
        return _allPlayers.FirstOrDefault(p => string.Equals(p?.characterName, name, comparison));
    }

    public static List<Character> GetAlivePlayers(bool includeLocal = true)
    {
        TryUpdateCache();
        return _allPlayers.Where(p => p != null && p.data != null && !p.data.dead && (includeLocal || !p.IsLocal)).ToList();
    }

    public static List<Character> GetDeadPlayers(bool includeLocal = true)
    {
        TryUpdateCache();
        return _allPlayers.Where(p => p != null && p.data != null && p.data.dead && (includeLocal || !p.IsLocal)).ToList();
    }

    public static List<Character> GetConsciousPlayers(bool includeLocal = true)
    {
        TryUpdateCache();
        return _allPlayers.Where(p => p != null && p.data != null && p.data.fullyConscious && (includeLocal || !p.IsLocal)).ToList();
    }

    public static List<Character> GetUnconsciousPlayers(bool includeLocal = true)
    {
        TryUpdateCache();
        return _allPlayers.Where(p => p != null && p.data != null && (p.data.passedOut || p.data.fullyPassedOut) && (includeLocal || !p.IsLocal)).ToList();
    }

    public static List<Character> GetClimbingPlayers(bool includeLocal = true)
    {
        TryUpdateCache();
        return _allPlayers.Where(p => p != null && p.data != null && (p.data.isClimbing || p.data.isRopeClimbing || p.data.isVineClimbing) && (includeLocal || !p.IsLocal)).ToList();
    }

    public static List<Character> GetZombiePlayers(bool includeLocal = true)
    {
        TryUpdateCache();
        return _allPlayers.Where(p => p != null && p.isZombie && (includeLocal || !p.IsLocal)).ToList();
    }

    public static List<Character> GetBotPlayers()
    {
        TryUpdateCache();
        return _allPlayers.Where(p => p != null && p.isBot).ToList();
    }

    public static float GetDistance(Character a, Character b)
    {
        if (a == null || b == null) return float.MaxValue;
        return Vector3.Distance(a.Center, b.Center);
    }

    public static float GetDistanceToLocalPlayer(Character character)
    {
        if (character == null) return float.MaxValue;
        var local = GetLocalPlayer();
        return local == null ? float.MaxValue : Vector3.Distance(character.Center, local.Center);
    }

    public static Character GetNearestPlayer(Vector3 position, bool includeLocal = true, Func<Character, bool> predicate = null)
    {
        TryUpdateCache();

        Character nearest = null;
        float nearestDistance = float.MaxValue;

        foreach (var player in _allPlayers)
        {
            if (player == null) continue;
            if (player.data?.dead != false) continue;
            if (!includeLocal && player.IsLocal) continue;
            if (predicate != null && !predicate(player)) continue;

            float distance = Vector3.Distance(position, player.Center);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearest = player;
            }
        }

        return nearest;
    }

    public static List<Character> GetPlayersInRadius(Vector3 center, float radius, bool includeLocal = true, Func<Character, bool> predicate = null)
    {
        TryUpdateCache();

        return _allPlayers.Where(p =>
            p != null &&
            p.data?.dead == false &&
            (includeLocal || !p.IsLocal) &&
            Vector3.Distance(center, p.Center) <= radius &&
            (predicate == null || predicate(p))
        ).ToList();
    }

    public static bool IsPlayerAlive(Character character)
    {
        return character?.data != null && !character.data.dead;
    }

    public static bool IsPlayerConscious(Character character)
    {
        return character?.data != null && character.data.fullyConscious;
    }

    public static bool IsPlayerLocal(Character character)
    {
        return character?.IsLocal == true;
    }

    public static bool IsPlayerBot(Character character)
    {
        return character?.isBot == true;
    }

    public static bool IsPlayerZombie(Character character)
    {
        return character?.isZombie == true;
    }

    public static Vector3 GetAveragePlayerPosition(bool includeLocal = true)
    {
        var players = includeLocal ? GetAllPlayers() : GetOtherPlayers();
        if (players.Count == 0) return Vector3.zero;

        Vector3 sum = Vector3.zero;
        int count = 0;

        foreach (var player in players)
        {
            if (player != null)
            {
                sum += player.Center;
                count++;
            }
        }

        return count > 0 ? sum / count : Vector3.zero;
    }

    public static List<Character> GetPlayersByDistance(Vector3 position, bool ascending = true)
    {
        GetAllPlayers().Where(p => p != null).ToList().Sort((a, b) =>
        {
            float distA = Vector3.Distance(position, a.Center);
            float distB = Vector3.Distance(position, b.Center);
            return ascending ? distA.CompareTo(distB) : distB.CompareTo(distA);
        });
        return GetAllPlayers().Where(p => p != null).ToList();
    }

    [ConsoleCommand]
    public static void DebugPlayers()
    {
        TryUpdateCache(true);

        Debug.Log($"=== Player Manager Debug ===");
        Debug.Log($"Total Players: {_allPlayers.Count}");
        Debug.Log($"Local Player: {(_localPlayer?.characterName ?? "None")}");
        Debug.Log($"Other Players: {_otherPlayers.Count}");
        Debug.Log($"Bots: {_allPlayers.Count(p => p?.isBot == true)}");
        Debug.Log($"Zombies: {_allPlayers.Count(p => p?.isZombie == true)}");
        Debug.Log($"Alive: {_allPlayers.Count(p => p?.data?.dead == false)}");
        Debug.Log($"Dead: {_allPlayers.Count(p => p?.data?.dead == true)}");

        for (int i = 0; i < _allPlayers.Count; i++)
        {
            var player = _allPlayers[i];
            if (player == null) continue;

            Debug.Log($"Player {i}: {player.characterName} | " +
                $"Alive: {!player.data?.dead} | " +
                $"Conscious: {player.data?.fullyConscious} | " +
                $"Local: {player.IsLocal} | " +
                $"Bot: {player.isBot} | " +
                $"Zombie: {player.isZombie} | " +
                $"Actor: {player.photonView?.OwnerActorNr} | " +
                $"Pos: {player.Center}");
        }
    }

    public static void OnCharacterSpawned(Character character)
    {
        RefreshCache();
    }

    public static void OnCharacterDespawned(Character character)
    {
        RefreshCache();
    }
}