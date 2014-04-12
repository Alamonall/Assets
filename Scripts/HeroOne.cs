using UnityEngine;
using System.Collections;

public class HeroOne :  Characters , IJudje{
	public HeroOne(){
		MaxHealt = 100;
		MaxMana = 200;
		Mana = MaxMana;
		Health = MaxHealt;
		GStrength = 5;
		Name = "Саня";
		GEndurance = 4;
		GIntelligence = 8;
		GAgility = 6;
		GMagic = 6;
		GLucky = 5;
		ActionPoint = 3;
		Level = 1;
		ToLevelUp = 500;
		CurrentExp = 0;
		ActionPoint = 3;
	}
	public void Display(){
		Debug.Log(" My Name = "  + Name);
	}

}
