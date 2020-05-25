using NUnit.Framework;
using ShannonEntropyCal;
using System;

namespace entropytests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestShannonEntropyLow()
        {
            string low = "abc123";
            if (EntropyCal.EntropyValue(low) > 3.0){
                Assert.Fail();
            }
        }

        [Test]
        public void TestShannonEntropyHigh()
        {
            string high = "ý¼¸²>æ{£¤@TçKA¥£åKPk.rPoSo}fÑú½§rêÆÀðke(9/¹©ÔRqTãîý`Çóè°T²þµ)ÁÄÒÙr7éijÈ·Ñø{.8'ü*=Å.ôþSø&ÏßP9D}\"û+îÏæ¼aZ-'ûÐÐ¼ÊZh5³ÒD®/YÙ¤(a·]Ðf";
            Assert.IsTrue(EntropyCal.EntropyValue(high) > 6.3);
        }

    }
}