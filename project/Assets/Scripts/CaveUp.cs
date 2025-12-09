public class CaveUp : InteractableObject
{
    public override void Interact()
    {
        DialogManager.AddDialog("Seraphine: I cannot go back there. I must continue my journey.");
        DialogManager.ShowDialog();
    }
}
