using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace TikTokLiveUnity.Example
{
    public class PlayerMoveHandler2:MonoBehaviour
    {
        private Vector3 Team1Target1Pos=Team2Spawner.Team1Target1Pos;
        private Vector3 Team1Target2Pos=Team2Spawner.Team1Target2Pos;
        private Vector3 Team1Target3Pos=Team2Spawner.Team1Target3Pos;
        private List<Vector3> Team1Targets=new List<Vector3>();
        private int target;
        private Image Team2PlayerHealthbar;
        /*private Image Team1Target1Healthbar;
        private Image Team1Target2Healthbar;
        private Image Team1Target3Healthbar;*/
        private Image Team1Target1Healthbar;
        private Image Team1Target2Healthbar;
        private Image Team1Target3Healthbar;
        private Image enemyHealthbar;
        private float maxHealth = Team2Spawner.maxHealth;
        private float maxTargetHealth = 3000f;
        private float currentHealth;
        private Rigidbody rb;
        private float currentEnemyHealth;

        private GameObject Team1Target1;
        private GameObject Team1Target2;
        private GameObject Team1Target3;

        private GameObject enemyPlayer=null;
        private Vector3 f = new Vector3();
        private Vector3 enemyPlayerPos = new Vector3();


        void Start()
        {
            if (maxHealth == 1500f)
            {
                Team1Target1Pos.y = 3;
                Team1Target2Pos.y = 3;
                Team1Target3Pos.y = 3;
            }
            
            Team1Targets.Add(Team1Target1Pos);
            Team1Targets.Add(Team1Target2Pos);
            Team1Targets.Add(Team1Target3Pos);
            target = Random.Range(0,3);

            Team2PlayerHealthbar = getHealthbar(gameObject);

            Team1Target1 = GameObject.Find("Building_Residential_color02");
            Team1Target2 = GameObject.Find("Building_House_01_color02");
            Team1Target3 = GameObject.Find("Building_Shoes Shop");

            if (Team1Target1 != null)
            {
                Team1Target1Healthbar = getHealthbar(Team1Target1);
                updateHealthbar(Team1Target1Healthbar,maxTargetHealth,Team2Spawner.currentTarget1Health);
            }

            if (Team1Target2 != null)
            {
                Team1Target2Healthbar = getHealthbar(Team1Target2);
                updateHealthbar(Team1Target2Healthbar,maxTargetHealth,Team2Spawner.currentTarget2Health);
            }

            if (Team1Target3 != null)
            {
                Team1Target3Healthbar = getHealthbar(Team1Target3);
                updateHealthbar(Team1Target3Healthbar,maxTargetHealth,Team2Spawner.currentTarget3Health);
            }
            
            rb = gameObject.GetComponent<Rigidbody>();
            
            currentHealth = Team2PlayerHealthbar.fillAmount*maxHealth;
            updateHealthbar(Team2PlayerHealthbar,maxHealth,currentHealth);

        }

        public static Image getHealthbar(GameObject g)
        {
            GameObject HealthbarCanvas = g.transform.GetChild(0).gameObject;
            GameObject Background = HealthbarCanvas.transform.GetChild(0).gameObject;
            GameObject Foreground = Background.transform.GetChild(0).gameObject;
            return Foreground.GetComponent<Image>();
        }

        void Update()
        {
            if (Team2Spawner.target1Destroyed&&!Team2Spawner.target2Destroyed&&!Team2Spawner.target3Destroyed)
            {
                if (target == 0)
                {
                    target = 1;
                }
            }
            else if (Team2Spawner.target2Destroyed&&!Team2Spawner.target1Destroyed&&!Team2Spawner.target3Destroyed)
            {
                if (target == 1)
                {
                    target = 2;
                }
            }
            else if (Team2Spawner.target3Destroyed&&!Team2Spawner.target2Destroyed&&!Team2Spawner.target1Destroyed)
            {
                if (target == 2)
                {
                    target = 0;
                }
            }
            else if (Team2Spawner.target1Destroyed&&Team2Spawner.target2Destroyed&&!Team2Spawner.target3Destroyed)
            {
                if (target == 0||target==1)
                {
                    target = 2;
                }
            }
            else if (Team2Spawner.target2Destroyed&&!Team2Spawner.target1Destroyed&&Team2Spawner.target3Destroyed)
            {
                if (target == 1||target==2)
                {
                    target = 0;
                }
            }
            else if (Team2Spawner.target3Destroyed&&!Team2Spawner.target2Destroyed&&Team2Spawner.target1Destroyed)
            {
                if (target == 2||target==0)
                {
                    target = 1;
                }
            }
            else if (Team2Spawner.target3Destroyed && Team2Spawner.target2Destroyed && Team2Spawner.target1Destroyed)
            {
                Destroy(gameObject);
            }
            
            if (enemyPlayer==null)
            {
                f = Team1Targets[target] - rb.transform.position;
                enemyPlayer=GameObject.Find("Team1Player(Clone)");
                if (enemyPlayer == null)
                {
                    enemyPlayer=GameObject.Find("Team1Boss(Clone)");
                }
                if (enemyPlayer != null)
                {
                    enemyHealthbar=getHealthbar(enemyPlayer);
                    currentEnemyHealth = enemyHealthbar.fillAmount * maxHealth;
                }
            }
            else if(enemyPlayer != null)
            {
                enemyPlayerPos = enemyPlayer.transform.position;
                if (maxHealth == 1500f)
                {
                    enemyPlayerPos.y = 3;
                }
                else
                {
                    enemyPlayerPos.y = 3;
                }
                f = enemyPlayerPos - rb.transform.position;
            }
            
            f = f.normalized;
            f = f * 8;
            rb.AddForce(f);
            Quaternion lookRotation = Quaternion.LookRotation(f.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 5f * Time.deltaTime);
        }

        private void OnCollisionStay(Collision col)
        {
            if (col.gameObject.name == "Building_Residential_color02")
            {
                if (currentHealth > 0)
                {
                    currentHealth = Team2PlayerHealthbar.fillAmount * maxHealth;
                    currentHealth -= Random.Range(0.5f, 6f);
                    Team2Spawner.currentTarget1Health -= Random.Range(0.5f, 6f);
                    updateHealthbar(Team2PlayerHealthbar,maxHealth,currentHealth);
                    updateHealthbar(Team1Target1Healthbar,maxTargetHealth,Team2Spawner.currentTarget1Health);
                }
                else
                {
                    Destroy(gameObject);
                }
                
                if (Team2Spawner.currentTarget1Health <= 0)
                {
                    Team1Target1.SetActive(false);
                    Team2Spawner.currentTarget1Health = 3000f;
                    Team2Spawner.target1Destroyed = true;
                }
            }
            else if(col.gameObject.name == "Building_House_01_color02")
            {
                if (currentHealth > 0)
                {
                    currentHealth = Team2PlayerHealthbar.fillAmount * maxHealth;
                    currentHealth -= Random.Range(0.5f, 6f);
                    Team2Spawner.currentTarget2Health -= Random.Range(0.5f, 6f);
                    updateHealthbar(Team2PlayerHealthbar,maxHealth,currentHealth);
                    updateHealthbar(Team1Target2Healthbar,maxTargetHealth,Team2Spawner.currentTarget2Health);
                }
                else
                {
                    Destroy(gameObject);
                }

                if (Team2Spawner.currentTarget2Health <= 0)
                {
                    Team1Target2.SetActive(false);
                    Team2Spawner.currentTarget2Health = 3000f;
                    Team2Spawner.target2Destroyed = true;
                }
                
            }
            else if (col.gameObject.name == "Building_Shoes Shop")
            {
                if (currentHealth > 0)
                {
                    currentHealth = Team2PlayerHealthbar.fillAmount * maxHealth;
                    currentHealth -= Random.Range(0.5f, 6f);
                    Team2Spawner.currentTarget3Health -= Random.Range(0.5f, 6f);
                    updateHealthbar(Team2PlayerHealthbar,maxHealth,currentHealth);
                    updateHealthbar(Team1Target3Healthbar,maxTargetHealth,Team2Spawner.currentTarget3Health);
                }
                else
                {
                    Destroy(gameObject);
                }
                
                if (Team2Spawner.currentTarget3Health <= 0)
                {
                    Team1Target3.SetActive(false);
                    Team2Spawner.currentTarget3Health = 3000f;
                    Team2Spawner.target3Destroyed = true;
                }
            }
            else if (col.gameObject.name == "Natures_Big Tree"||col.gameObject.name == "Natures_Big Tree"
                                                              ||col.gameObject.name == "Vehicle_Police Car"
                                                              ||col.gameObject.name == "Natures_Bush_01"
                                                              ||col.gameObject.name == "Natures_Rock_Big"
                                                              ||col.gameObject.name == "Props_Hydrant"
                                                              ||col.gameObject.name == "Props_Bench_1"
                                                              ||col.gameObject.name == "Props_Traffic cone"
                                                              ||col.gameObject.name == "Props_Bench_2"
                                                              ||col.gameObject.name == "Natures_Fir Tree (1)"
                                                              ||col.gameObject.name == "Natures_Rock_Big"
                                                              ||col.gameObject.name == "Props_Roof Antenna"
                                                              ||col.gameObject.name == "Vehicle_Ambulance"
                                                              ||col.gameObject.name == "Props_Coffee shop chair"
                                                              ||col.gameObject.name == "Natures_Rock_Big (1)"
                                                              ||col.gameObject.name == "Natures_Rock_Big (2)")
            {
                if (Vector3.Distance(rb.velocity,Vector3.zero)<0.1)
                {
                    if (rb.transform.position.x > 8&&rb.transform.position.x <=14)
                    {
                        rb.AddForce(Vector3.right*15,ForceMode.Impulse);
                        rb.AddForce(Vector3.forward*10,ForceMode.Impulse);
                    }
                    else if (rb.transform.position.x < 2)
                    {
                        rb.AddForce(Vector3.right*15,ForceMode.Impulse);
                        rb.AddForce(Vector3.forward*10,ForceMode.Impulse);
                    }
                    else if (rb.transform.position.x > 14)
                    {
                        rb.AddForce(Vector3.left*15,ForceMode.Impulse);
                        rb.AddForce(Vector3.forward*10,ForceMode.Impulse);
                    }
                    else if(rb.transform.position.x <=8&&rb.transform.position.x >=2)
                    {
                        rb.AddForce(Vector3.left*15,ForceMode.Impulse);
                        rb.AddForce(Vector3.forward*10,ForceMode.Impulse);
                    }
                }
            }
            else if (col.gameObject.name == "Team1Player(Clone)")
            {
                if (currentHealth > 0)
                {
                    currentHealth = Team2PlayerHealthbar.fillAmount * maxHealth;
                    currentEnemyHealth = enemyHealthbar.fillAmount * 400f;
                    currentHealth -= Random.Range(0.5f, 6f);
                    currentEnemyHealth -= Random.Range(0.5f, 6f);
                    updateHealthbar(Team2PlayerHealthbar,maxHealth,currentHealth);
                    updateHealthbar(enemyHealthbar,400f,currentEnemyHealth);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            else if (col.gameObject.name == "Team1Boss(Clone)")
            {
                if (currentHealth > 0)
                {
                    currentHealth = Team2PlayerHealthbar.fillAmount * maxHealth;
                    currentEnemyHealth = enemyHealthbar.fillAmount * 1500f;
                    currentHealth -= Random.Range(0.5f, 6f);
                    currentEnemyHealth -= Random.Range(0.5f, 6f);
                    updateHealthbar(Team2PlayerHealthbar,maxHealth,currentHealth);
                    updateHealthbar(enemyHealthbar,1500f,currentEnemyHealth);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }

        void updateHealthbar(Image healthbar,float maxHealth,float currentHealth)
        {
            healthbar.fillAmount = currentHealth / maxHealth;
        }
    }
}