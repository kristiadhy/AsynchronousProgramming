namespace AsynchronousProgramming;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        btnDownloadAsync = new Button();
        btnCancelAsync = new Button();
        prgBar = new ProgressBar();
        txtResult = new TextBox();
        btnDownloadAndDisplayAsync = new Button();
        SuspendLayout();
        // 
        // btnDownloadAsync
        // 
        btnDownloadAsync.Location = new Point(11, 7);
        btnDownloadAsync.Margin = new Padding(2);
        btnDownloadAsync.Name = "btnDownloadAsync";
        btnDownloadAsync.Size = new Size(118, 32);
        btnDownloadAsync.TabIndex = 1;
        btnDownloadAsync.Text = "Download Async";
        btnDownloadAsync.UseVisualStyleBackColor = true;
        // 
        // btnCancelAsync
        // 
        btnCancelAsync.Location = new Point(11, 43);
        btnCancelAsync.Margin = new Padding(2);
        btnCancelAsync.Name = "btnCancelAsync";
        btnCancelAsync.Size = new Size(308, 34);
        btnCancelAsync.TabIndex = 2;
        btnCancelAsync.Text = "Cancel Process";
        btnCancelAsync.UseVisualStyleBackColor = true;
        // 
        // prgBar
        // 
        prgBar.Location = new Point(8, 242);
        prgBar.Margin = new Padding(2);
        prgBar.Name = "prgBar";
        prgBar.Size = new Size(543, 20);
        prgBar.TabIndex = 3;
        // 
        // txtResult
        // 
        txtResult.Location = new Point(8, 81);
        txtResult.Margin = new Padding(2);
        txtResult.Multiline = true;
        txtResult.Name = "txtResult";
        txtResult.Size = new Size(544, 157);
        txtResult.TabIndex = 4;
        // 
        // btnDownloadAndDisplayAsync
        // 
        btnDownloadAndDisplayAsync.Location = new Point(133, 7);
        btnDownloadAndDisplayAsync.Margin = new Padding(2);
        btnDownloadAndDisplayAsync.Name = "btnDownloadAndDisplayAsync";
        btnDownloadAndDisplayAsync.Size = new Size(186, 32);
        btnDownloadAndDisplayAsync.TabIndex = 5;
        btnDownloadAndDisplayAsync.Text = "Download and Display Async";
        btnDownloadAndDisplayAsync.UseVisualStyleBackColor = true;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(560, 270);
        Controls.Add(btnDownloadAndDisplayAsync);
        Controls.Add(txtResult);
        Controls.Add(prgBar);
        Controls.Add(btnCancelAsync);
        Controls.Add(btnDownloadAsync);
        Margin = new Padding(2);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Asynchronous Programming";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
    private Button btnDownloadAsync;
    private Button btnCancelAsync;
    private ProgressBar prgBar;
    private TextBox txtResult;
    private Button btnDownloadAndDisplayAsync;
}
