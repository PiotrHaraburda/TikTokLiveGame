using System.Collections;
using System.Collections.Generic;
using TikTokLiveUnity.Example;
using UnityEngine;
using UnityEngine.Serialization;

public class Team2Spawner : MonoBehaviour
{
    public GameObject particleSpawn;
    public GameObject particleSpawn1;
    public GameObject team2PlayerPrefab;
    public GameObject team2BossPrefab;
    public AudioClip soundEffect1;
    
    public static Vector3 Team1Target1Pos = new Vector3(2.75999999f,2.8599975586f,4.4699993f);
    public static Vector3 Team1Target2Pos = new Vector3(8.71000004f,2.8810012817f,4.8412762f);
    public static Vector3 Team1Target3Pos = new Vector3(13.0960083f,2.8499992371f,4.557189f);
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
        currentTarget1Health = maxTargetHealth;
        currentTarget2Health = maxTargetHealth;
        currentTarget3Health = maxTargetHealth;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F4)||GiftRow.newGymSent)
        {
            GiftRow.newGymSent = false;
            Vector3 randomSpawnPos = new Vector3(Random.Range(4, 14), 3, Random.Range(-20, -18));
            objectInstance=Instantiate(team2PlayerPrefab, randomSpawnPos, Quaternion.identity);
            maxHealth = 400f;
            PlayerMoveHandler2 playerMoveHandler = objectInstance.AddComponent<PlayerMoveHandler2>();
            Instantiate(particleSpawn, randomSpawnPos, transform.rotation);
            
            AudioSource.PlayClipAtPoint(soundEffect1,new Vector3(8.5560236f,31.4699993f,0.9212761f));
        }
        
        if (Input.GetKeyDown(KeyCode.F2)||GiftRow.newLollipopSent)
        {
            GiftRow.newLollipopSent = false;
            Vector3 randomSpawnPos = new Vector3(Random.Range(4, 14), 3, Random.Range(-20, -18));
            objectInstance=Instantiate(team2BossPrefab, randomSpawnPos, Quaternion.identity);
            maxHealth = 1500f;
            PlayerMoveHandler2 playerMoveHandler = objectInstance.AddComponent<PlayerMoveHandler2>();
            Instantiate(particleSpawn1, randomSpawnPos, transform.rotation);
            Instantiate(particleSpawn1, randomSpawnPos, transform.rotation);
            Instantiate(particleSpawn1, randomSpawnPos, transform.rotation);
            
            AudioSource.PlayClipAtPoint(soundEffect1,new Vector3(8.5560236f,31.4699993f,-20.9212761f));
        }
    }
}
