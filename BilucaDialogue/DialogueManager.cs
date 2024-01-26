namespace BilucaDialogue
{
    public class DialogueManager
    {
        public bool IsFinished { get; private set; }
        private Dialogue? currentDialogue;
        private DialogueNode? currentDialogueNode;

        public void Start(Dialogue dialogue)
        {
            currentDialogue = dialogue;
            currentDialogueNode = currentDialogue.Start;
        }

        public void End()
        {
            currentDialogue = null;
            currentDialogueNode = null;
        }

        public void Next()
        {
            if(currentDialogue == null || currentDialogueNode == null)
                throw new InvalidOperationException("Nenhum diálogo foi iniciado");

            var nextNodes = currentDialogue?
                .GetNextDialogueNodes(currentDialogueNode)
                .ToArray();

            IsFinished = nextNodes.Length == 0;

            if(!IsFinished)
                currentDialogueNode = nextNodes[0];
            else
                End();
        }
    }
}