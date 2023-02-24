using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities;

public class Cannons : MonoBehaviour
{
    [SerializeField] [BoxGroup("Required Data")] private GameObject cannonBall;
    [SerializeField] [BoxGroup("Required Data")] private GameObject muzzle;

    [SerializeField] [BoxGroup("Cannon Properties")] private float reloadTime;
    [Space]
    [Tooltip("Contém os grupos de canhões começando com os frontais no índice 0, seguindo sentido horário.")]
    [SerializeField] [BoxGroup("Cannon Properties")] private List<CannonGroup> cannonGroups;

    public void UseCannons(int side)
    {
        if (Cooldown.TryStart(gameObject, cannonGroups[side].tag, reloadTime) == false) return;
        foreach (Transform cannon in cannonGroups[side].cannons)
        {
            if (cannon == null) continue;

            Instantiate(cannonBall, cannon.GetChild(0).position, cannon.rotation);
            GameObject muzzleObject = Instantiate(muzzle, cannon.GetChild(0).position, cannon.rotation);

            Destroy(muzzleObject, 2f);
        }
    }

}

[System.Serializable]
public struct CannonGroup
{
    public string tag;
    public List<Transform> cannons;
}
