namespace ch1seL.TonNet.Client.Tests
{
    public abstract class TonClientTestsBase
    {
        protected TonClient TonClient { get; }
        
        protected TonClientTestsBase()
        {
            TonClient = new TonClient();
        }
    }
}