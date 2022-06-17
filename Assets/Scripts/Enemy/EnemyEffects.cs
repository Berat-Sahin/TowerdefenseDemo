using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyEffects : MonoBehaviour
{

    [SerializeField] private Transform textDamageSpawnPosition;
    // Start is called before the first frame update

    private Enemy _enemy;

    void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

  

    private void EnemyHit(Enemy enemy, float damage)
    {
        if (_enemy == enemy)
        {
            GameObject newInstance = DamageTextManager.Instance.Pooler.GetInstanceFromPool();
            TextMeshProUGUI damageText = newInstance.GetComponent<ProjectileDamageText>().DamageText;
            damageText.text = damage.ToString();
            
            newInstance.transform.SetParent(textDamageSpawnPosition);
            newInstance.transform.position = textDamageSpawnPosition.position;
            newInstance.SetActive(true);
        }
    }

        private void OnEnable()
    {
        Projectile.OnEnemyHit += EnemyHit;
    }

    private void OnDisable()
    {
        Projectile.OnEnemyHit -= EnemyHit;
    }

    
}
