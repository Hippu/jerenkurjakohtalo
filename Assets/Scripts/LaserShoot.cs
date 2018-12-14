using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShoot : MonoBehaviour
{

    public List<AudioClip> laserClips;
    public Transform leftEye;
    public Transform rightEye;
    public GameObject laserPrefab;
    private Transform _currentEye;

    private float lastShot;
    public float ShootInterval;

    // Use this for initialization
    void Start()
    {
        lastShot = Time.time;
        _currentEye = leftEye;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastShot + ShootInterval) ShootAtRandom();
    }

    void ShootAtRandom()
    {
        lastShot = Time.time;
        if (_currentEye == leftEye) _currentEye = rightEye;
        else _currentEye = leftEye;

        var shotLaser = Instantiate(laserPrefab, _currentEye.position, _currentEye.rotation);
        var rotation = Quaternion.Euler(0f, 0f, Random.Range(-45f, 45f));
        shotLaser.transform.rotation = rotation;


        var audio = shotLaser.GetComponent<AudioSource>();

        audio.clip =
            laserClips[Random.Range(0, laserClips.Count)];
        audio.Play();
    }
}
