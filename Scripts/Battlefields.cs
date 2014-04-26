using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Battlefields : MonoBehaviour {
	Object WarriorTexture;
	Object ArcherTexture;
	Object MageTexture;
	Object ActCell;
	Object PosCell;
	Object En1;
	Object En2;
	Object En3;
	public int[,] Field;
	int HeightField, WidthField,i; // размерность поля по вертикали ( HeightField) и горизонтали (WidthField)
	public List<Charachters> Turn;
	public int Enemys; //кол-во врагов на поле
	public int Heroes; // кол-во героев на поле
	public GameObject WhoMakeStepNow; // Кто делает ход в данный момент
	bool EndOfStep; // Если true - значит ход сделан

	void Start(){
		HeightField = 5;
		WidthField = 15;
		WarriorTexture = Resources.Load("HeroOne");
		ArcherTexture = Resources.Load("HeroTwo");
		MageTexture = Resources.Load("HeroThree");
		ActCell = Resources.Load("Cell_Act");
		PosCell = Resources.Load("Cell_Pos");
		En1 = Resources.Load("EnemyOne");
		En2 = Resources.Load("EnemyTwo");
		En3 = Resources.Load("EnemyThree");
		Field = new int[HeightField, WidthField];
		RenderActCell();
		PrintMy("RenderActCell");
		RenderChars();
		PrintMy("RenderChars");
		Turn = new List<Charachters>();
		Turn.Add (new HeroOne());
		Turn.Add (new HeroTwo());
		Turn.Add (new HeroThree());
		Turn.Add (new EnemyOne());
		Turn.Add (new EnemyTwo());
		Turn.Add (new EnemyThree());
		Steps();
		//Step();

	}
	
	void Steps(){
			if(Enemys != 0 || Heroes != 0){
				i= i% (Enemys+Heroes);				
				WhoMakeStepNow = GameObject.Find(Turn[i].Type);
				int y_chars = Mathf.CeilToInt(WhoMakeStepNow.transform.position.x/15);
				int x_chars = Mathf.CeilToInt(WhoMakeStepNow.transform.position.y/15);
				move(x_chars, y_chars, Turn[i].ActionPoint);
				PrintMy("move");
				RenderPosCeil();
			}
	}
	//Отрисовка персонажей
	void RenderChars(){
		for(int i = 0; i < HeightField; i++){
			for(int j = 0; j < WidthField; j++){
				if(j == 0){
					if(i == 0){
						Instantiate(En1, new Vector3(j * 15, i * 15, -10),Quaternion.identity);
						Field[i,j] = 2;
					}
					if(i == 2){
						Instantiate(En2, new Vector3(j * 15, i * 15, -10),Quaternion.identity);
						Field[i,j] = 2;
					}
					if(i == 4){
						Instantiate(En3, new Vector3(j * 15, i * 15, -10),Quaternion.identity);
						Field[i,j] = 2;
					}
				}
				if(j == 14){
					if(i == 0){
						Instantiate(WarriorTexture, new Vector3( j * 15, i * 15, -10),Quaternion.identity);
						Field[i,j] = 3;
					}
					if(i == 2){
						Instantiate(ArcherTexture, new Vector3(j * 15 , i * 15, -10),Quaternion.identity);
						Field[i,j] = 3;
					}
					if(i == 4){
						Instantiate(MageTexture, new Vector3(j * 15 , i * 15, -10),Quaternion.identity);
						Field[i,j] = 3;
					}
				}
			}
		}

	}

	// Отрисовка клеток
	void RenderActCell(){
		for(int i = 0; i < HeightField; i++){
			for(int j = 0; j < WidthField; j++){
				Instantiate(ActCell, new Vector3(j * 15, i * 15),Quaternion.identity);
				if(Field[i,j] != 1 || Field[i,j] != 2 || Field[i,j] != 3){
					Field[i,j] = 0;
				}
			}
		}
	}

	// Отрисовка возможных ходов 
	void RenderPosCeil(){
		for(int i = 0; i < HeightField; i++){
			for(int j = 0; j < WidthField; j++){
				if(Field[i,j] == 1){
					Instantiate(PosCell, new Vector3(j*15,i * 15),Quaternion.identity);
				}
			}
		}	
	}

	//функция вывода массива
	void PrintMy(string sec){
		string next = "PrintMy " + sec + " \n";
		for(int i = 0; i < HeightField; i++){
			next+= "\n";
			for(int j = 0; j < WidthField; j++){
				next += Field[i,j];
			}
		}	
		print(next);
	}

	//Подсчет возможных ходов для персонажа, x и y_char координаты персонажа в массиве поля
	void move(int x_char, int y_char, int step){
		if(step > 0){
			Debug.Log("1");
			if(x_char > 0 && y_char > 0 && x_char < HeightField && y_char < WidthField){
				Debug.Log("2");
				move(x_char - 1, y_char, step - 1);
				move(x_char, y_char - 1, step - 1);
				move(x_char + 1, y_char, step - 1);
				move(x_char,y_char + 1, step - 1);
				if(Field[x_char,y_char] == 0){
					Debug.Log("3");
					Field[x_char, y_char] = 1;
				}
			}
			if(Field[x_char,y_char] == 0){
				Debug.Log("4");
				Field[x_char, y_char] = 1;
			}
		}
	}
	//Очистка клеток на след. ход
	void ClearAllCeils(){
		for(int i = 0; i < HeightField; i++){
			for(int j = 0; j < WidthField; j++){
				if(Field[i,j] == 1){
					Field[i,j] = 0;
				}
			}
		}
	}
}

