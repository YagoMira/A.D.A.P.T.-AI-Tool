using UnityEngine;
public class pnj_1_ejemplo : Agent 
{
new void Start() //DON'T MODIFY ANY LINE OF THIS FUNCTION!!!
 {
AddGoals();
/************/
base.Start(); //DON'T DELETE THIS LINE!!!
/************/
 ManageStates();
 }

 public void AddGoals() {}

 public void ManageStates() {}
}
