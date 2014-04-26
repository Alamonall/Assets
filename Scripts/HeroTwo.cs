using UnityEngine;
using System.Collections;

public class HeroTwo : Charachters{
	public HeroTwo(){
		Health = 100;
		MaxHealt = 100;
		Mana = 100;
		MaxMana = 100;
		GStrength = 6;
		Name = "Лена (Lena)";
		GEndurance = 6;
		GIntelligence = 4;
		GAgility = 6;
		GMagic = 2;
		GLucky = 5;
		Level = 1;
		CurrentExp = 0;
		ToLevelUp = 500;
		Defend = false;
		ActionPoint = 3;
		Type = "HeroTwo(Clone)";
	}

}
