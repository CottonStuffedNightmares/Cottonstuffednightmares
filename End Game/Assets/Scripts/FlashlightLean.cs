using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightLean : MonoBehaviour {

    //flashlight
    public GameObject Flashlight;
    public bool torchSwitchLimit;
    public float FlashlightCooldownTime = 0.4f;
    public bool DevMode = false;
    public float ChargeDropTime = 1.0f;
    public int TorchCharge = 1000;
    bool Torchflat = false;
    public int dimlight = 100;
    public int ChargeDecrementAmount = 1;
    public int ChargeIncrementAmount = 50;
    bool Delay;
    bool ChargeDelay;

    //contextual lean
    public GameObject CameraLeft;
    public GameObject CameraMiddle;
    public GameObject CameraRight;

    private Interactions OwlLight;
    private Owl OwlOn;

    [HideInInspector] public bool TorchActive;

    //ActiveItems
    Items items;

    //PauseMenu


    // Use this for initialization

    void Start()
    {
        torchSwitchLimit = false;
        Torchflat = false;
        Delay = false;
        ChargeDelay = false;
        Flashlight.SetActive(false);
        CameraLeft.SetActive(false);
        CameraRight.SetActive(false);
        CameraMiddle.GetComponent<Camera>().enabled = true;
        Time.timeScale = 1;

        TorchActive = true;

        items = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Items>();
        OwlLight = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Interactions>();
        OwlOn = GameObject.FindGameObjectWithTag("Owl").GetComponent<Owl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Flashlight.activeSelf)
        {
            OwlLight.TorchLine();
        }

        //flashlight
        {
             if (Flashlight.activeSelf == true && Delay == false)
             {
                 StartCoroutine(ChargeDecrement());
             }

             if (Input.GetKey(KeyCode.R) && Flashlight.activeSelf == false && ChargeDelay == false)
             {
                 StartCoroutine(ChargeIncrement());
             }

             if (TorchCharge <= dimlight)
             {
                 Flashlight.GetComponentInChildren<Light>().intensity = 0.2f;
             }

             if (TorchCharge < 0)
             {
                 Torchflat = true;
                 TorchCharge = 0;
                 //Debug.Log("flashlight is flat");
             }

             if (TorchCharge > 1000)
             {
                 Torchflat = false;
                 TorchCharge = 1000;
                 Flashlight.GetComponentInChildren<Light>().intensity = 0.5f;
                 //Debug.Log("torch is Fully charged");
             }


             if (Torchflat == false)
             {
                 if (torchSwitchLimit == false)
                 {
                     if (Input.GetKey(KeyCode.F) && Flashlight.activeSelf == false)
                     {
                         torchSwitchLimit = true;
                         Flashlight.SetActive(true);
                         StartCoroutine(FlashlightCooldown());
                         TorchActive = true;
                         //if (Flashlight.activeInHierarchy && Torchflat == false)
                         //    Debug.Log("Flashlight on");
                     }

                     else if (Input.GetKey(KeyCode.F) && Flashlight.activeSelf == true)
                     {
                         torchSwitchLimit = true;
                         Flashlight.SetActive(false);
                         StartCoroutine(FlashlightCooldown());
                        TorchActive = false;
                         //Debug.Log("Flashlight off");
                     }

                 }
             }

             if (Torchflat == true)
             {
                 Flashlight.SetActive(false);
             }
            

        }

        //camera lean
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                CameraMiddle.GetComponent<Camera>().enabled = false;
                CameraLeft.SetActive(true);
                Flashlight.transform.position = CameraLeft.transform.position;
            }

            else if (Input.GetKeyDown(KeyCode.B))
            {
                CameraMiddle.GetComponent<Camera>().enabled = false;
                CameraRight.SetActive(true);
                Flashlight.transform.position = CameraRight.transform.position;
            }

            else if (Input.GetKeyUp(KeyCode.V) || Input.GetKeyUp(KeyCode.B))
            {
                CameraLeft.SetActive(false);
                CameraRight.SetActive(false);
                CameraMiddle.GetComponent<Camera>().enabled = true;
                Flashlight.transform.eulerAngles = CameraMiddle.transform.eulerAngles;
                Flashlight.transform.position = CameraMiddle.transform.position;
            }
        }
    }


    private IEnumerator FlashlightCooldown()
    {
        yield return new WaitForSeconds(FlashlightCooldownTime);
        torchSwitchLimit = false;
    }

    public IEnumerator ChargeDecrement()
    {
        Delay = true;
        if (DevMode == true)
        {

        }

        else if (DevMode == false)
        {
            TorchCharge = TorchCharge - ChargeDecrementAmount;
            yield return new WaitForSeconds(ChargeDropTime);
        }
        Delay = false;
    }

    public IEnumerator ChargeIncrement()
    {
        ChargeDelay = true;
        if (DevMode == true)
        {

        }

        else if (DevMode == false)
        {
            TorchCharge = TorchCharge + ChargeIncrementAmount;
            yield return new WaitForSeconds(ChargeDropTime / 5);
        }
        ChargeDelay = false;
    }
}

//public void DeathCam(GameObject go)
//{
//    transform.LookAt(go.transform);

//    // stop player look/move inputs ?
//}
