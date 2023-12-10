using CodeBase.DataSaver.Common;

namespace CodeBase.GameFlow.ResultManager
{
    [Savable(nameof(Result), SaveMethod.Json)]
    public class Result
    {
        public int Kills;
    }
}