using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicForever : MonoBehaviour
{
    public static MusicForever Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
