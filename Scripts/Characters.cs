using UnityEngine;
using System.Collections;

public class Characters : MonoBehaviour {
	public int Health;
	public int MaxHealt;
	public int Mana;
	public int MaxMana;
	public int GStrength;
	public string Name;
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
	IJudje[] Turn;
	public Characters(){
		Turn = new IJudje[6];
		IJudje noCharachter = new noCharachter();
		for(int i = 0; i < 6; i++){
			Turn[i] = noCharachter;
		}
	}
	public void add(int slot, IJudje character){
		Turn[slot] = character;
	}

}
