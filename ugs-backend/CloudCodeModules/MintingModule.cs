using Openfort.SDK;
using Openfort.SDK.Model;
using Unity.Services.CloudCode.Apis;
using Unity.Services.CloudCode.Core;

namespace CloudCodeModules;

public class MintingModule: BaseModule
{
    private readonly SingletonModule _singleton;
    
    private readonly OpenfortClient _ofClient;
    private readonly int _chainId;
    private readonly PushClient _pushClient;

    public MintingModule(PushClient pushClient) 
    {
        _singleton = SingletonModule.Instance();
        _ofClient = _singleton.OfClient;
        _chainId = _singleton.ChainId;
        _pushClient = pushClient;
    }
    
    [CloudCodeFunction("MintNFT")]
    public async Task MintNFT(IExecutionContext context)
    {
        // Code to mint NFT
        var currentOfPlayer = _singleton.CurrentOfPlayer;
        var currentOfAccount = _singleton.CurrentOfAccount;

        if (currentOfPlayer == null || currentOfAccount == null)
        {
            throw new Exception("No Openfort account found for the player.");
        }

        Interaction interaction =
            new Interaction(null, SingletonModule.OfNftContract, null, "mint", new List<object>{currentOfAccount.Address});

        CreateTransactionIntentRequest request = new CreateTransactionIntentRequest(currentOfPlayer.Id, _chainId,
            SingletonModule.OfSponsorPolicy, null, false, 0, new List<Interaction> { interaction });

        var txResponse = await _ofClient.TransactionIntents.Create(request);

        await SendPlayerMessage(context, txResponse.Id);
    }
    
    [CloudCodeFunction("GetTransactionIntent")]
    public async Task<TransactionIntentResponse> GetTransactionIntent(string txId)
    {
        var response = await _ofClient.TransactionIntents.Get(new TransactionIntentGetRequest(txId));
        
        return response;
    }
    
    private async Task<string> SendPlayerMessage(IExecutionContext context, string message)
    {
        var response = await _pushClient.SendPlayerMessageAsync(context, message);
        return "Player message sent";
    }
}
