using System.Collections.Generic;
using System.Collections;

using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class LvGame
{
    public GameObject objLv;
    public GameObject table;
    public GameObject conditionTable;
}
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get => instance; }
    public List<Color> listColor;
    public Color currentColor;
    public List<GameObject> result;
    public LvGame[] lvGame;
    public int indexLvGameNow;
    public GameObject mouse;
    public GameObject panelWin;
    public GameObject vfxWin;
    public GameObject vfx;
    public void RandomColor()
    {
        if(currentColor == Color.white )
        {
            int randomIndex = Random.Range(0, listColor.Count);
            currentColor = listColor[randomIndex];
        }    
     
    }    
    public void ResetFullObj()
    {
        for (int i = 0; i < lvGame[indexLvGameNow].table.transform.childCount; i++)
        {
            lvGame[indexLvGameNow].table.transform.GetChild(i).GetComponent<Image>().color = Color.white;
        }
       
    }    
    public void CheckResult()
    {
        string strResult = "";
        for (int i = 0; i < result.Count; i++)
        {
            string text = result[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;

            // Loại bỏ ký tự xuống dòng và ký tự hồi đầu nếu có
            text = text.Replace("\n", "").Replace("\r", "");
            // Nối văn bản vào chuỗi kết quả
            strResult += text;
        }
        Debug.Log(strResult);
        for (int i = 0; i < lvGame[indexLvGameNow].conditionTable.transform.childCount; i++)
        {
            if(strResult == lvGame[indexLvGameNow].conditionTable.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text && lvGame[indexLvGameNow].conditionTable.transform.GetChild(i).GetComponent<Animator>().enabled == false)
            {
                lvGame[indexLvGameNow].conditionTable.transform.GetChild(i).GetComponent<Animator>().enabled = true;
                currentColor = Color.white;
                result.Clear();
                StartCoroutine(CoroutineVfx(lvGame[indexLvGameNow].conditionTable.transform.GetChild(i).transform.position));
                CheckWin();
                return;
            }    
        }
        // reset
        for (int i = 0; i < result.Count; i++)
        {
           result[i].GetComponent<Image>().color = Color.white;
        }
        currentColor = Color.white;
        result.Clear();

    }

    void CheckWin()
    {
        for (int i = 0; i < lvGame[indexLvGameNow].conditionTable.transform.childCount; i++)
        {
            if (lvGame[indexLvGameNow].conditionTable.transform.GetChild(i).GetComponent<Animator>().enabled == false)
            {
                return;
            }
        }
          
        panelWin.SetActive(true);
        vfxWin.SetActive(true);
    }    
    public void EventButtonContinue()
    {
        if (indexLvGameNow >= lvGame.Length - 1)
        {
            SceneManager.LoadScene(0);
            return;
        }   
        else
        {
            panelWin.SetActive(false);
            vfxWin.SetActive(false);
            indexLvGameNow++;
            lvGame[indexLvGameNow-1].objLv.SetActive(false);
            lvGame[indexLvGameNow].objLv.SetActive(true);
        }
        
    }    
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            CheckResult();
            mouse.SetActive(false);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            mouse.SetActive(true);


        }
        // Lấy vị trí của con trỏ chuột trong tọa độ màn hình
        Vector3 mousePos = Input.mousePosition;

        // Chuyển đổi vị trí từ tọa độ màn hình sang tọa độ thế giới
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        // Đặt lại vị trí của đối tượng theo tọa độ thế giới mới
        mouse.transform.position = mousePos;
    }
    IEnumerator CoroutineVfx(Vector3 pos)
    {
        vfx.SetActive(true);
        vfx.transform.position = pos;
        yield return new WaitForSeconds(1f);
        vfx.SetActive(false);
    }    

}
