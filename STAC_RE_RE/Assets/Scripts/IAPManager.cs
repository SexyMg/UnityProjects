﻿using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class IAPManager : MonoBehaviour, IStoreListener
{
    public static IAPManager instance;
    private static IStoreController storeController = null;
    private static string[] sProductIds;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            if (storeController == null)
            {
                sProductIds=new string[]{"1000_dia","delete_ad"};
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitStore()
    {
        ConfigurationBuilder builder=ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(sProductIds[0], ProductType.Consumable, new IDs {{sProductIds[0], GooglePlay.Name}});
        builder.AddProduct(sProductIds[1], ProductType.Consumable, new IDs {{sProductIds[1], GooglePlay.Name}});
        
        UnityPurchasing.Initialize(this,builder);
    }

    void IStoreListener.OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
    }
    
    public void OnInitializeFailed(InitializationFailureReason error)
    {
      
    }

    public void OnBtnPurchaseClicked(int index)
    {
        if (storeController == null)
        {
          
        }
        else
        {
            storeController.InitiatePurchase(sProductIds[index]);
        }
    }
    PurchaseProcessingResult IStoreListener.ProcessPurchase(PurchaseEventArgs e)
    {
        bool isSuccess = true;
        #if UNITY_ANDROID
        CrossPlatformValidator validator=new CrossPlatformValidator(GooglePlayTangle.Data(),AppleTangle.Data(),Application.identifier);
        try
        {
            IPurchaseReceipt[] result = validator.Validate(e.purchasedProduct.receipt);
            for (int i = 0; i < result.Length; i++)
                Analytics.Transaction(result[i].productID, e.purchasedProduct.metadata.localizedPrice, e.purchasedProduct.metadata.isoCurrencyCode, result[i].transactionID,null);
        }
        catch (IAPSecurityException)
        {
            isSuccess = false;
        }
        #endif

        if (isSuccess)
        {
            if (e.purchasedProduct.definition.id.Equals(sProductIds[0]))
            { 
                GoldManager.instance.GetGold(1000); 
            }
            else if(e.purchasedProduct.definition.id.Equals(sProductIds[1]))
            {
                PlayerPrefs.SetInt(BulletData.instance.DeleteAdKey,1);
                BulletData.instance.isDeleteAD = true;
            }
        }
        else
        {
           
        }

        return PurchaseProcessingResult.Complete;
    }

    void IStoreListener.OnPurchaseFailed(Product i, PurchaseFailureReason error)
    {
        if (!error.Equals(PurchaseFailureReason.UserCancelled))
        {
           
        }
    }
}
