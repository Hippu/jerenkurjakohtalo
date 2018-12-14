using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class Speech : MonoBehaviour
{
    [TextArea]
    public string dialogue;
    public float changeLineWait;
    private List<string> _lines;
    private int _lineIndex = 0;
    private float lastUpdated;

    // Use this for initialization
    void Start()
    {
        _lines = dialogue.Split('\n').ToList();
        lastUpdated = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastUpdated + changeLineWait < Time.time)
        {
            var text = GetComponent<TextMeshPro>();
            text.SetText(_lines[_lineIndex]);
            _lineIndex = (_lineIndex + 1 % (_lines.Count - 1));
            lastUpdated = Time.time;
        }
    }
}
