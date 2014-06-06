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
	static int[,] Field;
	public int XField, YField; // размерность поля по вертикали ( XField) и горизонтали (YField)
	public static List<Charachters> Turn;
	public static int Enemys; //кол-во врагов на поле
	public static int Heroes; // кол-во героев на поле
	GameObject WhoMakeStepNow; // Кто делает ход в данный момент
	bool EndOfStep; // Если true - значит ход сделан
	public static int TempActionPoints; // Текущее кол-во экшн поинтов
	public bool frag; // Отвечает за уничтожение this.обьектов
	List<GameObject> ListOfPosCell;
	public GUIStyle JournalMenuStyle;
	public int x_chars; // координаты ГГ в момент хода
	public int y_chars; // координаты ГГ в момент хода
	public static Charachters WhoStep; // кто ходит в данные момент, переменная для боев
	public int new_x, new_y;
	public static Charachters Defending;
	bool AIState = false; //при 1 запоминает ходы для ИИ
	int[] AIEnemyCoordsX = {14,14,14}; // координаты игроков для ИИ
	int[] AIEnemyCoordsY = {0,2,4}; // координаты игроков для ИИ
	int Temp;
	int[,] AIField; // поле ддя расчетов ИИ
	int[] Differences;
	int WALL = -1; // непроходимая ячейка
	int BLANK = -2; // свободная непомеченная ячейка
	int HERO = 3; // ячейка, в которой находится герой
	int ENEMY = 2;// ячейка, в которой находится враг
	static int EMPTY = 0; // свободная ячейка
	int[] px, py; // координаты точек на пути
	int len; //длина пути

	void Start(){
		i = -1;
		XField = 15;
		YField = 5;
		Enemys = 3;
		Heroes = 3;
		px = new int [XField*YField];
		py = new int [XField*YField];
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
		ListOfPosCell = new List<GameObject>();
		Turn = new List<Charachters>();
		Turn.Add (new HeroOne());
		Turn.Add (new HeroTwo());
		Turn.Add (new HeroThree());
		Turn.Add (new EnemyOne());
		Turn.Add (new EnemyTwo());
		Turn.Add (new EnemyThree());
		Differences = new int[3];
		AIField = new int[XField, YField];
		RenderActCell();
		RenderChars();
		PrintMy("AI ", AIField,XField,YField);
		Steps();
	}
	
	public void Steps(){
		i++;
		ClearAllCells();
		if(Heroes > 0 && Enemys > 0){
				int h = Enemys + Heroes;
				i = i % h;
				WhoMakeStepNow = GameObject.Find(Turn[i].Type);
				WhoStep = Turn[i];
				TempActionPoints = WhoStep.ActionPoint;
				x_chars = Mathf.CeilToInt(WhoMakeStepNow.transform.position.x/15);
				y_chars = Mathf.CeilToInt(WhoMakeStepNow.transform.position.y/15);
				if(WhoMakeStepNow.tag != "Enemys")
				{		
						move(x_chars, y_chars, TempActionPoints);
						RenderPosCell();
				}
				else
				{
					AIState = true;
					move(x_chars, y_chars, Turn[i].ActionPoint);
					AIState = false;
					for(int j = 0; j < 3; j++){ 
						Differences[j] = (Mathf.Abs(AIEnemyCoordsX[j] - x_chars) + Mathf.Abs(AIEnemyCoordsY[j] - y_chars));
					}
					AIStep();
				}
		}
		else
		{
			Application.LoadLevel("GlobalWorld");
		}
	}

	#region AI
	void AIStep(){
		Temp = 0;
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
		Defending = Turn[Temp];
		print("X = " + AIEnemyCoordsX[Temp] + " Y =  " + AIEnemyCoordsY[Temp]);
		if(AIField[AIEnemyCoordsX[Temp], AIEnemyCoordsY[Temp]] == 1) //проверка координатов ближайшего врага на то, есть ли он в пределах досягаемости
		{
			print("Enemy Cloee!");
			//выбираем ближайшую точку и атакуем
			if(AIField[AIEnemyCoordsX[Temp] - 1, AIEnemyCoordsY[Temp] - 1] == 1){
				AIMakeTransition(AIEnemyCoordsX[Temp] - 1, AIEnemyCoordsY[Temp] - 1);
				GameObject.Find(Defending.Type).GetComponent<CharAction>().Kill();
				//функция атаки
			}
			else if(AIField[AIEnemyCoordsX[Temp] + 1, AIEnemyCoordsY[Temp] + 1] == 1){
				AIMakeTransition(AIEnemyCoordsX[Temp] + 1, AIEnemyCoordsY[Temp] + 1);
				GameObject.Find(Defending.Type).GetComponent<CharAction>().Kill();
				//функция атаки
			}
			else if(AIField[AIEnemyCoordsX[Temp], AIEnemyCoordsY[Temp] - 1] == 1){
				AIMakeTransition(AIEnemyCoordsX[Temp], AIEnemyCoordsY[Temp] - 1);
				GameObject.Find(Defending.Type).GetComponent<CharAction>().Kill();
				//функция атаки
			}
			else if(AIField[AIEnemyCoordsX[Temp] + 1, AIEnemyCoordsY[Temp] - 1] == 1){
				AIMakeTransition(AIEnemyCoordsX[Temp] + 1, AIEnemyCoordsY[Temp] - 1);
				GameObject.Find(Defending.Type).GetComponent<CharAction>().Kill();
				//функция атаки
			}
			else if(AIField[AIEnemyCoordsX[Temp] + 1, AIEnemyCoordsY[Temp]] == 1){
				AIMakeTransition(AIEnemyCoordsX[Temp] + 1, AIEnemyCoordsY[Temp]);
				GameObject.Find(Defending.Type).GetComponent<CharAction>().Kill();
				//функция атаки
			}
			else if(AIField[AIEnemyCoordsX[Temp], AIEnemyCoordsY[Temp] + 1] == 1){
				AIMakeTransition(AIEnemyCoordsX[Temp], AIEnemyCoordsY[Temp] + 1);
				GameObject.Find(Defending.Type).GetComponent<CharAction>().Kill();
				//функция атаки
			}
			else if(AIField[AIEnemyCoordsX[Temp] - 1, AIEnemyCoordsY[Temp] + 1] == 1){
				AIMakeTransition(AIEnemyCoordsX[Temp] - 1, AIEnemyCoordsY[Temp] + 1);
				GameObject.Find(Defending.Type).GetComponent<CharAction>().Kill();
				//функция атаки
			}
			else if(AIField[AIEnemyCoordsX[Temp] - 1, AIEnemyCoordsY[Temp]] == 1){
				AIMakeTransition(AIEnemyCoordsX[Temp] - 1, AIEnemyCoordsY[Temp]);
				GameObject.Find(Defending.Type).GetComponent<CharAction>().Kill();
				//функция атаки
			}
		}
		else //если враг не в пределах досягаемости смотрим как к нему придти
		{
			AIWaveAlgorithm(x_chars, y_chars, AIEnemyCoordsX[Temp], AIEnemyCoordsY[Temp]);
			AIMakeTransition(px[WhoStep.ActionPoint],py[WhoStep.ActionPoint]);
			Steps();
		}
	}
	#endregion

		void AIWaveAlgorithm(int ax,int ay,int bx, int by)   // поиск пути из ячейки (ax, ay) в ячейку (bx, by)
		{
			int[] dx = {1, 0, -1, 0};   // смещения, соответствующие соседям ячейки
			int[] dy = {0, 1, 0, -1};   // справа, снизу, слева и сверху
			int d, x, y, k;
			bool stop = false;
			for(int q = 0; q < XField; q++)
				for(int w = 0; w < YField; w++)
					if(AIField[q,w] == 1 || AIField[q,w] >= 0)
						AIField[q,w] = BLANK;

			//PrintMy("Opps",AIField,15,5);
			// распространение волны
			d = 0;         // стартовая ячейка помечена 0
			AIField[ax,ay] = 0;
			do{
				stop = true;               // предполагаем, что все свободные клетки уже помечены
				for ( x = 0; x < XField; x++ )
					for ( y = 0; y < YField; y++ )
						if( AIField[x,y] == d )
							for ( k = 0; k < 4; k++ ) // проходим по всем непомеченным соседям
							{         
								if(( x + dx[k] ) > -1 && ( y + dy[k] ) > -1 && ( x + dx[k] ) < XField && ( y + dy[k] ) < YField)
								{
									if ( AIField[x + dx[k], y + dy[k]] == BLANK )
									{
										stop = false;                            // найдены непомеченные клетки
										AIField[x + dx[k], y + dy[k]] = d + 1;      // распространяем волну
									}
								}
							}
					d++;
		}while (!stop && AIField[bx,by] == BLANK);

			// восстановление пути
			len = AIField[bx,by];            // длина кратчайшего пути из (ax, ay) в (bx, by)
			x = bx;
			y = by;
			d = len;
			while ( d > 0 )
			{
				px[d] = x;
				py[d] = y;                   // записываем ячейку (x, y) в путь
				d--;
				for ( k = 0; k < 4; k++ ){
					if(( x + dx[k] ) > -1 && ( y + dy[k] ) > -1 && ( x + dx[k] ) < XField && ( y + dy[k] ) < YField )
						if ( AIField[x + dx[k], y + dy[k]] == d )
						{
							x = x + dx[k];
							y = y + dy[k];           // переходим в ячейку, которая на 1 ближе к старту
							break;
						}
				}
			}
			px[0] = ax;
			py[0] = ay;                    // теперь px[0..len] и py[0..len] - координаты ячеек пути
			//PrintMy("End",AIField,15,5);
	}


	public static void KillCell(int i, int j){
		Field[i,j] = EMPTY;
	}

	//Передвижение персонажа на клетку с координатами (x, y)
	public void MakeTransition(float x, float y){
		new_x = Mathf.CeilToInt(x/15);
		new_y = Mathf.CeilToInt(y/15);
		TempActionPoints -= (Mathf.Abs(x_chars - new_x) + Mathf.Abs(y_chars - new_y));
		AIEnemyCoordsX[i] = (new_x);
		AIEnemyCoordsY[i] = (new_y);
		Field[x_chars, y_chars] = EMPTY;
		Field[new_x, new_y] = HERO;
		WhoMakeStepNow.transform.position = new Vector3(x, y);
		if(TempActionPoints > 0)
		{
			x_chars = new_x;
			y_chars = new_y;
			ClearAllCells();
			move(new_x, new_y, TempActionPoints);
			RenderPosCell();	
		}
		else
			Steps();
	}
	//Передвижение ИИ на клетку с координатами (x, y)
	public void AIMakeTransition(float x, float y)
	{
		new_x = Mathf.CeilToInt(x);
		new_y = Mathf.CeilToInt(y);
		Field[x_chars, y_chars] = EMPTY;
		Field[new_x, new_y] = ENEMY;
		AIField[x_chars, y_chars] = BLANK;
		AIField[new_x, new_y] = WALL;
		WhoMakeStepNow.transform.position = new Vector3(x*15, y*15);
		Steps();
	}

	//Отрисовка персонажей
	void RenderChars(){
		for(int i = 0; i < XField; i++){
			for(int j = 0; j < YField; j++){
				if(i == 0){
					if(j == 0){
						Instantiate(En1, new Vector3(i * 15, j * 15, -10),Quaternion.identity);
						Field[i,j] = ENEMY;
						AIField[i,j] = WALL;
					}
					if(j == 2){
						Instantiate(En2, new Vector3(i * 15, j * 15, -10),Quaternion.identity);
						Field[i,j] = ENEMY;
						AIField[i,j] = WALL;
					}
					if(j == 4){
						Instantiate(En3, new Vector3(i * 15, j * 15, -10),Quaternion.identity);
						Field[i,j] = ENEMY;
						AIField[i,j] = WALL;
					}
				}
				if(i == 14){
					if(j == 0){
						Instantiate(WarriorTexture, new Vector3( i * 15, j * 15, -10),Quaternion.identity);
						Field[i,j] = HERO;
					}
					if(j == 2){
						Instantiate(ArcherTexture, new Vector3(i * 15 , j * 15, -10),Quaternion.identity);
						Field[i,j] = HERO;	
					}
					if(j == 4){
						Instantiate(MageTexture, new Vector3(i * 15 , j * 15, -10),Quaternion.identity);
						Field[i,j] = HERO;
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
					Field[i,j] = EMPTY;
					AIField[i,j] = BLANK;
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
	void PrintMy(string sec, int[,] mass, int x, int y){
		string next = "PrintMy " + sec + ":\n";
		for(int i = 0; i < y; i++){
			next+= "\n";
			for(int j = 0; j < x; j++)
				next += mass[j,i];
		}	
		print(next);
	}

	void PrintMy(string message, int[] A){
		print (message);
		for(int i = 0; i < A.Length; i++)
			print(i+ " = " + A[i]);
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
					if(AIState)
						AIField[x_char,y_char] = 1;
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

	void OnGUI(){
		GUI.TextField(new Rect(0, Screen.height - 230, 200, 230),WhoStep.Type,JournalMenuStyle);
		if(GUI.Button (new Rect (Screen.width - 200, Screen.height - 100, 200, 100),"Next Step", JournalMenuStyle)){
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

