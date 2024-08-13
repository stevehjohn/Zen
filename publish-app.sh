rm -rf Zen.app

dotnet publish ./src/Zen.Desktop.Host/Zen.Desktop.Host.csproj -c Release -r osx-arm64 -p:PublishReadyToRun=false -p:TieredCompilation=false -p:PublishTrimmed=false --self-contained

cd ./src/Zen.Desktop.Host/bin/Release/net8.0/osx-arm64/publish
pwd
chmod +xxx Zen.Desktop.Host
cd -
mkdir Zen.app
cd Zen.app
mkdir Contents
cd Contents
mkdir MacOS
cp -R ../../src/Zen.Desktop.Host/bin/Release/net8.0/osx-arm64/publish/* MacOS
mkdir Resources
mv MacOS/_Content Resources
cp ../../icon.icns Resources
cp ../../src/Zen.Desktop.Host/Info.plist .