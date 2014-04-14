using UnityEngine;
using System.Collections;

public class Battlefields : MonoBehaviour {
	Object WarriorTexture;
	Object ArcherTexture;
	Object MageTexture;
	Object ActCell;
	Object PosCell;
	Object En1;
	Object En2;
	Object En3;
	int[,] Field;
	int n, m;
	void Start(){
		n = 6;
		m = 15;
		WarriorTexture = Resources.Load("Warrior");
		ArcherTexture = Resources.Load("Archer");
		MageTexture = Resources.Load("MAge");
		ActCell = Resources.Load("Cell_Act");
		PosCell = Resources.Load("Cell_Pos");
		En1 = Resources.Load("Blue");
		En2 = Resources.Load("Green");
		En3 = Resources.Load("Red");
		Field = new int[n,m];
		Marking();
	}
	void NextMove(){


	]
	void Marking(){
		int c = 15;
		for(int i = 0; i < n; i++){
			for(int j = 0; j < m; j++){
			//	Debug.Log("i = " + i + "j = " + j);
				if(j == 1){
					if(i == 0){
						Instantiate(En1, new Vector3(j * 15 + c, (i + 1) * 15, -10),Quaternion.identity);
						Field[i,j] = 2;
					}
					if(i == 2){
						Instantiate(En2, new Vector3(j * 15 + c, (i + 1) * 15, -10),Quaternion.identity);
						Field[i,j] = 2;
					}
					if(i == 4){
						Instantiate(En3, new Vector3(j * 15 + c, (i + 1) * 15, -10),Quaternion.identity);
						Field[i,j] = 2;
					}
				}
				if(j == 14){
					if(i == 0){
						Instantiate(WarriorTexture, new Vector3( j * 15 + c, (i + 1) * 30, -10),Quaternion.identity);
						Field[i,j] = 3;
					}
					if(i == 2){
						Instantiate(ArcherTexture, new Vector3(j * 15 + (c + 2), i * 30, -10),Quaternion.identity);
						Field[i,j] = 3;
					}
					if(i == 3){
						Instantiate(MageTexture, new Vector3(j * 15 + (c + 3), i * 30, -10),Quaternion.identity);
						Field[i,j] = 3;
					}
				}
				Instantiate(ActCell, new Vector3(j*15 + c, (i + 1) * 15),Quaternion.identity);
				if(Field[i,j] != 1 || Field[i,j] != 2 || Field[i,j] != 3){
					Field[i,j] = 0;
				}
			}
			c=c+4;
		}

	}
	void Step(){
		int c = 15;
		for(int i = 0; i < n; i++){
			for(int j = 0; j < m; j++){
				if(Field[i,j] == 1)
					Instantiate(PosCell, new Vector3(j*15 + c, (i + 1) * 15),Quaternion.identity);
			}
			c=c+4;
		}
			
	}
	void move(int x1, int x2, int step){
		if(step > 0){
			if(x1 > 1 && x2 >= 0){
				move(x1 - 1,x2,step - 1);
			}
			if(x2 > 1 && x1 >= 0){
				move(x1, x2 - 1, step - 1);
			}
			if(x1 < n && x2 < m){
				move(x1 + 1, x2, step - 1);
			}
			if(x2 < m && x1 < n){
				move(x1,x2 + 1, step - 1);
			}
			if(Field[x1,x2] == 0)
				Field[x1,x2] = 1;
		}
		else{
			if(Field[x1,x2] == 0){
				Field[x1,x2] = 1;

			}
		}
	}
}

