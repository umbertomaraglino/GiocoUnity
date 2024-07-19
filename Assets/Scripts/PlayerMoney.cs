using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoney : MonoBehaviour
{

    public int money;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Money")){
            Destroy(other.gameObject);
            money += 1;
        }
    }
}
