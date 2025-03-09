using ChestCore.MVC;

namespace ChestCore.StateMachine
{

    /// <summary>
    /// ChestUnlockedState class. Handles Functionality of when Chest is in Unlocked state.
    /// Chest switches to this state after UNLOCKING state.
    /// <summary>
    
    public class ChestUnlockedState : ChestBaseState
    {
        public ChestUnlockedState(ChestStateMachine _chestSM) : base(_chestSM) { }

        /*
            Method gets called when Chest enters OPEN state.
        */
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            SetTimerText();
        }

        /*
            Sets the Timer Text to UNLOCKED.
        */

        private void SetTimerText()
        {
            ChestController chestController = chestSM.GetChestController();
            chestController.GetChestView().Timer_Text.text = "UNLOCKED";
        }
    }

}