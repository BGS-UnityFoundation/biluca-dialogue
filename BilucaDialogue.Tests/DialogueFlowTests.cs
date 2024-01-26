namespace BilucaDialogue.Tests
{
    internal class DialogueFlowTests
    {
        [Test]
        public void Should_have_a_simple_dialogue()
        {
            var dialogue = new Dialogue();
            dialogue.Add(new DialogueNode());
            dialogue.Add(new DialogueNode());

            var manager = new DialogueManager();
            manager.Start(dialogue);

            manager.Next();
            Assert.That(manager.IsFinished, Is.True);

            manager.Next();
            Assert.That(manager.IsFinished, Is.True);
        }
    }
}
