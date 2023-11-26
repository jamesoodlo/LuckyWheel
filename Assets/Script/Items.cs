using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class Items : MonoBehaviour
{
    public bool hasValue;
    public string text;
    public TextMeshProUGUI itemText;
    public float Radius;

    void Update()
    {
        itemText.text = text;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
