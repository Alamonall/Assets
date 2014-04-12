using UnityEngine;
using System.Collections;

public class HeroTwo : Characters, IJudje{
	public HeroTwo(){
		GStrength = 10;
		GIntelligence = 2;
		GAgility = 4;
		GEndurance = 4;
		GMagic = 5;
		GLucky = 5;
		Name = "Лена";
		Health = 100;
		MaxHealt = 100;
		Mana = 200;
		MaxMana = 200;
		Level = 1;
		CurrentExp = 0;
		ToLevelUp = 500;
		Armor = null;
		Weapon = null;
		Defend = false;
		ActionPoint = 4;
	}
	public void Display(){
		Debug.Log(" My Name = "  + Name);
	}
}
