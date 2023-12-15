# Openfort Unity Gaming Services Integration
  
## Overview
This is a sample project to showcase the Openfort integration with [Unity Gaming Services](https://unity.com/solutions/gaming-services), a complete service ecosystem for Unity live games.

The sample includes:
  - [**`ugs-backend`**](https://github.com/openfort-xyz/ugs-unity-game-services-sample/tree/main/OpenfortIntegration)
    
    A .NET Core project with [Cloud Code C# modules](https://docs.unity.com/ugs/en-us/manual/cloud-code/manual/modules#Cloud_Code_C#_modules) that implement [Openfort C# SDK](https://www.nuget.org/packages/Openfort.SDK/1.0.21) methods. Hosted in UGS.

  - [**`unity-client`**](https://github.com/openfort-xyz/ugs-unity-game-services-sample/tree/main/unity-client)

    A Unity sample game that connects to ``ugs-backend`` through [Cloud Code](https://docs.unity.com/ugs/manual/cloud-code/manual). It uses [Openfort Unity SDK](https://github.com/openfort-xyz/openfort-csharp-unity) to deserialize its responses.  

The sample uses [Unity Authentication](https://docs.unity.com/ugs/en-us/manual/authentication/manual/get-started) to sign in as a new anonymous player. It then [creates an Openfort player](https://github.com/openfort-xyz/ugs-unity-game-services-sample/blob/ab1a5f69346910c18ea88579f6fce81cdcde489a/ugs-backend/CloudCodeModules/PlayersModule.cs#L31) and a [custodial account](https://github.com/openfort-xyz/ugs-unity-game-services-sample/blob/ab1a5f69346910c18ea88579f6fce81cdcde489a/ugs-backend/CloudCodeModules/PlayersModule.cs#L38) using Openfort and it [links it](https://github.com/openfort-xyz/ugs-unity-game-services-sample/blob/ab1a5f69346910c18ea88579f6fce81cdcde489a/ugs-backend/CloudCodeModules/PlayersModule.cs#L43) to the Unity player. Then the player can frictionlessly [mint an NFT](https://github.com/openfort-xyz/ugs-unity-game-services-sample/blob/ab1a5f69346910c18ea88579f6fce81cdcde489a/ugs-backend/CloudCodeModules/MintingModule.cs#L26).

## Prerequisites
+ **Get started with Openfort**
  + [Sign in](https://dashboard.openfort.xyz/login) or [sign up](https://dashboard.openfort.xyz/register) and create a new dashboard project

+ **Get started with UGS**
  + [Complete basic prerequisites](https://docs.unity.com/ugs/manual/overview/manual/getting-started#Prerequisites)
  + [Create a project](https://docs.unity.com/ugs/manual/overview/manual/getting-started#CreateProject)

## Setup Openfort dashboard
  + [Add a Contract](https://dashboard.openfort.xyz/assets/new)
    
    This sample requires a contract to run. We use [0x38090d1636069c0ff1Af6bc1737Fb996B7f63AC0](https://mumbai.polygonscan.com/address/0x38090d1636069c0ff1Af6bc1737Fb996B7f63AC0) (NFT contract deployed in 80001 Mumbai). You can use this for the guide:

    <div align="center">
    <img
      width="50%"
      height="50%"
      src="https://blog-cms.openfort.xyz/uploads/ugs_integration_4_9397f3633b.png?updated_at=2023-12-14T15:59:33.808Z"
      alt='Contract Info'
    />
    </div>

  + [Add a Policy](https://dashboard.openfort.xyz/policies/new)
    
    We aim to cover gas fees for users. Set a new gas policy:

    <div align="center">
    <img
      width="50%"
      height="50%"
      src="https://blog-cms.openfort.xyz/uploads/ugs_integration_5_ab3d8ad48d.png?updated_at=2023-12-14T15:59:33.985Z"
      alt='Gas Policy'
    />
    </div>

    Now, add a rule so our contract uses this policy:

    <div align="center">
    <img
      width="50%"
      height="50%"
      src="https://blog-cms.openfort.xyz/uploads/ugs_integration_6_6727e69146.png?updated_at=2023-12-14T15:59:33.683Z"
      alt='Policy Rule'
    />
    </div>

## Set up [`ugs-backend`](https://github.com/openfort-xyz/ugs-unity-game-services-sample/tree/main/ugs-backend)

- ### Set Openfort dashboard variables

  Open the [solution](https://github.com/openfort-xyz/ugs-unity-game-services-sample/blob/main/ugs-backend/CloudCodeModules.sln) with your preferred IDE, open [``SingletonModule.cs``](https://github.com/openfort-xyz/ugs-unity-game-services-sample/blob/main/OpenfortIntegration/OpenfortIntegration/SingletonModule.cs) and fill in these variables:

  <div align="center">
    <img
      width="50%"
      height="50%"
      src="https://blog-cms.openfort.xyz/uploads/ugs_integration_1_6001ca1099.png?updated_at=2023-12-13T17:45:19.990Z"
      alt='Singleton Module'
    />
    </div>

  - [Retrieve the **Secret key**](https://dashboard.openfort.xyz/apikeys)
  - [Retrieve the **Contract API ID**](https://dashboard.openfort.xyz/assets)
  - [Retrieve the **Policy API ID**](https://dashboard.openfort.xyz/policies)

- ### Package Code
  Follow [the official documentation steps](https://docs.unity.com/ugs/en-us/manual/cloud-code/manual/modules/getting-started#Package_code).
- ### Deploy to UGS
  Follow [the official documentation steps](https://docs.unity.com/ugs/en-us/manual/cloud-code/manual/modules/getting-started#Deploy_a_module_project).

## Set up [``unity-client``](https://github.com/openfort-xyz/ugs-unity-game-services-sample/tree/main/unity-client)

Follow the [official documentation steps](https://docs.unity.com/ugs/manual/authentication/manual/get-started#Link_your_project) to link the ``unity-client`` to your UGS Project ID.

## Test in Editor
Play the **Main** scene and click ***Sign in*** button. After some authentication-related logs, this panel should appear:

<div align="center">
    <img
      width="50%"
      height="50%"
      src="https://blog-cms.openfort.xyz/uploads/playfab_opensea_img_32_35f675ded4.png?updated_at=2023-11-19T11:06:40.788Z"
      alt='Game Scene'
    />
</div>

Select ***Mint***. After a brief period, you should see a representation of your newly minted NFT:

<div align="center">
    <img
      width="50%"
      height="50%"
      src="https://blog-cms.openfort.xyz/uploads/ugs_integration_3_181e80ff26.png?updated_at=2023-12-14T10:02:43.778Z"
      alt='Mint Panel'
    />
</div>

In the [Openfort Players dashboard](https://dashboard.openfort.xyz/players), a new player entry should be visible. On selecting this player:

<div align="center">
    <img
      width="50%"
      height="50%"
      src="https://blog-cms.openfort.xyz/uploads/playfab_opensea_img_34_706b0d267e.png?updated_at=2023-11-19T11:06:46.177Z"
      alt='Player Entry'
    />
</div>

You'll notice that a `mint` transaction has been successfully processed:

<div align="center">
    <img
      width="50%"
      height="50%"
      src="https://blog-cms.openfort.xyz/uploads/ugs_integration_7_75cf7a4264.png?updated_at=2023-12-14T16:05:01.500Z"
      alt='Mint Transaction'
    />
</div>

Additionally, by choosing your **Mumbai Account** and viewing ***NFT Transfers***, the transaction is further confirmed:

<div align="center">
    <img
      width="50%"
      height="50%"
      src="https://blog-cms.openfort.xyz/uploads/ugs_integration_8_6b345bd148.png?updated_at=2023-12-14T16:05:00.991Z"
      alt='Etherscan'
    />
</div>

Keep in mind that the sample is designed so a player can mint only once. By default, UGS Authentication will use the same player per device. If you want to sign in with a new player check the ***Clear Session Token*** in ***AuthController***:

<div align="center">
    <img
      width="50%"
      height="50%"
      src="https://blog-cms.openfort.xyz/uploads/ugs_integration_2_b0fae3ec75.png?updated_at=2023-12-14T09:56:42.591Z"
      alt='Clear Session Token'
    />
</div>

## Conclusion

Upon completing the above steps, your Unity game will be fully integrated with Openfort and UGS. Always remember to test every feature before deploying to guarantee a flawless player experience.

For a deeper understanding of the underlying processes, check out the [tutorial video](https://www.youtube.com/watch?v=Anl9X253RC8). 

## Get support
If you found a bug or want to suggest a new [feature/use case/sample], please [file an issue](../../issues).

If you have questions, or comments, or need help with code, we're here to help:
- on Twitter at https://twitter.com/openfortxyz
- on Discord: https://discord.com/invite/t7x7hwkJF4
- by email: support+youtube@openfort.xyz
