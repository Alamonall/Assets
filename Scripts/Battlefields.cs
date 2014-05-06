using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Battlefields : MonoBehaviour {
	Object WarriorTexture;
	Object ArcherTexture;
	Object MageTexture;
	Object ActCeil;
	Object PosCeil;
	Object En1;
	Object En2;
	Object En3;
	int i;
	public int[,] Field;
	public int XField, YField; // размерность поля по вертикали ( XField) и горизонтали (YField)
	public List<Charachters> Turn;
	public int Enemys; //кол-во врагов на поле
	public int Heroes; // кол-во героев на поле
	public static GameObject WhoMakeStepNow; // Кто делает ход в данный момент
	public static bool EndOfStep; // Если true - значит ход сделан
	public int TempActionPoints;
	public static bool frag; // Отвечает за уничтожение this.обьектов
	List<GameObject> ListOfPosCeil;
	public GUIStyle JournalMenuStyle;
	public int x_chars; // координаты ГГ в момент хода
	public int y_chars; // координаты ГГ в момент хода
	public int LeftActionPoints; // отвечает за кол-во оставшихся шагов 
	public Charachters WhoStep; // кто ходит в данные момент, переменная для боев
	public int new_x;
	public int new_y;

	void Start(){
		XField = 15;
		YField = 5;
		Enemys = 3;
		Heroes = 3;
		frag = false;
		EndOfStep = false;
		WarriorTexture = Resources.Load("HeroOne");
		ArcherTexture = Resources.Load("HeroTwo");
		MageTexture = Resources.Load("HeroThree");
		ActCeil = Resources.Load("Cell_Act");
		PosCeil = Resources.Load("Cell_Pos");
		En1 = Resources.Load("EnemyOne");
		En2 = Resources.Load("EnemyTwo");
		En3 = Resources.Load("EnemyThree");
		Field = new int[XField, YField];
		ListOfPosCeil = new List<GameObject>();
		Turn = new List<Charachters>();
		Turn.Add (new HeroOne());
		Turn.Add (new HeroTwo());
		Turn.Add (new HeroThree());
		Turn.Add (new EnemyOne());
		Turn.Add (new EnemyTwo());
		Turn.Add (new EnemyThree());
		RenderActCeil();
		RenderChars();
		Steps();
	}
	
	void Steps(){
		if(Heroes > 0 && Enemys > 0){
				Debug.Log(" Enemys = " + Enemys + " Heroes = " + Heroes);
				int h = Enemys + Heroes;
				i = i % h;
				//Debug.Log(" i = " + i);
				while(!GameObject.Find(Turn[i].Type))
					i++;
				WhoMakeStepNow = GameObject.Find(Turn[i].Type);
				WhoStep = Turn[i];
				//if(WhoMakeStepNow.tag != "Enemys"){
						TempActionPoints = Turn[i].ActionPoint;
						x_chars = Mathf.CeilToInt(WhoMakeStepNow.transform.position.x/15);
						y_chars = Mathf.CeilToInt(WhoMakeStepNow.transform.position.y/15);
				//		Debug.Log(" x = " + x_chars + " y = " +  y_chars);
						move(x_chars, y_chars, Turn[i].ActionPoint);
						if(!EndOfStep)
							RenderPosCeil();
						if(EndOfStep){
								EndOfStep = false;
								i++;
								ClearAllCeils();
						     	Steps();
						}
				//}
			}
		else
			Application.LoadLevel("GlobalWorld");
	}

	public void KillCell(int i, int j){
		Field[i,j] = 0;
	}
	//Передвижение персонажа на клетку с координатами (x, y)
	public void MakeTransition(float x, float y){
		new_x = Mathf.CeilToInt(x/15);
		new_y = Mathf.CeilToInt(y/15);
		int old_x = Mathf.CeilToInt(WhoMakeStepNow.transform.position.x/15);
		int old_y = Mathf.CeilToInt(WhoMakeStepNow.transform.position.y/15);
		Field[old_x, old_y] = 0;
		//Debug.Log(" TempActionPoints = " + TempActionPoints + " old x = " + Mathf.Abs(Mathf.CeilToInt(WhoMakeStepNow.transform.position.x/15)) + " old y = " + Mathf.Abs(Mathf.CeilToInt(WhoMakeStepNow.transform.position.y/15)));
		LeftActionPoints = TempActionPoints - (Mathf.Abs(old_x - new_x) + Mathf.Abs(old_y - new_y));
		//Debug.Log("LeftActionPoints = " + LeftActionPoints + "new x = " + new_x + " new y = " + new_y);
		//Debug.Log(" NewNew x = " + Mathf.Abs(Mathf.CeilToInt(WhoMakeStepNow.transform.position.x/15) - new_x) + " NewNew y = " + Mathf.Abs(Mathf.CeilToInt(WhoMakeStepNow.transform.position.y/15) - new_y));
		WhoMakeStepNow.transform.position = new Vector3(x, y);
		Field[new_x, new_y] = 3;
		ClearAllCeils();
		if(LeftActionPoints != 0){
			move(new_x, new_y, LeftActionPoints);
			RenderPosCeil();	
			TempActionPoints = LeftActionPoints;
		}
		else{
			i++;
			Steps();
		}
	}

	//Отрисовка персонажей
	void RenderChars(){
		for(int i = 0; i < XField; i++){
			for(int j = 0; j < YField; j++){
				if(i == 0){
					if(j == 0){
						Instantiate(En1, new Vector3(i * 15, j * 15, -10),Quaternion.identity);
						Field[i,j] = 2;
					}
					if(j == 2){
						Instantiate(En2, new Vector3(i * 15, j * 15, -10),Quaternion.identity);
						Field[i,j] = 2;
					}
					if(j == 4){
						Instantiate(En3, new Vector3(i * 15, j * 15, -10),Quaternion.identity);
						Field[i,j] = 2;
					}
				}
				if(i == 14){
					if(j == 0){
						Instantiate(WarriorTexture, new Vector3( i * 15, j * 15, -10),Quaternion.identity);
						Field[i,j] = 3;
					}
					if(j == 2){
						Instantiate(ArcherTexture, new Vector3(i * 15 , j * 15, -10),Quaternion.identity);
						Field[i,j] = 3;
					}
					if(j == 4){
						Instantiate(MageTexture, new Vector3(i * 15 , j * 15, -10),Quaternion.identity);
						Field[i,j] = 3;
					}
				}
			}
		}

	}

	// Отрисовка клеток
	void RenderActCeil(){
		for(int i = 0; i < XField; i++){
			for(int j = 0; j < YField; j++){
				Instantiate(ActCeil, new Vector3(i * 15, j * 15),Quaternion.identity);
				if(Field[i,j] != 1 || Field[i,j] != 2 || Field[i,j]!= 3){
					Field[i,j] = 0;
				}
			}
		}
	}

	// Отрисовка возможных ходов 
	void RenderPosCeil(){
		for(int i = 0; i < XField; i++){
			for(int j = 0; j < YField; j++){
				if(Field[i,j] == 1){
					ListOfPosCeil.Add(Instantiate(PosCeil, new Vector3(i * 15,j * 15),Quaternion.identity) as GameObject);
				}
			}
		}	
	}

	//функция вывода массива
	void PrintMy(string sec){
		string next = "PrintMy " + sec + " \n";
		for(int i = 0; i < XField; i++){
			next+= "\n";
			for(int j = 0; j < YField; j++){
				next += Field[i,j];
			}
		}	
		print(next);
	}

	//Подсчет возможных ходов для персонажа, x и y_char координаты персонажа в массиве поля
	// HeightField = 5, WidthField = 15
	void move(int x_char, int y_char, int step){
		if(step > 0){
			//Debug.Log("1 x = " + x_char + " y = " + y_char);
			if(x_char >= 1 && y_char >= 0){
				move(x_char - 1, y_char, step - 1);
			}
			if(x_char >= 0 && y_char >= 1){
				move(x_char, y_char - 1, step - 1);
			}
			if(x_char < XField && y_char <= YField){
				move(x_char + 1, y_char, step - 1);
			}
			if(x_char <= XField && y_char < YField){
				move(x_char,y_char + 1, step - 1);
			}
//			if(x_char > -1 && y_char > -1 && x_char < XField && y_char < YField){
//				if(Field[x_char, y_char] == 0){
//					//Debug.Log(" i = " + x_char + " j = " + y_char);
//					Field[x_char, y_char] = 1;
//					//Debug.Log(Field[x_char,y_char]);
//					}
//				}
			}
		if(x_char > -1 && y_char > -1 && x_char < XField && y_char < YField){
				if(Field[x_char, y_char] == 0){
					Field[x_char, y_char] = 1;
				}
		}
	}

	//Очистка клеток на след. ход
	void ClearAllCeils(){
		for(int i = 0; i < XField; i++){
			for(int j = 0; j < YField; j++){
				if(Field[i,j] == 1){
					Field[i,j] = 0;
				}
			}
		}
		for(int i = 0; i < ListOfPosCeil.Count; i++ ){
			Destroy(ListOfPosCeil[i]);
		}
	}

	void OnGUI(){
		if(GUI.Button (new Rect (Screen.width / 2, (Screen.height / 10) * 9,Screen.width / 15, Screen.height / 15),"Next Step", JournalMenuStyle)){
			if(EndOfStep){
				EndOfStep = false;
			}
			else if(!EndOfStep){
				EndOfStep = true;
			}
			Steps();
	  }
	}

	void Update(){
		if(Input.GetKey(KeyCode.B))
			Application.LoadLevel("GlobalWorld");
		if(Input.GetKeyDown(KeyCode.KeypadEnter)){
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

