using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class CollectibleManager : MonoBehaviour
{

    public enum Collectibles
    {
        COINS, META
    }

    public enum Items
    {
        MAGNET, MINIMAP, STIM
    }

    public static CollectibleManager instance;

    [SerializeField]
    private TMP_Text coinText, metaText;
    private void Awake()
    {
        
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        coinText.text = $"{Scriptables.PlayerScore.coins}";
        metaText.text = $"{Scriptables.PlayerMetaScore.coins}";
    }

    private void FixedUpdate()
    {
        if (coinText != null && metaText != null) return;
        coinText = GameObject.Find("Coin Text (TMP)").GetComponent<TMP_Text>();
        metaText = GameObject.Find("Meta Text (TMP)").GetComponent<TMP_Text>();
        coinText.text = $"{Scriptables.PlayerScore.coins}";
        metaText.text = $"{Scriptables.PlayerMetaScore.coins}";

    }

    private void OnEnable()
    {
        //Coin
        Coin.OnCoinCollected += StoreCoin;
        Coin.OnCoinCollected += PlayCoinSound;
        
        //Meta
        Meta.OnMetaCollected += StoreMeta;
        Meta.OnMetaCollected += PlayMetaSound;
        Meta.OnMetaCollected += MoveToShop;
    }


    private void OnDisable()
    {
        Coin.OnCoinCollected -= StoreCoin;
        Coin.OnCoinCollected -= PlayCoinSound;

        //Meta
        Meta.OnMetaCollected -= StoreMeta;
        Meta.OnMetaCollected -= PlayMetaSound;
        Meta.OnMetaCollected -= MoveToShop;
    }
    

    void StoreCoin()
    {
        Scriptables.PlayerScore.coins++;
        print($"Coin Collected!\nCoins: {Scriptables.PlayerScore.coins}");
        coinText.text = $"{Scriptables.PlayerScore.coins}";
    }

    void PlayCoinSound()
    {
        AudioManager.instance.Play("CoinPickUp");
    }


    private void PlayMetaSound(string obj)
    {
        AudioManager.instance.Play("CoinPickUp");
    }

    private void StoreMeta(string obj)
    {
        Scriptables.PlayerMetaScore.coins++;
        metaText.text = $"{Scriptables.PlayerMetaScore.coins}";
    }
    private void MoveToShop(string obj)
    {
        ChangeScene.instance.changeScene(obj);
    }

    public bool DecreaseCollectible(int amount, Collectibles collectibleType)
    {
        switch (collectibleType)
        {
            case Collectibles.COINS:
                if (Scriptables.PlayerScore.coins >= amount)
                {
                    Scriptables.PlayerScore.coins -= amount;
                    coinText.text = $"{Scriptables.PlayerScore.coins}";
                    print("bought");
                    return true;
                }
                else
                {
                    print("don't have enought money");
                    return false;
                }
            case Collectibles.META:
                if (Scriptables.PlayerMetaScore.coins >= amount)
                {
                    Scriptables.PlayerMetaScore.coins -= amount;
                    metaText.text = $"{Scriptables.PlayerMetaScore.coins}";
                    print("bought");
                    return true;
                }
                else
                {
                    print("don't have enought Meta Poimts");
                    return false;
                }
            default:
                print("Type Doesn't Exist");
                return false;
        }

    }

    public bool DecreaseCollectibleCoin(int amount)
    {
        if (Scriptables.PlayerScore.coins >= amount)
        {
            Scriptables.PlayerScore.coins -= amount;
            coinText.text = $"{Scriptables.PlayerScore.coins}";
            print("bought");
            return true;
        }
        else
        {
            print("don't have enought money");
            return false;
        }
    }


    /// <summary>
    /// returns 0 if the item is not valid or if you don't have meta points
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="itemType"></param>
    /// <returns></returns>
    public int DecreaseCollectibleMeta(int amount, Items itemType)
    {
        if (Scriptables.PlayerMetaScore.coins >= amount)
        {
            Scriptables.PlayerMetaScore.coins -= amount;
            metaText.text = $"{Scriptables.PlayerMetaScore.coins}";
            print("Purchased upgrade.");

            switch (itemType)
            {
                case Items.MAGNET:
                    return Scriptables.Magnet.UpgradeVariable();
                case Items.MINIMAP:
                    return Scriptables.Minimap.UpgradeVariable();
                case Items.STIM:
                    return Scriptables.Stim.UpgradeVariable();
                default:
                    print("item type is not Valid");
                    return 0;
            }
        }
        else
        {
            print("Not enough meta points.");
            return 0;
        }
    }
}
