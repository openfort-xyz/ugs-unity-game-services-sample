# Openfort Unity Gaming Services Integration
  
## Overview
This is a sample project to showcase the Openfort integration with [Unity Gaming Services](https://unity.com/solutions/gaming-services), a complete service ecosystem for Unity live games.

The sample includes:
  - [**`ugs-backend`**](https://github.com/openfort-xyz/ugs-unity-game-services-sample/tree/main/OpenfortIntegration)
    
    A .NET Core project with [Cloud Code C# modules](https://docs.unity.com/ugs/en-us/manual/cloud-code/manual/modules#Cloud_Code_C#_modules) that implement [Openfort C# SDK](https://www.nuget.org/packages/Openfort.SDK/1.0.21) methods. Hosted in UGS.

  - [**`unity-client`**](https://github.com/openfort-xyz/ugs-unity-game-services-sample/tree/main/unity-client)

    A Unity sample game that connects to ``ugs-backend`` through [Cloud Code](https://docs.unity.com/ugs/manual/cloud-code/manual). It uses [Openfort Unity SDK](https://github.com/openfort-xyz/openfort-csharp-unity) to deserialize its responses.  

The sample uses [Unity Authentication](https://docs.unity.com/ugs/en-us/manual/authentication/manual/get-started) to sign in as a new anonymous player. It then [creates an Openfort player](https://github.com/openfort-xyz/ugs-unity-game-services-sample/blob/ab1a5f69346910c18ea88579f6fce81cdcde489a/ugs-backend/CloudCodeModules/PlayersModule.cs#L31) and a [custodial account](https://github.com/openfort-xyz/ugs-unity-game-services-sample/blob/ab1a5f69346910c18ea88579f6fce81cdcde489a/ugs-backend/CloudCodeModules/PlayersModule.cs#L38) using Openfort and it [links it](https://github.com/openfort-xyz/ugs-unity-game-services-sample/blob/ab1a5f69346910c18ea88579f6fce81cdcde489a/ugs-backend/CloudCodeModules/PlayersModule.cs#L43) to the Unity player. Then the player can frictionlessly [mint an NFT](https://github.com/openfort-xyz/ugs-unity-game-services-sample/blob/ab1a5f69346910c18ea88579f6fce81cdcde489a/ugs-backend/CloudCodeModules/MintingModule.cs#L26).

## Application Workflow

//TODO

## Prerequisites
+ **Get started with Openfort**
  + [Sign in](https://dashboard.openfort.xyz/login) or [sign up](https://dashboard.openfort.xyz/register) and create a new dashboard project

+ **Get started with UGS**
  + [Complete basic prerequisites](https://docs.unity.com/ugs/manual/overview/manual/getting-started#Prerequisites)
  + [Create a project](https://docs.unity.com/ugs/manual/overview/manual/getting-started#CreateProject)

## Setup Openfort dashboard
  + [Add a Contract](https://dashboard.openfort.xyz/assets/new)
    
    This sample requires a contract to run. We use [0x51216BFCf37A1D2002A9F3290fe5037C744a6438](https://sepolia.etherscan.io/address/0x51216bfcf37a1d2002a9f3290fe5037c744a6438) (NFT contract deployed in 11155111 Sepolia). You can use this for the guide:

    ![Alt text](https://strapi-oube.onrender.com/uploads/playfab_opensea_img_2eae75d470.png?updated_at=2023-11-19T11:06:46.685Z)

  + [Add a Policy](https://dashboard.openfort.xyz/policies/new)
    
    We aim to cover gas fees for users. Set a new gas policy:

    ![Alt text](https://strapi-oube.onrender.com/uploads/playfab_opensea_img_1_ecaa326bfe.png?updated_at=2023-11-19T11:06:46.377Z)

    Now, add a rule so our contract uses this policy:

    ![Alt text](https://strapi-oube.onrender.com/uploads/playfab_opensea_img_2_493f681e5a.png?updated_at=2023-11-19T11:06:42.679Z)

## Set up [`ugs-backend`](https://github.com/openfort-xyz/ugs-unity-game-services-sample/tree/main/ugs-backend)

- ### Set Openfort dashboard variables

  Open the [solution](https://github.com/openfort-xyz/ugs-unity-game-services-sample/blob/main/ugs-backend/CloudCodeModules.sln) with your preferred IDE, open [``SingletonModule.cs``](https://github.com/openfort-xyz/ugs-unity-game-services-sample/blob/main/OpenfortIntegration/OpenfortIntegration/SingletonModule.cs) and fill in these variables:

  ![](https://strapi-oube.onrender.com/uploads/ugs_integration_1_6001ca1099.png?updated_at=2023-12-13T17:45:19.990Z)

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

![Game Scene](https://strapi-oube.onrender.com/uploads/playfab_opensea_img_32_35f675ded4.png?updated_at=2023-11-19T11:06:40.788Z)

Select ***Mint***. After a brief period, you should see a representation of your newly minted NFT:

![Alt text](https://strapi-oube.onrender.com/uploads/ugs_integration_3_181e80ff26.png?updated_at=2023-12-14T10:02:43.778Z)

In the [Openfort Players dashboard](https://dashboard.openfort.xyz/players), a new player entry should be visible. On selecting this player:

![Player Entry](https://strapi-oube.onrender.com/uploads/playfab_opensea_img_34_706b0d267e.png?updated_at=2023-11-19T11:06:46.177Z)

You'll notice that a `mint` transaction has been successfully processed:

![Alt text](https://strapi-oube.onrender.com/uploads/playfab_opensea_img_3_ceeb1a3c2c.png?updated_at=2023-11-19T11:06:46.084Z)

Additionally, by choosing your **Sepolia Account** and viewing ***NFT Transfers***, the transaction is further confirmed:

![Alt text](https://strapi-oube.onrender.com/uploads/playfab_opensea_img_4_97e49a99aa.png?updated_at=2023-11-19T11:06:47.187Z)

Keep in mind that the sample is designed so a player can mint only once. By default, UGS Authentication will use the same player per device. If you want to sign in with a new player check the ***Clear Session Token*** in ***AuthController***:

![](https://strapi-oube.onrender.com/uploads/ugs_integration_2_b0fae3ec75.png?updated_at=2023-12-14T09:56:42.591Z)

## Conclusion

Upon completing the above steps, your Unity game will be fully integrated with Openfort and UGS. Always remember to test every feature before deploying to guarantee a flawless player experience.

For a deeper understanding of the underlying processes, check out the [tutorial video](https://youtu.be/PHNodBmbEfA). 

## Get support
If you found a bug or want to suggest a new [feature/use case/sample], please [file an issue](../../issues).

If you have questions, or comments, or need help with code, we're here to help:
- on Twitter at https://twitter.com/openfortxyz
- on Discord: https://discord.com/invite/t7x7hwkJF4
- by email: support+youtube@openfort.xyz
