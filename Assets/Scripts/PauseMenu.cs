using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject panel;

    // Start is called before the first frame update
    public void Onpause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (panel.activeSelf)
            {
                // Se il pannello è attivo, disattivalo e riprendi il gioco
                panel.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                // Se il pannello è inattivo, attivalo e metti in pausa il gioco
                panel.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
}

