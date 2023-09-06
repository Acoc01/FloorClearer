using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellCasting : MonoBehaviour
{
    private const int _maxBallCombination = 3;
    private const int _maxStoredSpells = 2;

    private List<ElementBall> elementBalls = new List<ElementBall>();
    public Queue<Spell> storedSpells = new Queue<Spell>();
    private SpellCombination MagicMixer;

    public GameObject LightBall;
    public GameObject ArcaneBall;
    public GameObject SpiritBall;

    public SpellUI SpellSlots;

    private void Start(){
        MagicMixer = GetComponent<SpellCombination>();
    }
    private void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CreateElementBall(Element.Light);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CreateElementBall(Element.Arcane);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CreateElementBall(Element.Spirit);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            DeleteElementBall();
        }
        // else if (Input.GetKeyDown(KeyCode.E))
        // {
        //     CombineElements();
        // }
        else if (Input.GetMouseButtonDown(0))
        {
            CastSpell();
        }
    }
    private void CastSpell()
    {
        if (storedSpells.Count > 0)
        {
            Spell combinedSpell = storedSpells.Dequeue();
            SpellSlots.UseSpell();
            combinedSpell.CastSpell();
        }
    }
    private void CombineElements()
    {
        if (elementBalls.Count == _maxBallCombination && storedSpells.Count < _maxStoredSpells)
        {
            Debug.Log("Combining...");
            for (int i = 0; i < _maxBallCombination; i++)
                Debug.Log(elementBalls[i].elementType);

            // Create a new combined spell
            Spell tempSpell = MagicMixer.Combine(elementBalls);
            storedSpells.Enqueue(tempSpell);
            SpellSlots.AddSpell(tempSpell);

            // Clear the list of elementBalls
            foreach (ElementBall ball in elementBalls)
            {
                Destroy(ball.gameObject);
            }
            elementBalls.Clear();
            
        }
        else
        {
            Debug.Log("not enough elements");
            return;
        }
    }

    private void CreateElementBall(Element elementType)
    {
        if (elementBalls.Count < _maxBallCombination) // Check if there's room to create a new ball
        {
            GameObject newBall = null;
            // Instantiate a new ElementBall prefab
            if (elementType == Element.Light)
            {
                newBall = Instantiate(LightBall, transform.position, Quaternion.identity);
            }
            else if (elementType == Element.Arcane)
            {
                newBall = Instantiate(ArcaneBall, transform.position, Quaternion.identity);
            }
            else if (elementType == Element.Spirit)
            {
                newBall = Instantiate(SpiritBall, transform.position, Quaternion.identity);
            }

           
            ElementBall ballScript = newBall.GetComponent<ElementBall>();
            ballScript.elementType = elementType;

            // Assign the camera to the ball's caster variable
            ballScript.caster = Camera.main;

            // Add the new ball to the list and update ball numbers\
            ballScript.ballNumber = elementBalls.Count;
            elementBalls.Add(ballScript); 

            if(elementBalls.Count == _maxBallCombination)
                CombineElements();
        }
    }

    private void DeleteElementBall()
    {
        if (elementBalls.Count > 0)
        {
            ElementBall ballToDelete = elementBalls[elementBalls.Count - 1];
            if (ballToDelete != null)
            {
                elementBalls.RemoveAt(elementBalls.Count - 1);
                Destroy(ballToDelete.gameObject);
            }

        }
    }
}
