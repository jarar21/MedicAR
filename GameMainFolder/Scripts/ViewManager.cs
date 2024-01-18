using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    public GameObject TiltView;
    public GameObject ViewCanvas;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Set3DView());  
    }

    IEnumerator Set3DView()
    {
        yield return new WaitForSecondsRealtime(3.5f);
        TiltView.SetActive(false);
        ViewCanvas.SetActive(true);
    }
}
