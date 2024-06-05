using AsynchronousProgramming.DataModel;

namespace AsynchronousProgramming.Method;
internal static class BasicMethod
{
    public static List<string> InitializeData()
    {
        List<string> websiteUrl = [
        "https://www.yahoo.com",
        "https://www.google.com",
        "https://www.microsoft.com",
        "https://www.cnn.com"
        ];

        return websiteUrl;
    }

    public static async Task DownloadAsync(IProgress<ProgressStatusReport> progress)
    {
        HttpClient client = new();
        List<string> websiteUrlList = InitializeData();
        List<WebsiteModel> websiteList = new();
        ProgressStatusReport progressStatus = new();
        progress.Report(progressStatus); //Trigger the progress update when data has no content so the progress bar will start from zero
        int progressPercentage = 0;

        //Loop the initialized website urls
        //1.Yahoo
        //2.Google
        //3.Microsoft
        //4.Cnn
        foreach (string websiteUrl in websiteUrlList)
        {
            //Get the result of each downloaded
            var websiteContent = await DownloadWebsiteFromUrl(websiteUrl, client);
            //Put it in the website list
            websiteList.Add(websiteContent);

            //Update the progress to set the value of progressStatus.PercentageComplete
            //NOTE: We can get the downloaded count by using websiteList.Count(), but we want to separate the progressPercentage just to make it readable

            progressPercentage += 1;
            progressStatus.WebsiteDownloadedList = websiteList;
            progressStatus.PercentageComplete = (progressPercentage * 100) / websiteUrlList.Count;

            //Report progress update, it will trigger the ProgressChanged event handler
            progress.Report(progressStatus);
        }
    }

    public static async Task DownloadParallelAsync(IProgress<ProgressStatusReport> progress)
    {
        HttpClient client = new();
        List<string> websiteUrlList = InitializeData();
        List<WebsiteModel> websiteList = new();
        List<Task<WebsiteModel>> tasks = new();
        ProgressStatusReport progressStatus = new();
        progress.Report(progressStatus); //Trigger the progress update when data has no content so the progress bar will start from zero
        int progressPercentage = 0;

        IEnumerable<Task<WebsiteModel>> downloadWebsiteTaskFromQuery = from webisteUrl in websiteUrlList select DownloadWebsiteFromUrl(webisteUrl, client);
        List<Task<WebsiteModel>> downloadWebsiteTask = downloadWebsiteTaskFromQuery.ToList();

        while (downloadWebsiteTask.Any())
        {
            Task<WebsiteModel> finishedTask = await Task.WhenAny(downloadWebsiteTask);
            var websiteDownloaded = await finishedTask;
            websiteList.Add(websiteDownloaded);

            progressPercentage += 1;
            progressStatus.WebsiteDownloadedList = websiteList;
            progressStatus.PercentageComplete = (progressPercentage * 100) / websiteUrlList.Count;
            //Report progress update, it will trigger the ProgressChanged event handler
            progress.Report(progressStatus);

            downloadWebsiteTask.Remove(finishedTask);
        }
    }

    public static async Task<WebsiteModel> DownloadWebsiteFromUrl(string websiteURL, HttpClient client)
    {
        WebsiteModel website = new();
        website.WebsiteUrl = websiteURL;
        website.WebsiteData = await client.GetStringAsync(websiteURL);

        return website;
    }

    public static List<string> GetReportResult(List<WebsiteModel> websites)
    {
        List<string> resultTexts = new();
        foreach (var site in websites)
        {
            string resultText = $"{site.WebsiteUrl} : {site.WebsiteData.Length}";
            resultTexts.Add(resultText);
        }
        return resultTexts;
    }
}
