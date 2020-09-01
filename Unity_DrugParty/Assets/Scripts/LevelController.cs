using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelController : MonoBehaviour
{

    // List sem elementos Duplicados
    HashSet<string> Itens = new HashSet<string>();

	public Sprite bebida;
    public Sprite doce;
    public Sprite beck;
    public Sprite agua;
    private bool isPaused = false;
    public static int part = 1;

    //[SerializeField]
    //GameObject Camera;
    [SerializeField]
    GameObject imagemGameOver;
    [SerializeField]
    //GameObject Player;
    Player scriptPlayer;

    [SerializeField]
    Camera mainCamera;

    bool actived = false;

    void Awake(){ 
        this.imagemGameOver.SetActive(false);
        scriptPlayer = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    IEnumerator AnimationCamera2(){
        float n = 1.2f;
        bool soma = true;
        while(true){
            mainCamera.orthographicSize = n;
            yield return new WaitForSeconds(0.01f);
            if(soma)
                n += 0.001f;
            else
                n -= 0.001f;

            if(n > 1.4f)
                soma = false;
            else if(n < 1.2f)
                soma = true;

            if(Mathf.RoundToInt(Random.Range(0f,5000f)) % 50 == 0){
                soma = !soma;
            }
        }
    }
     
    IEnumerator AnimationCamera(){
        float min = mainCamera.transform.localEulerAngles.z;
        float max = 0f;
        float i = 0;
        bool soma = true;
        float n = 1.3f;
        int random = 0;
        while(true){
            mainCamera.transform.localEulerAngles = new Vector3(0,0,i);
            //mainCamera.transform.Rotate(0,0,i,Space.Self);
            yield return new WaitForSeconds(0.001f);
            if(soma)
                i += 0.1f;
            else
                i -= 0.1f;

            if(i > 6f)
                soma = false;
            else if(i < -6f)
                soma = true;

            if(Mathf.RoundToInt(Random.Range(0f,5000f)) % 1000 == 0){
                soma = !soma;
            }
        }
    }

    void Update(){
        if(!actived && Player.bebado){
            actived = true;
            StartCoroutine(AnimationCamera());
            StartCoroutine(AnimationCamera2());
        }
        if(actived && !Player.bebado){
            actived = false;
            StopAllCoroutines();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            changePause();
            OnApplicationPause(isPaused);
        }
        if (isPaused)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                changePause();
                OnApplicationPause(isPaused);
            }
        }
    }

    public static void Finish(){
        part++;
        LoadScene.Load("Cutscene");
    }

    public void addItem(Item itemPass){
        if (Itens.Contains(itemPass.name) == true)
        {
            return;
        }
    	float x = Camera.main.transform.position.x, y = Camera.main.transform.position.y;
        x = x - 1.1f;
        y = y + 1.1f;
        float newItemx = 0.25f;
        x += newItemx * Itens.Count;
        Itens.Add(itemPass.name);

        GameObject newItem = new GameObject("Item X: "+ x +" Y: "+ y);
        newItem.transform.localPosition = new Vector3(x, y, 0);
        newItem.transform.parent = Camera.main.transform;
        //newItem.transform.localScale = new Vector3(2,2,2);
        SpriteRenderer spriteRenderer = newItem.AddComponent<SpriteRenderer>();
        switch (itemPass.name)
        {
            case "Beck":
                spriteRenderer.sprite = beck;
                break;
            case "Agua":
                spriteRenderer.sprite = agua;
                break;
            case "Doce":
                spriteRenderer.sprite = doce;
                break;
            case "Bebida":
                spriteRenderer.sprite = bebida;
                break;
        }
        spriteRenderer.sortingOrder = 30;
    }

    void OnGUI()
    {
        if (isPaused)
            GUI.Label(new Rect(512, 200, 100, 50), " ===========\nGAME PAUSED\n ===========");
    }
    void OnApplicationFocus(bool hasFocus)
    {
        isPaused = !hasFocus;
        OnApplicationPause(isPaused);

    }
    public void changePause()
    {
        this.isPaused = !this.isPaused;
    }
    void OnApplicationPause(bool pause)
    {
        this.imagemGameOver.SetActive(pause);
        scriptPlayer.isPaused = pause;
    }
}