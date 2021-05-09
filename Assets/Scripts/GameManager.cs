using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    private Camera _camera;
    public List<GameObject> firstFences = new List<GameObject>();
    public List<GameObject> secondFences = new List<GameObject>();
    public List<GameObject> firstScrews = new List<GameObject>();
    public List<GameObject> secondScrew = new List<GameObject>();
    private Vector3 targetCameraPosition;
    

    private void OnValidate()
    {
        if (_camera == null)
            _camera = Camera.main;
    }

    private void Start()
    {
        targetCameraPosition = new Vector3(-3, 5, -10);
        foreach (var VARIABLE in secondFences)
        {
          VARIABLE.GetComponent<Collider>().enabled = false;
        }
    }

    public void OnClick()
    {
        //SceneManager.LoadScene("SampleScene");
        //canvas.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        
        if (Fence.count == firstFences.Count)
        {
            foreach (var VARIABLE in firstScrews)
            {
                VARIABLE.SetActive(true);
            }
        }
        if (Screw.count + Fence.count-3 == firstScrews.Count)
        {
            _camera.transform.position = Vector3.Lerp(_camera.transform.position,targetCameraPosition,Time.deltaTime);
            _camera.transform.rotation = Quaternion.Lerp(_camera.transform.rotation,Quaternion.Euler(0,0,-90),Time.deltaTime);
            foreach (var VARIABLE in secondFences)
            {
                VARIABLE.GetComponent<Collider>().enabled = true;
            }
        }

        if (Fence.count == 7)
        {
            foreach (var VARIABLE in secondScrew)
            {
                VARIABLE.SetActive(true);
            }
        }

        if (Fence.count + Screw.count == 12)
        {
            canvas.gameObject.SetActive(true);
        }
        
    }
}
