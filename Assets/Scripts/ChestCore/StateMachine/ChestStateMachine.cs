using ChestCore.MVC;

namespace ChestCore.StateMachine
{
    /*
       Enum for Chest State.
   */
    public enum ChestState
    {
        LOCKED,
        QUEUED,
        UNLOCKING,
        UNLOCKED
    }
    /*
        ChestStateMachine class. Acts as a State Machine for Chest. Contains Reference to all different States & ChestController.
    */
    public class ChestStateMachine
    {
        public ChestBaseState currentChestState = null;
        public ChestState currentChestStateEnum = ChestState.LOCKED;
        private ChestLockedState chestlockedState;
        private ChestQueuedState chestqueuedState;
        private ChestUnlockingState chestunlockingState;
        private ChestUnlockedState chestunlockedState;
        private ChestController chestController;

        /*
            Constructor to Initialize all ChestStates & set reference to ChestController.
        */
        public ChestStateMachine(ChestController _chestController)
        {
            chestController = _chestController;
            chestlockedState = new ChestLockedState(this);
            chestqueuedState = new ChestQueuedState(this);
            chestunlockingState = new ChestUnlockingState(this);
            chestunlockedState = new ChestUnlockedState(this);
            ResetSM();
        }

        /*
            Resets the State to LOCKED. Gets called everytime when Chest is Spawned.
        */
        public void ResetSM()
        {
            SwitchState(ChestState.LOCKED);
        }

        /*
            Returns Reference to the ChestController connected with the StateMachine.
        */
        public ChestController GetChestController()
        {
            return chestController;
        }

        /*
            Switches the currentState of Chest.
            Also checks if we are trying to switch again into the currentState.
        */
        public void SwitchState(ChestState chestState)
        {
            ChestBaseState newState = GetChestBaseStateFromEnum(chestState);
            if (currentChestState == newState)
            {
                return;
            }
            if (currentChestState != null)
                currentChestState.OnStateExit();
            currentChestState = newState;
            currentChestStateEnum = chestState;
            currentChestState.OnStateEnter();
        }

        /*
            Returns reference to the ChestBaseState object with respect to the Enum Value.
        */
        public ChestBaseState GetChestBaseStateFromEnum(ChestState chestState)
        {
            if (chestState == ChestState.LOCKED)
            {
                return chestlockedState;
            }
            else if (chestState == ChestState.QUEUED)
            {
                return chestqueuedState;
            }
            else if (chestState == ChestState.UNLOCKING)
            {
                return chestunlockingState;
            }
            else if (chestState == ChestState.UNLOCKED)
            {
                return chestunlockedState;
            }
            else
            {
                return null;
            }
        }
    }
}
