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
	public static GameObject WhoMakeStepNow; // Кто делает ход в данный момент
	public static bool EndOfStep; // Если true - значит ход сделан
	public int TempActionPoints;
	public static bool frag; // Отвечает за уничтожение this.обьектов

	void Start(){
		HeightField = 5;
		WidthField = 15;
		frag = false;
		EndOfStep = false;
		WarriorTexture = Resources.Load("HeroOne");
		ArcherTexture = Resources.Load("HeroTwo");
		MageTexture = Resources.Load("HeroThree");
		ActCell = Resources.Load("Cell_Act");
		PosCell = Resources.Load("Cell_Pos");
		En1 = Resources.Load("EnemyOne");
		En2 = Resources.Load("EnemyTwo");
		En3 = Resources.Load("EnemyThree");
		Field = new int[HeightField, WidthField];
		Turn = new List<Charachters>();
		Turn.Add (new HeroOne());
		Turn.Add (new HeroTwo());
		Turn.Add (new HeroThree());
		Turn.Add (new EnemyOne());
		Turn.Add (new EnemyTwo());
		Turn.Add (new EnemyThree());
		RenderActCell();
		RenderChars();
		Steps();
	}
	
	void Steps(){
			if(Enemys != 0 || Heroes != 0){
				i= i% (Enemys+Heroes);	
				WhoMakeStepNow = GameObject.Find(Turn[i].Type);
				TempActionPoints = Turn[i].ActionPoint;
				int y_chars = Mathf.CeilToInt(WhoMakeStepNow.transform.position.x/15);
				int x_chars = Mathf.CeilToInt(WhoMakeStepNow.transform.position.y/15);
				move(x_chars, y_chars, Turn[i].ActionPoint);
				if(!EndOfStep)
					RenderPosCeil();
				if(EndOfStep){
						EndOfStep = false;
						i++;
						ClearAllCeils();
				     	Steps();
				}
			}
	}
	//Передвижение персонажа на клетку с координатами (x, y)
	public void MakeTransition(float x, float y){
		int new_x = Mathf.CeilToInt(x/15);
		int new_y = Mathf.CeilToInt(y/15);
		int LeftActionPoints = TempActionPoints - Mathf.Abs(Mathf.CeilToInt(WhoMakeStepNow.transform.position.x/15) - new_x) + Mathf.Abs(Mathf.CeilToInt(WhoMakeStepNow.transform.position.y/15) - new_y);
		WhoMakeStepNow.transform.position = new Vector3(x, y);
		ClearAllCeils();
		Debug.Log("LeftActionPoints = " + LeftActionPoints + " ");
		frag = true;
		move(new_x, new_y, LeftActionPoints);
		RenderPosCeil();			                              

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
	// HeightField = 5, WidthField = 15
	void move(int x_char, int y_char, int step){
		if(step >= 0){
			//Debug.Log("1 x = " + x_char + " y = " + y_char);
			if(x_char >= 1 && y_char >= 0){
				move(x_char - 1, y_char, step - 1);
			}
			if(x_char >= 0 && y_char >= 1){
				move(x_char, y_char - 1, step - 1);
			}
			if(x_char < HeightField && y_char <= WidthField){
				move(x_char + 1, y_char, step - 1);
			}
			if(x_char <= HeightField && y_char < WidthField){
				move(x_char,y_char + 1, step - 1);
			}
			if(x_char > -1 && y_char > -1 && x_char < HeightField && y_char < WidthField){
				if(Field[ x_char, y_char] == 0){
					Field[x_char, y_char] = 1;
				}
			}
			if(x_char > -1 && y_char > -1 && x_char < HeightField && y_char < WidthField){
				if(Field[x_char,y_char] == 0){
					Field[x_char, y_char] = 1;
				}
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


	void Update(){
		if(Input.GetKeyDown(KeyCode.S)){
			if(EndOfStep){
				EndOfStep = false;
			}
			else if(!EndOfStep){
				EndOfStep = true;
			}
			Steps();
		}
	}
}

