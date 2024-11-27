using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject uiCamera;
    public GameObject mainMenu;
    public TMP_InputField playerNameInput;
    public TMP_InputField joinCodeInput;
    public TMP_Text joinCodeText;

    private void Awake()
    {
        Instance = this;
    }

    public void StartHost()
    {
        RelayManager.Instance.CreateRelay();
        uiCamera.SetActive(false);
        mainMenu.SetActive(false);
    }

    public void StartClient()
    {
        RelayManager.Instance.JoinRelay(joinCodeInput.text);
        uiCamera.SetActive(false);
        mainMenu.SetActive(false);
    }
}
