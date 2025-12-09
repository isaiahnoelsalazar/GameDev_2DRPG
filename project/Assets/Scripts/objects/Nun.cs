public class Nun : InteractableObject
{
    int counter = 0;

    public override void Interact()
    {
        if (counter <= 0)
        {
            DialogManager.AddDialog("Seraphine: Hello?");
            DialogManager.AddDialog("Nun: ...");
            DialogManager.AddDialog("Nun: Child. I can sense the mark of the Celestial Watcher from you.");
            DialogManager.AddDialog("Seraphine: This mark... What does it mean?");
            DialogManager.AddDialog("Nun: It means that the time has come.");
            DialogManager.AddDialog("Nun: You have been chosen as the hero that will save this world.");
            DialogManager.AddDialog("Seraphine: I understand. Then where shall I go?");
            DialogManager.AddDialog("Nun: The Umbra Mountain.");
            DialogManager.AddDialog("Nun: That is where you will continue your journey.");
            DialogManager.AddDialog("Seraphine: Then I will go.");
            DialogManager.AddDialog("Nun: I will pray for your success.");
            DialogManager.AddDialog("Seraphine: Thank you.");
            DialogManager.ShowDialog();
        }
        else
        {
            if (counter > 10)
            {
                DialogManager.AddDialog("Nun: ...");
                DialogManager.ShowDialog();
            }
            else
            {
                DialogManager.AddDialog("Nun: I will pray for your success.");
                DialogManager.ShowDialog();
            }
        }
        counter++;
    }
}
