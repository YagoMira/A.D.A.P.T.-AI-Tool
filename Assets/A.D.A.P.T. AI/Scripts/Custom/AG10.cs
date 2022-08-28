using UnityEngine;
public class AG10 : Agent 
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
