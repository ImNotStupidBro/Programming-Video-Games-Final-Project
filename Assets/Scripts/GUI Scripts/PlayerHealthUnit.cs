using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthUnit : MonoBehaviour
{
    public Player currentPlayer;
    public int index;

    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        int currentPlayerHP = currentPlayer.GetHealthPoints();

        AnimatorUpdater();
    }

    public void Init(Player player, int ndx)
    {
        currentPlayer = player;
        index = ndx;
    }

    public void AnimatorUpdater()
    {
        anim.SetBool("playerHPGreaterThanIndex", (currentPlayer.GetHealthPoints() - 1) > index);
        anim.SetBool("playerHPEqualToIndex", (currentPlayer.GetHealthPoints() - 1) == index);
    }
}
