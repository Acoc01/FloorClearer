using System.Collections.Generic;
using UnityEngine;

public class SpellCombination : MonoBehaviour
{
    public ArcaneMissilesSpell arcaneMissilesSpellPrefab;
    public IceShardSpell iceShardSpellPrefab;
    public Spell Combine(List<ElementBall> elementBalls)
    {
        int _spellID = 0;

        // Check if there are enough element balls to perform a combination
        if (elementBalls.Count != 3)
        {
            Debug.Log("Not enough element balls for combination.");

        }
        // Check the elements of the balls and determine the resulting spell
        for (int i = 0; i < 3; i++)
        {
            if (elementBalls[i].elementType == Element.Light)
                _spellID += 1;
            else if (elementBalls[i].elementType == Element.Arcane)
                _spellID += 10;
            else if (elementBalls[i].elementType == Element.Spirit)
                _spellID += 100;
        }

        return getCombinedSpell(_spellID);
    }

    private Spell getCombinedSpell(int SID)
    {
        Debug.Log("Combined Spell ID: "+ SID);
        if (SID == 30)
        {
            return arcaneMissilesSpellPrefab;
        }
        else if (SID == 120){
            return iceShardSpellPrefab;
        }
        else
        {
            return null;
        }
    }
}
