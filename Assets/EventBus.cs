﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gardarike;
using Google.Protobuf.Collections;

public class EventBus : MonoBehaviour
{
    public const string NEW_BUILDING_EVENT = "NEW_BUILDING";
    public static EventBus instance;

    public event Action<BuildItemInfo> onBuildingRegistrationEvent;
    public event Action<BuildItemInfo> onBulidingComplete;
    public event Action<float[,]> onTerrainGenerationFinished;
    public event Action<int, int, float[,]> onTerrainLoadingComplete;
    public event Action<RepeatedField<Building>, int> onMapObjectsLoadingComplete;
    public event Action<string, RepeatedField<Character>> onLoginComplete;
    public event Action<string> onErrorShowRequest;
    public event Action<string, string> onInfoMessageShowRequest;
    public event Action<long, string, string, string> onInputDialogShowRequest;
    public event Action<string, string> onLoginRequest;
    public event Action<Character> onSelectCharacterRequest;
    public event Action onLoadChatHistoryRequest;
    public event Action<RepeatedField<ChatMessage>> onChatHistoryLoaded;
    public event Action<string> onMapLoadRequest;
    public event Action<RepeatedField<Gardarike.Town>> onCharacterSelected;
    public event Action<ChatMessage> onNewMessageArrived;
    public event Action<string> onChatMessagePublishRequest;
    public event Action<Character> onCharacterUpdateArrived;
    public event Action<bool> onOpenOrCloseLoadingDialog;
    public event Action<int> onPeopleCountIncreased;
    public event Action<int> onSpawnTreesRequest;
    public event Action onMapReady;
    public event Action<GetWorldMapResponse> onWorldMapChunkLoaded;
    //public event Action<ResourceUpdatedEvent> onResourceUpdateArrived;
    public event Action onJobMarketLoadingRequest;
    public event Action<string, string, string> onRegistrationRequest;
    public event Action<long, DialogResult> onDialogResulted;
    public event Action onRegistrationComplete;
    public event Action<Vector2D, String> onNewTownRequest;
    public event Action<String> onNewCharacterRequest;
    
    private void Awake()
    {
        instance = this;
    }

    public void RegisterBuilding(BuildItemInfo building)
    {
        onBuildingRegistrationEvent?.Invoke(building);
    }

    public void BuildingComplete(BuildItemInfo building)
    {
        onBulidingComplete?.Invoke(building);
    }

    public void TerrainGenerationFinished(float[,] heights)
    {
        onTerrainGenerationFinished?.Invoke(heights);
    }

    public void TerrainLoaded(int width, int height, float[,] heights)
    {
        onTerrainLoadingComplete?.Invoke(width, height, heights);
    }

    public void MapObjectsLoaded(RepeatedField<Building> buildings, int treesCount) {
        onMapObjectsLoadingComplete?.Invoke(buildings, treesCount);
    }

    public void LoginComplete(string sessionId, RepeatedField<Character> characters) {
        onLoginComplete?.Invoke(sessionId, characters);
    }

    public void ShowError(string error)
    {
        onErrorShowRequest?.Invoke(error);
    }

    public void ShowInfo(string title, string message)
    {
        onInfoMessageShowRequest?.Invoke(title, message);
    }

    /**
    Return dialog id, you can use complete callback for this dialog using provided id
    */
    public long ShowInputDialog(string title, string bodyMessage, string property)
    {
        // TODO maybe bad generation and not the right place for it
        var randomId = UnityEngine.Random.Range(0, 9999999);

        onInputDialogShowRequest?.Invoke(randomId, title, bodyMessage, property);

        return randomId;
    }

    public void LoginRequest(string username, string password)
    {
        onLoginRequest?.Invoke(username, password);
    }

    public void SelectCharacterRequest(Character character)
     {
        onSelectCharacterRequest?.Invoke(character);
    }

    public void LoadMap(string sessionId) {
        onMapLoadRequest?.Invoke(sessionId);
    }

    public void CharacterSelectionConfirmed(RepeatedField<Gardarike.Town> town) {
        onCharacterSelected?.Invoke(town);
    }

    public void LoadChatHistory() {
        onLoadChatHistoryRequest?.Invoke();
    }

    public void ChatHistoryLoaded(RepeatedField<ChatMessage> chatMessages) {
        onChatHistoryLoaded?.Invoke(chatMessages);
    }

    public void NewMessageArrived(ChatMessage chatMessage) {
        onNewMessageArrived?.Invoke(chatMessage);
    }

    public void SendChatMessage(string text) {
        onChatMessagePublishRequest?.Invoke(text);
    }

    public void CharacterUpdateArrived(Character newState)
    {
        onCharacterUpdateArrived?.Invoke(newState);
    }

    public void OpenLoadingDialog()
    {
        onOpenOrCloseLoadingDialog?.Invoke(true);
    }
    public void CloseLoadingDialog()
    {
        onOpenOrCloseLoadingDialog?.Invoke(false);
    }

    public void PeopleCountIncreased(int newPeople)
    {
        onPeopleCountIncreased?.Invoke(newPeople);
    }

    public void SpawnTrees(int count)
    {
        onSpawnTreesRequest?.Invoke(count);
    }

    public void MapIsReady()
    {
        onMapReady?.Invoke();
    }

    // public void UpdateResources(ResourceUpdatedEvent update) {
    //     onResourceUpdateArrived?.Invoke(update);
    // }

    public void RequestJobMarketInfo() {
        onJobMarketLoadingRequest?.Invoke();
    }

    public void SendRegistrationRequest(string user, string password, string email)
    {
        onRegistrationRequest?.Invoke(user, password, email);
    }

    public void NotifyDialogResulted(long dialogId, DialogResult result)
    {
        onDialogResulted?.Invoke(dialogId, result);
    }
    
    public void NotifyRegistrationComplete()
    {
        onRegistrationComplete?.Invoke();
    }

    public void SendNewTownRequest(Vector2D location, String name)
    {
        onNewTownRequest?.Invoke(location, name);
    }

    public void SendNewCharacterRequest(string name)
    {
        onNewCharacterRequest?.Invoke(name);
    }

    public void WorldMapChunkLoaded(GetWorldMapResponse chunkInfo)
    {
        onWorldMapChunkLoaded?.Invoke(chunkInfo);
    }
}
