start_web:
	systemctl start kestrel-dotnetlive-accountweb.service

stop_web:
	systemctl stop kestrel-dotnetlive-accountweb.service

start_api:
	systemctl start kestrel-dotnetlive-accountapi.service

stop_api:
	systemctl stop kestrel-dotnetlive-accountapi.service

delete_current_build:
	rm -rf /var/dotnetlive/pubsite/dotnetlive.accountweb/
	rm -rf /var/dotnetlive/pubsite/dotnetlive.accountapi/

publish_web:
	git clean -df
	git pull
	dotnet restore src/accountweb/DotNetLive.AccountWeb.sln 
	cd src/accountweb/DotNetLive.AccountWeb && npm install && bower install --allow-root && gulp default
	dotnet publish src/accountweb/DotNetLive.AccountWeb/DotNetLive.AccountWeb.csproj -c "Release" -o /var/dotnetlive/pubsite/dotnetlive.accountweb/ 

publish_api:
	git clean -df
	git pull
	dotnet restore src/accountapi/DotNetLive.AccountApi.sln 
	dotnet publish src/accountapi/DotNetLive.AccountApi/DotNetLive.AccountApi.csproj -c "Release" -o /var/dotnetlive/pubsite/dotnetlive.accountapi/ 

deploy_web: stop_web publish_web start_web
deploy_api: stop_api publish_api start_api

deploy: delete_current_build deploy_api deploy_web
