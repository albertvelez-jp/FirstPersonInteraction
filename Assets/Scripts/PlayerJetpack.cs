using NUnit.Framework;
using System.Collections;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class PlayerJetpack : MonoBehaviour
{
    public Rigidbody rb;
    public Image fuelimage;
    public PlayerMovement playerMovementScript;

    public float jetpackforce;
    public float fuelDuration;
    public float rechargedDelay;
    public float rechargeDuration;
    public float fuel;
    public bool usingjetpack;
    public bool rechargingjetpack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        fuelimage.fillAmount = fuel;
        if(playerMovementScript.isGrounded == false && Input.GetKey(KeyCode.Space) && fuel > 0f)
        {
            usingjetpack = true;
            rb.AddForce(Vector3.up * jetpackforce, ForceMode.Acceleration);
            fuel -= Time.deltaTime / fuelDuration;
        }
        else
        {
            usingjetpack = false;
        }
        if (playerMovementScript.isGrounded == true && fuel < 1f && rechargingjetpack == false)
        {
            if (fuel <= 0f)
            {
                StartCoroutine(RechargeAfterDelay());
            }
            else
            {
                StartCoroutine(RechargeInstant());
            }
        }
    }
    IEnumerator RechargeInstant()
    {
        rechargingjetpack = true;
        float timer = 0f;
        float startFuel = fuel;

        while (timer < rechargeDuration)
        {
            timer += Time.deltaTime;
            fuel = Mathf.Lerp(startFuel, 1f, timer / rechargeDuration);
            yield return null;
        }

        fuel = 1f;
        rechargingjetpack = false;
    }
    IEnumerator RechargeAfterDelay()
    {
        rechargingjetpack = true;
        yield return new WaitForSeconds(rechargedDelay);
        float timer = 0f;
        float startFuel = fuel;

        while (timer < rechargeDuration)
        {
            timer += Time.deltaTime;
            fuel = Mathf.Lerp(startFuel, 1f, timer / rechargeDuration);
            yield return null;
        }

        fuel = 1f;
        rechargingjetpack = false;
    }
}
