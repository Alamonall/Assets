using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharAction : MonoBehaviour {
	List<Charachters> Turn; //Очередь из Battlefields
	int CanAttack = 0; // Может ли гг атаковать this персонажа
	string ThisTag; // Тэг this обьекта
	Charachters WhoStep;// Кто ходит в данный момент (переменная из Battlefields)
	Transform ThisChar;
	bool tuggle;

	void Start(){
		tuggle = true;
		ThisTag = this.gameObject.tag;
		Turn = Battlefields.Turn;
		WhoStep = Battlefields.WhoStep;
		ThisChar = this.gameObject.transform;
	}



	void OnMouseEnter(){
		if(GameObject.Find(WhoStep.Type) != null)
			if(GameObject.Find(WhoStep.Type).tag != ThisTag){
				renderer.material.color = Color.black;
				CanAttack = 1;
				CutMove();
			}
	}


	//Функция, которая проверяет есть ли враг поблизости 
	void CutMove(){
		float xAttack = Battlefields.AIEnemyCoordsX[Battlefields.i];
		float yAttack = Battlefields.AIEnemyCoordsY[Battlefields.i];
		float xDef = ThisChar.position.x/15;
		float yDef = ThisChar.position.y/15;
		Debug.Log("xA = " + xAttack + "; yA = " + yAttack);
		Debug.Log ("xD = " + xDef + "; yD = " + yDef);
		if(xAttack + 1 == xDef && yAttack == yDef){
			CanAttack +=1;
		}
		if(xAttack == xDef && yAttack + 1 == yDef){
			CanAttack +=1;
		}
		if(xAttack - 1 == xDef && yAttack == yDef){
			CanAttack +=1;
		}
		if(xAttack == xDef && yAttack - 1 == yDef){
			CanAttack +=1;
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
		if(ThisTag == "Enemys")
			Battlefields.Enemys--;
		else if(ThisTag == "Charachters")
			Battlefields.Heroes--;
		for(int i = 0; i < Turn.Count; i++){
			if(Turn[i].Type == this.gameObject.name){
				Debug.Log(" I killed " + Turn[i]);
				Turn[i].Death = true;
				Battlefields.TempActionPoints = 0;
				Destroy(this.gameObject);
			}
		}
	}

	void Update () {
		if(Input.GetMouseButtonDown(0)){
			if(CanAttack == 2){
				CanAttack = 0;
				Battlefields.KillCell(Mathf.CeilToInt(ThisChar.position.x/15), Mathf.CeilToInt(ThisChar.position.y/15));
				GameObject.Find("Main Camera").GetComponent<Battlefields>().MakeTransition(ThisChar.position.x,ThisChar.position.y);
				Kill();

			}
		}
		if (Input.GetKeyDown (KeyCode.K)) {
						if (tuggle)
								tuggle = false;
						else if (!tuggle)
								tuggle = true;
						this.gameObject.renderer.enabled = tuggle;
				}
	}
}
