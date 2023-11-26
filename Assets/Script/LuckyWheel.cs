using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LuckyWheel : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] private int numOfitems = 0;
    public Items[] items;
    public GameObject rewardPanel;
    public TextMeshProUGUI rewardText;
    public TMP_InputField valueInput;

    public Transform Pin;
    public float Radius;
    public LayerMask detectLayers;

    private float t;
    public bool isSpinning = false;
    public float SpinPower;
    public float StopPower;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rewardPanel.SetActive(false);
    }


    private void Update()
    {
        items = GetComponentsInChildren<Items>();

        if (rb.angularVelocity > 0)
        {
            rb.angularVelocity -= StopPower * Time.deltaTime;

            rb.angularVelocity = Mathf.Clamp(rb.angularVelocity, 0, 1440);
        }

        if (rb.angularVelocity == 0 && isSpinning)
        {
            t += 1 * Time.deltaTime;

            if (t >= 0.5f)
            {
                DetectItems();

                rewardPanel.SetActive(true);
                isSpinning = false;
                t = 0;
            }
        }
    }

    public void Spin()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (!isSpinning)
            {
                SpinPower = Random.Range(250, 280);
                StopPower = Random.Range(10, 20);

                rb.AddTorque(SpinPower);
                isSpinning = true;
            }
        }
    }


    public void DetectItems()
    {
        Collider2D[] items = Physics2D.OverlapCircleAll(Pin.position, Radius, detectLayers);

        foreach (Collider2D item in items)
        {
            if(!item.GetComponent<Items>().hasValue)
            {
                rewardText.text = "Don't have anything";
            }
            else
            {
                rewardText.text = item.GetComponent<Items>().text;
            }
            
            Debug.Log(item.GetComponent<Items>().text);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(Pin.position, Radius);
    }

    public void SetValueInItems()
    {
        if (string.IsNullOrEmpty(valueInput.text))
        {
            return;
        }
        else
        { 
            if (!items[numOfitems].hasValue && numOfitems < 8)
            {
                items[numOfitems].text = valueInput.text;
                items[numOfitems].hasValue = true;
                numOfitems++;
            }
        }
    }

    public void ClearValue()
    {
        numOfitems = 0;
        valueInput.text = null;

        for (int i = 0; i < items.Length; i++)
        {
            items[i].text = "Input Value";
            items[i].hasValue = false;
        }
    }

}
