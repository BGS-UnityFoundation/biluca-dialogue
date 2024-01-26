namespace BilucaDialogue
{
    public class Dialogue
    {
        private Dictionary<Guid, DialogueNode> dialogueNodes;
        public IEnumerable<DialogueNode> DialogueNodesValues => dialogueNodes.Values;
        public DialogueNode Start => dialogueNodes.Values.First();

        public Dialogue()
        {
            dialogueNodes = new();
        }

        public DialogueNode Get(Guid id)
        {
            if(dialogueNodes.TryGetValue(id, out DialogueNode dialogueNode))
                return dialogueNode;

            return null;
        }

        public void Add(DialogueNode node)
        {
            dialogueNodes.Add(node.Id, node);
        }

        public IEnumerable<DialogueNode> GetNextDialogueNodes(DialogueNode node)
        {
            if(node.NextDialogueNodes == null) yield return null;

            foreach(var childId in node.NextDialogueNodes)
            {
                if(dialogueNodes.TryGetValue(childId, out DialogueNode childNode))
                {
                    yield return childNode;
                }
            }
        }

        public IEnumerable<DialogueNode> GetNextDialogueNodesRecursively(DialogueNode node)
        {
            var nodes = new List<DialogueNode>();

            var notSearchedNodes = new Stack<DialogueNode>();
            notSearchedNodes.Push(node);

            do
            {
                var searchingNode = notSearchedNodes.Pop();
                foreach(var newNode in GetNextDialogueNodes(searchingNode))
                {
                    if(nodes.Contains(newNode))
                        continue;

                    notSearchedNodes.Push(newNode);
                }
                nodes.Add(searchingNode);
            } while(notSearchedNodes.Count > 0);

            nodes.RemoveAt(0);

            return nodes.AsEnumerable();
        }

        public IEnumerable<DialogueNode> GetPreviousDialogueNodes(DialogueNode node)
        {
            if(node.PreviousDialogueNodes == null) yield return null;

            foreach(var nodeId in node.PreviousDialogueNodes)
            {
                if(dialogueNodes.TryGetValue(nodeId, out DialogueNode dialogueNode))
                {
                    yield return dialogueNode;
                }
            }
        }

        public IEnumerable<DialogueNode> GetPreviousDialogueNodesRecursively(DialogueNode node)
        {
            var nodes = new List<DialogueNode>();

            var notSearchedNodes = new Stack<DialogueNode>();
            notSearchedNodes.Push(node);

            do
            {
                var searchingNode = notSearchedNodes.Pop();
                foreach(var newNode in GetPreviousDialogueNodes(searchingNode))
                {
                    if(nodes.Contains(newNode))
                        continue;

                    notSearchedNodes.Push(newNode);
                }
                nodes.Add(searchingNode);
            } while(notSearchedNodes.Count > 0);

            nodes.RemoveAt(0);

            return nodes.AsEnumerable();
        }

        public bool IsStartLine(DialogueNode node)
        {
            return node == Start;
        }
    }
}
