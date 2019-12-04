using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abyss : MonoBehaviour
{
    private GameManager gameManager = GameManager.instance;
    public GameObject FPS;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.tag.Equals("Player"))
        //{
        gameManager.LoadGameOver();
        //}
    }
}
