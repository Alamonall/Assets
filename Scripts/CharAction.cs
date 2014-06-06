using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharAction : MonoBehaviour {
	List<Charachters> Turn; //Очередь из Battlefields
	int CanAttack = 0; // Может ли гг атаковать this персонажа
	string ThisTag; // Тэг this обьекта
	Charachters WhoStep;// Кто ходит в данный момент (переменная из Battlefields)
	Transform ThisChar;

	void Start(){
		ThisTag = this.gameObject.tag;
		Turn = Battlefields.Turn;
		WhoStep = Battlefields.WhoStep;
		ThisChar = this.gameObject.transform;
	}



	void OnMouseEnter(){
		if(GameObject.Find(WhoStep.Type).tag != ThisTag){
			renderer.material.color = Color.red;
			CanAttack = 1;
			CutMove();
		}
	}


	//Функция, которая проверяет есть ли враг поблизости 
	void CutMove(){
		float xAttack = GameObject.Find(WhoStep.Type).transform.position.x/15;
		float yAttack = GameObject.Find(WhoStep.Type).transform.position.y/15;
		float xDef = ThisChar.position.x/15;
		float yDef = ThisChar.position.y/15;
		//Debug.Log("Pos Attack x = " + xAttack + " y = " + yAttack);
		//Debug.Log("Pos Def x = " + xDef + " y = " + yDef);
		if(xAttack + 1 == xDef && yAttack == yDef){
			CanAttack +=1;
			//Debug.Log("CanAttack 1");
		}
		if(xAttack == xDef && yAttack + 1 == yDef){
			CanAttack +=1;
			//Debug.Log("CanAttack 2");
		}
		if(xAttack - 1 == xDef && yAttack == yDef){
			CanAttack +=1;
			//Debug.Log("CanAttack 3");
		}
		if(xAttack == xDef && yAttack - 1 == yDef){
			CanAttack +=1;
			//Debug.Log("CanAttack 4");
		}
	}

	void OnMouseExit(){
		renderer.material.color = Color.white;
		CanAttack = 0;
	}

	public void Defend(float x, float y){
		Debug.Log("Im Defend!");
		if(x == ThisChar.position.x/15 && y == ThisChar.position.y/15){
			Kill();
		}
	}

	public void Kill(){
		for(int i = 0; i < Turn.Count; i++){
			if(Turn[i].Type == this.gameObject.name){
				Turn.RemoveAt(i);
			}
		}
		Destroy(this.gameObject);
	}

	void Update () {
		if(Input.GetMouseButton(0)){
			if(CanAttack == 2){
				Battlefields.TempActionPoints = 0;
				if(ThisTag == "Enemys")
					Battlefields.Enemys--;
				else if(ThisTag == "Charachters")
					Battlefields.Heroes--;
				Battlefields.KillCell(Mathf.CeilToInt(ThisChar.position.x/15), Mathf.CeilToInt(ThisChar.position.y/15));
				Kill();

			}
		}
	}
}
