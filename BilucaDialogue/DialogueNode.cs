namespace BilucaDialogue
{
    public class DialogueNode
    {
        public Guid Id { get; private set; }
        private Spearker? spearker;
        private string text;
        private readonly List<Guid> nextDialogueNodes = new();
        private readonly List<Guid> previousDialogueNodes = new();

        public DialogueNode()
        {
            Id = Guid.NewGuid();
            text = "example";
        }

        public Spearker? Spearker {
            get { return spearker; }
            set { spearker = value; }
        }

        public string Text {
            get { return text; }
            set { text = value; }
        }

        public List<Guid> NextDialogueNodes { get { return nextDialogueNodes; } }

        public List<Guid> PreviousDialogueNodes { get { return previousDialogueNodes; } }


        public bool HasLink(DialogueNode node)
        {
            return !nextDialogueNodes.Contains(node.Id)
                && !previousDialogueNodes.Contains(node.Id);
        }

        public void Link(DialogueNode nextDialogueNode)
        {
            NextDialogueNodes.Add(nextDialogueNode.Id);
            nextDialogueNode.PreviousDialogueNodes.Add(Id);
        }

        public void Unlink(DialogueNode nextDialogueNode)
        {
            NextDialogueNodes.Remove(nextDialogueNode.Id);
            nextDialogueNode?.PreviousDialogueNodes.Remove(Id);
        }
    }
}