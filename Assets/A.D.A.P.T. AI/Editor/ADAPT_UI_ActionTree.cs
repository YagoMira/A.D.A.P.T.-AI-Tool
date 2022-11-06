using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ADAPT.UI
{
    [CanEditMultipleObjects]
    public class ADAPT_UI_ActionTree : EditorWindow
    {
        static Rect[] nodeRect;
        static Rect[] goalRect;
        Rect agentNameRect = new Rect(20, 10, 220, 50);
        public static Agent agent;
        static Action[] action_list;
        Texture2D node_sprite_action, node_sprite_action_toGoal, node_sprite_goal;
        GUIStyle nodeStyle = new GUIStyle(), nodeStyle_2 = new GUIStyle(), agentNameStyle = new GUIStyle(), goalStyle = new GUIStyle();


        public static void WindowProperties()
        {
            ADAPT_UI_ActionTree window = GetWindow<ADAPT_UI_ActionTree>();
            window.titleContent = new GUIContent("Action Tree");
            window.minSize = new Vector2(1000, 550);
            window.maxSize = new Vector2(1200, 620);
        }

        public static void SetAgent(ADAPT_UI_ActionTree window, Agent a)
        {
            //window.agent = a;
            agent = a;
            Initializate();
        }

        void OnInspectorUpdate()
        {
            //this.window.agent = a;
            Repaint();
        }

        private static void Initializate() //Helps to initialize the rect sizes for GUI.Windows.
        {
            int increase_x = 0, increase_x_goal = 0;
            int increase_y = 0, increase_y_goal = 0;
            int goal_Counter = 0;

            if (agent != null)
                SetAllAgentActions(agent);

            /******PREDEFINED NODE POSITIONS*******/
            if (action_list != null)
            {
                nodeRect = new Rect[action_list.Length];
                nodeRect[0] = new Rect(100, 100, 100, 100); //Defaut node rect size

                for (int i = 0; i < action_list.Length; i++)
                {
                    nodeRect[i] = new Rect(nodeRect[0].x + increase_x, nodeRect[0].y + increase_y, nodeRect[0].width, nodeRect[0].height);

                    if ((increase_y + 110) <= 400) //Move into the y axis.
                    {
                        increase_y += 110;
                    }
                    else //Move into the x axis.
                    {
                        increase_x += 160;
                        increase_y = 0;
                    }
                }
            }

            if (agent != null)
            {
                if (agent.goals != null)
                {
                    increase_x_goal = 0;
                    increase_y_goal = 0;

                    if(agent.goals.Count > 0)
                    {
                        goalRect = new Rect[agent.goals.Count];
                        goalRect[0] = new Rect(600, 100, 100, 100);

                        foreach (Agent.Goal g in agent.goals)
                        {
                            goalRect[goal_Counter] = new Rect(goalRect[0].x + increase_x_goal, goalRect[0].y + increase_y_goal, goalRect[0].width, goalRect[0].height);

                            if ((increase_y_goal + 110) <= 1200) //Move into the y axis.
                            {
                                increase_y_goal += 110;
                            }
                            else //Move into the x axis.
                            {
                                increase_x_goal += 160;
                                increase_y_goal = 0;
                            }

                            goal_Counter++;
                        }

                        goal_Counter = 0;
                    }
                    
                }
                /**************/
            }
        }

        void OnGUI()
        {
            int goal_id = 0;

            WindowProperties();

            /******RELOAD RESOURCES*******/
            node_sprite_action = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/A.D.A.P.T. AI/Editor/Resources/Node_Blue.png");
            node_sprite_action_toGoal = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/A.D.A.P.T. AI/Editor/Resources/Node_Green.png");
            node_sprite_goal = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/A.D.A.P.T. AI/Editor/Resources/Node_Red.png");

            nodeStyle.normal.background = node_sprite_action;
            nodeStyle.alignment = TextAnchor.MiddleCenter;
            nodeStyle.richText = true;
            nodeStyle.fontSize = 10;

            nodeStyle_2.normal.background = node_sprite_action_toGoal;
            nodeStyle_2.alignment = TextAnchor.MiddleCenter;
            nodeStyle_2.richText = true;
            nodeStyle_2.fontSize = 10;

            goalStyle.normal.background = node_sprite_goal;
            goalStyle.alignment = TextAnchor.MiddleCenter;
            goalStyle.richText = true;
            goalStyle.fontSize = 10;

            agentNameStyle.normal.textColor = Color.white;
            agentNameStyle.fontSize = 30;
            agentNameStyle.richText = true;
            /**************/

            if (agent != null)
                GUI.Label(agentNameRect, "<b>" + agent.agentName + "</b>", agentNameStyle);

            /******DRAW NODES - ACTIONS*******/
            BeginWindows();
            if (agent != null)
            {
                    if (agent.actionsToRun != null) //In case of has some actions.
                    {
                        if (action_list != null)
                        {
                            for (int j = 0; j < action_list.Length; j++)
                            {
                                if (agent.receivedActions.Contains(action_list[j])) //Actions in plan
                                {

                                    nodeRect[j] = GUI.Window(j, nodeRect[j], WindowFunction, "<b>" + action_list[j].actionName + "</b>\n<color='white'>PRIORITY:</color> " + action_list[j].totalPriority
                                        + "\n<color='white'>COST:</color> " + action_list[j].totalCost, nodeStyle_2);
                                }
                                else
                                {
                                    nodeRect[j] = GUI.Window(j, nodeRect[j], WindowFunction, "<b>" + action_list[j].actionName + "</b>\n<color='white'>PRIORITY:</color> " + action_list[j].totalPriority
                                        + "\n<color='white'>COST:</color> " + action_list[j].totalCost, nodeStyle);
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < action_list.Length; i++)
                        {
                            nodeRect[i] = GUI.Window(i, nodeRect[i], WindowFunction, "<b>" + action_list[i].actionName + "</b>\n<color='white'>PRIORITY:</color> " + action_list[i].totalPriority
                                + "\n<color='white'>COST:</color> " + action_list[i].totalCost, nodeStyle);
                        }
                    }
            }
            /**************/


            /******DRAW NODES - GOALS*******/
            int goal_Counter = 0;

            if (agent != null)
            {
                if (agent.goals != null)
                {
                    if (action_list != null)
                        goal_id = action_list.Length;

                    foreach (Agent.Goal g in agent.goals)
                    {
                        goalRect[goal_Counter] = GUI.Window(goal_id, goalRect[goal_Counter], WindowFunction, "<b>" + g.goal_precondition.key + "</b>", goalStyle);
                        goal_Counter++;
                        goal_id++;
                    }

                    goal_Counter = 0;
                }
            }
            /**************/
            
            /**************/


            /******NODE COLORS AND LINKS******/
            int searchGoal = 0; //In case of need to search goal by string in list.

            Handles.BeginGUI();
            if (agent != null)
            {
                if (agent.actionsToRun != null) //In case of has some actions.
                {
                    if (action_list != null)
                    {
                        if(agent.receivedActions != null )
                        {
                            if(agent.receivedActions.Count > 0)
                            {
                                for (int i = 0; i < agent.receivedActions.Count; i++)
                                {
                                    for (int j = 0; j < action_list.Length; j++)
                                    {
                                        if (agent.receivedActions[i].actionName == action_list[j].actionName)
                                        {
                                            if (action_list.Length % 2 != 0)
                                            {
                                                if ((j + 1) < action_list.Length)
                                                {
                                                    if (i != agent.receivedActions.Count - 1)
                                                    {
                                                        Handles.DrawBezier(nodeRect[j].center, nodeRect[j + 1].center, new Vector2(nodeRect[j].xMax + 50f, nodeRect[j].center.y), new Vector2(nodeRect[j + 1].xMin - 50f, nodeRect[j + 1].center.y), Color.white, null, 5f);
                                                    }

                                                }
                                                else if (i == agent.receivedActions.Count - 1)
                                                {
                                                    for (int z = 0; z < agent.goals.Count; z++)
                                                    {
                                                        if (agent.goals[z].goal_precondition.key == agent.currentGoal)
                                                        {
                                                            if (z == 0)
                                                                searchGoal = 0;
                                                            Handles.DrawBezier(nodeRect[j].center, goalRect[searchGoal].center, new Vector2(nodeRect[j].xMax + 50f, nodeRect[j].center.y), new Vector2(goalRect[searchGoal].xMin - 50f, goalRect[searchGoal].center.y), Color.yellow, null, 5f);
                                                        }
                                                        searchGoal++;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if ((j + 1) < action_list.Length)
                                                {
                                                    if (i != agent.receivedActions.Count - 1)
                                                    {
                                                        Handles.DrawBezier(nodeRect[j].center, nodeRect[j + 1].center, new Vector2(nodeRect[j].xMax + 50f, nodeRect[j].center.y), new Vector2(nodeRect[j + 1].xMin - 50f, nodeRect[j + 1].center.y), Color.white, null, 5f);
                                                    }
                                                    else
                                                    {
                                                        for (int z = 0; z < agent.goals.Count; z++)
                                                        {
                                                            if (agent.goals[z].goal_precondition.key == agent.currentGoal)
                                                            {
                                                                if (z == 0)
                                                                    searchGoal = 0;
                                                                Handles.DrawBezier(nodeRect[j].center, goalRect[searchGoal].center, new Vector2(nodeRect[j].xMax + 50f, nodeRect[j].center.y), new Vector2(goalRect[searchGoal].xMin - 50f, goalRect[searchGoal].center.y), Color.yellow, null, 5f);
                                                            }
                                                            searchGoal++;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                } 
            }
            //Handles.DrawBezier(windowRect.center, windowRect2.center, new Vector2(windowRect.xMax + 50f, windowRect.center.y), new Vector2(windowRect2.xMin - 50f, windowRect2.center.y), Color.white, null, 5f);
            Handles.EndGUI();
            /**************/
            EndWindows();
        }

        public static void SetAllAgentActions(Agent a)
        {
            if (a != null)
                action_list = a.gameObject.GetComponents<Action>();
            //return null;
        }

        void WindowFunction(int windowID)
        {
            GUI.DragWindow();
        }
    }
}
