using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public GUIText scoreText;
    private int score;
    void Start()
    {
        score = 0;
        updateScore();
        StartCoroutine (SpawnWaves());
    }
    void updateScore()
    {
        scoreText.text = "Score: " + score;
    }
    public void addScore(int newScoreVaule)
    {
        score += newScoreVaule;
        updateScore();
    }

 
        IEnumerator SpawnWaves()
        {
            while (true)
            {
                for (int i = 0; i < hazardCount; i++)
                {
                    Vector3 spawnPostion = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(hazard, spawnPostion, spawnRotation);
                    yield return new WaitForSeconds(spawnWait);
                }
                yield return new WaitForSeconds(spawnWait);
            }       
        }
}



