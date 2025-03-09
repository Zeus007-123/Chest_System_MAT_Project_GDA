using System.Collections;
using UnityEngine;
using ChestCore.MVC;

namespace ChestCore.StateMachine
{

    /// <summary>
    /// ChestUnlockingState class. Handles Functionality of when Chest is in UNLOCKING state.
    /// Chest switches to this state from QUEUED state, when the gameObject associated is DEQUEUED.
    /// <summary>
    
    public class ChestUnlockingState : ChestBaseState
    {
        public ChestUnlockingState(ChestStateMachine _chestSM) : base(_chestSM) { }

        /*
            Method gets called when Chest enters QUEUED state.
        */
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            chestSM.GetChestController().GetChestView().StartCoroutine(StartTimer());
        }

        /*
            StartTimer Method. Gets Called in OnStateEnter function.
            Decrease value of UNLOCK_TIME & updates text in MM::SS format.
            Switches State to Open After Timer goes to 0.
        */
        private IEnumerator StartTimer()
        {
            ChestModel chestModel = chestSM.GetChestController().GetChestModel();
            ChestView chestView = chestSM.GetChestController().GetChestView();
            while (chestModel.UNLOCK_TIME > 0)
            {
                chestModel.UpdateUnlockTime(Time.deltaTime);
                chestView.Timer_Text.text = GetTimeText(chestModel.UNLOCK_TIME);
                yield return new WaitForEndOfFrame();
            }
            chestSM.SwitchState(ChestState.UNLOCKED);
            chestSM.GetChestController().DequeueChest();
        }

        /*
            Gets Time in MM::SS Format based on UNLOCK TIME.
        */
        private string GetTimeText(float UNLOCK_TIME)
        {
            int minutes = (int)UNLOCK_TIME / 60;
            int seconds = (int)UNLOCK_TIME % 60;
            string finalText = "";
            if (minutes < 10)
            {
                finalText = "0" + minutes + ":";
            }
            else
            {
                finalText = minutes + ":";
            }
            if (seconds < 10)
            {
                finalText += "0" + seconds;
            }
            else
            {
                finalText += seconds;
            }
            return finalText;
        }
    }

}
