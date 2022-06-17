using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{

    public static Action<Enemy> OnEndReached;



    [SerializeField] private float moveSpeed = 3f;

    public float MoveSpeed { get; set; }
    public Waypoint Waypoint { get; set; }

    public EnemyHealth EnemyHealth { get; set; }

    public Vector3 CurrentPointPosition => Waypoint.GetWaypointPosition(_currentWaypointIndex);
    

    private int _currentWaypointIndex;
    private Vector3 _lastPointPosition;

    private EnemyHealth _enemyHealth;
   


    // Start is called before the first frame update
    void Start()
    {
        MoveSpeed = moveSpeed;
        _currentWaypointIndex = 0;

        _enemyHealth = GetComponent<EnemyHealth>();
        EnemyHealth = GetComponent<EnemyHealth>();

        _lastPointPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();


        if (CurrentPointPositionReached())
        {
            UpdateCurrentPointIndex();
        }
    }

       private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, 
            CurrentPointPosition, MoveSpeed * Time.deltaTime);
    }

       private bool CurrentPointPositionReached()
    {
        float distanceToNextPointPosition = (transform.position - CurrentPointPosition).magnitude;
        if (distanceToNextPointPosition < 0.1f)
        {
            _lastPointPosition = transform.position;
            return true;
        }

        return false;
    }

        private void UpdateCurrentPointIndex()
    {
        int lastWaypointIndex = Waypoint.Points.Length - 1;
        if (_currentWaypointIndex < lastWaypointIndex)
        {
            _currentWaypointIndex++;
        }
        else
        {
            EndPointReached();
        }
    }

        private void EndPointReached()
    {

        OnEndReached?.Invoke(this);
        _enemyHealth.ResetHealth();
        ObjectPooler.ReturnToPool(gameObject);


     
    }

    public void ResetEnemy()
    {
        _currentWaypointIndex = 0;
    }

}
