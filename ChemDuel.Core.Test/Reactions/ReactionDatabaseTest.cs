using ChemDuel.Core.Reactions;

namespace ChemDuel.Core.Test.Reactions;

    [TestFixture]
    public class ReactionDatabaseTest
    {
        [Test]
        public void TestLoadFromFile()
        {
            var db = ReactionDatabase.LoadFromFile("../../../reactions.rdb");
            Assert.That(db.Name, Is.EqualTo("ChemDuel"));
            Assert.That(db.Version, Is.EqualTo("1.0.0"));
            Assert.That(db.Description, Is.EqualTo("ChemDuel"));
            
            Assert.That(db.Reactions.Length, Is.EqualTo(3));
            
            var reaction = db.Reactions[0];
            Assert.That(reaction.Reactants.Length, Is.EqualTo(2));
            Assert.That(reaction.Reactants[0], Is.EqualTo("H_{2}"));
            Assert.That(reaction.Reactants[1], Is.EqualTo("N_{2}"));
            Assert.That(reaction.Products.Length, Is.EqualTo(1));
            Assert.That(reaction.Products[0], Is.EqualTo("NH_{3}"));
            Assert.That(reaction.Condition, Is.EqualTo("ht;hp;cat"));
            Assert.That(reaction.Reversible, Is.True);
            Assert.That(reaction.Attributes.Count, Is.EqualTo(1));
            Assert.That(reaction.Attributes["dH"], Is.EqualTo("-92.22"));
        }

        [Test]
        public void TestSaveToFile()
        {
            var db = new ReactionDatabase("ChemDuel", "1.0.0", "ChemDuel", [
                new Reaction(["H_{2}", "O_{2}"], ["H_{2}O"], "ht", true,
                    new Dictionary<string, string> { { "attr", "abc" } })
            ]);
            db.SaveToFile("../../../test_reactions.rdb");

            var db2 = ReactionDatabase.LoadFromFile("../../../test_reactions.rdb");
            Assert.That(db2.Name, Is.EqualTo(db.Name));
            Assert.That(db2.Version, Is.EqualTo(db.Version));
            Assert.That(db2.Description, Is.EqualTo(db.Description));
            Assert.That(db2.Reactions.Length, Is.EqualTo(db.Reactions.Length));
            Assert.That(db2.Reactions[0].Reactants.Length, Is.EqualTo(db.Reactions[0].Reactants.Length));
            Assert.That(db2.Reactions[0].Reactants[0], Is.EqualTo(db.Reactions[0].Reactants[0]));
            Assert.That(db2.Reactions[0].Reactants[1], Is.EqualTo(db.Reactions[0].Reactants[1]));
            Assert.That(db2.Reactions[0].Products.Length, Is.EqualTo(db.Reactions[0].Products.Length));
            Assert.That(db2.Reactions[0].Products[0], Is.EqualTo(db.Reactions[0].Products[0]));
        }
    }
