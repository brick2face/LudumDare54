using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElectricSpawner : MonoBehaviour
{
    public static ElectricSpawner instance;
    public GameObject[] SpawnPoints;
    public GameObject ElectroBall;
    public float StartTimeBetweenSpawns;
    public float TimeBetweenSpawns;
    public float AmountOfCharge = 0;
    public float MaxCharge = 100;
    public Slider ChargeSlider;
    public int ChargePerBar;
    void Start()
    {
        instance = this;
        ChargeSlider.maxValue = MaxCharge;
        ChargeSlider.value = AmountOfCharge;

    }
    void Update()
    {
        SpawnElectroballs();

    }

    private void SpawnElectroballs()
    {
        if (TimeBetweenSpawns <= 0)
        {
            int RandomSpawnpoint = Random.Range(0, SpawnPoints.Length);
            Instantiate(ElectroBall, SpawnPoints[RandomSpawnpoint].transform.position, Quaternion.identity);
            TimeBetweenSpawns = StartTimeBetweenSpawns;
        }
        else
        {
            TimeBetweenSpawns -= Time.deltaTime;
        }
    }
    public void ChargeBar()
    {
        AmountOfCharge += ChargePerBar;
        ChargeSlider.value = AmountOfCharge;
    }
    public void UnChargeBar()
    {
        AmountOfCharge -= 30;
        if (AmountOfCharge <0)
        {
            AmountOfCharge = 0;
        }
            ChargeSlider.value = AmountOfCharge;
        
    }
}
