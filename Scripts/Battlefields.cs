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
	int i;
	public int[,] Field;
	int XField, YField; // размерность поля по вертикали ( XField) и горизонтали (YField)
	public List<GameObject> Turn;
	public int Enemys; //кол-во врагов на поле
	public int Heroes; // кол-во героев на поле
	public static GameObject WhoMakeStepNow; // Кто делает ход в данный момент
	public static bool EndOfStep; // Если true - значит ход сделан
	public int TempActionPoints; // Текущее кол-во экшн поинтов
	public static bool frag; // Отвечает за уничтожение this.обьектов
	List<GameObject> ListOfPosCell;
	public GUIStyle JournalMenuStyle;
	public int x_chars; // координаты ГГ в момент хода
	public int y_chars; // координаты ГГ в момент хода
	public GameObject WhoStep; // кто ходит в данные момент, переменная для боев
	public int new_x,new_y; //Для запоминания новых координат
	bool BattleIsEnd = false;
	int AIState = 0; //при 1 запоминает ходы для ИИ
	List<int> AIEnemyCoordsX; // координаты игроков для ИИ
	List<int> AIEnemyCoordsY; // координаты игроков для ИИ
	int[,] AIField; // поле ддя расчетов ИИ
	int[] Differences;
	bool AIorPlayer = false; // Кто ходит Игрок (false) или ИИ (true)? 

	void Start()
	{
		i = -1;
		XField = 15;
		YField = 5;
		Enemys = 3;
		Heroes = 3;
		frag = false;
		WarriorTexture = Resources.Load("HeroOne");
		ArcherTexture = Resources.Load("HeroTwo");
		MageTexture = Resources.Load("HeroThree");
		ActCell = Resources.Load("Cell_Act");
		PosCell = Resources.Load("Cell_Pos");
		En1 = Resources.Load("EnemyOne");
		En2 = Resources.Load("EnemyTwo");
		En3 = Resources.Load("EnemyThree");
		Field = new int[XField, YField];
		AIField = new int[XField, YField]; 
		ListOfPosCell = new List<GameObject>();
		Turn = new List<GameObject>();
		Turn.Add (HeroOne());
		Turn.Add (HeroTwo());
		Turn.Add (HeroThree());
		Turn.Add (EnemyOne());
		Turn.Add (EnemyTwo());
		Turn.Add (EnemyThree());
		//RenderActCell();
		//RenderChars();
		//Steps();
	}
	
	public void Steps(){
		i++;
		ClearAllCells();
		if(Heroes > 0 && Enemys > 0)
		{
				int h = Enemys + Heroes;
				i = i % h;
				if(!Turn[i])
					Steps();
				WhoMakeStepNow = GameObject.Find(Turn[i]).GetComponent<GameObject>().
				WhoStep = Turn[i];
				TempActionPoints = Turn[i].ActionPoint;
				x_chars = Mathf.CeilToInt(WhoMakeStepNow.transform.position.x/15);
				y_chars = Mathf.CeilToInt(WhoMakeStepNow.transform.position.y/15);
//				if(WhoMakeStepNow.tag != "Enemys")
//				{		
						move(x_chars, y_chars, TempActionPoints);
						RenderPosCell();
//				}
//				else
//				{
//					AIState = 1;
//					move(x_chars, y_chars, Turn[i].ActionPoint);
//					AIState = 0;
//					for(int j = 0; j < 3; j++)
//					{ 
//						Debug.Log(j);
//						Differences[i] = Mathf.Abs(AIEnemyCoordsX[j] - x_chars) + Mathf.Abs(AIEnemyCoordsY[j] - y_chars);
//					}
//					AIStep();
//					i++;
//					Steps();
//				}
		}
		else
		{
			Debug.Log("EndBattle");
			BattleIsEnd = true;
		}
	}

	void AIStep(){
		int Temp = 0;
		if(Differences[0] <= Differences[1])
		{
			if(Differences[0] <= Differences[2])
				Temp = 0;
			else
				Temp = 2;
		}

		else
		{
			if(Differences[1] <= Differences[2])
				Temp = 1;
			else 
				Temp = 2;
		}
		AIorPlayer = true;
		if(AIField[AIEnemyCoordsX[Temp], AIEnemyCoordsY[Temp]] == 1)
		{
			if(AIField[AIEnemyCoordsX[Temp] - 1, AIEnemyCoordsY[Temp] - 1] == 1){
				MakeTransition(AIEnemyCoordsX[Temp] - 1, AIEnemyCoordsY[Temp] - 1);
			}
			else if(AIField[AIEnemyCoordsX[Temp] + 1, AIEnemyCoordsY[Temp] + 1] == 1){
				MakeTransition(AIEnemyCoordsX[Temp] + 1, AIEnemyCoordsY[Temp] + 1);
			}
			else if(AIField[AIEnemyCoordsX[Temp], AIEnemyCoordsY[Temp] - 1] == 1){
				MakeTransition(AIEnemyCoordsX[Temp], AIEnemyCoordsY[Temp] - 1);
			}
			else if(AIField[AIEnemyCoordsX[Temp] + 1, AIEnemyCoordsY[Temp] - 1] == 1){
				MakeTransition(AIEnemyCoordsX[Temp] + 1, AIEnemyCoordsY[Temp] - 1);
			}
			else if(AIField[AIEnemyCoordsX[Temp] + 1, AIEnemyCoordsY[Temp]] == 1){
				MakeTransition(AIEnemyCoordsX[Temp] + 1, AIEnemyCoordsY[Temp]);
			}
			else if(AIField[AIEnemyCoordsX[Temp], AIEnemyCoordsY[Temp] + 1] == 1){
				MakeTransition(AIEnemyCoordsX[Temp], AIEnemyCoordsY[Temp] + 1);
			}
			else if(AIField[AIEnemyCoordsX[Temp] - 1, AIEnemyCoordsY[Temp] + 1] == 1){
				MakeTransition(AIEnemyCoordsX[Temp] - 1, AIEnemyCoordsY[Temp] + 1);
			}
			else if(AIField[AIEnemyCoordsX[Temp] - 1, AIEnemyCoordsY[Temp]] == 1){
				MakeTransition(AIEnemyCoordsX[Temp] - 1, AIEnemyCoordsY[Temp]);
			}
		}
		else
		{
					if(AIField[x_chars + WhoStep.ActionPoint, y_chars]== 1)
					{
						MakeTransition(x_chars + WhoStep.ActionPoint, y_chars);
					}
					else
					{
						MakeTransition(x_chars * 15, y_chars + WhoStep.ActionPoint*15);
					}
		}
	}

	public void KillCell(int i, int j)
	{
		Field[i,j] = 0;
	}

	//Передвижение персонажа на клетку с координатами (x, y)
	public void MakeTransition(float x, float y){
		new_x = Mathf.CeilToInt(x/15);
		new_y = Mathf.CeilToInt(y/15);
		Field[x_chars, y_chars] = 0;
		Field[new_x, new_y] = 3;
		TempActionPoints -= (Mathf.Abs(x_chars - new_x) + Mathf.Abs(y_chars - new_y));
		WhoMakeStepNow.transform.position = new Vector3(x, y);
		x_chars = new_x;
		y_chars = new_y;
		if(TempActionPoints > 0)
		{
			ClearAllCells();
			move(new_x, new_y, TempActionPoints);
			RenderPosCell();	
		}
		else
		{
			Steps();
		}
	}

	//Отрисовка персонажей
	void RenderChars(){
		for(int i = 0; i < XField; i++){
			for(int j = 0; j < YField; j++){
				if(i == 0){
					if(j == 0)
					{
						Instantiate(En1, new Vector3(i * 15, j * 15, -10),Quaternion.identity);
						Field[i,j] = 2;
					}
					if(j == 2)
					{
						Instantiate(En2, new Vector3(i * 15, j * 15, -10),Quaternion.identity);
						Field[i,j] = 2;
					}
					if(j == 4)
					{
						Instantiate(En3, new Vector3(i * 15, j * 15, -10),Quaternion.identity);
						Field[i,j] = 2;
					}
				}
				if(i == 14){
					if(j == 0)
					{
						Instantiate(WarriorTexture, new Vector3( i * 15, j * 15, -10),Quaternion.identity);
						Field[i,j] = 3;
					}
					if(j == 2)
					{
						Instantiate(ArcherTexture, new Vector3(i * 15 , j * 15, -10),Quaternion.identity);
						Field[i,j] = 3;
					}
					if(j == 4)
					{
						Instantiate(MageTexture, new Vector3(i * 15 , j * 15, -10),Quaternion.identity);
						Field[i,j] = 3;
					}
				}
			}
		}

	}

	// Отрисовка клеток
	void RenderActCell(){
		for(int i = 0; i < XField; i++){
			for(int j = 0; j < YField; j++){
				Instantiate(ActCell, new Vector3(i * 15, j * 15),Quaternion.identity);
				if(Field[i,j] != 1 || Field[i,j] != 2 || Field[i,j]!= 3){
					Field[i,j] = 0;
					AIField[i,j] = 0;
				}
			}
		}
	}

	// Отрисовка возможных ходов 
	void RenderPosCell(){
		for(int i = 0; i < XField; i++){
			for(int j = 0; j < YField; j++){
				if(Field[i,j] == 1){
					ListOfPosCell.Add(Instantiate(PosCell, new Vector3(i * 15,j * 15),Quaternion.identity) as GameObject);
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
		}
		if(x_char > -1 && y_char > -1 && x_char < XField && y_char < YField){
				if(Field[x_char, y_char] == 0){
					if(AIState == 1){
						AIField[x_char, y_char] = 1;
					}
					else
						Field[x_char, y_char] = 1;
				}
		}
	}

	//Очистка клеток на след. ход
	void ClearAllCells(){
		for(int i = 0; i < XField; i++){
			for(int j = 0; j < YField; j++){
				if(Field[i,j] == 1){
					Field[i,j] = 0;
				}
			}
		}
		for(int i = 0; i < ListOfPosCell.Count; i++ ){
			Destroy(ListOfPosCell[i]);
		}
	}

	void EndOfTheBattle(){
		string EndBattleText = "Battle is End";
		if(Enemys == 0)
			EndBattleText = "We won!"; 
		GUI.BeginGroup(new Rect(Screen.width/3, Screen.height/4,600,500),"", JournalMenuStyle);
		if(GUI.Button(new Rect( 200, 350, 200, 100),EndBattleText,JournalMenuStyle))
				Application.LoadLevel("GlobalWorld");
		GUI.EndGroup();
	}


	void OnGUI(){
		if(BattleIsEnd)
			EndOfTheBattle();
		if(GUI.Button (new Rect (Screen.width - 200, Screen.height - 70, 200, 70),"Next Step", JournalMenuStyle)){
			Steps();
	  }
	}

	void Update(){
		if(Input.GetKey(KeyCode.B))
			Application.LoadLevel("GlobalWorld");
		if(Input.GetKeyDown(KeyCode.KeypadEnter)){
			MakeTransition(WhoMakeStepNow.transform.position.x,WhoMakeStepNow.transform.position.y);
		}
	}
}

