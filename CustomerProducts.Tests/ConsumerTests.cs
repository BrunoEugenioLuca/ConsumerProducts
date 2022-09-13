namespace ConsumerProducts.Tests
{
    public class ConsumerTests
    {
        private Consumer _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new Consumer();
        }

        [Test]
        public async Task Should_Execute_Post_On_FattureInCloud()
        {
            await _sut.RequestApiAsync();
            Assert.Pass();
        }
    }
}