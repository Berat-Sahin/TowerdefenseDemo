using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamageTextManager : MonoBehaviour
{
    public ObjectPooler Pooler { get; set; }

    public static DamageTextManager Instance;

    private void Awake(){
        Instance = this;
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        Pooler = GetComponent<ObjectPooler>();
    }
}
