using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TracingController : MonoBehaviour
{
    public Button nextButton;
    public Button backButton;
    public GameObject capitalSlide;
    public GameObject smallLetterSlide;

    private void Start()
    {
        nextButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(false);
        capitalSlide.SetActive(true);
        smallLetterSlide.SetActive(false);
    }
    // method showing the fuctionality while clicking nextButton 
    public void OnNextButtonClick()
    {
        capitalSlide.SetActive(false);
        smallLetterSlide.SetActive(true);
        nextButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(true);
    }public void OnBackButtonClick()
    {
        capitalSlide.SetActive(true);
        smallLetterSlide.SetActive(false);
        nextButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(false);
    }

}
