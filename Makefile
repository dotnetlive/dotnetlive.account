start:
	systemctl start kestrel-dotnetlive-account.service

stop:
	systemctl stop kestrel-dotnetlive-account.service

delete_current_build:
	rm -rf /var/dotnetlive/pubsite/dotnetlive.account/

publish:
	git clean -df
	git pull
	dotnet restore src/DotNetLive.AccountWeb.sln 
	cd src/DotNetLive.AccountWeb && npm install && bower install --allow-root && gulp default
	dotnet publish src/DotNetLive.AccountWeb/DotNetLive.AccountWeb.csproj -c "Release" -o /var/dotnetlive/pubsite/dotnetlive.account/ 

deploy: stop publish start
