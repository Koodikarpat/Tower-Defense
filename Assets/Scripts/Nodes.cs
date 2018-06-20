﻿using UnityEngine;
using UnityEngine.EventSystems;

public class Nodes : MonoBehaviour {

    private Color startColor;
    public Color hoverColor;

    public float targetTime = 2f;

    public GameObject text;

    public Vector3 positionOffset = new Vector3(0, 0, -1);

    public GameObject turret;

    private SpriteRenderer rend;

    BuildManager buildManager;
    public GameObject gameMaster;


    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
        gameMaster = GameObject.Find("GameMaster");

       // Debug.Log(GameObject.Find("Canvas").transform.childCount);
        text = GameObject.Find("GameUICanvas").transform.Find("NoMoneyBG").gameObject;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    void OnMouseDown()
    {
        GameObject turretToBuild = buildManager.getTurretToBuild();
        if (turret != null)
        {
            buildManager.selectNode(this);
            return;
        }
        else if(turretToBuild == null)
        {
            buildManager.DeselectNode();
            return;
        }


        
        if (turretToBuild != null && GameMaster.goldamount >= turretToBuild.GetComponent<turret>().cost)
        {
            
            if (buildManager.getTurretToBuild() == null)
                return;

            if (!buildManager.CanBuild)
                return;

            // GameObject turretToBuild = buildManager.getTurretToBuild();
            turret = (GameObject)Instantiate(turretToBuild, new Vector3(transform.position.x, transform.position.y, -1), transform.rotation);
            gameMaster.GetComponent<GameMaster>().goldupdate(-turret.GetComponent<turret>().cost);

            /*if (text.activeSelf)
            {
                text.SetActive(false);

            }*/
        }
        //else if (turretToBuild != null) text.SetActive(true);
        //timerEnded();
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;
        rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    void Update()
    {

        if (targetTime <= 0.0f)
        {
            timerEnded();
        }

        /*if (text.activeSelf)
        {
            targetTime -= Time.deltaTime;
        }
        */
    }

    void timerEnded()
    {
        text.SetActive(false);
        targetTime = 2f;
    }
}
