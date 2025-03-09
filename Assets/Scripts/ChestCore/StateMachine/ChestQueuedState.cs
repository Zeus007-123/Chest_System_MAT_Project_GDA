using ChestCore.MVC;

namespace ChestCore.StateMachine
{
    /// <summary>
    /// ChestQueuedState class. Handles Functionality of when Chest is in QUEUED state.
    /// Chest switches to this state after LOCKED state, when there is space in Waiting Queue.
    /// <summary>
    
    public class ChestQueuedState : ChestBaseState
    {
        public ChestQueuedState(ChestStateMachine _chestSM) : base(_chestSM) { }

        /*
            Method gets called when Chest enters QUEUED state.
        */
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            SetTimerText();
        }

        /*
            Sets the Timer Text to QUEUED.
        */
        private void SetTimerText()
        {
            ChestController chestController = chestSM.GetChestController();
            chestController.GetChestView().Timer_Text.text = "QUEUED";
        }
    }
}
