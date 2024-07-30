using CodeMonkey.Utils;
using UnityEngine;

public class ChangeAnim : MonoBehaviour
{
    [SerializeField] GameObject[] animatedObjects;
    [SerializeField] GameObject[] toggleBoxes;
    [SerializeField] GameObject Menu;
    [SerializeField] Button_Sprite changeAnimButton;
    [SerializeField] Button_Sprite toggleBoxButton;
    
    int i = 0;

    private void Start()
    {
        changeAnimButton.ClickFunc = () => NextAnim();
        toggleBoxButton.ClickFunc = () => ToggleBox();
    }

    //void Update()
    //{
    //    if (Input.GetKey(KeyCode.LeftAlt))
    //    {
    //        if (Input.GetKeyDown(KeyCode.J))
    //        {
    //            ToggleMenu();
    //        }
    //    }
    //}

    void ToggleMenu()
    {
        Menu.SetActive(!Menu.activeSelf);
    }

    void ToggleBox()
    {
        foreach (GameObject box in toggleBoxes)
        {
            box.SetActive(!box.activeSelf);
        }
    }

    public void NextAnim()
    {
        animatedObjects[i].SetActive(false);
        i++;
        if ((animatedObjects.Length > i))
        {
            animatedObjects[i].SetActive(true);
        }
        else
        {
            i = 0;
            animatedObjects[i].SetActive(true);
        }
    }
}