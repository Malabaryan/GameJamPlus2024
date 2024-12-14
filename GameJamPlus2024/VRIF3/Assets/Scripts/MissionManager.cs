using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [SerializeField] private DeliverBoxBehavior deliverBox;

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
                return;
            }

            deliverBox.UpdateDesiredFlower(SeedBehavior.SeedType.Flower);
            secondMission = true;
            
        }
    }
}
