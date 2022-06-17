using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawnManager : MonoBehaviour
{

    [SerializeField] private GameObject turretPrefab;
    private Waypoint _waypoint;
    private int[] choosen;
    private  List<Vector3> pointPositions;
    
    public void SpawnTurret(){

        if(pointPositions.Count==0){

            return;
        }


        GameObject turretInstance = Instantiate(turretPrefab);
        int randomIndex = Random.Range(0,pointPositions.Count-1);
        turretInstance.transform.localPosition = pointPositions[randomIndex];
        pointPositions.RemoveAt(randomIndex);
        
        
    }



    // Start is called before the first frame update
    void Start()
    {
        _waypoint = GetComponent<Waypoint>();
        pointPositions = new List<Vector3>(_waypoint.Points);
        
    }

    


}
