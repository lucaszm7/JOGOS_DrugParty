using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour {

    [SerializeField]    
    CutsceneObject[] _listCustscene;
    [SerializeField]    
    internal string name;

    Texture2D RectangleTexture;
    GameObject[] listOcultos;
    SpriteRenderer spriteRenderer;
    CutsceneObject _currentScene;
	int index = -1;
	int max = 0;
	float delay = 0.03f;
    [SerializeField]
 	string TextinLine = "";
    [SerializeField]    
    internal bool update;

    static Vector3 _currentPosition = new Vector3(0,0,-10);

    
	bool IsComplete{
		get{
            if(_currentScene.Type == CutscenesType.Legend) return true;
			if(index == -1 || index > max) return false;
			return TextinLine.Equals(_currentScene.Text);
		}
	}

    virtual public void SetName(string newName){
        name = newName;
    } 

    internal void Go()
    {
        Debug.Log("Isso ai");
        RectangleTexture = Resources.Load<Texture2D>("selection");
        listOcultos = new GameObject[2];
        listOcultos[0] = GameObject.FindWithTag("Player");
        listOcultos[1] = GameObject.Find("PlayerCamera");
        spriteRenderer = GameObject.FindWithTag("Cutscene").GetComponent<SpriteRenderer>();
        TextAsset CutsceneInfo = Resources.Load<TextAsset>("Cutscenes/"+name+"/controller");
        _listCustscene = XMLParser.ParserCutscene(CutsceneInfo.text);
        max = _listCustscene.Length;
        Step();
        SwapPosition(true);
    }

    internal void SwapPosition(bool mode){
        update = mode;
        if(mode){
            Camera.main.transform.position = new Vector3(-16.4f, -1.7f, -10);

            //Camera.main.transform.position = new Vector3(-10, -10, -10);//this.transform.position;
            Camera.main.orthographicSize = 4.49f;
            ToggleActive(false);
        }else{
            Camera.main.transform.position = CutsceneController._currentPosition;
            Camera.main.orthographicSize = 1.3f;
            ToggleActive(true);
        }
    }

    void ToggleActive(bool mode){
        foreach(GameObject go in listOcultos){
            go.SetActive(mode);
        }
    }

    public virtual void Finish(){
        SwapPosition(false);
    }


    void Update(){
        if(!update) return;
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Fire1")){
            StopAllCoroutines();
            if(IsComplete){
            	if(index >= max-1){
                    Finish();
                }else{
                    Step();
    		    }
            }else{
                TextinLine = _currentScene.Text;
            }
        }
    }

    void Step(){
        index++;
        _currentScene = _listCustscene[index];
        TextinLine = "";
        StartCoroutine(Writing());
        spriteRenderer.sprite = Resources.Load<Sprite>("Cutscenes/"+name+"/"+_currentScene.Id);
    }

    IEnumerator Writing(){
    	string text = _currentScene.Text;
        int i =0;
        while(!IsComplete){ 
            TextinLine += text[i];
            i++;
            yield return new WaitForSeconds(delay);
        }
    }

    void OnGUI(){
        if(!update) return;
    	if(index == -1 || index > max) return;
		string title = _currentScene.Title;
        CutscenesType type = _currentScene.Type;
        int width = Camera.main.pixelWidth;
        int height = Camera.main.pixelHeight;   
        Rect gui;
        if(title != ""){
            float widthText = LengthMessage(title) + 15;
            if(type == CutscenesType.Dialog){
                gui = new Rect(15,height-110,widthText,25);
            }else{
                gui = new Rect(15,height-40,widthText,25);    
            }
            GUI.DrawTexture(gui,RectangleTexture);
            gui.x += 12;
            gui.y += 1;
            GUILayout.BeginArea(gui);
            GUILayout.Label(title);
            GUILayout.EndArea();            
        }

        if(type == CutscenesType.Dialog){
            gui = new Rect(30,height-80,width-60,70);
            GUI.DrawTexture(gui,RectangleTexture);
            gui.x += 10;
            gui.width -= 20;
            gui.y += 10;
            gui.height -= 20;
            GUILayout.BeginArea(gui);
            GUILayout.Label(TextinLine);
            GUILayout.EndArea();
        }
    }

     float LengthMessage(string message){
        GUIStyle style = GUI.skin.box;
        return style.CalcSize( new GUIContent(message) ).x;
     }
}
