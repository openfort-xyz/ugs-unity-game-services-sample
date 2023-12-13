using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Openfort.Model;
using Unity.Services.CloudCode;
using Unity.Services.CloudCode.Subscriptions;
using UnityEngine;

public class MintingController : BaseController
{
    [Header("NFT related")]
    public GameObject nftsPanel;
    public NftPrefab nftPrefab;
    
    public async void AuthController_OnAuthSuccess_Handler(string ofPlayerId)
    {
        await SubscribeToPlayerMessages();
        
        await Task.Delay(1000);
        GetAccountNftInventory();
    }

    public async void MintNFT()
    {
        statusText.text = "Minting NFT...";
        await CloudCodeService.Instance.CallModuleEndpointAsync("OpenfortIntegration", "MintNFT");
        // Let's wait for the message from backend --> Inside SubscribeToPlayerMessages()
    }
    
    public async void GetAccountNftInventory()
    {
        statusText.text = "Getting NFT inventory...";
        
        try
        {
            var inventoryList = await CloudCodeService.Instance.CallModuleEndpointAsync<InventoryListResponse>("OpenfortIntegration", "GetAccountNftInventory");

            if (inventoryList.Data.Count == 0)
            {
                statusText.text = "Player has no NFTs.";
                viewPanel.SetActive(true);
            }
            else
            {
                statusText.text = "NFT inventory retrieved.";
                nftsPanel.SetActive(true);
                
                foreach (var nft in inventoryList.Data)
                {
                    var instantiatedNft = Instantiate(nftPrefab, nftsPanel.transform);
                    instantiatedNft.Setup(nft.AssetType.ToString(), nft.TokenId.ToString());
                    Debug.Log(nft);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task GetTransactionIntent(string transactionIntentId)
    {
        statusText.text = "Getting transaction intent...";

        var functionParams = new Dictionary<string, object> { { "txId", transactionIntentId } };
        var txIntent = await CloudCodeService.Instance.CallModuleEndpointAsync<TransactionIntentResponse>("OpenfortIntegration", "GetTransactionIntent", functionParams);

        while (txIntent.Response.Status == 0)
        {
            // Perhaps add a delay here for the API calls to not continuously hit an endpoint.
            await Task.Delay(1000);

            txIntent = await CloudCodeService.Instance.CallModuleEndpointAsync<TransactionIntentResponse>("OpenfortIntegration", "GetTransactionIntent", functionParams);
        }

        if (txIntent.Response.Status == 1)
        {
            statusText.text = "Transaction successful. Please wait...";
            
            await Task.Delay(8000);
            GetAccountNftInventory();
        }
    }
    
    private Task SubscribeToPlayerMessages()
    {
        // Register callbacks, which are triggered when a player message is received
        var callbacks = new SubscriptionEventCallbacks();
        callbacks.MessageReceived += async @event =>
        {
            Debug.Log(DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss.fffK"));

            var txId = @event.Message;
            Debug.Log("Transaction ID: " + txId);
            
            await GetTransactionIntent(txId);
        };
        callbacks.ConnectionStateChanged += @event =>
        {
            Debug.Log($"Got player subscription ConnectionStateChanged: {JsonConvert.SerializeObject(@event, Formatting.Indented)}");
        };
        callbacks.Kicked += () =>
        {
            Debug.Log($"Got player subscription Kicked");
        };
        callbacks.Error += @event =>
        {
            Debug.Log($"Got player subscription Error: {JsonConvert.SerializeObject(@event, Formatting.Indented)}");
        };
        return CloudCodeService.Instance.SubscribeToPlayerMessagesAsync(callbacks);
    }
}