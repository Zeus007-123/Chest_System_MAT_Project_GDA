using System;
using UnityEngine;
using Utilities;
using ChestCore.MVC;
using ChestCore.StateMachine;

namespace Services
{

    /*
        EventService MonoSingleton Class. Used to Handle All Events which happen in Game.
    */
    public class EventService : GenericMonoSingleton<EventService>
    {
        public event Action onSlotsFull;
        public event Action<Vector2Int, Vector2Int, ChestType> onChestIsSpawned;
        public event Action<int, int, int, ChestState, ChestType, GameObject> onChestIsClicked;
        public event Action<int, int> onCollectCoinsGems;
        public event Action onNotEnoughCoinsGems;

        /*
            Invokes the Slots are Full Event. Gets Executed when Slots are Full.
        */
        public void InvokeSlotsFullEvent()
        {
            onSlotsFull?.Invoke();
        }

        /*
            Invokes the Chest Is Spawned Event. Gets Executed whenever New Chest is Spawned.
        */
        public void InvokeChestIsSpawnedEvent(Vector2Int COIN_RANGE, Vector2Int GEM_RANGE, ChestType chestType)
        {
            onChestIsSpawned?.Invoke(COIN_RANGE, GEM_RANGE, chestType);
        }

        /*
            Invokes the Chest Is Clicked Event. Gets Executed whenever Chest is Clicked.
        */
        public void InvokeChestIsClickedEvent(int COINS, int GEMS, int GEMS_TO_UNLOCK, ChestState chestState, ChestType chestType, GameObject gameObject)
        {
            onChestIsClicked?.Invoke(COINS, GEMS, GEMS_TO_UNLOCK, chestState, chestType, gameObject);
        }

        /*
            Invokes the Collect Coins & Gems Event. Gets Executed whenever Total Coins & Gems change.
        */
        public void InvokeCollectCoinsGemsEvent(int COINS, int GEMS)
        {
            onCollectCoinsGems?.Invoke(COINS, GEMS);
        }

        /*
            Invokes Not Enough Coins / Gems Event. Gets Executed whenever Coins / Gems are not Enough for Exploring / Unlocking Chests.
        */
        public void InvokeNotEnoughCoinsGemsEvent()
        {
            onNotEnoughCoinsGems?.Invoke();
        }
    }
}