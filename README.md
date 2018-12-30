# Server software for SOCVR.org

This is the software that reads from the socvr-website-content repository and renders it as web pages.

## How to build the docker image

Make sure you have Docker or Docker For Windows installed and available on the command line. Then run the following command from the root of the repository:

    docker build -t socvr-website-server --build-arg AppVersion=<AppVersion>

Replace `<AppVersion>` with something that can identify this build, like a date or the git commit id. This will be used ONLY in Azure App Insights reporting. If you are not using App Insights, you may exclude the entire `--build-arg AppVersion=<AppVersion>` flag.

## How to run the docker image

The only configuration requirement this image needs is specific environment variables set. The particular list can be found in the sample Docker Compose file in this repository. By default, the app listens on port 80, though you can change that with [the `ASPNETCORE_URLS` env var](https://stackoverflow.com/a/40836125/1043380).
