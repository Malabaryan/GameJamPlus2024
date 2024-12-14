using BNG;
using System.Collections;
using UnityEngine;

public class DeliverBoxBehavior : MonoBehaviour
{
    [Header("Deliver Box Properties")]
    [SerializeField] private SnapZone bouquetSlot;
    [SerializeField] private SeedBehavior.SeedType desiredFlower;
    [SerializeField] private Transform lidTransform;
    [SerializeField] private Transform deliverPoint;
    [SerializeField] private Transform deliverVisualsTransform;
    [SerializeField] private float deliverAnimationLifetime = 6f;
    [SerializeField] private float deliverValidDistance = 8f;
    [SerializeField] private float deliverSpeed = 10f;

    [Header("Deliver Button Materials")]
    [SerializeField] private GameObject buttonMesh;
    [SerializeField] private Material wrongMaterial;
    [SerializeField] private Material correctMaterial;

    [Header("Audio clips")]
    public AudioClip placedCorrectFlower; //Might want to change this with the mission manager later.
    [SerializeField] private AudioClip placedWrongFlower;
    [SerializeField] private AudioClip notCloseToDeliverPoint;
    [SerializeField] private AudioClip teleportSFX;
    public AudioClip triedToDeliverWrongFlower; //Should be good to have some angry grandma noises.
    public AudioClip succesfulDeliver; //Might want to add grandma multiple audio options for this.
    private bool lidClosed = false;
    private bool correctFlower = false;
    private bool shootingDelivery = false;

    void Start()
    {
        buttonMesh.GetComponent<MeshRenderer>().material = wrongMaterial;
        deliverPoint.transform.SetParent(null);
        //shootingDelivery = true;
    }

    void Update()
    {
        if(shootingDelivery)
            ShootDelivery();
    }

    public void UpdateDesiredFlower(SeedBehavior.SeedType newDesiredFlower)
    {
        desiredFlower = newDesiredFlower;
    }
    public void CheckDesiredFlower()
    {
        if (bouquetSlot.HeldItem != null)
        {
            if (desiredFlower == bouquetSlot.HeldItem.transform.GetComponent<BouqueteBehavior>().flowerType)
            {
                Debug.Log("FLOR DESEADA");
                correctFlower = true;
                buttonMesh.GetComponent<MeshRenderer>().material = correctMaterial;
                if(placedCorrectFlower != null)
                    AudioSource.PlayClipAtPoint(placedCorrectFlower, transform.position);
            }
            else
            {
                Debug.Log("FLOR NO DESEADA");
                correctFlower = false;
                buttonMesh.GetComponent<MeshRenderer>().material = wrongMaterial;
                if (placedWrongFlower != null)
                    AudioSource.PlayClipAtPoint(placedWrongFlower, transform.position);
            }
        }
        else
        {
            buttonMesh.GetComponent<MeshRenderer>().material = wrongMaterial;
        }
    }

    public void CloseTheLid()
    {
        lidClosed = !lidClosed;
        lidTransform.localEulerAngles = new Vector3(lidClosed ? 0 : 130f, 0, 0);
        CheckDesiredFlower();

        //If not close enough to the deliver point
        if (Vector3.Distance(transform.position, deliverPoint.transform.position) > deliverValidDistance)
        {
            if(notCloseToDeliverPoint != null)
                AudioSource.PlayClipAtPoint(notCloseToDeliverPoint, transform.position);
            return;
        }

        if (correctFlower)
        {
            //GameObject bouquete = bouquetSlot.HeldItem.gameObject;
            //bouquetSlot.GrabGrabbable(null);
            //Destroy(bouquete);
            bouquetSlot.ReleaseAll();
            deliverVisualsTransform.gameObject.SetActive(true);
            shootingDelivery = true;
            correctFlower = false;
            //Update mission information
            GameObject.Find("MissionManager").GetComponent<MissionManager>().CompleteMission();
            AudioSource.PlayClipAtPoint(teleportSFX, transform.position);
        }
    }

    private void ShootDelivery()
    {
        //Move Deliver VFX
        Vector3 deliverDirection = (deliverPoint.position - transform.position).normalized * -1;
        deliverVisualsTransform.transform.Translate(deliverDirection * deliverSpeed * Time.deltaTime);

        //Check deliver distance
        if (Vector3.Distance(deliverVisualsTransform.transform.position, deliverPoint.transform.position) < 0.3f)
        {
            shootingDelivery = false;
            StartCoroutine(ParticlesCooldown());
        }
    }

    private void OnDrawGizmos()
    {
        if (deliverPoint != null) { 
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(deliverPoint.transform.position, 0.3f);
            Gizmos.DrawWireSphere(deliverPoint.transform.position, deliverValidDistance);
        }
    }

    private IEnumerator ParticlesCooldown()
    {
        yield return new WaitForSeconds(deliverAnimationLifetime);
        deliverVisualsTransform.gameObject.SetActive(false);
        deliverVisualsTransform.localPosition = Vector3.zero + new Vector3(0, 0.2f, 0);
        if(succesfulDeliver != null)
            AudioSource.PlayClipAtPoint(succesfulDeliver, transform.position + new Vector3(0, 2f, 0));
    }
}
