using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeReference] [BoxGroup("Required Data")] private SOInputReader inputReader;
    [SerializeReference] [TitleGroup("Required Data/Components")] private Cannons cannonModule;

    private void UseCannons(int side)
    {
        if (cannonModule == null)
        {
            Debug.LogWarning("O script que controla os canhões, não foi referenciado.");
            return;
        }

        cannonModule.UseCannons(side);
    }

    #region Event listeners
    private void OnEnable()
    {
        inputReader.shootCannons += UseCannons;
    }

    private void OnDisable()
    {
        inputReader.shootCannons -= UseCannons;
    }


    #endregion
}
