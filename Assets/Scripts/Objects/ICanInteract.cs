using System.Collections;

public interface ICanInteract
{
    // Method where you find the behavior of the object
    IEnumerator Interaction();

    // Method where you find the text to show to the player
    void Description();
}
