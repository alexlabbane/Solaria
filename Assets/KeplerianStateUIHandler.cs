using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class KeplerianStateUIHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public void changeOrbitalInputs()
    {
        System.Random test = new System.Random();
        Dropdown dropdown = GameObject.Find("DefineOrbitDropdown").GetComponent<Dropdown>();
        GameObject keplerianInput = GameObject.Find("KeplerianElementInput");
        GameObject cartesianInput = GameObject.Find("CartesianElementInput");

        if(dropdown.value == 0)
        {
            //keplerianInput.gameObject.SetActive(true);
            cartesianInput.gameObject.SetActive(false);
            Debug.Log(dropdown.value);
        }

        if(dropdown.value == 1)
        {
            //cartesianInput.SetActive(true);
            keplerianInput.SetActive(false);
            Debug.Log(dropdown.value);
        }
    }
}
