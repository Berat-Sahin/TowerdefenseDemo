using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{

    [SerializeField] GameObject button;
    [SerializeField] TextMeshProUGUI ScoreText ;
    
    private int _score;
    public static UIManager Instance;
    private void Awake(){
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        _score=0;
        ScoreText.text = "Score: "+ _score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAgain(){
        SceneManager.LoadScene("GameScene");
        Time.timeScale=1;
    }

    private void setPlayAgain(Enemy enemy){
        
        button.SetActive(true);
        Time.timeScale=0;
        
    }
    private void updateScore(Enemy enemy){
        
        _score++;
        ScoreText.text = "Score: "+ _score.ToString();
        
    }




      private void OnEnable()
    {
        EnemyHealth.OnEnemyKilled += updateScore;
        Enemy.OnEndReached += setPlayAgain;
        
    
    }

    private void OnDisable()
    {

        EnemyHealth.OnEnemyKilled -= updateScore;
        Enemy.OnEndReached -= setPlayAgain;

    }

    

}
