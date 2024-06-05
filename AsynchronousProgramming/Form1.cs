using AsynchronousProgramming.DataModel;
using AsynchronousProgramming.Method;

namespace AsynchronousProgramming;

public partial class Form1 : Form
{
    CancellationTokenSource cts = new CancellationTokenSource();

    public Form1()
    {
        InitializeComponent();

        btnDownloadAsync.Click += async (s, e) => await BtnAsynchronous_Click(s, e);
        btnDownloadAndDisplayAsync.Click += async (s, e) => await BtnAsynchronousParallel_Click(s, e);
    }

    private async Task BtnAsynchronous_Click(object? sender, EventArgs e)
    {
        Progress<ProgressStatusReport> progress = new();
        progress.ProgressChanged += ReportProgress;
        await BasicMethod.DownloadAsync(progress);
    }

    private async Task BtnAsynchronousParallel_Click(object s, EventArgs e)
    {
        Progress<ProgressStatusReport> progress = new();
        progress.ProgressChanged += ReportProgress;
        await BasicMethod.DownloadParallelAsync(progress);
    }

    private void ReportProgress(object? sender, ProgressStatusReport e)
    {
        prgBar.Value = e.PercentageComplete;
        PrintResult(e.WebsiteDownloadedList);
    }

    private void PrintResult(List<WebsiteModel> websiteDownloaded)
    {
        var resultList = BasicMethod.GetReportResult(websiteDownloaded);
        var resultListText = string.Join(Environment.NewLine, resultList);
        txtResult.Text = resultListText;
    }
}
