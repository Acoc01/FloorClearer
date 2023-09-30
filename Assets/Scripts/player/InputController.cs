using UnityEngine;

public class InputController : MonoBehaviour
{
    private FirstPersonMovement playerMovement;
    private PlayerSpellCasting playerSpellCasting;
    public PlayerUI gameScreenManager;

    void Start()
    {
        playerMovement = GetComponent<FirstPersonMovement>();
        playerSpellCasting = GetComponent<PlayerSpellCasting>();
        gameScreenManager.isGamePaused = false;
    }

    private void Update()
    {
        if (!gameScreenManager.isGamePaused)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            bool isSprinting = Input.GetKey(KeyCode.LeftShift);
            bool isWalking = Input.GetKey(KeyCode.LeftControl);

            playerMovement.HandleMovement(horizontalInput, verticalInput, isSprinting, isWalking);

            if (Input.GetButtonDown("Jump"))
            {
                playerMovement.HandleJump();
            }

            if (
                Input.GetKeyDown(KeyCode.Alpha1) ||
                Input.GetKeyDown(KeyCode.Alpha2) ||
                Input.GetKeyDown(KeyCode.Alpha3) ||
                Input.GetKeyDown(KeyCode.R) ||
                Input.GetKeyDown(KeyCode.E)
            )
            {
                KeyCode keyPressed = ConvertToKeyCode(Input.inputString);
                playerSpellCasting.CastSpellBasedOnKey(keyPressed);
            }
            if (Input.GetMouseButtonDown(0))
            {
                playerSpellCasting.CastSpell();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gameScreenManager.isGamePaused){
                gameScreenManager.OpenPauseScreen();
                gameScreenManager.isGamePaused= true;
            }else{
                gameScreenManager.ExitPauseScreen();
                gameScreenManager.isGamePaused = false;
            }
        }
    }

    KeyCode ConvertToKeyCode(string input)
    {
        if (input.Length == 1)
        {
            char character = input[0];
            Debug.Log(input);
            if (char.IsDigit(character))
            {
                return (KeyCode)System.Enum.Parse(typeof(KeyCode), "Alpha" + character);
            }
            else if (char.IsLetter(character))
            {
                return (KeyCode)System.Enum.Parse(typeof(KeyCode), character.ToString().ToUpper());
            }
        }

        return KeyCode.None;
    }

}
