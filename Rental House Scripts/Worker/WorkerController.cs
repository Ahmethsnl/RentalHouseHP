using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerController : MonoBehaviour
{
    [SerializeField] private WalletData myWallet;
    [SerializeField] private WorkerData myData;
    [SerializeField] private CinemachineDollyCart myDolly;

    private float myCurrentPosition;
    private bool isPositioning;

    public AudioSource soundCoin;
    public LayerMask layer;

    private void Update()
    {
        myDolly.m_Speed = isPositioning ? 0 : myData.workerSpeed;
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit ,6f, layer))
        {
            myWallet.money += 1;
        }
    }

    public void SetMyPosition(CinemachineSmoothPath path, float amount)
    {
        myDolly.m_Path = path;
        myCurrentPosition = amount;
        myDolly.m_Speed = 0;
        isPositioning = true;
        StartCoroutine(LetsSetPosition());
    }

    private IEnumerator LetsSetPosition()
    {
        isPositioning = true;
        float timeElapsed = 0;
        for (float i = 0; i < 1f; i += Time.deltaTime)
        {
            timeElapsed += Time.deltaTime;
            myDolly.m_Position = Mathf.Lerp(myDolly.m_Position, myCurrentPosition, timeElapsed);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        isPositioning = false;
    }
}
