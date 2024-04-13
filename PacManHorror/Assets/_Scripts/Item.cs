using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{

    public enum ItemTypes { SPEED, MAP, MAGNET };

    public ItemTypes itemType;

    public int price;

    void Start()
    {
        InitText();
    }
    private void OnTriggerEnter(Collider other) //On strigger stay for VR
    {
        //if (HandManager.instance.WasButtonPressed(HandManager.Buttons.TRIGGERBUTTON))
        //{
        if (other.CompareTag(PlayerTags.hands))
        {
            Purchase();

        }
        //}
    }

    public void Purchase()
    {
        if (Scriptables.PlayerScore.coins >= price)
        {
            Scriptables.PlayerScore.coins -= (price / 2);
            switch (itemType)
            {
                case ItemTypes.SPEED:
                    Scriptables.Stim.active = true;
                    Scriptables.Stim.ChangeObjectActivity(true); //This turns on the object model
                    break;
                case ItemTypes.MAP:
                    Scriptables.Minimap.active = true;
                    Scriptables.Minimap.ChangeObjectActivity(true);
                    break;
                case ItemTypes.MAGNET:
                    Scriptables.Magnet.active = true;
                    Scriptables.Magnet.ChangeObjectActivity(true);
                    break;
            }
            Destroy(this.gameObject);
        }
        else
            Debug.Log("Cannot afford");
    }

    private void InitText()
    {
        Text text = transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Text>();

        switch (itemType)
        {
            case ItemTypes.SPEED:
                text.text = "Stim\n";
                break;
            case ItemTypes.MAP:
                text.text = "Minimap\n";
                break;
            case ItemTypes.MAGNET:
                text.text = "Magnet\n";
                break;
        }
        text.text += price + " coins";
    }
}
