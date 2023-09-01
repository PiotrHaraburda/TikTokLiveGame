using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace TikTokLiveUnity.Example
{
    public class PlayerMoveHandler:MonoBehaviour
    {
        private Vector3 Team2Target1Pos=Team1Spawner.Team2Target1Pos;
        private Vector3 Team2Target2Pos=Team1Spawner.Team2Target2Pos;
        private Vector3 Team2Target3Pos=Team1Spawner.Team2Target3Pos;
        private List<Vector3> Team2Targets=new List<Vector3>();
        private int target;
        private Image Team1PlayerHealthbar;
        /*private Image Team1Target1Healthbar;
        private Image Team1Target2Healthbar;
        private Image Team1Target3Healthbar;*/
        private Image Team2Target1Healthbar;
        private Image Team2Target2Healthbar;
        private Image Team2Target3Healthbar;
        private Image enemyHealthbar;
        private float maxHealth = Team1Spawner.maxHealth;
        private float maxTargetHealth = 3000f;
        private float currentHealth;
        private float currentEnemyHealth;
        private Rigidbody rb;

        private GameObject Team2Target1;
        private GameObject Team2Target2;
        private GameObject Team2Target3;
        
        private GameObject enemyPlayer=null;
        private Vector3 f = new Vector3();
        private Vector3 enemyPlayerPos = new Vector3();

        void Start()
        {
            if (maxHealth == 1500f)
            {
                Team2Target1Pos.y = 5;
                Team2Target2Pos.y = 5;
                Team2Target3Pos.y = 5;
            }
            Team2Targets.Add(Team2Target1Pos);
            Team2Targets.Add(Team2Target2Pos);
            Team2Targets.Add(Team2Target3Pos);
            target = Random.Range(0,3);

            Team1PlayerHealthbar = getHealthbar(gameObject);

            Team2Target1 = GameObject.Find("Building_Stadium");
            Team2Target2 = GameObject.Find("Building_House_03_color02");
            Team2Target3 = GameObject.Find("Building_Gas Station");

            if (Team2Target1 != null)
            {
                Team2Target1Healthbar = getHealthbar(Team2Target1);
                updateHealthbar(Team2Target1Healthbar,maxTargetHealth,Team1Spawner.currentTarget1Health);
            }

            if (Team2Target2 != null)
            {
                Team2Target2Healthbar = getHealthbar(Team2Target2);
                updateHealthbar(Team2Target2Healthbar,maxTargetHealth,Team1Spawner.currentTarget2Health);
            }

            if (Team2Target3 != null)
            {
                Team2Target3Healthbar = getHealthbar(Team2Target3);
                updateHealthbar(Team2Target3Healthbar,maxTargetHealth,Team1Spawner.currentTarget3Health);
            }
            
            rb = gameObject.GetComponent<Rigidbody>();
            
            currentHealth = Team1PlayerHealthbar.fillAmount*maxHealth;
            
            updateHealthbar(Team1PlayerHealthbar,maxHealth,currentHealth);

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
            if (Team1Spawner.target1Destroyed&&!Team1Spawner.target2Destroyed&&!Team1Spawner.target3Destroyed)
            {
                if (target == 0)
                {
                    target = 1;
                }
            }
            else if (Team1Spawner.target2Destroyed&&!Team1Spawner.target1Destroyed&&!Team1Spawner.target3Destroyed)
            {
                if (target == 1)
                {
                    target = 2;
                }
            }
            else if (Team1Spawner.target3Destroyed&&!Team1Spawner.target2Destroyed&&!Team1Spawner.target1Destroyed)
            {
                if (target == 2)
                {
                    target = 0;
                }
            }
            else if (Team1Spawner.target1Destroyed&&Team1Spawner.target2Destroyed&&!Team1Spawner.target3Destroyed)
            {
                if (target == 0||target==1)
                {
                    target = 2;
                }
            }
            else if (Team1Spawner.target2Destroyed&&!Team1Spawner.target1Destroyed&&Team1Spawner.target3Destroyed)
            {
                if (target == 1||target==2)
                {
                    target = 0;
                }
            }
            else if (Team1Spawner.target3Destroyed&&!Team1Spawner.target2Destroyed&&Team1Spawner.target1Destroyed)
            {
                if (target == 2||target==0)
                {
                    target = 1;
                }
            }
            else if (Team1Spawner.target3Destroyed && Team1Spawner.target2Destroyed && Team1Spawner.target1Destroyed)
            {
                Destroy(gameObject);
            }

            if (enemyPlayer==null)
            {
                f = Team2Targets[target] - rb.transform.position;
                enemyPlayer=GameObject.Find("Team2Player(Clone)");
                if (enemyPlayer == null)
                {
                    enemyPlayer=GameObject.Find("Team2Boss(Clone)");
                }
                if (enemyPlayer != null)
                {
                    enemyHealthbar=getHealthbar(enemyPlayer);
                    currentEnemyHealth = enemyHealthbar.fillAmount * maxHealth;
                }
            }
            else
            {
                enemyPlayerPos = enemyPlayer.transform.position;
                if (maxHealth == 1500f)
                {
                    enemyPlayerPos.y = 5;
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
            if (col.gameObject.name == "Building_Stadium")
            {
                if (currentHealth > 0)
                {
                    currentHealth = Team1PlayerHealthbar.fillAmount * maxHealth;
                    currentHealth -= Random.Range(0.5f,6f);
                    Team1Spawner.currentTarget1Health -= Random.Range(0.5f, 6f);
                    updateHealthbar(Team1PlayerHealthbar,maxHealth,currentHealth);
                    updateHealthbar(Team2Target1Healthbar,maxTargetHealth,Team1Spawner.currentTarget1Health);
                }
                else
                {
                    Destroy(gameObject);
                }
                
                if (Team1Spawner.currentTarget1Health <= 0)
                {
                    Team2Target1.SetActive(false);
                    Team1Spawner.currentTarget1Health = 3000f;
                    Team1Spawner.target1Destroyed = true;
                }
            }
            else if(col.gameObject.name == "Building_House_03_color02")
            {
                if (currentHealth > 0)
                {
                    currentHealth = Team1PlayerHealthbar.fillAmount * maxHealth;
                    currentHealth -= Random.Range(0.5f, 6f);
                    Team1Spawner.currentTarget2Health -= Random.Range(0.5f, 6f);
                    updateHealthbar(Team1PlayerHealthbar,maxHealth,currentHealth);
                    updateHealthbar(Team2Target2Healthbar,maxTargetHealth,Team1Spawner.currentTarget2Health);
                }
                else
                {
                    Destroy(gameObject);
                }

                if (Team1Spawner.currentTarget2Health <= 0)
                {
                    Team2Target2.SetActive(false);
                    Team1Spawner.currentTarget2Health = 3000f;
                    Team1Spawner.target2Destroyed = true;
                }
                
            }
            else if (col.gameObject.name == "Building_Gas Station")
            {
                if (currentHealth > 0)
                {
                    currentHealth = Team1PlayerHealthbar.fillAmount * maxHealth;
                    currentHealth -= Random.Range(0.5f, 6f);
                    Team1Spawner.currentTarget3Health -= Random.Range(0.5f, 6f);
                    updateHealthbar(Team1PlayerHealthbar,maxHealth,currentHealth);
                    updateHealthbar(Team2Target3Healthbar,maxTargetHealth,Team1Spawner.currentTarget3Health);
                }
                else
                {
                    Destroy(gameObject);
                }
                
                if (Team1Spawner.currentTarget3Health <= 0)
                {
                    Team2Target3.SetActive(false);
                    Team1Spawner.currentTarget3Health = 3000f;
                    Team1Spawner.target3Destroyed = true;
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
            else if (col.gameObject.name == "Team2Player(Clone)")
            {
                if (currentHealth > 0)
                {
                    currentHealth = Team1PlayerHealthbar.fillAmount * maxHealth;
                    currentEnemyHealth = enemyHealthbar.fillAmount * 400f;
                    currentHealth -= Random.Range(0.5f, 6f);
                    currentEnemyHealth -= Random.Range(0.5f, 6f);
                    updateHealthbar(Team1PlayerHealthbar,maxHealth,currentHealth);
                    updateHealthbar(enemyHealthbar,400f,currentEnemyHealth);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            else if (col.gameObject.name == "Team2Boss(Clone)")
            {
                if (currentHealth > 0)
                {
                    currentHealth = Team1PlayerHealthbar.fillAmount * maxHealth;
                    currentEnemyHealth = enemyHealthbar.fillAmount * 1500f;
                    currentHealth -= Random.Range(0.5f, 6f);
                    currentEnemyHealth -= Random.Range(0.5f, 6f);
                    updateHealthbar(Team1PlayerHealthbar,maxHealth,currentHealth);
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