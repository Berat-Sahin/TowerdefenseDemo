using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{

    [SerializeField] private Transform projectileSpawnPosition;

     private ObjectPooler _pooler;
     private Projectile _currentProjectileLoaded;
     private Turret _turret;
     [SerializeField] private float maxDamage = 10f;
     [SerializeField] private float minDamage = 50f;
     
     public float Damage { get; set; }
     
     [SerializeField] private float delayBtwnAttacks = 2f;


     
     private float _nextAttackTime;


    // Start is called before the first frame update
    void Start()
    {
        _pooler=GetComponent<ObjectPooler>();
        _turret=GetComponent<Turret>();
        
        Damage = (int) UnityEngine.Random.Range(minDamage,maxDamage);
        LoadProjectile();


        
    }

    // Update is called once per frame
    void Update()
    {
                 if(IsTurretEmpty()){
             LoadProjectile();
         }

        

         if(Time.time > _nextAttackTime ){

              if(_turret.CurrentEnemyTarget != null && _currentProjectileLoaded != null && _turret.CurrentEnemyTarget.EnemyHealth.CurrentHealth >0f ){

             _currentProjectileLoaded.transform.parent = null;
             _currentProjectileLoaded.SetEnemy(_turret.CurrentEnemyTarget);
            

         }

            _nextAttackTime=Time.time+delayBtwnAttacks;

         }
        
    }

       private void LoadProjectile(){
         
         GameObject newInstance = _pooler.GetInstanceFromPool();
         newInstance.transform.localPosition = projectileSpawnPosition.position;
         newInstance.transform.SetParent(projectileSpawnPosition);
         newInstance.SetActive(true);

         _currentProjectileLoaded=newInstance.GetComponent<Projectile>();
         _currentProjectileLoaded.TurretOwner=this;
         _currentProjectileLoaded.Damage = Damage;
        _currentProjectileLoaded.ResetProjectile();


     }

    public void ResetTurretProjectile(){
         _currentProjectileLoaded = null;

     }

     private bool IsTurretEmpty(){
         return _currentProjectileLoaded == null;
     }


}
