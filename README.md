# biluca-dialogue

Biluca dialogue é uma biblioteca disponível para a criação de diálogos em jogos de uma forma simples e direta.

### Utilização

Exemplo de utilização na Godot

```csharp
// Configuração do diálogo
var dialogue = new Dialogue();

var n1 = new DialogueNode
{
    Text = "Primeira frase do diálogo"
};

var n2 = new DialogueNode
{
    Text = "Última frase do diálogo"
};
n1.Link(n2);

dialogue.Add(n1);
dialogue.Add(n2);

// Elementos da cena para demonstração
var dialogueText = GetNode<Label>("DialogueText");
var next = GetNode<Button>("NextBtn");
next.Visible = false;
var start = GetNode<Button>("StartBtn");

// Configuração do DialogueManager
manager = new DialogueManager();
manager.OnNext += (n) =>
{
    dialogueText.Text = n.Text;
};
manager.OnEnd += () =>
{
    next.Visible = false;
};

// Ações dos botões da cena
next.Pressed += () =>
{
    dialogueText.Text = "";
    manager.Next();
};

start.Pressed += () =>
{
    manager.Start(dialogue);
    next.Visible = true;
};
}
```