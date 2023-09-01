using System.Collections;
using System.Collections.Generic;
using TikTokLiveUnity.Example;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Team1Spawner : MonoBehaviour
{
    public GameObject particleSpawn;
    public GameObject particleSpawn1;
    public GameObject team1PlayerPrefab;
    public GameObject team1BossPrefab;
    public AudioClip soundEffect1;
    
    public static Vector3 Team2Target1Pos = new Vector3(2.75999999f,2.8599975586f,-20.4699993f);
    public static Vector3 Team2Target2Pos = new Vector3(8.71000004f,2.8810012817f,-23.8412762f);
    public static Vector3 Team2Target3Pos = new Vector3(13.0960083f,2.8499992371f,-22.557189f);
    GameObject objectInstance;
    private float maxTargetHealth = 3000f;
    public static float currentTarget1Health=3000f;
    public static float currentTarget2Health=3000f;
    public static float currentTarget3Health=3000f;

    public static float maxHealth;

    
    public static bool target1Destroyed = false;
    public static bool target2Destroyed = false;
    public static bool target3Destroyed = false;

    void Start()
    {
        AudioSource musicAudioSource = transform.GetComponent<AudioSource>();
        musicAudioSource.Play();
        currentTarget1Health = maxTargetHealth;
        currentTarget2Health = maxTargetHealth;
        currentTarget3Health = maxTargetHealth;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3)||GiftRow.newRoseSent)
        {
            GiftRow.newRoseSent = false;
            Vector3 randomSpawnPos = new Vector3(Random.Range(4, 14), 3, Random.Range(0, 2));
            objectInstance=Instantiate(team1PlayerPrefab, randomSpawnPos, Quaternion.Euler(0, -180, 0));
            maxHealth = 400f;
            PlayerMoveHandler playerMoveHandler = objectInstance.AddComponent<PlayerMoveHandler>();
            Instantiate(particleSpawn, randomSpawnPos, transform.rotation);
            
            AudioSource.PlayClipAtPoint(soundEffect1,new Vector3(8.5560236f,31.4699993f,60.9212761f));
            
        }

        if (Input.GetKeyDown(KeyCode.F1)||GiftRow.newPerfumeSent)
        {
            GiftRow.newPerfumeSent = false;
            Vector3 randomSpawnPos = new Vector3(Random.Range(4, 14), 6, Random.Range(0, 2));
            objectInstance=Instantiate(team1BossPrefab, randomSpawnPos, Quaternion.Euler(0, -180, 0));
            maxHealth = 1500f;
            PlayerMoveHandler playerMoveHandler = objectInstance.AddComponent<PlayerMoveHandler>();
            Instantiate(particleSpawn1, randomSpawnPos, transform.rotation);
            Instantiate(particleSpawn1, randomSpawnPos, transform.rotation);
            Instantiate(particleSpawn1, randomSpawnPos, transform.rotation);
            
            AudioSource.PlayClipAtPoint(soundEffect1,new Vector3(8.5560236f,31.4699993f,20.9212761f));
        }
    }
}
