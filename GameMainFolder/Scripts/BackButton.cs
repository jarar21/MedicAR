using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BackButton : MonoBehaviour
{
    public GameObject BackButtonPrefab;

    private void OnEnable()
    {
        BackButtonPrefab.SetActive(true);

    }
    private void OnDisable()
    {
        BackButtonPrefab.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
