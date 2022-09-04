using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADAPT.UI
{
    public class ADAPT_UI_Resources
    {
        #region PATHS
        public string logo_path = "Logo_ADAPT";
        #endregion

        #region AGENTS_UI
        public string agents_text = "Agents";
        public string agent_description_text = "Select a GameObject from the scene\n or hierarchy to make it an agent.";
        public string selected_gameobject_text = "Selected GameObject: ";
        public string agent_button = "TURN INTO AN AGENT!";
        public string agent_exists_text = "This GameObject is already an Agent!";
        public string agent_script_exists_text = "This Agent already exists! or is being imported by Unity, WAIT!.";
        public string agent_name_text = "Name for the NEW Agent: ";
        #endregion

        #region ACTIONS_UI
        public string actions_text = "Actions";
        public string add_precondition_button_text = "Add Precondition";
        public string action_description_text = "Select a Agent to add a new Action.";
        public string action_button = "CREATE A NEW ACTION!";
        public string action_name_text = "Name for the NEW Action: ";
        public string action_not_exists_text = "This GameObject is not an Agent! Make it an agent first!";
        public string action_file_exists_text = "The Action already exists! or is being imported by Unity, WAIT!.";
        #endregion

        #region PREDEFINED_ACTIONS_UI
        public string description_text = "In this window you can add predefined actions to the current selected agent in the hierarchy.\nFor add a specefic action, just press the correct "+" button next to it.";
        public string action_file_added_text = "The Action is already added to the agent!.";
        #endregion
    }
}

