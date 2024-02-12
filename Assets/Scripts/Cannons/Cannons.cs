using UnityEngine;

[CreateAssetMenu(fileName = "New Cannon", menuName = "Cannon")]
public class Cannons : ScriptableObject
{
    public new string name;
    public string description;

    public float fireRate;
    public int numBullet;
    public int damage;

    public bool isTurrel;
}
