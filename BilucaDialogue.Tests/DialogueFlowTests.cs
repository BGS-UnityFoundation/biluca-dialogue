using BilucaDialogue.Helpers;

namespace BilucaDialogue.Tests
{
    internal class DialogueFlowTests
    {
        [Test]
        public void Should_throw_error_when_trying_to_move_next_on_no_started_dialogue()
        {
            var manager = new DialogueManager();
            Assert.Throws<InvalidOperationException>(() => manager.Next());
        }

        [Test]
        public void Should_have_a_simple_dialogue()
        {
            var dialogue = new Dialogue();

            var n1 = new DialogueNode();
            var n2 = new DialogueNode();
            n1.Link(n2);

            dialogue.Add(n1);
            dialogue.Add(n2);

            var manager = new DialogueManager();

            var onNextTest = new EventTest<DialogueNode>(manager, nameof(manager.OnNext));
            var onEndTest = new EventTest(manager, nameof(manager.OnEnd));

            manager.Start(dialogue);

            manager.Next();

            Assert.That(manager.IsFinished, Is.False);

            manager.Next();

            Assert.Multiple(() => {
                Assert.That(manager.IsFinished, Is.True);
                Assert.That(onNextTest.TriggerCount, Is.EqualTo(2));
                Assert.That(onNextTest.Parameter, Is.EqualTo(n2));
                Assert.That(onEndTest.WasTriggered, Is.True);
            });
        }
    }
}
