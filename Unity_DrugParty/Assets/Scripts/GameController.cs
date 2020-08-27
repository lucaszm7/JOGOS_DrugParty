using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{

    static public GameController instance;
    // List sem elementos Duplicados
    HashSet<string> Itens = new HashSet<string>();

	public Sprite bebida;
    public Sprite doce;
    public Sprite beck;
    public Sprite agua;
    private bool isPaused = false;
    public static int part = 1;

    [SerializeField]
    private GameObject imagemGameOver;
    [SerializeField]
    private GameObject Player;
    private Player scriptPlayer;

    void Awake(){ 
        instance = this;
        this.imagemGameOver.SetActive(false);
        scriptPlayer = this.Player.GetComponent<Player>();
    }
     
    void Update()
    {
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
        instance.StartCoroutine(SceneSwitch());
    }

    static IEnumerator SceneSwitch(){
        AsyncOperation load = SceneManager.LoadSceneAsync("Cutscene", LoadSceneMode.Single);
        yield return load;
        SceneManager.UnloadSceneAsync("Main");
    }

    public void addItem(Item itemPass){
        if (Itens.Contains(itemPass.name) == true)
        {
            return;
        }
    	float x = Camera.main.transform.position.x, y = Camera.main.transform.position.y;
        x = x - 3.0f;
        y = y - 1.2f;
        float newItemx = 0.3f;
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