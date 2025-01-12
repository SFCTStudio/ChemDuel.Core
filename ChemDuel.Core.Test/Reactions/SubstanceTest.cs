using ChemDuel.Core.Reactions;

namespace ChemDuel.Core.Test.Reactions;
[TestFixture]
public class SubstanceTest
{
    [Test]
    public void TestParse()
    {
        Assert.That(Substance.Parse("H_{2}"), Is.EqualTo(new Substance("H_{2}")));
        Assert.That(Substance.Parse("O^{-2}"), Is.EqualTo(new Substance("O^{-2}", -2)));
        Assert.That(Substance.Parse("NH_{4}^{+}"), Is.EqualTo(new Substance("NH_{4}^{+}", 1)));
    }

    [Test]
    public void TestCalculateAtom()
    {
        var result = new Substance("H_{2}O").CalculateAtom();
        Assert.That(result[Element.H], Is.EqualTo(2));
        Assert.That(result[Element.O], Is.EqualTo(1));
        
        result = new Substance("NH_{4}^{+}").CalculateAtom();
        Assert.That(result[Element.N], Is.EqualTo(1));
        Assert.That(result[Element.H], Is.EqualTo(4));
        
        result = new Substance("Fe_{2}(SO_{4})_{3}").CalculateAtom();
        Assert.That(result[Element.Fe], Is.EqualTo(2));
        Assert.That(result[Element.S], Is.EqualTo(3));
        Assert.That(result[Element.O], Is.EqualTo(12));
        
        result = new Substance("[Al(OH)_{4}]^{-}").CalculateAtom();
        Assert.That(result[Element.Al], Is.EqualTo(1));
        Assert.That(result[Element.O], Is.EqualTo(4));
        Assert.That(result[Element.H], Is.EqualTo(4));
    }
}