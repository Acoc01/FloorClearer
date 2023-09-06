using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellUI : MonoBehaviour
{
    public GameObject spellImagePrefab;
    public Transform spellImagesParent;

    private Queue<Spell> storedSpells = new Queue<Spell>();
    private List<Image> spellImages = new List<Image>();

    public Sprite placeHolderSpell;

    public void AddSpell(Spell spell)
    {
        storedSpells.Enqueue(spell);

        // Create a new UI image for the added spell
        Image spellImage = Instantiate(spellImagePrefab, spellImagesParent).GetComponent<Image>();
        spellImage.sprite = placeHolderSpell; // Using the placeHolderSpell sprite
        spellImages.Add(spellImage);

        UpdateUIPositions();
    }

    public void UseSpell()
    {
        if (storedSpells.Count > 0)
        {
            storedSpells.Dequeue();

            // Remove the oldest UI image
            if (spellImages.Count > 0)
            {
                Destroy(spellImages[0].gameObject);
                spellImages.RemoveAt(0);
                UpdateUIPositions();
            }
        }
    }

    private void UpdateUIPositions()
    {
        float xOffset = 50f;
        float xPosition = 0f;

        foreach (Image spellImage in spellImages)
        {
            spellImage.transform.localPosition = new Vector3(xPosition, 0f, 0f);
            xPosition += xOffset;
        }
    }
}
