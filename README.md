# LinqSqLiteSample
Example of using Linq to interact with an [SQLite](https://www.sqlite.org/) database (uses Entity Framework to do so).

It's what's shown in [this blog post](http://www.bricelam.net/2012/10/entity-framework-on-sqlite.html) but updated for version 6 of the Entity Framework, and it outputs to text files after each database change so the changes in each step can be viewed in a diff tool.

## Project setup

There were a few bugs I ran into trying to get SQLite and Entity Framework installed and working. I found workarounds and here's what I did.

1. Make sure Entity Framework is *not* already installed (it gets installed by SQLite package).
	- Otherwise, you later get the error: *"A numeric comparison was attempted on "$(TargetPlatformVersion)" that evaluates to "" instead of a number, in condition "'$(TargetPlatformVersion)' > '8.0'"* ([bug](https://nuget.codeplex.com/workitem/3996)).
2. Install the SQLite library. At time of writing, SQLite's NuGet installer doesn't like if the project's .NET version is 4.5. It causes an error later on: "Unable to determine the provider name for provider factory of type 'System.Data.SQLite.SQLiteFactory'" (see [thread](http://stackoverflow.com/questions/26327811/unable-to-determine-the-provider-name-for-provider-factory-of-type-system-data/30515816#30515816), [bug report](https://system.data.sqlite.org/index.html/info/2be4298631)). To work around the bug:
	1. Set the project's Target Framework to .NET 4.
	2. Install the NuGet package **System.Data.SQLite**.
	3. Set the target framework back to .NET 4.5.
4. In App.config, add the connection string inside `<configuration>` *but somewhere after* `<configSections>` (order matters). Example:

        <connectionStrings>
            <add name="ChinookContext" connectionString="Data Source=|DataDirectory|Chinook_Sqlite_AutoIncrementPKs.sqlite" providerName="System.Data.SQLite.EF6" />
        </connectionStrings>

5. Add the database to the project and set "Copy to Output Directory" to Always. Or set up a build mechanism to get the database where you want it.
6. Write program code. I modified the example code from [this blog post](http://www.bricelam.net/2012/10/entity-framework-on-sqlite.html).