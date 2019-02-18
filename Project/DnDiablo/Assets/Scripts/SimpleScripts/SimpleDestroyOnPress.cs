using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDestroyOnPress : MonoBehaviour {

    [SerializeField] private KeyCode key = KeyCode.Keypad6;

    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            Destroy(this.gameObject);
        }
    }
}
