namespace BilucaDialogue
{
    public class DialogueManager
    {
        public bool IsFinished { get; private set; }
        private Dialogue currentDialogue;
        private DialogueNode currentDialogueNode;

        public void Start(Dialogue dialogue)
        {
            currentDialogue = dialogue;
            currentDialogueNode = currentDialogue.Start;
        }

        public void Next()
        {
            var nextNodes = currentDialogue
                .GetNextDialogueNodes(currentDialogueNode)
                .ToArray();

            IsFinished = nextNodes.Length == 0;

            if(!IsFinished)
                currentDialogueNode = nextNodes[0];
        }
    }
}