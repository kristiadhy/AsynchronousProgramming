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
        "https://www.amazon.com",
        "https://www.facebook.com",
        "https://www.twitter.com",
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
        //...
        foreach (string websiteUrl in websiteUrlList)
        {
            //Get the result of each downloaded
            var websiteContent = await DownloadWebsiteFromUrlAsync(websiteUrl, client);
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

        //Download website asynchronously from website url list and save the task to the IEnumerable
        //Reference link: https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/start-multiple-async-tasks-and-process-them-as-they-complete
        IEnumerable<Task<WebsiteModel>> downloadWebsiteTaskFromQuery = from webisteUrl in websiteUrlList select DownloadWebsiteFromUrlAsync(webisteUrl, client);
        //Set the task to list
        List<Task<WebsiteModel>> downloadWebsiteTask = downloadWebsiteTaskFromQuery.ToList();

        //Check if there are any website task in the list.
        //We will remove the completed task from the list later on
        while (downloadWebsiteTask.Any())
        {
            //Here is how we check if there is any finished task
            Task<WebsiteModel> finishedTask = await Task.WhenAny(downloadWebsiteTask);
            //Get the website model of finished task
            var websiteDownloaded = await finishedTask;
            //Put website model to website list
            websiteList.Add(websiteDownloaded);

            //Update the progress to set the value of progressStatus.PercentageComplete
            //NOTE: We can get the downloaded count by using websiteList.Count(), but we want to separate the progressPercentage just to make it readable
            progressPercentage += 1;
            progressStatus.WebsiteDownloadedList = websiteList;
            progressStatus.PercentageComplete = (progressPercentage * 100) / websiteUrlList.Count;
            //Report progress update, it will trigger the ProgressChanged event handler
            progress.Report(progressStatus);

            //Remove finished task from the list
            downloadWebsiteTask.Remove(finishedTask);
        }
    }

    public static async Task<WebsiteModel> DownloadWebsiteFromUrlAsync(string websiteURL, HttpClient client)
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
