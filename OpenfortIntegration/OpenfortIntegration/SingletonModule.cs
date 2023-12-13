using Openfort.SDK;
using Openfort.SDK.Model;

namespace OpenfortIntegration;

public class SingletonModule
{
    private static SingletonModule instance;
    private static readonly object Padlock = new object();

    private const string OfApiKey = "YOUR_OF_API_KEY";
    public const string OfNftContract = "YOUR_NFT_CONTRACT_ID";
    public const string OfSponsorPolicy = "YOUR_POLICY_ID";
    
    public OpenfortClient OfClient { get; private set; }
    public int ChainId { get; } = 80001;
    public PlayerResponse? CurrentOfPlayer { get; set; }
    public AccountResponse? CurrentOfAccount { get; set; }

    SingletonModule()
    {
        OfClient = new OpenfortClient(OfApiKey);
    }

    public static SingletonModule Instance()
    {
        lock (Padlock)
        {
            if (instance == null)
            {
                instance = new SingletonModule();
            }
            return instance;
        }
    }
}
