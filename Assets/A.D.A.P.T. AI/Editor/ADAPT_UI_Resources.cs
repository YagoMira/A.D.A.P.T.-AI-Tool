using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADAPT.UI
{
    public class ADAPT_UI_Resources : MonoBehaviour
    {
        #region PATHS
        public string logo_path = "Logo_ADAPT";
        #endregion

        #region AGENTS_UI
        public string agent_description_text = "Select a GameObject from the scene\n or hierarchy to make it an agent.";
        public string selected_gameobject_text = "Selected GameObject: ";
        public string agent_button = "TURN INTO AN AGENT!";
        public string agent_exists_text = "This GameObject is already an Agent!";
        #endregion

        #region ACTIONS_UI
        public string add_precondition_button_text = "Add Precondition";
        #endregion
    }
}

