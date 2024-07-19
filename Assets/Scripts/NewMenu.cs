using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class NewMenu : MonoBehaviour
{

    public GameObject panel;


    public void PlayGame(){
        SceneManager.LoadScene("SampleScene");
        //SceneManager.UnloadScene("NewMenu");
    }

    public void OnResume(){
        panel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnMainMenu(){
        Debug.Log("cliccato");
        SceneManager.LoadScene("NewMenu");
        Time.timeScale = 1f;
        //SceneManager.UnloadScene(sceneName: "SampleScene");
    }

    public void OnShop(InputAction.CallbackContext context){
        Debug.Log("cliccato");
        Vector2 attackPosition = transform.position; // Center of the attack
        Vector2 attackSize = new Vector2(10f, 10f); // Size of the rectangle (width x height)
        float attackAngle = 0f; // Angle of the rectangle, if needed
        // Perform the overlap box check
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPosition, attackSize, attackAngle);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Shop") && context.performed)
            {
                canvas_shop.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

}
