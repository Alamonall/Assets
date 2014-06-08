using UnityEngine;
using System.Collections;

public class EnemyTwo : Charachters {
	public EnemyTwo(){
		Health = 100;
		MaxHealt = 100;
		Mana = 100;
		MaxMana = 100;
		Name = "Wolf (EnemyTwo)";
		ActionPoint = 3;
		Type = "EnemyTwo(Clone)";
		Death = false;
	}
}
