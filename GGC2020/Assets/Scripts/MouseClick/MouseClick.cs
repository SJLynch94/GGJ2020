using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    public List<string> ClickTagList;
    public string UITag;
    public string TownHallTag;

    private GameObject CurrentSelected;
    //  private GameObject CurrentSelected;
    private GameObject menu;

    public GameObject MenuPrefab;
    public GameObject BuyingPrefab;
    public GameObject TownHallPrefab;
    // public GameObject MenuContentHolder;
    private GameObject MainCam;


    private void Start()
    {
        MainCam = GameObject.FindWithTag("MainCamera");
    }



    void Update()
    {

        GameObject[] temp = GameObject.FindGameObjectsWithTag("GameManager");
        if (temp[0].GetComponent<GameManager>().currentGamestate == Gamestate.Play)
        {


            if (Input.GetMouseButtonDown(0))
            {
                PointerEventData pointerData = new PointerEventData(EventSystem.current);
                pointerData.position = Input.mousePosition;

                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerData, results);

                RaycastHit hitInfo = new RaycastHit();
                bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);

                if (results.Count > 0)
                {
                    //WorldUI is my layer name
                    if (results[0].gameObject.layer == LayerMask.GetMask("UI"))
                    {
                        string dbg = "Root Element: {0} \n GrandChild Element: {1}";
                        Debug.Log(string.Format(dbg, results[results.Count - 1].gameObject.name, results[0].gameObject.name));
                        results.Clear();
                    }
                }
                else
                {
                    if (hit)
                    {

                        if (menu != null && menu.gameObject.activeSelf)
                        {
                            if (hitInfo.transform.gameObject.name != CurrentSelected.name)
                            {
                                menu.gameObject.SetActive(false);
                            }

                        }

                        if (hitInfo.transform.gameObject.tag != TownHallTag)
                        {


                            //if (menu != null && menu.gameObject.activeSelf)
                            //{
                            //    if (hitInfo.transform.gameObject.name != CurrentSelected.name)
                            //    {
                            //        menu.gameObject.SetActive(false);
                            //    }

                            //}
                            Debug.Log("Hit " + hitInfo.transform.gameObject.name);
                            foreach (var s in ClickTagList)
                            {
                                if (hitInfo.transform.gameObject.tag == s)
                                {
                                    CurrentSelected = hitInfo.transform.gameObject;
                                    var g = FindObjectOfType<GameManager>();
                                    if (g.currentGamePlayState == GamePlayState.Building)
                                    {
                                        if (!CurrentSelected.GetComponent<BuildingBase>().IsBought)
                                        {
                                            if (!CurrentSelected.transform.Find("BuyingMenu(Clone)"))
                                            {
                                                menu = Instantiate(BuyingPrefab, new Vector3(0, 0, 0), Quaternion.Euler(45, 180, BuyingPrefab.transform.rotation.z));
                                                menu.transform.SetParent(hitInfo.transform);

                                                menu.transform.localPosition = new Vector3(0, -0.5f, 0);
                                                menu.SetActive(true);

                                                menu.GetComponent<Canvas>().worldCamera = MainCam.GetComponent<Camera>();
                                                Debug.Log("It's Working! Buying");
                                                break;
                                            }

                                            if (CurrentSelected.transform.Find("BuyingMenu(Clone)") != null)
                                            {
                                                menu = CurrentSelected.transform.Find("BuyingMenu(Clone)").gameObject;
                                                if (menu != null && !menu.gameObject.activeSelf)
                                                {
                                                    menu.gameObject.SetActive(true);
                                                }
                                                else if (menu != null)
                                                {
                                                    menu.gameObject.SetActive(false);
                                                }

                                            }

                                            if (CurrentSelected.transform.Find("TownHallMenuCanvas(Clone)") != null)
                                            {
                                                menu = CurrentSelected.transform.Find("TownHallMenuCanvas(Clone)").gameObject;
                                                if (menu != null && !menu.gameObject.activeSelf)
                                                {
                                                    menu.gameObject.SetActive(true);
                                                }
                                                else if (menu != null)
                                                {
                                                    menu.gameObject.SetActive(false);
                                                }

                                            }
                                        }
                                    }

                                    if (!CurrentSelected.transform.Find("BuildingMenuCanvas(Clone)") && CurrentSelected.GetComponent<BuildingBase>().IsBought)
                                    {
                                        menu = Instantiate(MenuPrefab, new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y, hitInfo.transform.position.z), Quaternion.Euler(45, 180, MenuPrefab.transform.rotation.z));
                                        menu.transform.SetParent(hitInfo.transform);

                                        menu.transform.localPosition = new Vector3(0, 1, 0);




                                        menu.SetActive(true);

                                        menu.GetComponent<Canvas>().worldCamera = MainCam.GetComponent<Camera>();
                                    }

                                    if (CurrentSelected.transform.Find("BuildingMenuCanvas(Clone)"))
                                    {
                                        menu = CurrentSelected.transform.Find("BuildingMenuCanvas(Clone)").gameObject;
                                        if (menu != null && !menu.gameObject.activeSelf)
                                        {
                                            menu.gameObject.SetActive(true);
                                        }
                                        else if (menu != null)
                                        {
                                            menu.gameObject.SetActive(false);
                                        }

                                    }

                                    if (CurrentSelected.transform.Find("TownHallMenuCanvas(Clone)") != null)
                                    {
                                        menu = CurrentSelected.transform.Find("TownHallMenuCanvas(Clone)").gameObject;
                                        if (menu != null && !menu.gameObject.activeSelf)
                                        {
                                            menu.gameObject.SetActive(true);
                                        }
                                        else if (menu != null)
                                        {
                                            menu.gameObject.SetActive(false);
                                        }

                                    }
                                    //menu.transform

                                    //menu = CurrentSelected.transform.Find("BuildingMenu");

                                    Debug.Log("It's Working!");
                                    break;
                                }
                            }
                        }
                        else
                        {
                            CurrentSelected = hitInfo.transform.gameObject;
                            var g = FindObjectOfType<GameManager>();
                            if (g.currentGamePlayState == GamePlayState.Building)
                            {
                                if (!CurrentSelected.transform.Find("TownHallMenuCanvas(Clone)"))
                                {
                                    menu = Instantiate(TownHallPrefab, new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y, hitInfo.transform.position.z), Quaternion.Euler(45, 180, TownHallPrefab.transform.rotation.z));
                                    menu.transform.SetParent(hitInfo.transform);

                                    menu.transform.localPosition = new Vector3(0, 2, 0);




                                    menu.SetActive(true);

                                    menu.GetComponent<Canvas>().worldCamera = MainCam.GetComponent<Camera>();
                                }

                                if (CurrentSelected.transform.Find("TownHallMenuCanvas(Clone)") != null )
                                {
                                    menu = CurrentSelected.transform.Find("TownHallMenuCanvas(Clone)").gameObject;
                                    if (menu != null && !menu.gameObject.activeSelf)
                                    {
                                        menu.gameObject.SetActive(true);
                                    }
                                    else if (menu != null)
                                    {
                                        menu.gameObject.SetActive(false);
                                    }

                                }

                                if (CurrentSelected.transform.Find("BuyingMenu(Clone)") != null)
                                {
                                    menu = CurrentSelected.transform.Find("BuyingMenu(Clone)").gameObject;
                                    if (menu != null && !menu.gameObject.activeSelf)
                                    {
                                        menu.gameObject.SetActive(true);
                                    }
                                    else if (menu != null)
                                    {
                                        menu.gameObject.SetActive(false);
                                    }

                                }


                                if (CurrentSelected.transform.Find("BuildingMenuCanvas(Clone)") != null)
                                {
                                    menu = CurrentSelected.transform.Find("BuildingMenuCanvas(Clone)").gameObject;
                                    if (menu != null && !menu.gameObject.activeSelf)
                                    {
                                        menu.gameObject.SetActive(true);
                                    }
                                    else if (menu != null)
                                    {
                                        menu.gameObject.SetActive(false);
                                    }

                                }

                            }
                        }
                    }
                }


                Debug.Log("Mouse is down");
            }
        }
        //void SetData()
        //{
    }
    //}
}
