using UnityEngine;
//using Services;
//using Chest.StateMachine;

namespace ChestCore.MVC
{

    /*
        ChestController class. Processes data from ChestModel & sends it to ChestView. 
        Handles Logic for Setting Attributes. Contains reference to ChestModel, ChestView & ChestSM.
    */
    public class ChestController
    {
        private ChestModel chestModel;
        private ChestView chestView;
        //private ChestSM chestSM;

        /*
            Constructor to set References to ChestModel & ChestView.
            Also initializes Chest StateMachine. 
        */
        public ChestController(ChestModel _chestModel, ChestView _chestView)
        {
            chestModel = _chestModel;
            chestView = _chestView;
            //chestSM = new ChestSM(this);
        }

        /*
            Gets Reference of ChestModel attached with the Controller.
        */
        public ChestModel GetChestModel()
        {
            return chestModel;
        }

        /*
            Gets Reference of ChestView attached with the Controller.
        */
        public ChestView GetChestView()
        {
            return chestView;
        }

        /*
            Gets Reference to the State Machine attached with the Controller.
        */
        /*public ChestSM GetChestSM()
        {
            return chestSM;
        }*/

        /*
            Method is executed whenever Chest is Clicked. Triggers the Popup.
            Uses ChestService to contact PopupService. 
        */
        /*public void OnChestButtonClicked()
        {
            ChestService.Instance.TriggerPopUp(this);
        }*/

        /*
            Gets called everytime new Chest is Found.
            Sets View Attributes by fetching it from ChestModel.
            Sets Chest Sprite & Chest Type (Common, Mini, Rare, Legendary).
        */
        public void SetViewAttributes()
        {
            // SET SPRITE & TEXT & TIMER
            SetChestSprite(chestModel.CHEST_SPRITE);
            SetChestTypeText(chestModel.CHEST_TYPE);
        }

        /*
            Sets Chest Sprite for the gameObject attached with ChestView.
        */
        public void SetChestSprite(Sprite chestSprite)
        {
            chestView.ChestSprite.sprite = chestSprite;
        }

        /*
            Sets the ChestType Text by taking ChestType as Input.
        */
        public void SetChestTypeText(ChestType chestType)
        {
            if (chestType == ChestType.COMMON)
            {
                chestView.Chest_Type.text = "COMMON";
            }
            else if (chestType == ChestType.RARE)
            {
                chestView.Chest_Type.text = "RARE";
            }
            else if (chestType == ChestType.EPIC)
            {
                chestView.Chest_Type.text = "EPIC";
            }
            else
            {
                chestView.Chest_Type.text = "LEGENDARY";
            }
        }

        /*
            
        */
        /*public void DequeueChest()
        {
            ChestService.Instance.DequeueChestFromWaitingQueue();
        }*/

    }

}