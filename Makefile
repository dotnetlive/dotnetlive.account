install:
	

start:
	supervisorctl start kestral.dotnetlive.account

stop:
	systemctl stop kestral.dotnetlive.account

delete_current_build:
	rm -rf /var/dotnetlive/pubsite/dotnetlive.account/

publish:
	git clean -df
	git pull
	dotnet restore src/Feinian.sln 
	cd src/DotNetLive.AccountWeb && npm install && bower install --allow-root && gulp default
	dotnet publish src/DotNetLive.AccountWeb/DotNetLive.AccountWeb.csproj -c "Release" -o /var/dotnetlive/pubsite/dotnetlive.account/ 

deploy: stop publish start
