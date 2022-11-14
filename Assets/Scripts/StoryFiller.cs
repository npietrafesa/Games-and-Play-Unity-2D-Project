public static class StoryFiller
{
    public static StoryNode FillStory()
    {
        var root = CreateNode(
            "You are a space pioneer, tasked with discovering new planets and assessing their habitability. One day, while you are attempting to explore an uncharted planet, your ship is taken out by a magnetic storm. You begin losing altitude, rapidly descending towards this unknown planet.",
            new[]
            {
                "> Continue..."
            }, 0);

        var node1 = CreateNode(
            "You crash onto the planet. Somehow, you are alive, but your ship was not spared from the same fate. Stranded on this unknown planet, you are unsure what to do.",
            new[]
            {
                "> Explore the planet",
                "> Try to repair your ship"
            }, 1);

        var node2 = CreateNode(
            "You decide to explore the planet. While you are wandering around, you can't help but feel like you are being watched. Behind you, you spot three aliens approaching you. Normally, you are wary of foreign aliens, but given your situation, perhaps it is possible to try and communicate with them.",
            new[]
            {
                "> Try to talk to the aliens",
                "> Run away back to your ship"
            }, 2);

        var node3 = CreateNode(
            "Driven by fear, you just can't help but stay in your ship. You quickly realize there are no materials to repair your ship. Too afraid to leave the ship, you spend the rest of your days within the confines of your ship. You soon run out of food and water and eventually meet your demise. GAME OVER",
            new[]
            {
                "> Restart Game"
            }, 3);

        var node4 = CreateNode(
            "You realize that they probably pose no threat to you, they are more curious than anything about your existence. It seems that they speak some foreign language, so you gesture towards your crashed ship and point up into the sky. The aliens talk amongst themselves a bit, and then say something to you foreign to your ears. They grab you, and start dragging you away towards a large cave, and take your laser gun from you. They lead you into a chamber where another one of their kind stands. This must be their king.",
            new[]
            {
                "> Tell the king about your situation",
                "> Fight the king!"
            }, 4);

        var node5 = CreateNode(
            "Worried that you may have been lead into a trap, you try to flee. However, the other aliens do not let you leave. Left with no other option, you push the other aliens away and use the one weapon the aliens couldn't take from you: your fists. You try to punch the king, but he dodges it. Angrily, it punches you back and you are sent flying. Your last moments are spent being beat up by some unknown alien screaming what you assume are profanities at you. GAME OVER",
            new[]
            {
                "> Restart Game"
            }, 5);

        var node6 = CreateNode(
            ">'We will let you leave. We have spare ships that you can take, under one condition: you must tell the other worlds about the glorius King Thog! The universe must know who I am!' He gives out a great bellowing laugh. You are taken aback, since you were sure the aliens could not understand you. 'Uh, sure, I can do that,' you say confusedly. 'Very well, here is your ship then. I wish you luck on your journey of making me famous.' You get your ship and are finally able to leave the planet! Back in space, you wonder if you really should fulfill this alien's strange wish. THE END",
            new[]
            {
                "> I'm Free!"
            }, 6);

        root.NextNode[0] = node1;

        node1.NextNode[0] = node2;
        node1.NextNode[1] = node3;

        node2.NextNode[0] = node4;
        node2.NextNode[1] = node3;

        node3.LoseGame = true;

        node4.NextNode[0] = node6;
        node4.NextNode[1] = node5;

        node5.LoseGame = true;

        node6.IsFinal = true;

        return root;
    }

    private static StoryNode CreateNode(string history, string[] options, int index)
    {
        var node = new StoryNode
        {
            History = history,
            Answers = options,
            Index = index,
            NextNode = new StoryNode[options.Length]
        };
        return node;
    }
}