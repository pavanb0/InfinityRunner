using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject obstacle;
    public Transform spawnpoint;
    public TextMeshProUGUI scoretext;
    public GameObject gameButton;
    public GameObject player;
    public int score = 0 ;
    void Start()
    {
        GameStart();
    }

    IEnumerator SpawnObstacle()
    {
        while (true)
        {
            float wait = Random.Range(1.0f, 2.0f);
            yield return new WaitForSeconds(wait);
            Instantiate(obstacle,spawnpoint.position,Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScoreUp()
    {
        score++;
        scoretext.text = score.ToString();
    }
    public void GameStart()
    {
        player.SetActive(true);
        gameButton.SetActive(false);
        StartCoroutine("SpawnObstacle");
        InvokeRepeating("ScoreUp",2f,1f);
    }

}
