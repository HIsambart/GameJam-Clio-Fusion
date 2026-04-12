using UnityEngine;
using Utils;

public class AudioManager : MonoBehaviourSingleton<AudioManager>
{
    [Header("Ambiance")]
    
    public AudioClip BruitDuTokamak;
    public AudioClip MusiqueInGame;
    public AudioClip MusiqueMenuDELancement;
    
    [Header("End Game")]
    public AudioClip BruitVictoire;
    public AudioClip BruitDefaite;
    
    [Header("Mini game")]
    
    public AudioClip BruitWarning;
    public AudioClip BruitJeuPression;
    public AudioClip BruitJeuRotation;
    public AudioClip BruitJeuxRecette;
    public AudioClip BruitDeFeu;
    
    [Header("Other")]
    public AudioClip BruitDebrit;
    public AudioClip BruitUI;
    public AudioClip Perso_Pas1;
    public AudioClip Perso_Pas2;
    
    
    public void PlaySound(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }
}
