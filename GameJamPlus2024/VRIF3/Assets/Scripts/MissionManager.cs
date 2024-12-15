using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour
{
    [SerializeField] private DeliverBoxBehavior deliverBox;

    [SerializeField] private Image flowerImage;
    [SerializeField] private Sprite flowerSprite;
    [SerializeField] private TMP_Text missionText;
    [SerializeField] private GameObject secondImageGameObject;

    public AudioClip finishedDemo;

    private bool secondMission = false;

    public void CompleteMission()
    {
        if (deliverBox != null)
        {
            if(secondMission)
            {
                //Oh God I hate my self for this, but I gotta run!
                if(finishedDemo != null)
                    AudioSource.PlayClipAtPoint(finishedDemo, deliverBox.transform.position);
                missionText.text = "Thank you sweetheart! See you tomorrow!";
                return;
            }

            deliverBox.UpdateDesiredFlower(SeedBehavior.SeedType.Flower);
            flowerImage.sprite = flowerSprite;
            missionText.text = "Lovely! Now I need a pine flower!";
            secondImageGameObject.SetActive(true);
            secondMission = true;
            
        }
    }
}
