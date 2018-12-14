using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameStart : MonoBehaviour
{

    private PlayableDirector _director;
    public List<GameObject> toBeEnabled;

    // Use this for initialization
    void Start()
    {
        _director = GetComponent<PlayableDirector>();
        _director.stopped += OnIntroStop;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _director.Stop();
        }
    }

    void OnIntroStop(PlayableDirector director)
    {
        foreach (var item in toBeEnabled)
        {
            item.SetActive(true);
        }
        GameObject.Destroy(gameObject);
    }
}
