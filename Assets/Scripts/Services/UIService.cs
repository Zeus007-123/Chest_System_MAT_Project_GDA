using UnityEngine;
using Utilities;
using ScriptableObjects;
using TMPro;

namespace Services
{

    /*
        UIService MonoSingleton class. Handles Menu UI (Coin Count, Gem Coint, Spawn Button)
    */
    public class UIService : GenericMonoSingleton<UIService>
    {
        public int COIN_COUNT { get; private set; }
        public int GEM_COUNT { get; private set; }
        [SerializeField] TextMeshProUGUI COIN_TEXT;
        [SerializeField] TextMeshProUGUI GEM_TEXT;
        [SerializeField] int EXPLORE_COST = 50;
        private int coinCount = 100;
        private int gemCount = 50;

        /*
            Sets Value of Initial COINS & GEMS.
        */
        private void Start()
        {
            COIN_COUNT = coinCount;
            GEM_COUNT = gemCount;
        }

        /*
            Subscribes to the onCollectCoinGem Event.
        */
        private void OnEnable()
        {
            EventService.Instance.onCollectCoinsGems += UpdateCoinsAndGems;
        }

        /*
            Method Gets Executed whenever Spawn Button is Clicked.
            Fetches Chest Configuration & Sets Chest Slot.
            Initiates NotEnoughCoins, ChestSpawned, SlotFull event based on Conditions.
        */
        public void SpawnChest()
        {
            SoundService.Instance.PlayAudio(SoundType.BUTTON_CLICK);
            if (COIN_COUNT < EXPLORE_COST)
            {
                EventService.Instance.InvokeNotEnoughCoinsGemsEvent();
                return;
            }
            (GameObject, ChestSO) chestValues = ChestCoreService.Instance.FetchChestFromPool();
            GameObject Chest = chestValues.Item1;
            ChestSO chestConfig = chestValues.Item2;
            if (Chest != null)
            {
                Transform chestSlotTransform = ChestSlotService.Instance.GetChestSlot();
                Chest.transform.SetParent(chestSlotTransform, false);
                Chest.SetActive(true);
                UpdateCoinsAndGems(-EXPLORE_COST, 0);
                EventService.Instance.InvokeChestIsSpawnedEvent(chestConfig.CHEST_COINS_RANGE, chestConfig.CHEST_GEMS_RANGE, chestConfig.CHEST_TYPE);
            }
            else
            {
                EventService.Instance.InvokeSlotsFullEvent();
            }
        }

        /*
            UpdateCoinAndGems Method. Updates the COIN & GEM Count.
        */
        private void UpdateCoinsAndGems(int COINS, int GEMS)
        {
            COIN_COUNT += COINS;
            GEM_COUNT += GEMS;
            COIN_TEXT.text = COIN_COUNT.ToString();
            GEM_TEXT.text = GEM_COUNT.ToString();
        }

        /*
            Unsubscribes to the onCollectCoinGem Event.
        */
        private void OnDisable()
        {
            EventService.Instance.onCollectCoinsGems -= UpdateCoinsAndGems;
        }
    }

}