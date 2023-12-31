using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellCasting : MonoBehaviour
{
    private const int _maxBallCombination = 3;
    private const int _maxStoredSpells = 2;
    
    public BasicAttackFactory basicAttackFactory;
    public Transform aimPointer;

    private List<ElementBall> elementBalls = new List<ElementBall>();
    private SpellCombination MagicMixer;
    public Queue<Spell> storedSpells = new Queue<Spell>();
    public GameObject LightBall;
    public GameObject ArcaneBall;
    public GameObject SpiritBall;

    public SpellUI SpellSlots;

    private void Start(){
        MagicMixer = GetComponent<SpellCombination>();
        basicAttackFactory.Setup();
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
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if(elementBalls.Count == _maxBallCombination)
                CombineElements();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            CastSpell();
        }
    }
    private void CastSpell()
    {
        if (storedSpells.Count > 0)
        {
            //Spell combinedSpell = storedSpells.Peek();
            Spell combinedSpell = storedSpells.Dequeue();
            SpellSlots.UseSpell();
            combinedSpell.CastSpell();
        }
        else {
            basicAttackFactory.CreateBasicAttack(aimPointer);
        }
    }

    private void CombineElements()
    {
        if (elementBalls.Count == _maxBallCombination && storedSpells.Count < _maxStoredSpells)
        {
            Debug.Log("Combining...");
            for (int i = 0; i < _maxBallCombination; i++)
                Debug.Log(elementBalls[i].elementType);

            Spell tempSpell = MagicMixer.Combine(elementBalls);
            storedSpells.Enqueue(tempSpell);
            SpellSlots.AddSpell(tempSpell);

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
        if (elementBalls.Count == _maxBallCombination) 
        {
            Destroy(elementBalls[elementBalls.Count - 1].gameObject);
            elementBalls.RemoveAt(elementBalls.Count - 1);
        }
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

        foreach (ElementBall ball in elementBalls){
            ball.ballNumber += 1;
        }

        ElementBall ballScript = newBall.GetComponent<ElementBall>();
        ballScript.elementType = elementType;

        ballScript.caster = Camera.main;
        ballScript.ballNumber = 0;
        elementBalls.Insert(0,ballScript); 
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
