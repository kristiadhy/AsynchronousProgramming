namespace AsynchronousProgramming.DataModel;
internal class ProgressStatusReport
{
    public int PercentageComplete { get; set; } = 0;
    public List<WebsiteModel> WebsiteDownloadedList { get; set; } = [];
}
