using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ChestCore.MVC
{

    /// <summary>
    /// ChestView class. Inherits from Monobheviour. Handles the Visual aspect of the Gameobject.
    /// Keeps reference to ChestController.
    ///
    public class ChestView : MonoBehaviour
    {
        private ChestController chestController;
        public Image ChestSprite;
        public TextMeshProUGUI Chest_Type;
        public TextMeshProUGUI Timer_Text;

        /*
            Returns Reference to the ChestController connected with the View.
        */
        public ChestController GetChestController()
        {
            return chestController;
        }

        /*
            Sets Reference to the ChestController to connect it with the View.
        */
        public void SetChestController(ChestController _chestController)
        {
            chestController = _chestController;
        }

        /*
            Method is Executed when the Chest is Clicked.
        */
        public void OnChestClicked()
        {
            chestController.OnChestButtonIsClicked();
        }

    }

}
