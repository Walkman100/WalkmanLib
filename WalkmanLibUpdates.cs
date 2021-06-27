using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
// add a reference to System.Web.Extensions

public partial class WalkmanLib {
    public struct VersionInfo {
        /// <summary>Name of the tag.</summary>
        public string TagName;

        /// <summary>Title of the release.</summary>
        public string Title;

        /// <summary>Body of the release. Consists of raw release text, optionally parse it as markdown before displaying it.</summary>
        public string Body;
    }

    /// <summary>Gets information about the latest release in a GitHub project.</summary>
    /// <param name="projectName">Name of the project repository.</param>
    /// <param name="projectOwner">Owner of the project repository. Default: Walkman100</param>
    /// <returns>A <see cref="VersionInfo"/> object populated with information about the latest release.</returns>
    public static VersionInfo GetLatestVersionInfo(string projectName, string projectOwner = "Walkman100") {
        // https://stackoverflow.com/a/2904963/2999220
        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        // https://stackoverflow.com/a/16655779/2999220
        string address = string.Format("https://api.github.com/repos/{0}/{1}/releases/latest", projectOwner, projectName);
        var client = new WebClient();

        // https://stackoverflow.com/a/22134980/2999220
        client.Headers.Add("User-Agent", "anything");

        var reader = new StreamReader(client.OpenRead(address));
        string jsonObject = reader.ReadToEnd();

        // https://stackoverflow.com/a/38944715/2999220
        var jss = new JavaScriptSerializer();
        Dictionary<string, object> jsonObjectDict = jss.Deserialize<Dictionary<string, object>>(jsonObject);

        return new VersionInfo() {
            TagName = (string)jsonObjectDict["tag_name"],
            Title = (string)jsonObjectDict["name"],
            Body = (string)jsonObjectDict["body"]
        };
    }

    /// <summary>Gets a download link for the latest installer released in a GitHub project.</summary>
    /// <param name="projectName">Name of the project repository.</param>
    /// <param name="projectOwner">Owner of the project repository. Default: Walkman100</param>
    /// <param name="fileName">Name of the file. Defaults to "<paramref name="projectName"/>-Installer.exe". Use {0} to replace with the version string.</param>
    /// <returns>Download URI in a <see cref="string"/>.</returns>
    public static string GetLatestDownloadLink(string projectName, string projectOwner = "Walkman100", string fileName = null) {
        string versionString;
        versionString = GetLatestVersionInfo(projectName, projectOwner).TagName;

        if (fileName == null) {
            fileName = projectName + "-Installer.exe";
        } else {
            fileName = string.Format(fileName, versionString);
        }

        return string.Format("https://github.com/{0}/{1}/releases/download/{2}/{3}", projectOwner, projectName, versionString, fileName);
    }

    /// <summary>Gets the latest version released in a GitHub project. Note if the tag name is not in version format will throw an Exception.</summary>
    /// <param name="projectName">Name of the project repository.</param>
    /// <param name="projectOwner">Owner of the project repository. Default: Walkman100</param>
    /// <returns>The latest release version parsed as a <see cref="Version"/> object.</returns>
    public static Version GetLatestVersion(string projectName, string projectOwner = "Walkman100") {
        string versionString;
        versionString = GetLatestVersionInfo(projectName, projectOwner).TagName;

        if (versionString.StartsWith("v")) {
            versionString = versionString.Substring(1);
        }

        return Version.Parse(versionString);
    }

    /// <summary>Checks if an update release is available in a GitHub project.</summary>
    /// <param name="projectName">Name of the project repository.</param>
    /// <param name="currentVersion"><see cref="Version"/> to check against.</param>
    /// <param name="projectOwner">Owner of the project repository. Default: Walkman100</param>
    /// <returns><see langword="true"/> if latest version is newer than <paramref name="currentVersion"/>, else <see langword="false"/>.</returns>
    public static bool CheckIfUpdateAvailable(string projectName, Version currentVersion, string projectOwner = "Walkman100") {
        Version latestVersion = GetLatestVersion(projectName, projectOwner);

        return latestVersion > currentVersion;
    }

    /// <summary>Checks if an update release is available in a GitHub project in a BackgroundWorker.</summary>
    /// <param name="projectName">Name of the project repository.</param>
    /// <param name="currentVersion"><see cref="Version"/> to check against.</param>
    /// <param name="checkComplete">Use "<c>[returnVoid]</c>" and put your return void in place of <c>returnVoid</c> (see Returns info).</param>
    /// <param name="projectOwner">Owner of the project repository. Default: Walkman100</param>
    /// <returns>
    /// Create a Sub with the signature "<c>Object sender, ComponentModel.RunWorkerCompletedEventArgs e</c>", and within check <c>e.Error</c> for an exception before
    /// using "<c>(bool)e.Result</c>": <see langword="true"/> if latest version is newer than <paramref name="currentVersion"/>, else <see langword="false"/>.
    /// </returns>
    public static void CheckIfUpdateAvailableInBackground(string projectName, Version currentVersion, System.ComponentModel.RunWorkerCompletedEventHandler checkComplete, string projectOwner = "Walkman100") {
        var bwUpdateCheck = new System.ComponentModel.BackgroundWorker();

        bwUpdateCheck.DoWork += BackgroundUpdateCheck;
        bwUpdateCheck.RunWorkerCompleted += checkComplete;

        bwUpdateCheck.RunWorkerAsync(new object[] {projectName, projectOwner, currentVersion});
    }

    private static void BackgroundUpdateCheck(object sender, System.ComponentModel.DoWorkEventArgs e) {
        string projectName = (string)((object[])e.Argument)[0];
        string projectOwner = (string)((object[])e.Argument)[1];
        Version currentVersion = (Version)((object[])e.Argument)[2];

        Version latestVersion;
        int retries = 0;
        while (true) {
            try {
                latestVersion = GetLatestVersion(projectName, projectOwner);
                break;
            } catch (WebException) {
                retries += 1;
                if (retries > 2) {
                    throw;
                }
            }
        }

        e.Result = latestVersion > currentVersion;
    }

    // Example code to use the background update check:
    //void Main() {
    //    WalkmanLib.CheckIfUpdateAvailableInBackground(projectName, Application.ProductVersion, UpdateCheckReturn);
    //}
    //void UpdateCheckReturn(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e) {
    //    if (e.Error == null) {
    //        Console.WriteLine("Update available: " + (bool)e.Result);
    //    } else {
    //        Console.WriteLine("Error checking for updates: " + e.Error.Message);
    //    }
    //}
}
