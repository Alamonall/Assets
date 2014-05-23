using UnityEngine;
using System.Collections;

public class EnemyOne: MonoBehaviour {
	public int Health;
	public int MaxHealt;
	public int Mana;
	public int MaxMana;
	public string Name;
	public int GStrength;
	public int GEndurance;
	public int GIntelligence;
	public int GAgility;
	public int GMagic;
	public static int GLucky;
	public int Level;
	public int CurrentExp;
	public int ToLevelUp;
	public float Procent = 1.5f;
	public object Armor;
	public object Weapon;
	public ArrayList[] Effects;
	public bool Defend;
	public int ActionPoint;
	public string Type;

	void Start(){
		Health = 100;
		MaxHealt = 100;
		Mana = 100;
		MaxMana = 100;
		GStrength = 6;
		Name = "Гоблин";
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
		Type = "EnemyOne(Clone)";
	}
	
}
