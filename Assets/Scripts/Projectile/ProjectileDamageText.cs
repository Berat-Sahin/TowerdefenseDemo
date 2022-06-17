using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProjectileDamageText : MonoBehaviour
{

    public TextMeshProUGUI DamageText => GetComponentInChildren<TextMeshProUGUI>();


    public void ReturnTextToPool()
    {
        transform.SetParent(null);
        ObjectPooler.ReturnToPool(gameObject);
    }
}
