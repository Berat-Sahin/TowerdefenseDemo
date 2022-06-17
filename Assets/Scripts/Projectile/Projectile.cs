using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : MonoBehaviour
{
    public static Action<Enemy, float> OnEnemyHit;

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float minDistanceToDealDamage = 0.1f;

    public TurretProjectile TurretOwner { get; set; }
    
    public float Damage { get; set; }

    private Enemy _enemyTarget;

    void Update()
    {
           if(_enemyTarget!=null){
            MoveProjectile();
        }

       
        
    }

    private void MoveProjectile(){

        transform.position=Vector2.MoveTowards(transform.position,_enemyTarget.transform.position,moveSpeed*Time.deltaTime);
        float distanceToTarget= (_enemyTarget.transform.position - transform.position).magnitude;

        if(distanceToTarget < minDistanceToDealDamage ){

            OnEnemyHit?.Invoke(_enemyTarget, Damage);
            _enemyTarget.EnemyHealth.DealDamage(Damage);
            TurretOwner.ResetTurretProjectile();

            ObjectPooler.ReturnToPool(gameObject); 
        }

    }

    // Update is called once per frame


    public void SetEnemy(Enemy enemy){
        
        _enemyTarget=enemy;

    }

    public void ResetProjectile(){
        _enemyTarget=null;
        transform.localRotation = Quaternion.identity;
    }
}
