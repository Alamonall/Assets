using UnityEngine;
using System.Collections;

public class CharAction : MonoBehaviour {
	Charachters Turn; // Кто ходит в данный момент (переменная из Battlefields)
	int CanAttack = 0; // Может ли гг атаковать this персонажа
	string ThisTag; // Тэг this обьекта

	void Start(){
		ThisTag = this.gameObject.tag;
	}

	void OnMouseEnter(){
		Turn = GameObject.Find("Main Camera").GetComponent<Battlefields>().WhoStep;
		if(GameObject.Find(Turn.Type).tag != ThisTag){
			renderer.material.color = Color.red;
			CanAttack = 1;
			CutMove();
		}
	}

	//Функция, которая проверяет есть ли враг поблизости 
	void CutMove(){
		float xAttack = GameObject.Find(Turn.Type).transform.position.x/15;
		float yAttack = GameObject.Find(Turn.Type).transform.position.y/15;
		float xDef = this.gameObject.transform.position.x/15;
		float yDef = this.gameObject.transform.position.y/15;
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

	void Update () {
		if(Input.GetMouseButton(0)){
			if(CanAttack == 2){
				GameObject.Find("Main Camera").GetComponent<Battlefields>().Steps();
				if(ThisTag == "Enemys")
					GameObject.Find("Main Camera").GetComponent<Battlefields>().Enemys--;
				else if(ThisTag == "Charachters")
					GameObject.Find("Main Camera").GetComponent<Battlefields>().Heroes--;
				GameObject.Find("Main Camera").GetComponent<Battlefields>().KillCell(Mathf.CeilToInt(this.gameObject.transform.position.x/15), Mathf.CeilToInt(this.gameObject.transform.position.y/15));
					Destroy(this.gameObject);
			}
		}
	}
}
