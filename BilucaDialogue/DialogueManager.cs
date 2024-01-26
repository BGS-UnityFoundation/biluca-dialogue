namespace BilucaDialogue
{
    public class DialogueManager
    {
        public event Action<DialogueNode>? OnNext;
        public event Action? OnEnd;

        public bool IsFinished { get; private set; }
        private Dialogue? currentDialogue;
        private DialogueNode? currentDialogueNode;

        private void SetCurrentNode(DialogueNode node)
        {
            currentDialogueNode = node;
            OnNext?.Invoke(currentDialogueNode);
        }

        public void Start(Dialogue dialogue)
        {
            currentDialogue = dialogue;
            SetCurrentNode(currentDialogue.Start);
        }

        public void End()
        {
            currentDialogue = null;
            currentDialogueNode = null;
            OnEnd?.Invoke();
        }

        public void Next()
        {
            if(currentDialogue == null || currentDialogueNode == null)
                throw new InvalidOperationException("Nenhum diálogo foi iniciado");

            var nextNodes = currentDialogue
                .GetNextDialogueNodes(currentDialogueNode)
                .ToArray();

            IsFinished = nextNodes.Length == 0;

            if(!IsFinished)
                SetCurrentNode(nextNodes[0]);
            else
                End();
        }
    }
}