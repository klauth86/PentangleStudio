﻿using UnityEngine;

public class Edge : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameSingleton.Singleton.GetSceneLoader().LoadScore();
    }
}