using UnityEngine;

public class CrystalInfo : MonoBehaviour
{
    [Tooltip("Information about this crystal")]
    [TextArea]
    public string crystalInfo = "Default crystal information.";

    [Tooltip("The 3D text object to display info")]
    public GameObject infoTextObject;
}