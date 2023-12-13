using System.Diagnostics;
using Unity.Services.CloudCode.Core;
using Unity.Services.CloudCode.Apis;
using Openfort.SDK;
using Openfort.SDK.Client;
using Openfort.SDK.Model;
using Unity.Services.CloudSave.Model;

namespace OpenfortIntegration;

public class PlayersModule: BaseModule
{
    private readonly SingletonModule _singleton;
    
    private readonly OpenfortClient _ofClient;
    private readonly int _chainId;

    public PlayersModule()
    {
        _singleton = SingletonModule.Instance();
        
        _ofClient = _singleton.OfClient;
        _chainId = _singleton.ChainId;
    }
    
    [CloudCodeFunction("CreateOpenfortPlayer")]
    public async Task<PlayerResponse?> CreateOpenfortPlayer(IExecutionContext context, IGameApiClient gameApiClient, string playerName)
    {
        // Create player
        var request = new PlayerCreateRequest(playerName);
        var player = await _ofClient.Players.Create(request);
        
        // Save Openfort Player to Singleton
        _singleton.CurrentOfPlayer = player;
        
        // Create account
        var accRequest = new CreateAccountRequest(_chainId, null!, null, null!, 0L, player.Id);
        var account = await _ofClient.Accounts.Create(accRequest);

        _singleton.CurrentOfAccount = account;
        
        // Now you can call SaveData with the context
        await SaveData(context, gameApiClient, "OpenfortPlayerId", player.Id);
        await SaveData(context, gameApiClient, "OpenfortAccountId", account.Id);
        return player;
    }
    
    [CloudCodeFunction("GetAccountNftInventory")]
    public async Task<InventoryListResponse?> GetAccountNftInventory()
    {
        // Important to remember that this account persists in the Singleton until a new Openfort player is created.
        var currentOfAccount = _singleton.CurrentOfAccount;

        if (currentOfAccount == null)
        {
            Debug.WriteLine("Openfort account is null.");
            return null;
        }
        
        var request = new AccountInventoryListRequest(currentOfAccount.Id);
        var inventoryList = await _ofClient.Inventories.GetAccountNftInventory(request);
        
        return inventoryList;
    }
    
    private async Task SaveData(IExecutionContext context, IGameApiClient gameApiClient, string key, object value)
    {
        try
        {
            await gameApiClient.CloudSaveData.SetItemAsync(context, context.AccessToken, context.ProjectId,
                context.PlayerId, new SetItemBody(key, value));
        }
        catch (ApiException ex)
        {
            throw new Exception($"Failed to save data for playerId {context.PlayerId}. Error: {ex.Message}");
        }
    }
}