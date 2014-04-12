using UnityEngine;
using System.Collections;

public class EnemyThree : Characters, IJudje {
	void Start(){
		MaxHealt = 100;
		MaxMana = 200;
		Mana = MaxMana;
		Health = MaxHealt;
		GStrength = 5;
		Name = "Goblin3";
		GEndurance = 4;
		GIntelligence = 8;
		GAgility = 6;
		GMagic = 6;
		GLucky = 5;
		ActionPoint = 3;
	}
	
	public void Display(){
		Debug.Log(" My Name = "  + Name);
	}
	
}
