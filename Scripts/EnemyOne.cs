using UnityEngine;
using System.Collections;

public class EnemyOne: Charachters {
	public EnemyOne(){
		Health = 100;
		MaxHealt = 100;
		Mana = 100;
		MaxMana = 100;
		Name = "Goblin (EnemyOne)";
		ActionPoint = 3;
		Type = "EnemyOne(Clone)";
		Death = false;
	}
	
}
