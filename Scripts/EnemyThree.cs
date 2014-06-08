using UnityEngine;
using System.Collections;

public class EnemyThree : Charachters {
	public EnemyThree(){
		Health = 100;
		MaxHealt = 100;
		Mana = 100;
		MaxMana = 100;
		Name = "Ork (EnemyThree)";
		ActionPoint = 3;
		Type = "EnemyThree(Clone)";
		Death = false;
	}
}
