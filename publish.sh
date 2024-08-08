rm *.zip
dotnet publish ./src/Zen.Desktop.Host/Zen.Desktop.Host.csproj -c Release -r osx-x64 /p:PublishReadyToRun=true /p:TieredCompilation=false --self-contained /p:PublishReadyToRunShowWarnings=true
dotnet publish ./src/Zen.Desktop.Host/Zen.Desktop.Host.csproj -c Release -r osx-arm64 /p:PublishReadyToRun=true /p:TieredCompilation=false --self-contained /p:PublishReadyToRunShowWarnings=true
dotnet publish ./src/Zen.Desktop.Host/Zen.Desktop.Host.csproj -c Release -r win-x64 /p:PublishReadyToRun=true /p:TieredCompilation=false --self-contained /p:PublishReadyToRunShowWarnings=true
dotnet publish ./src/Zen.Desktop.Host/Zen.Desktop.Host.csproj -c Release -r linux-x64 /p:PublishReadyToRun=true /p:TieredCompilation=false --self-contained /p:PublishReadyToRunShowWarnings=true

cd ./src/Zen.Desktop.Host/bin/Release/net8.0/osx-x64/publish
pwd
chmod +xx Zen.Desktop.Host
rm *.zip
zip -r Zen.Desktop.Host.macOS.Intel.zip *
cd -
mv ./src/Zen.Desktop.Host/bin/Release/net8.0/osx-x64/publish/Zen.Desktop.Host.macOS.Intel.zip .

cd ./src/Zen.Desktop.Host/bin/Release/net8.0/osx-arm64/publish
pwd
chmod +xx Zen.Desktop.Host
rm *.zip
zip -r Zen.Desktop.Host.macOS.Apple.zip *
cd -
mv ./src/Zen.Desktop.Host/bin/Release/net8.0/osx-arm64/publish/Zen.Desktop.Host.macOS.Apple.zip .

cd ./src/Zen.Desktop.Host/bin/Release/net8.0/win-x64/publish
pwd
rm *.zip
zip -r Zen.Desktop.Host.Windows.Intel.zip *
cd -
mv ./src/Zen.Desktop.Host/bin/Release/net8.0/win-x64/publish/Zen.Desktop.Host.Windows.Intel.zip .

cd ./src/Zen.Desktop.Host/bin/Release/net8.0/linux-x64/publish
pwd
rm *.zip
zip -r Zen.Desktop.Host.Linux.Intel.zip *
cd -
mv ./src/Zen.Desktop.Host/bin/Release/net8.0/linux-x64/publish/Zen.Desktop.Host.Linux.Intel.zip .
