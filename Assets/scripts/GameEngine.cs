using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameEngine : MonoBehaviour
{
    public GameObject endPoint;
    public GameObject player;

    public float timeToEnd = 60.0f;
    public static float currentTime { get; private set; }

    public GameObject endDialog;

    public int MaxMoney = 10;
    public static int currentMoney { get; private set; }

    public Terrain terrain;

    bool isEndGame;
     
    private List<GameObject> spawnedList;
    public GameObject spawnPrefab;

    public static int GetMoney(){
        return currentMoney;
}
    public static void AddMoney(int count)
    {
        currentMoney+=count;
    }
    public static int GetTime()
    {  
        return (int)(currentTime % 60.0f);
    }

    
    // Start is called before the first frame update
    void Start()
    {
        spawnedList = new List<GameObject>();
        // currentTime = timeToEnd;
        isEndGame = false;
        currentTime = timeToEnd;
        currentMoney = 0;

        Time.timeScale = 1.0f;
        endDialog.SetActive(false);


        RespawnItems();
        RespawnPlayer();
    }

   
    

    // Update is called once per frame
    void Update()
    {
        if (!isEndGame)
        {
            if (currentTime <= 0.0f)
            {
                ShowGameOver(false);
            }
            else
            {
                currentTime -= Time.deltaTime;

            }
        }
       
    }

   public  void ShowGameOver(bool isEndPoint)
    {
        Time.timeScale = 0.0f;
        //currentTime = 0.0f;
        isEndGame = true;
        endDialog.SetActive(true);

       // Debug.Log("ShowGameOver " + isEndPoint.ToString());

        Transform ResultText = endDialog.transform.Find("ResultText");
        if (ResultText)
        {
            ResultText.GetComponent<Text>().text = isEndPoint ? ("Вы собрали " + currentMoney + " из " + MaxMoney + " монет") : "Время вышло";
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void RespawnItems()
    {
        foreach (GameObject go in spawnedList)
        {
            Destroy(go);
        }

        spawnedList = new List<GameObject>();

        for (int i = 0; i < MaxMoney; i++)
        {
            int xPos = Random.Range(5, (int)terrain.terrainData.size.x - 5);
            int zPos = Random.Range(30, (int)terrain.terrainData.size.z - 30);

         
           
            Vector3 vpos = new Vector3(xPos, 0, zPos);

            GameObject go = Instantiate(spawnPrefab, new Vector3(xPos, Terrain.activeTerrain.SampleHeight(vpos)+3.0f, zPos), Quaternion.identity);
            spawnedList.Add(go);
        }
    }

    void RespawnPlayer()
    {
        int zPos = Random.Range(5, 10);
        int xPos = Random.Range(2, (int)terrain.terrainData.size.x - 2);
     
        Vector3 vpos = new Vector3(xPos, 0, zPos);

        player.transform.position = (new Vector3(xPos, Terrain.activeTerrain.SampleHeight(vpos) + 3.0f, zPos));

        zPos = Random.Range((int)terrain.terrainData.size.z - 10, (int)terrain.terrainData.size.z - 5);
        xPos = Random.Range(2, (int)terrain.terrainData.size.x - 2);
        vpos.x = xPos;
        vpos.z = zPos;
       
        endPoint.transform.position = new Vector3(xPos, Terrain.activeTerrain.SampleHeight(vpos) + 5.0f, zPos);
    }
}
