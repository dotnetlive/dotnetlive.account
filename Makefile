{
    "DbSettings": {
        "CommandDbConnectionString": "Server=qd1.niusys.com;User Id=dev_user; Password=dotnet.live; Database=blog;",
        "QueryDbConnectionString": "Server=qd1.niusys.com;User Id=dev_user; Password=dotnet.live; Database=blog;"
    },
    "AppSettings": {
        "MainSite": "http://dotnet.live"
    },
    "SecuritySettings": {
        "DomainName": ".dotnet.live",
        "DataProtectionPath": "/data/dotnetlive/dpkeys"
    },
    "Logging": {
        "IncludeScopes": false,
        "LogLevel": {
            "Default": "Warning"
        }
    }
}	dotnet restore src/accountweb/DotNetLive.AccountWeb.sln 
	cd src/accountweb/DotNetLive.AccountWeb && npm install && bower install --allow-root && gulp default
	dotnet publish src/accountweb/DotNetLive.AccountWeb/DotNetLive.AccountWeb.csproj -c "Release" -o /data/dotnetlive/pubsite/dotnetlive.accountweb/ 

publish_api:
	git clean -df
	git pull
	dotnet restore src/accountapi/DotNetLive.AccountApi.sln 
	dotnet publish src/accountapi/DotNetLive.AccountApi/DotNetLive.AccountApi.csproj -c "Release" -o /data/dotnetlive/pubsite/dotnetlive.accountapi/ 

deploy_web: stop_web publish_web start_web
deploy_api: stop_api publish_api start_api

deploy: delete_current_build deploy_api deploy_web
