using UnityEngine;
public class Agente_Ejemplo_A : Agent 
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
