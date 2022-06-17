using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayAgain : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setPlayAgain(Enemy enemy){
        

        Debug.Log($"selamlar");
        gameObject.SetActive(true);
        
    }

      private void OnEnable()
    {
        Enemy.OnEndReached += setPlayAgain;
 
    }

    private void OnDisable()
    {
        Enemy.OnEndReached -= setPlayAgain;

    }
}
