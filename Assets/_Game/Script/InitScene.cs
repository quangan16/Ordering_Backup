using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class InitScene : MonoBehaviour
{

    [SerializeField] private Canvas canvasLoading;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
        canvasLoading.gameObject.SetActive(false);
    }
}