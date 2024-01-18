using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject borrowPanel;
    public GameObject returnPanel;
    public GameObject addPanel;
    private GameObject currentPanel;
    public GameObject showBorrowBookBtn;
    public GameObject returnBookBtn;
    public GameObject addBookBtn;
    



    void Start()
    {
        startPanel.SetActive(true);
        borrowPanel.SetActive(false);
        returnPanel.SetActive(false);
        addPanel.SetActive(false);

        showBorrowBookBtn.GetComponent<Button>().onClick.AddListener(ShowBorrowPanel);
        returnBookBtn.GetComponent<Button>().onClick.AddListener(ShowReturnPanel);
        addBookBtn.GetComponent<Button>().onClick.AddListener(ShowAddPanel);
    }

    // BorrowPanel'i aç
    void ShowBorrowPanel()
    {
        currentPanel = borrowPanel;
        startPanel.SetActive(false);
        borrowPanel.SetActive(true);
    }

    // ReturnPanel'i aç
    void ShowReturnPanel()
    {
        currentPanel = returnPanel;
        startPanel.SetActive(false);
        returnPanel.SetActive(true);
    }

    // AddPanel'i aç
    void ShowAddPanel()
    {
        currentPanel = addPanel;
        startPanel.SetActive(false);
        addPanel.SetActive(true);
    }

    // Diğer panellerden StartPanel'e dön
    public void BackToStartPanel()
    {
        currentPanel.SetActive(false);
        startPanel.SetActive(true);
    }
}
